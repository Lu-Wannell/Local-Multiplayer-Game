using System.Collections;
using UnityEngine;

public class ClawController : MonoBehaviour
{
    [SerializeField] private bool isDescending;
    [SerializeField] private bool isResetting = false;
    [SerializeField] private bool isGrabbing = false;
    [SerializeField] private bool canWin = false;

    [SerializeField] private float grabDelay = 3f;
    [SerializeField] private float moveSpeed = 0.5f; 
    [SerializeField] private float clawRiseSpeed = 3f;

    [SerializeField] private float lMVelocity;
    [SerializeField] private float rMVelocity;

    [SerializeField] private Transform clawStartPoint;
    [SerializeField] private Transform clawEndPoint;
    [SerializeField] private Transform clawCurrentPosition;

    [SerializeField] private HingeJoint upperLeftHinge;
    [SerializeField] private HingeJoint lowerLeftHinge;
    [SerializeField] private HingeJoint upperRightHinge;
    [SerializeField] private HingeJoint lowerRightHinge;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       /* upperLeftHinge = GetComponent<HingeJoint>();
        lowerLeftHinge = GetComponent<HingeJoint>();
        upperRightHinge = GetComponent<HingeJoint>();
        lowerRightHinge = GetComponent<HingeJoint>();*/

        
    }

    // Update is called once per frame
    void Update()
    {
        // while descending the claw moves towards the clawendpoint
        if (isDescending)
        {
            clawCurrentPosition.position = Vector3.MoveTowards(clawCurrentPosition.position, clawEndPoint.position, (moveSpeed * Time.deltaTime));

            if (clawCurrentPosition.position == clawEndPoint.position)
            {
                isDescending = false;

                //Retrieve current motor settings
                JointMotor ULMotor = upperLeftHinge.motor;
                JointMotor LLMotor = lowerLeftHinge.motor;
                JointMotor URMotor = upperRightHinge.motor;
                JointMotor LRMotor = lowerRightHinge.motor;

                // Modify existing motor settings
                ULMotor.targetVelocity = -ULMotor.targetVelocity;
                LLMotor.targetVelocity = -LLMotor.targetVelocity;
                URMotor.targetVelocity = -URMotor.targetVelocity;
                LRMotor.targetVelocity = -LRMotor.targetVelocity;

                // Reassign modified motors
                upperLeftHinge.motor = ULMotor;
                lowerLeftHinge.motor = LLMotor;
                upperRightHinge.motor = URMotor;
                lowerRightHinge.motor = LRMotor;


                isGrabbing = true;//after reaching the endpoiunt the claw will start its grab action
            }
        }

        //
        if (isGrabbing) 
        {
            StartCoroutine(GrabAction(grabDelay));
            


        }
        else
        {

        }


        if (isResetting)
        {
            clawReset();
           
        }
    }

    public void clawReset()
    {
        clawCurrentPosition.position = clawStartPoint.position;//sets the claws position to the start position.

        //Retrieve current motor settings
        JointMotor ULMotor = upperLeftHinge.motor;
        JointMotor LLMotor = lowerLeftHinge.motor;
        JointMotor URMotor = upperRightHinge.motor;
        JointMotor LRMotor = lowerRightHinge.motor;

        // Modify existing motor settings
        ULMotor.targetVelocity = lMVelocity;
        LLMotor.targetVelocity = lMVelocity;
        URMotor.targetVelocity = rMVelocity;
        LRMotor.targetVelocity = rMVelocity;

        // Reassign modified motors
        upperLeftHinge.motor = ULMotor;
        lowerLeftHinge.motor = LLMotor;
        upperRightHinge.motor = URMotor;
        lowerRightHinge.motor = LRMotor;

        isResetting = false;
        isDescending = true;
    }

    IEnumerator GrabAction( float delayTime)
    {
        
        yield return new WaitForSeconds(delayTime);

        // start moving claw back to start position
        clawCurrentPosition.position = Vector3.MoveTowards(clawCurrentPosition.position, clawStartPoint.position, (clawRiseSpeed * Time.deltaTime));
        yield return new WaitForSeconds(delayTime);
        isGrabbing = false;
    }
}

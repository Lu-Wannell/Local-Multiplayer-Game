using System.Collections;
using UnityEngine;

public class ClawController : MonoBehaviour
{
    [SerializeField] private bool isDescending;
    [SerializeField] private bool isResetting = false;
    [SerializeField] private bool isGrabbing = false;
    [SerializeField] private float moveSpeed = 0.5f;

    [SerializeField] private Transform clawStartPoint;
    [SerializeField] private Transform clawEndPoint;
    [SerializeField] private Transform clawCurrentPosition;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
                isGrabbing = true;//after reaching the endpoiunt the claw will start its grab action
            }
        }

        if (isGrabbing) 
        { 

        }


        if (isResetting)
        {
            clawReset();
        }
    }

    private void clawReset()
    {
        clawCurrentPosition.position = clawStartPoint.position;//sets the claws position to the start position.
        isResetting = false;
    }

    IEnumerator DelayedAction( float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
    }
}

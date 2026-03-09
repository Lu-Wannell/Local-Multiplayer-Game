using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplePlayerKeyboard : MonoBehaviour
{
    #region Variables
    [Header("Actions (drag from your Input Actions asset")] // Adds a label in the inspector for organisation
    [SerializeField] public InputActionReference p1Move;
    [SerializeField] private InputActionReference p1Jump;
    [SerializeField] private InputActionReference p1Grab;

    [SerializeField] public InputActionReference p2Move;
    [SerializeField] private InputActionReference p2Jump;
    [SerializeField] private InputActionReference p2Grab;

    [SerializeField] private InputActionReference roundReset;
    [SerializeField] private InputActionReference roundNext;

    [Header("Players")]
    [SerializeField] public Transform p1;       //Transform for Player One
    [SerializeField] public Transform p2;       //Transform for Player Two

    [SerializeField] public Transform p1StartPos;
    [SerializeField] public Transform p2StartPos;

    [SerializeField] private Transform p1ClawR;
    [SerializeField] private Transform p1ClawL;
    [SerializeField] private Transform p2ClawR;
    [SerializeField] private Transform p2ClawL;

    public SpriteRenderer clawGR;
    public SpriteRenderer clawGL;
    public SpriteRenderer clawRR;
    public SpriteRenderer clawRL;   

    public Sprite closedGR;
    public Sprite closedGL;
    public Sprite closedRR;
    public Sprite closedRL;
    public Sprite openGR;
    public Sprite openGL;
    public Sprite openRR;
    public Sprite openRL;


    [SerializeField] private Rigidbody rb1;
    [SerializeField] private Rigidbody rb2;

    [SerializeField] private TriggerDetector redTrigger;
    [SerializeField] private TriggerDetector greenTrigger;

    [SerializeField] private float crabSpeed = 5f;
    [SerializeField] private float currentSpeed = 5f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float jumpVelocity = 0.1f;

    [Header("Grab Settings")]
    [SerializeField] private GameObject p1GrabbedObjectR;
    [SerializeField] private GameObject p1GrabbedObjectL;
    [SerializeField] private GameObject p2GrabbedObjectR;
    [SerializeField] private GameObject p2GrabbedObjectL;

    [SerializeField] public GameObject p1GrabbableObjectR;
    [SerializeField] public GameObject p1GrabbableObjectL;
    [SerializeField] public GameObject p2GrabbableObjectR;
    [SerializeField] public GameObject p2GrabbableObjectL;

    [Header("Particles")]
    [SerializeField] private ParticleSystem p1DustParticles;
    [SerializeField] private ParticleSystem p2DustParticles;

    [SerializeField] private Vector2 p1DustStartPos;
    [SerializeField] private Vector2 p2DustStartPos;

    [SerializeField] private float dustAdjustor = 0.4f;

    [Header("Level")]
    [SerializeField] private bool p1IsGrounded = true;
    [SerializeField] private bool p2IsGrounded = true;

    [Header("Scripts")]
    [SerializeField] private RoundController roundController;


    #endregion

    private void Start()
    {
        p1DustStartPos = p1DustParticles.transform.localPosition;
        p2DustStartPos = p2DustParticles.transform.localPosition;
    }

    private void OnEnable()        //Called when the component becomes enabled/active
    {
        EnablePlayerActions();

        roundReset.action.Enable();
        roundNext.action.Enable();
    }
        
    private void OnDisable()        //Called when the component becomes disabled/inactive
    {
        DisablePlayerActions();

        roundReset.action.Disable();
        roundNext.action.Disable();
    }

    private void Update()
    {
        var m1 = p1Move.action.ReadValue<float>();
        var m2 = p2Move.action.ReadValue<float>();

        var g1 = p1Grab.action.ReadValue<float>();
        var g2 = p2Grab.action.ReadValue<float>();

        float p1eularZ = p1.transform.eulerAngles.z;
        float p1currentAngle = Mathf.DeltaAngle(0f, p1eularZ);

        if (redTrigger.canWalk)
        {
            if (!p1DustParticles.isPlaying) { p1DustParticles.Play(); }
            if (p1) p1.position += new Vector3(m1, 0f, 0f) * currentSpeed * Time.deltaTime;       //Only move player 1 if the transform reference exists and if can walk;
            if (m1 > 0) { p1DustParticles.transform.localPosition = p1DustStartPos; p1DustParticles.transform.rotation = Quaternion.Euler(0, 0, 0); }
            else if (m1 < 0) 
            {
                Vector2 ParticlePos = p1DustStartPos;
                ParticlePos.x = +dustAdjustor;
                p1DustParticles.transform.localPosition = ParticlePos;                
                p1DustParticles.transform.rotation = Quaternion.Euler(0, 180, 0); 
            }
            else { p1DustParticles.Stop(); }
        }
        else if (p1currentAngle >= -60f && p1currentAngle <= 60f) //if crab tipped over but still within angle range it has slightly slower speed
        { if (p1) p1.position += new Vector3(m1, 0f, 0f) * currentSpeed / 3 * 2 * Time.deltaTime;
            p1DustParticles.Stop();
        }
        else //large speed reduction when not able to touch ground with either set of legs
        { if (p1) p1.position += new Vector3(m1, 0f, 0f) * currentSpeed / 16 * Time.deltaTime;
            p1DustParticles.Stop();
        }
       

        float p2eularZ = p2.transform.eulerAngles.z;
        float p2currentAngle = Mathf.DeltaAngle(0f, p2eularZ);
        
        
        if (greenTrigger.canWalk)
        {
            if (!p2DustParticles.isPlaying)
            { p2DustParticles.Play(); }
            if (p2) p2.position += new Vector3(m2, 0f, 0f) * currentSpeed * Time.deltaTime;       //Only move player 2 if the transform reference exists and if can walk;
            if (m2 > 0) { p2DustParticles.transform.localPosition = p2DustStartPos; p2DustParticles.transform.rotation = Quaternion.Euler(0, 0, 0); }
            else if (m2 < 0)
            {
                Vector2 ParticlePos = p2DustStartPos;
                ParticlePos.x = +dustAdjustor;
                p2DustParticles.transform.localPosition = ParticlePos;               
                p2DustParticles.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else { p2DustParticles.Stop(); }
        }
        else if (p2currentAngle >= -60f && p2currentAngle <= 60f) //if crab tipped over but still within angle range it has slightly slower speed
        { if (p2) p2.position += new Vector3(m2, 0f, 0f) * currentSpeed / 3 * 2 * Time.deltaTime;
            p2DustParticles.Stop();
        }
        else //large speed reduction when not able to touch ground with either set of legs
        { if (p2) p2.position += new Vector3(m2, 0f, 0f) * currentSpeed / 16 * Time.deltaTime;
            p2DustParticles.Stop();
        }


        // checks the direction of the rotation for player One
        if (g1 <0)//Left Claw
        {
            if(p1GrabbableObjectL != null)
            { p1GrabbedObjectL = p1GrabbableObjectL; }
                
            
            if (p1) p1.RotateAround(p1ClawL.position, new Vector3(0f, 0f, g1), -rotationSpeed * Time.deltaTime);   //Only rotate player 1 if the transform reference exists;
            //Debug.Log(p1GrabbedObjectL);
            if (p1GrabbedObjectL != null) { GrabObject(p1Grab, p1GrabbedObjectL, p1ClawL); }
            clawRL.sprite = closedRL;
        }
        else if(g1> 0)//Right Claw
        {
            if (p1GrabbableObjectR != null)
            { p1GrabbedObjectR = p1GrabbableObjectR; }

            if (p1) p1.RotateAround(p1ClawR.position, new Vector3(0f, 0f, g1), -rotationSpeed * Time.deltaTime);   //Only rotate player 1 if the transform reference exists;     
            //Debug.Log("Right");
            if (p1GrabbedObjectR != null) { GrabObject(p1Grab, p1GrabbedObjectR, p1ClawR); }
            clawRR.sprite = closedRR;
        }
        else
        {
            if (p1GrabbedObjectR != null) { DropObject(p1Grab, p1GrabbedObjectR); p1GrabbedObjectR = null; }
            if (p1GrabbedObjectL != null) { DropObject(p1Grab, p1GrabbedObjectL); p1GrabbedObjectL = null; }
            clawRL.sprite = openRL;
            clawRR.sprite = openRR;
        }

        // checks the direction of the rotation for player Two
        if (g2 < 0)//Left Claw
        {
            if (p2GrabbableObjectL != null)
            { p2GrabbedObjectL = p2GrabbableObjectL; }


            if (p2) p2.RotateAround(p2ClawL.position, new Vector3(0f, 0f, g2), -rotationSpeed * Time.deltaTime);   //Only rotate player 1 if the transform reference exists;
            //Debug.Log(p1GrabbedObjectL);
            if (p2GrabbedObjectL != null) { GrabObject(p2Grab, p2GrabbedObjectL, p2ClawL); }
            clawGL.sprite = closedGL;

        }
        else if (g2 > 0)//Right Claw
        {
            if (p2GrabbableObjectR != null)
            { p2GrabbedObjectR = p2GrabbableObjectR; }

            if (p2) p2.RotateAround(p2ClawR.position, new Vector3(0f, 0f, g2), -rotationSpeed * Time.deltaTime);   //Only rotate player 1 if the transform reference exists;     
            //Debug.Log("Right");
            if (p2GrabbedObjectR != null) { GrabObject(p2Grab, p2GrabbedObjectR, p2ClawR); }
            clawGR.sprite = closedGR;
        }
        else
        {
            if (p2GrabbedObjectR != null) { DropObject(p2Grab, p2GrabbedObjectR); p2GrabbedObjectR = null; }
            if (p2GrabbedObjectL != null) { DropObject(p2Grab, p2GrabbedObjectL); p2GrabbedObjectL = null; }
            clawGL.sprite = openGL;
            clawGR.sprite = openGR;
        }

        //Debug.Log(p1IsGrounded);
        //jump for player one only when on the ground
        if (p1Jump.action.ReadValue<float>() == 1 && p1IsGrounded)
        {
            rb1.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            p1IsGrounded = false;
        }

        //jump for player two only when on the ground
        if (p2Jump.action.ReadValue<float>() == 1 && p2IsGrounded)
        {
            rb2.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            p2IsGrounded = false;
        }


        if (roundReset.action.ReadValue<float>() == 1)
        {
            roundController.roundReset();
        }

        if (roundNext.action.ReadValue<float>() == 1)
        {
            roundController.roundNext();
        }

        //checks if player 1 is grounded
        Collider[] hitcolliders = Physics.OverlapSphere(rb1.position, 1f);
        foreach (var hitcollider in hitcolliders)
        {
            if (hitcollider.tag == "Ground" && hitcollider.tag != "Claw" && hitcollider.gameObject != p1GrabbedObjectL && hitcollider.gameObject != p1GrabbedObjectR)
            {
                p1IsGrounded = true;
            }
        }

        // checks if player 2 is grounded
        Collider[] hitcolliders2 = Physics.OverlapSphere(rb2.position, 1f);
        foreach (var hitcollider in hitcolliders2)
        {
            if (hitcollider.tag == "Ground" && hitcollider.tag != "Claw" && hitcollider.gameObject != p2GrabbedObjectL && hitcollider.gameObject != p2GrabbedObjectR)
            {
                p2IsGrounded = true;
            }
        }


    }

    //a gizmo for player 1 ground check
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(rb1.position, 1f);
    }

    

    private void GrabObject(InputActionReference grabReference, GameObject grabbedObject, Transform parentClaw)
    {
        Debug.Log("InsideGrab");
        if (grabReference.action.WasPressedThisFrame())
        {
            Debug.Log("InsideIf");
            grabbedObject.transform.SetParent(parentClaw);
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }
        
        
    }
    //called when a player drops an item
    private void DropObject(InputActionReference grabReference, GameObject grabbedObject)
    {
        if (grabReference.action.WasReleasedThisFrame())
        {
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            grabbedObject.transform.SetParent(null);
            grabbedObject = null;
        }
        
    }

    public void playerRotationReset()
    {
        //Reset all rotations of players to zero
        p1.eulerAngles = new Vector3(0, 0, 0);
        p2.eulerAngles = new Vector3(0, 0, 0);
    }

    public void EnablePlayerActions()
    {
        p1Move.action.Enable();          //Enables player One's input action so it can read input
        p1Grab.action.Enable();
        p1Jump.action.Enable();

        p2Move.action.Enable();          //Enables player Two's input action so it can read input
        p2Grab.action.Enable();
        p2Jump.action.Enable();
    }

    public void DisablePlayerActions()
    {
        p1Move.action.Disable();        //Disables player One's action (stops reading input)
        p1Grab.action.Disable();
        p1Jump.action.Disable();

        p2Move.action.Disable();        //Disables player Two's action (stops reading input)
        p2Grab.action.Disable();
        p2Jump.action.Disable();
    }

}

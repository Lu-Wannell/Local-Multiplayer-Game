using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplePlayerKeyboard : MonoBehaviour
{
    [Header("Actions (drag from your Input Actions asset")] // Adds a label in the inspector for organisation
    [SerializeField] private InputActionReference p1Move;
    [SerializeField] private InputActionReference p1Jump;
    [SerializeField] private InputActionReference p1Grab;

    [SerializeField] private InputActionReference p2Move;
    [SerializeField] private InputActionReference p2Jump;
    [SerializeField] private InputActionReference p2Grab;

    [SerializeField] private InputActionReference roundReset;
    [SerializeField] private InputActionReference roundNext;

    [Header("Players")]
    [SerializeField] private Transform p1;       //Transform for Player One
    [SerializeField] private Transform p2;       //Transform for Player Two

    [SerializeField] private Transform p1ClawR;
    [SerializeField] private Transform p1ClawL;
    [SerializeField] private Transform p2ClawR;
    [SerializeField] private Transform p2ClawL;


    [SerializeField] private Rigidbody rb1;
    [SerializeField] private Rigidbody rb2;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float jumpVelocity = 0.1f;

    [Header("Level")]
    [SerializeField] private bool p1IsGrounded = true;
    [SerializeField] private bool p2IsGrounded = true;

    [Header("Scripts")]
    [SerializeField] private RoundController roundController;

    private void OnEnable()        //Called when the component becomes enabled/active
    {
        p1Move.action.Enable();          //Enables player One's input action so it can read input
        p1Grab.action.Enable();
        p1Jump.action.Enable();

        p2Move.action.Enable();          //Enables player Two's input action so it can read input
        p2Grab.action.Enable();
        p2Jump.action.Enable();

        roundReset.action.Enable();
        roundNext.action.Enable();
    }
        
    private void OnDisable()        //Called when the component becomes disabled/inactive
    {
        p1Move.action.Disable();        //Disables player One's action (stops reading input)
        p1Grab.action.Disable();
        p1Jump.action.Disable();

        p2Move.action.Disable();        //Disables player Two's action (stops reading input)
        p2Grab.action.Disable();
        p2Jump.action.Disable();

        roundReset.action.Disable();
        roundNext.action.Disable();
    }

    private void Update()
    {
        var m1 = p1Move.action.ReadValue<float>();
        var m2 = p2Move.action.ReadValue<float>();

        var g1 = p1Grab.action.ReadValue<float>();
        var g2 = p2Grab.action.ReadValue<float>();

        if (p1) p1.position += new Vector3(m1, 0f, 0f) * speed * Time.deltaTime;       //Only move player 1 if the transform reference exists;
        if (p2) p2.position += new Vector3(m2, 0f, 0f) * speed * Time.deltaTime;       //Only move player 2 if the transform reference exists;


        // checks the direction of the rotation for player One
        if(g1 >0)
        {
            if (p1) p1.RotateAround(p1ClawL.position, new Vector3(0f, 0f, g1), -rotationSpeed * Time.deltaTime);   //Only rotate player 1 if the transform reference exists;
        }
        else if(g1< 0)
        {
            if (p1) p1.RotateAround(p1ClawR.position, new Vector3(0f, 0f, g1), -rotationSpeed * Time.deltaTime);   //Only rotate player 1 if the transform reference exists;
        }

        // checks the direction of the rotation for player Two
        if (g2 > 0)
        {
            if (p2) p2.RotateAround(p2ClawL.position, new Vector3(0f, 0f, g2), -rotationSpeed * Time.deltaTime);   //Only rotate player 2 if the transform reference exists;
        }
        else if (g2 < 0)
        {
            if (p2) p2.RotateAround(p2ClawR.position, new Vector3(0f, 0f, g2), -rotationSpeed * Time.deltaTime);   //Only rotate player 2 if the transform reference exists;
        }


        if (p1Jump.action.ReadValue<float>() == 1 && p1IsGrounded)
        {
            rb1.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            p1IsGrounded = false;
        }


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

        Collider[] hitcolliders = Physics.OverlapSphere(rb1.position, 1f);
        foreach (var hitcollider in hitcolliders)
        {
            if (hitcollider.tag == "Ground")
            {
                p1IsGrounded = true;
            }
        }

        Collider[] hitcolliders2 = Physics.OverlapSphere(rb2.position, 1f);
        foreach (var hitcollider in hitcolliders2)
        {
            if (hitcollider.tag == "Ground")
            {
                p2IsGrounded = true;
            }
        }


    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(rb1.position, 1f);
    }

}

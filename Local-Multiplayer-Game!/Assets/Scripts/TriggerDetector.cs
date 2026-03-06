using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    [SerializeField] public bool canWalk;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            canWalk = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            canWalk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            canWalk = false;
        }
    }
}

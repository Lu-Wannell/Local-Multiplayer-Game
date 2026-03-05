using UnityEngine;

public class WinZone : MonoBehaviour
{
    [SerializeField] private Transform claw;
    [SerializeField] private Transform players;

    public Vector3 boxCenter = Vector3.zero;
    public Vector3 boxSize = Vector3.one;
    public LayerMask playerLayer;

    private Rigidbody winningPlayerRb;
    public bool isPlayerOneWinning = false;
    public bool isPlayerTwoWinning = false;

    [SerializeField] private ParticleSystem confetti;

    [SerializeField] private MultiplePlayerKeyboard multiplePlayerKeyboard;
    [SerializeField] private ClawController clawController;

    
    private void OnTriggerStay(Collider other)
    {
        
        if (other.attachedRigidbody != null && clawController.isClosed && winningPlayerRb == null)
        {
            winningPlayerRb = other.attachedRigidbody;
            winningPlayerRb.transform.SetParent(transform);
            winningPlayerRb.isKinematic = true;
            Instantiate(confetti);
            Debug.Log(other.gameObject);

        }
    }

    public void EnablePlayer()
    {
        multiplePlayerKeyboard.p1.position = multiplePlayerKeyboard.p1StartPos.position;
        multiplePlayerKeyboard.p2.position = multiplePlayerKeyboard.p2StartPos.position;
        if (winningPlayerRb != null)
        {
            winningPlayerRb.isKinematic = false;
            winningPlayerRb.transform.SetParent(null);
            winningPlayerRb = null;
        }
        
    }

    public void CheckForPlayers()
    {
        //convert the check space from local space to world space so it can move to alternate locations;
        Vector3 worldCenter = transform.TransformPoint(boxCenter);
        Vector3 worldHalfExtents = transform.TransformVector(boxSize * 0.5f);
        Quaternion worldRotation = transform.rotation;

        Collider[] hitcollider = Physics.OverlapBox(worldCenter, worldHalfExtents, worldRotation, playerLayer);

        if (hitcollider.Length > 0 )
        {
            Instantiate(confetti);
            foreach (Collider collider in hitcollider) {}
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(boxCenter,boxSize);
    }
}

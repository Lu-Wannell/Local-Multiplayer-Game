using UnityEngine;

public class WinZone : MonoBehaviour
{
    [SerializeField] private Transform claw;
    [SerializeField] private Transform players;

    public Vector3 boxCenter = Vector3.zero;
    public Vector3 boxSize = Vector3.one;
    public LayerMask playerLayer;

    public bool isPlayerOneWinning = false;
    public bool isPlayerTwoWinning = false;

    [SerializeField] private ParticleSystem confetti;

    [SerializeField] private MultiplePlayerKeyboard multiplePlayerKeyboard;

    
        
    

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

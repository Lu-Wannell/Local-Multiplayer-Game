using UnityEngine;

public class ClawController : MonoBehaviour
{
    [SerializeField] private bool isDescending;
    [SerializeField] private float moveSpeed;

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
        if (isDescending)
        {
            clawCurrentPosition.position = Vector3.MoveTowards(clawCurrentPosition.position, clawEndPoint.position, (moveSpeed * Time.deltaTime));
        }
    }
}

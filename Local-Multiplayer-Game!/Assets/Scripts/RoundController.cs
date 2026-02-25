using UnityEngine;

public class RoundController : MonoBehaviour
{
    
    [SerializeField] private int currentRound;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void roundReset()
    {
        currentRound = 1;
    }

    private void roundNext()
    {
        currentRound += currentRound;
    }
}

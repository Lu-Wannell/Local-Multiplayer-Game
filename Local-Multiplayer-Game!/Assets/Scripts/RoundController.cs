using UnityEngine;

public class RoundController : MonoBehaviour
{
    
    [SerializeField] private int currentRound = 0;
    [SerializeField] private float roundTimeRemaining = 45f;
    [SerializeField] private float startingRoundTime = 45f;
    [SerializeField] private bool isRoundGoing = false;
    [SerializeField] private bool isRoundOver = false;

    [SerializeField] private ClawController clawController;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRoundGoing)
        {
            if (roundTimeRemaining > 0)
            {
                roundTimeRemaining -= Time.deltaTime;               
            }
            else
            {               
                roundTimeRemaining = 0;
                isRoundGoing = false;
                
            }
        }
    }

    public void roundReset()
    {
        currentRound = 1;
    }

    public void roundNext()
    {
        currentRound += 1;
        roundTimeRemaining = startingRoundTime;
        isRoundGoing = true;
        clawController.clawReset();
    }
}

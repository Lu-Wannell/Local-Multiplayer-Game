using UnityEngine;

public class RoundController : MonoBehaviour
{
    
    [SerializeField] private int currentRound;
    [SerializeField] private float roundTimeRemaining = 45f;
    [SerializeField] private float startingRoundTime = 45f;
    [SerializeField] private bool isTimerCounting = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerCounting)
        {
            if (roundTimeRemaining > 0)
            {
                roundTimeRemaining -= Time.deltaTime;               
            }
            else
            {               
                roundTimeRemaining = 0;
                isTimerCounting = false;
            }
        }
    }

    private void roundReset()
    {
        currentRound = 1;
    }

    private void roundNext()
    {
        currentRound += currentRound;
        roundTimeRemaining = startingRoundTime;
        isTimerCounting = true;
    }
}

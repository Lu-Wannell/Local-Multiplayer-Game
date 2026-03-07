using UnityEngine;
using DG.Tweening;

public class ObjectShaker : MonoBehaviour
{

    [SerializeField] private Transform objectTransform;

    [SerializeField] private float duration;// duration of Tween
    [SerializeField] private Vector3 strength;// strength of shake on each axis
    [SerializeField] private int vibrato;// how much shake will vibrate
    [SerializeField] private float randomness; //Adds randomness to shake
    [SerializeField] private bool snapping;// when true tween snaps to intergers
    [SerializeField] private bool fadeOut = true;//shake will automatically fade out smoothly
    [SerializeField] private ShakeRandomnessMode randomnessMode;
  

    public void ShakeClaw()
    { transform.DOShakePosition(duration, strength, vibrato, randomness, snapping, fadeOut, randomnessMode); }    

}

using UnityEngine;

public class SpriteCycler : MonoBehaviour
{
    public Sprite[] sprites;
    public float cycleTime = 0.5f;

    private SpriteRenderer sr;
    private int index = 0;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sprites.Length > 0)
        {
            sr.sprite = sprites[0];
            InvokeRepeating("NextSprite", cycleTime, cycleTime);
        }
    }

    void NextSprite()
    {
        index = (index + 1) % sprites.Length;
        sr.sprite = sprites[index];
    }
}

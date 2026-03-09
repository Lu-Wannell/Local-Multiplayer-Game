using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class OptionCycler : MonoBehaviour
{
    public TMP_Text accessoryText;
    public List<string> accessories;
    private int currentIndex = 0;

    void Start()
    {
        if (accessories.Count > 0)
            accessoryText.text = accessories[currentIndex];
    }

    public void NextAccessory()
    {
        if (accessories.Count == 0) return;
        currentIndex = (currentIndex + 1) % accessories.Count;
        accessoryText.text = accessories[currentIndex];
    }

    public void PreviousAccessory()
    {
        if (accessories.Count == 0) return;
        currentIndex = (currentIndex - 1 + accessories.Count) % accessories.Count;
        accessoryText.text = accessories[currentIndex];
    }
}



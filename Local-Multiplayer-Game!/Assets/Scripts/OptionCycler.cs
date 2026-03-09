using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class OptionCycler : MonoBehaviour
{

    public TMP_Text accessoryText;
    public List<string> accessories;
    public List<GameObject> accessoryImages;

    int currentIndex = 0;

    void Start()
    {
        UpdateAccessory();
    }

    public void NextAccessory()
    {
        currentIndex = (currentIndex + 1) % accessories.Count;
        UpdateAccessory();
    }

    public void PreviousAccessory()
    {
        currentIndex = (currentIndex - 1 + accessories.Count) % accessories.Count;
        UpdateAccessory();
    }

    void UpdateAccessory()
    {
        accessoryText.text = accessories[currentIndex];

        for (int i = 0; i < accessoryImages.Count; i++)
            accessoryImages[i].SetActive(i == currentIndex);
    }
}



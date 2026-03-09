using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnotherAccessory : MonoBehaviour
{
    public string playerID = "Player2";   
    public List<GameObject> accessories;
    public TMP_Text selectText;

    public int currentIndex = 0;

    void Start()
    {
        UpdateAccessory();
    }

    public void Next()
    {
        currentIndex = (currentIndex + 1) % accessories.Count;
        UpdateAccessory();
    }

    public void Previous()
    {
        currentIndex = (currentIndex - 1 + accessories.Count) % accessories.Count;
        UpdateAccessory();
    }

    public void Select()
    {
        PlayerPrefs.SetInt(playerID + "_Accessory", currentIndex);
        PlayerPrefs.Save();
        if (selectText != null)
            selectText.text = "Selected";

        Debug.Log(playerID + " selected index: " + currentIndex);
    }

    void UpdateAccessory()
    {
        for (int i = 0; i < accessories.Count; i++)
            accessories[i].SetActive(i == currentIndex);
    }
}

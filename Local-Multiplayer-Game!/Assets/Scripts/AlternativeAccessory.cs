using System.Collections.Generic;
using UnityEngine;

public class AlternativeAccessory : MonoBehaviour
{
    public string playerID = "Player2";
    public List<GameObject> accessories;

    void Start()
    {
        int index = PlayerPrefs.GetInt(playerID + "_Accessory", 0);

        for (int i = 0; i < accessories.Count; i++)
        {
            accessories[i].SetActive(i == index);
        }

        Debug.Log(playerID + " loaded accessory: " + index);
    }
}

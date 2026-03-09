using TMPro;
using UnityEngine;

public class Select : MonoBehaviour
{
    public TMP_Text selectText;
    public GameObject leftButton;
    public GameObject rightButton;

    public void SelectObject()
    {
        selectText.text = "Selected";
        leftButton.SetActive(false);
        rightButton.SetActive(false);
    }
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AssessoriesManager : MonoBehaviour
{
    //Changing the text for my assessory stuff and for when you click the select button ish
    public TMP_Text accessoryText;
    public TMP_Text selectText;
    //Just listing out my shizz
    public List<string> accessoryNames;
    public List<GameObject> accessories;
    //Buttons for scrollin'
    public GameObject leftButton;
    public GameObject rightButton;
    //Loading sample scene after both players have accepted
    public string nextSceneName;
    //Supposed to load the number of which object you selected but its a LITTLE B*TCH
    public int currentIndex = 0;
    //reading if you selected the buttons set-up
    static int playersReady = 0;

    void Start()
    {
        //supposed to load into sample scene but NOooOOOOOOooo
        if (accessoryText != null)
            UpdateAccessory();
        //b*tch
        LoadAccessory();
    }

    public void Next()
    { //rotatin' like rotisarie chicken~
        currentIndex = (currentIndex + 1) % accessoryNames.Count;
        UpdateAccessory();
    }

    public void Previous()
    {//goin' back
        currentIndex = (currentIndex - 1 + accessoryNames.Count) % accessoryNames.Count;
        UpdateAccessory();
    }

    public void Select()
    { //checking to see if both players have selected
        PlayerPrefs.SetInt(gameObject.name + "_Accessory", currentIndex); //b*tchy index doesn't motherf*cking work... ugh

        if (selectText != null)
            selectText.text = "Selected";
        // disabling choice, cause I ain't coding choice changes, no, sir-ee
        if (leftButton != null)
            leftButton.SetActive(false);

        if (rightButton != null)
            rightButton.SetActive(false);

        playersReady++;
        //if you're confused by this, wtf are you coding girl, get a hobby
        if (playersReady >= 2 && nextSceneName != "") //its sample scene but I'm lazy
            SceneManager.LoadScene(nextSceneName);
    }

    void UpdateAccessory()
    { //setting sprites active for preview
        if (accessoryText != null)
            accessoryText.text = accessoryNames[currentIndex];

        for (int i = 0; i < accessories.Count; i++)
            accessories[i].SetActive(i == currentIndex); //ChAnGiNg ThE iMaGe~ OOOOOOOoooOOOOOh
    }

    void LoadAccessory()
    { // i hate you i hate you i hate you i hate you i hate you i hate you i hate you i hate you i hate you i hate you i hate you i hate you i hate you i hate you i hate you i hate you
        int index = PlayerPrefs.GetInt(gameObject.name + "_Accessory", 0);

        for (int i = 0; i < accessories.Count; i++)
            accessories[i].SetActive(i == index); //you suck, why dont you work? cow. >:(
    }
}

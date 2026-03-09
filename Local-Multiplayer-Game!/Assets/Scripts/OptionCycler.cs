using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class OptionCycler : MonoBehaviour
{

    public TMP_Text textUI;
    public string[] options = { "Hat", "Crown", "Glasses" };

    int index = 0;
    float prevInput = 0f;

    void Start()
    {
        textUI.text = options[index];
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Debug.Log("Movement event fired with value: " + context.ReadValue<float>());
        float input = context.ReadValue<float>();
        if (input != 0 && prevInput == 0)
        {
            index += input > 0 ? 1 : -1;
            if (index < 0) index = options.Length - 1;
            else if (index >= options.Length) index = 0;
            textUI.text = options[index];
        }
        prevInput = input;
    }
}



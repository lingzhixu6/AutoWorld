using Database;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Serialization;

public class RegisterBtn : MonoBehaviour
{
    private DataBridge _dataBridge;
    public InputField email_inputfield;
    public InputField company_inputfield;
    public InputField password_inputfield;
    public InputField retypePassword_inputfield;
    public Button register_button;
    
    void Start()
    {
        _dataBridge = DataBridge.GetInstance();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (email_inputfield.isFocused)
            {
                company_inputfield.Select();
            }
            else if (company_inputfield.isFocused)
            {
                password_inputfield.Select();
            }
            else if (password_inputfield.isFocused)
            {
                retypePassword_inputfield.Select();
            }
            else if (retypePassword_inputfield.isFocused)
            {
                email_inputfield.Select();
            }
        }
    }
    
    public void InvokeRegisterBtn()
    {
        if (password_inputfield.text != "" && retypePassword_inputfield.text != "" && email_inputfield.text != "" && company_inputfield.text != "")
        {
            if (password_inputfield.text.Equals(retypePassword_inputfield.text))
            {
                int defaultBalanceWhenRegister = 1000;
                _dataBridge.CreatePlayerWithEmailAndPassword(email_inputfield.text, company_inputfield.text, password_inputfield.text, defaultBalanceWhenRegister);
                ClearInputfield();
            }
            else
            {
                EditorUtility.DisplayDialog("Failure", "Passwords Do Not Match", "Close");
            }
        }
        else
        {
            EditorUtility.DisplayDialog("Failure", "Fill in All Details", "Close");
        }
    }

    private void ClearInputfield()
    {
        email_inputfield.text = "";
        company_inputfield.text = "";
        password_inputfield.text = "";
        retypePassword_inputfield.text = "";
    }


}

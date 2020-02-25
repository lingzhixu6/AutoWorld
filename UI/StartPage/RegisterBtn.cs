using System;
using Database;
using Firebase.Auth;
using Unity.UNetWeaver;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Serialization;
using IBM;

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
        Debug.Log("Register button Start() is called");
        _dataBridge = DataBridge.GetInstance();
        _dataBridge.UpdateGooglePlayService();
        // Test.Main();
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

    private bool CheckAllInputfieldsAreFilled()
    {
        if (password_inputfield.text != "" && retypePassword_inputfield.text != "" && email_inputfield.text != "" && company_inputfield.text != "")
        {
            return true;
        }
        EditorUtility.DisplayDialog("Failure", "Fill in All Details", "Close");
        return false;
    }

    private bool CheckPasswordIsValid()
    {
        if (password_inputfield.text.Equals(retypePassword_inputfield.text))
        {
            if (password_inputfield.text.Length >= 6)
            {
                return true;
            }
        }
        EditorUtility.DisplayDialog("Failure", "Passwords must match and be no shorter than 6 characters", "Close");
        return false;
    }
    
    public void InvokeRegisterBtn()
    {
        if (CheckAllInputfieldsAreFilled() && CheckPasswordIsValid())
        {
            _dataBridge = DataBridge.GetInstance();
            _dataBridge.CreatePlayerWithEmailAndPassword(email_inputfield.text,password_inputfield.text);
            _dataBridge.WritePlayer(email_inputfield.text, company_inputfield.text);
            ClearInputfield();
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

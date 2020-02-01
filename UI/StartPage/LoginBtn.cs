using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Database;
using Firebase.Auth;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


public class LoginBtn : MonoBehaviour
{
    public InputField email_inputfield;
    public InputField password_inputfield;
    private string _username;
    private string _password;
    private string _form;
    private DataBridge _dataBridge;
    
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
                password_inputfield.Select();
            }
            else if (password_inputfield.isFocused)
            {
                email_inputfield.Select();
            }
        }
    }
    
    public void InvokeLoginBtn()
    {
        if (email_inputfield.text != "" && password_inputfield.text != "")
        {
            AuthController.AuthUser(email_inputfield.text, password_inputfield.text);
            //above code is faulted. But that does not stop the code below executing!!
            _dataBridge.GetPlayer();
            email_inputfield.GetComponent<InputField>().text = "";
            password_inputfield.GetComponent<InputField>().text = "";
            SceneManager.LoadScene(0);
        }
        else
        {
            EditorUtility.DisplayDialog("Error", "Fill in all details to login", "Close");
        }
        
    }

    private bool CheckAuthStatus()
    {
        return FirebaseAuth.DefaultInstance.CurrentUser != null;
    }

}

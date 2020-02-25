using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Database;
using Firebase.Auth;
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
        Debug.Log("Login button Start() is called");
        _dataBridge = DataBridge.GetInstance();
        _dataBridge.UpdateGooglePlayService();
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
            AuthController authController = new AuthController();
            string temp1 = email_inputfield.text;
            string temp2 = password_inputfield.text;
            authController.AuthUser(temp1, temp2);
            _dataBridge = DataBridge.GetInstance();
            _dataBridge.ReadPlayer();
            if (DataBridge.playerEmail == null || Player.singletonPlayer == null)
            {
                EditorUtility.DisplayDialog("Waiting", "Waiting for response from authentication server, please try again.", "Close");
                return;
            }
            ClearInputfield();
            SceneManager.LoadScene(0);
            Debug.Log(Player.singletonPlayer.company);
            Debug.Log(Player.singletonPlayer.email);
            Debug.Log(Player.singletonPlayer.balance);
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

    private void ClearInputfield()
    {
        email_inputfield.text = "";
        password_inputfield.text = "";
    }
}

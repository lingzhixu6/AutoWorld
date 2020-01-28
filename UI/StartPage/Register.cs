using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;
using UnityEditor;

public class Register : MonoBehaviour
{
    public GameObject username;
    // public GameObject name;    This object is never initialized. May be the cause of nullReference error.
    public GameObject password;
    public GameObject retypePassword;
    private string Username;
    private string Company;
    private string Password;
    private string RetypePassword;
    private string form;
    AuthController ac;

    // Start is called before the first frame update
    void Start()
    {
        this.ac = gameObject.AddComponent<AuthController>();

    }

    public void RegisterButton()
    {
        //EditorUtility.DisplayDialog("Title here", "Your text", "Ok");
        if (Password != "" && RetypePassword != "" && Username != "" && Company != "")
        {
            if (Password.Equals(RetypePassword))
            {
                startRegister(Username, Company, Password);
                username.GetComponent<InputField>().text = "";
                name.GetComponent<InputField>().text = "";
                password.GetComponent<InputField>().text = "";
                retypePassword.GetComponent<InputField>().text = "";
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "Passwords Do Not Match", "OK");

            }
        }

        else
        {
            EditorUtility.DisplayDialog("Error", "Fill in All Details", "OK");

        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                name.GetComponent<InputField>().Select();
            }
            else if(name.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            else if(password.GetComponent<InputField>().isFocused)
            {
                retypePassword.GetComponent<InputField>().Select();
            }
            else if(retypePassword.GetComponent<InputField>().isFocused)
            {
                username.GetComponent<InputField>().Select();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            RegisterButton();
        }
        Username = username.GetComponent<InputField>().text;
        Company = name.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        RetypePassword = retypePassword.GetComponent<InputField>().text;
    }
    
    public void startRegister(string email, string company, string password)
    {
        string hashPassword = AuthController.Hash(password);
        // if (db.GetExists("Players", "companyName", companyName) == false)
        // {
        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, hashPassword).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                AuthController.getErrorMessage((AuthError)e.ErrorCode);
                FirebaseDatabase.DefaultInstance.GoOffline();
                return;
            }
            
            if (task.IsFaulted)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                AuthController.getErrorMessage((AuthError)e.ErrorCode);
                FirebaseDatabase.DefaultInstance.GoOffline();
                return;
            }
            
            if (task.IsCompleted)
            {    
                DataBridge.PostUser(company, email, balance);
                EditorUtility.DisplayDialog("Successful", "Registration Complete", "OK");
            }
        });
             
        //}

        // else
        // {
        EditorUtility.DisplayDialog("Error", "Company Name already exists", "OK");
        // }


    }


}

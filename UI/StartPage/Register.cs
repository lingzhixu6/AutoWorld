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
    public GameObject name;
    public GameObject password;
    public GameObject retypePassword;
    private string Username;
    private string Name;
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
        if (Password != "" && RetypePassword != "" && Username != "" && Name != "")
        {
            if (Password.Equals(RetypePassword))
            {
                startRegister(Username, Name, Password);
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
    // Update is called once per frame
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
        Name = name.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        RetypePassword = retypePassword.GetComponent<InputField>().text;

    }
    
    public void startRegister(string email, string companyName, string password)
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
                DataBridge.PostUser(companyName, email);
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

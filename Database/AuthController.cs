using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Firebase;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Unity.Editor;
using UnityEditor;

public class AuthController : MonoBehaviour
{
    
    public void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                if (task.Exception != null)
                {
                    Debug.LogError("Failed to initialise Firebase" + task.Exception.ToString());
                }
   
            }
        );
    }
    
   
    public void AuthUser(string email, string password)
    {
      //  string hashPassword = Hash(password);
        Credential credential = EmailAuthProvider.GetCredential(email, password);
        FirebaseAuth.DefaultInstance.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                getErrorMessage((AuthError)e.ErrorCode);
            } 
            if (task.IsFaulted)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                getErrorMessage((AuthError)e.ErrorCode);
            }
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }
    

    public void Logout()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            FirebaseAuth.DefaultInstance.SignOut();
        }
    }

    public static void getErrorMessage(AuthError errorCode)
    {
        string msg = "";
        msg = errorCode.ToString();
        switch (errorCode)
        {
            case AuthError.InvalidEmail:
                msg = "Email entered is invalid";
                break;
            case AuthError.WrongPassword:
                msg = "Wrong Password";
                break;
            case AuthError.AccountExistsWithDifferentCredentials:
                msg = "Account already exists with different credentials";
                break;
            case AuthError.WeakPassword:
                msg = "Weak password entered";
                break;
            case AuthError.EmailAlreadyInUse:
                msg = "Email entered has already been used";
                break;
        }
        EditorUtility.DisplayDialog("Error", msg, "OK");

    }
    

    public static string Hash(string stringToHash) //done for extra security even though firebase is already secure
    {
        using (var sha1 = new SHA1Managed())
        {
            return BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(stringToHash)));
        }
    }

}

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

namespace Database
{
    public class AuthController
    {

        public void AuthUser(string email, string password)
        {
            FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
                if (task.IsCanceled) {
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted) {
                    Debug.LogError(task.Exception);
                    return;
                }
                if (task.IsCompleted)
                {
                    DataBridge.playerEmail = email;
                }
            });
        }
        
        
        // public static void PromptErrorMessage(AuthError errorCode)
        // {
        //     string msg = "";
        //     msg = errorCode.ToString();
        //     switch (errorCode)
        //     {
        //         case AuthError.InvalidEmail:
        //             msg = "Email entered is invalid";
        //             break;
        //         case AuthError.WrongPassword:
        //             msg = "Wrong Password";
        //             break;
        //         case AuthError.AccountExistsWithDifferentCredentials:
        //             msg = "Account already exists with different credentials";
        //             break;
        //         case AuthError.WeakPassword:
        //             msg = "Weak password entered";
        //             break;
        //         case AuthError.EmailAlreadyInUse:
        //             msg = "Email entered has already been used";
        //             break;
        //     }
        //     EditorUtility.DisplayDialog("Error", msg, "Close");
        // }

        
        // public void Logout()
        // {
        //     if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        //     {
        //         FirebaseAuth.DefaultInstance.SignOut();
        //     }
        // }

    }
}
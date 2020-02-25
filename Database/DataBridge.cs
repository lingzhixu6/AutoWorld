using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using UnityEngine;
using Firebase.Unity.Editor;
using Firebase.Auth;
using UnityEditor;

namespace Database
{
    public class DataBridge
    {
        //private static bool _dataExists; 
        private static DatabaseReference _rootReference = FirebaseDatabase.DefaultInstance.RootReference;
        private static readonly DataBridge _dataBridge;
        public static string playerEmail; 
        
        private DataBridge()
        {
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://autoworld-2.firebaseio.com/");
        }
        
        public static DataBridge GetInstance()
        {
            return new DataBridge();         
        }

        public void UpdateGooglePlayService()            //As required by Firebase Guide Doc
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available) {
                    // Create and hold a reference to your FirebaseApp,
                    // where app is a Firebase.FirebaseApp property of your application class.
                    //   app = Firebase.FirebaseApp.DefaultInstance;

                    // Set a flag here to indicate whether Firebase is ready to use by your app.
                } else {
                    UnityEngine.Debug.LogError(System.String.Format(
                        "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    // Firebase Unity SDK is not safe to use here.
                }
            });
        }
        
        string encodeUserEmail(string userEmail) {
            return userEmail.Replace(".", ",");
        }       
        string decodeUserEmail(string userEmail) {
            return userEmail.Replace(",", ".");
        }
        
        public void WritePlayer(string email, string company)
        {
            int defaultBalance = 1000;
            // Player newPlayer = new Player(company, email, defaultBalance); 
            // string json = JsonUtility.ToJson(newPlayer);
            string encodedEmail = encodeUserEmail(email);
            _rootReference.Child("Players").Child(encodedEmail).Child("company").SetValueAsync(company);
            _rootReference.Child("Players").Child(encodedEmail).Child("balance").SetValueAsync(defaultBalance);
        }
        
        public void ReadPlayer()
        {
            FirebaseDatabase.DefaultInstance.GetReference("Players").GetValueAsync().ContinueWith(
                task =>
                {
                    if (task.IsFaulted)
                    {
                        EditorUtility.DisplayDialog("Error", "Get player faulted", "Close");
                    }

                    if (task.IsCanceled)
                    {
                        EditorUtility.DisplayDialog("Error", "Get player canceled", "Close");
                    }

                    if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;
                        string companyResult = snapshot.Child(encodeUserEmail(playerEmail)).Child("company").GetRawJsonValue();
                        string balanceResult = snapshot.Child(encodeUserEmail(playerEmail)).Child("balance").GetRawJsonValue();
                        Player.singletonPlayer = new Player(companyResult, decodeUserEmail(playerEmail), balanceResult);
                    }
                },TaskScheduler.FromCurrentSynchronizationContext());
        }
        
        
        // private void Exists(String table, String record, String item)
        // {
        //     var id = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        //     FirebaseDatabase.DefaultInstance
        //         .GetReference(table).OrderByChild(record).EqualTo(item)
        //         .GetValueAsync().ContinueWith(task =>
        //         {
        //             if (task.IsFaulted)
        //             {
        //                 Firebase.FirebaseException e =
        //                     task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
        //                 Debug.Log(e.ToString());
        //             }
        //             else if (task.IsCompleted)
        //             {
        //                 DataSnapshot snapshot = task.Result;
        //                 if (snapshot.Exists)
        //                 {
        //                     _dataExists = true;
        //                 }
        //             }
        //         });
        // }

        
        // public bool GetExists(String table, String record, String item)
        // {
        //     _dataExists = false;
        //     Exists(table, record, item);
        //     return _dataExists;
        // }
        

        public int GetPlayerBalance()
        {
            int balanceResult = 0;
            FirebaseDatabase.DefaultInstance.GetReference("Players").Child(playerEmail).Child("balance").GetValueAsync().ContinueWith(
                task => 
                {
                    if (task.IsFaulted) {
                        EditorUtility.DisplayDialog("Error", "Get balance faulted", "Close");
                    }
                    else if (task.IsCompleted) {
                        DataSnapshot snapshot = task.Result;
                        balanceResult = (int) snapshot.Value;
                    }
                });
            return balanceResult;
        }

        public void UpdatePlayerMaterialQty()        //Update = Get and Post
        {
            int steelQtyBeforePurchase = 0;
            int glassQtyBeforePurchase = 0;
            int aluminumQtyBeforePurchase = 0;
            int rubberQtyBeforePurchase = 0;
            FirebaseDatabase.DefaultInstance.GetReference("Players").Child(playerEmail).GetValueAsync().ContinueWith(
                task => 
                {
                    if (task.IsFaulted) {
                        EditorUtility.DisplayDialog("Error", "Purchase Failed -Fail to retrieve player material amount", "Close");
                    }
                    else if (task.IsCompleted) {
                        DataSnapshot snapshot = task.Result;
                        steelQtyBeforePurchase = (int) snapshot.Child("Material Possession").Child("Steel").Value;
                        glassQtyBeforePurchase = (int) snapshot.Child("Material Possession").Child("Glass").Value;
                        aluminumQtyBeforePurchase = (int) snapshot.Child("Material Possession").Child("Aluminum").Value;
                        rubberQtyBeforePurchase = (int) snapshot.Child("Material Possession").Child("Rubber").Value;
                        //Dangerous Nested Call to post below
                        string steelQtyAfterPurchase = JsonUtility.ToJson(steelQtyBeforePurchase + InputFieldBean.steelQuantity);
                        _rootReference.Child("Players").Child(playerEmail).SetRawJsonValueAsync(steelQtyAfterPurchase);
                        string glassQtyAfterPurchase = JsonUtility.ToJson(glassQtyBeforePurchase + InputFieldBean.glassQuantity);
                        _rootReference.Child("Players").Child(playerEmail).SetRawJsonValueAsync(glassQtyAfterPurchase);
                        string aluminumQtyAfterPurchase = JsonUtility.ToJson(aluminumQtyBeforePurchase + InputFieldBean.aluminumQuantity);
                        _rootReference.Child("Players").Child(playerEmail).SetRawJsonValueAsync(aluminumQtyAfterPurchase);
                        string rubberQtyAfterPurchase = JsonUtility.ToJson(rubberQtyBeforePurchase + InputFieldBean.rubberQuantity);
                        _rootReference.Child("Players").Child(playerEmail).SetRawJsonValueAsync(rubberQtyAfterPurchase);
                    }
                });
        }

        public void CreatePlayerWithEmailAndPassword(string email, string password)
        {
            FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError(task.Exception);
                    return;
                }

                if (task.IsCompleted)
                {
                    EditorUtility.DisplayDialog("Success", "Registration Succeeded", "Close");
                    // string filePath = "/Users/lingzhixu/Desktop"; 
                    // using (FileStream fs = File.Create(filePath))     
                    // {    
                    //     // Add some text to file    
                    //     Byte[] title = new UTF8Encoding(true).GetBytes("New Text File");    
                    //     fs.Write(title, 0, title.Length);    
                    //     byte[] author = new UTF8Encoding(true).GetBytes("Mahesh Chand");    
                    //     fs.Write(author, 0, author.Length);    
                    // }    
                } 
            },TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
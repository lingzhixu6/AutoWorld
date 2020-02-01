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
        private static bool _dataExists; 
        private static DatabaseReference _rootReference;
        private static readonly DataBridge _dataBridge;
        private static string _playerID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        private DataBridge()
        {
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://autoworld-3e603.firebaseio.com/");
            _rootReference = FirebaseDatabase.DefaultInstance.RootReference;
        }
        
        public static DataBridge GetInstance()
        {
            return _dataBridge ?? new DataBridge();          //Null Coalescing Operator in C#
        }
        
        public void GetPlayer()
        {
            FirebaseDatabase.DefaultInstance.GetReference("Players").Child(_playerID).GetValueAsync().ContinueWith(
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
                        string company = (string) snapshot.Child("company").Value;
                        string email = (string) snapshot.Child("email").Value;
                        int balance = (int) snapshot.Child("balance").Value;
                        Player.player = new Player(company, email, balance);
                        EditorUtility.DisplayDialog("Error", "Get player SUCCEEDED!", "Close");
                    }
                });
        }

        // public string GetPlayerCompany()
        // {
        //     return player.company;
        // }
        
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
            FirebaseDatabase.DefaultInstance.GetReference("Players").Child(_playerID).Child("balance").GetValueAsync().ContinueWith(
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
            FirebaseDatabase.DefaultInstance.GetReference("Players").Child(_playerID).GetValueAsync().ContinueWith(
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
                        _rootReference.Child("Players").Child(_playerID).SetRawJsonValueAsync(steelQtyAfterPurchase);
                        string glassQtyAfterPurchase = JsonUtility.ToJson(glassQtyBeforePurchase + InputFieldBean.glassQuantity);
                        _rootReference.Child("Players").Child(_playerID).SetRawJsonValueAsync(glassQtyAfterPurchase);
                        string aluminumQtyAfterPurchase = JsonUtility.ToJson(aluminumQtyBeforePurchase + InputFieldBean.aluminumQuantity);
                        _rootReference.Child("Players").Child(_playerID).SetRawJsonValueAsync(aluminumQtyAfterPurchase);
                        string rubberQtyAfterPurchase = JsonUtility.ToJson(rubberQtyBeforePurchase + InputFieldBean.rubberQuantity);
                        _rootReference.Child("Players").Child(_playerID).SetRawJsonValueAsync(rubberQtyAfterPurchase);
                    }
                });
        }

        public void CreatePlayerWithEmailAndPassword(string email, string company, string password, int balance)
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
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                // if (task.IsCompleted)
                // {
                //     Player newPlayer = new Player(company, email, balance);
                //     var id = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
                //     string json = JsonUtility.ToJson(newPlayer);
                //     
                //     _rootReference.Child("Players").Child(id).SetRawJsonValueAsync(json);                    EditorUtility.DisplayDialog("Success", "Registration Succeeded", "Close");
                // }
             });
        }
    }
}
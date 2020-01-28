using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using UnityEngine;
using Firebase.Unity.Editor;
using Firebase.Auth;
using UnityEditor;

namespace DataBridgeNS
{
    public class DataBridge
    {
        public static volatile Player player;
        private static bool _dataExists; 
        private static DatabaseReference _playerReference;
        private static DatabaseReference _rootReference;
        private static DataBridge _dataBridge = new DataBridge();
        private static string _playerID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        private DataBridge()
        {
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://autoworld-3e603.firebaseio.com/");
            _playerReference = FirebaseDatabase.DefaultInstance.GetReference("Players");
            _rootReference = FirebaseDatabase.DefaultInstance.RootReference;
        }
        
        public static DataBridge GetInstance()
        {
            return _dataBridge;
        }
        
        
        
        public void GetPlayer()
        {
            FirebaseDatabase.DefaultInstance.GetReference("Players").Child(_playerID).GetValueAsync().ContinueWith(
                task =>
                {
                    if (task.IsFaulted)
                    {
                        Firebase.FirebaseException e =
                            task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                        Debug.Log(e.ToString());
                    }

                    if (task.IsCanceled)
                    {
                        //Do something
                    }

                    if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;
                        string company = (string) snapshot.Child("company").Value;
                        string email = (string) snapshot.Child("email").Value;
                        string balance = (string) snapshot.Child("balance").Value;
                        player = new Player(company, email, balance);
                    }
                });
        }

        public string GetPlayerCompany()
        {
            return player.company;
        }
        
        private void Exists(String table, String record, String item)
        {
            var id = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
            FirebaseDatabase.DefaultInstance
                .GetReference(table).OrderByChild(record).EqualTo(item)
                .GetValueAsync().ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Firebase.FirebaseException e =
                            task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                        Debug.Log(e.ToString());
                    }
                    else if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;
                        if (snapshot.Exists)
                        {
                            _dataExists = true;
                        }
                    }
                });
        }

        public bool GetExists(String table, String record, String item)
        {
            _dataExists = false;
            Exists(table, record, item);
            return _dataExists;
        }


        public void PostUser(string company, string email, string balance)
        {
            Player newPlayer = new Player(company, email, balance);
            var id = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
            string json = JsonUtility.ToJson(newPlayer);
            _rootReference.Child("Players").Child(id).SetRawJsonValueAsync(json);
        }

        private int GetPlayerBalance()
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
    }
}
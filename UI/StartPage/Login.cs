using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;


public class Login : MonoBehaviour
{
    public GameObject usernameLog;
    public GameObject passwordLog;
    private string Username;
    private string Password;
    private string form;
    AuthController ac;
    private DataBridge dataBridge;
    
    // Start is called before the first frame update
    void Start()
    {
        ac = gameObject.AddComponent<AuthController>();
        dataBridge = new DataBridge();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (usernameLog.GetComponent<InputField>().isFocused)
            {
                passwordLog.GetComponent<InputField>().Select();
            }
            else if (passwordLog.GetComponent<InputField>().isFocused)
            {
                usernameLog.GetComponent<InputField>().Select();
            }
        }
        Username = usernameLog.GetComponent<InputField>().text; 
        Password = passwordLog.GetComponent<InputField>().text;
     
    }
    
    public void InitLogin()
    {
        if (Username != "" && Password != "")
        {
            ac.AuthUser(Username, Password);
            dataBridge.InitUser();
            usernameLog.GetComponent<InputField>().text = "";
            passwordLog.GetComponent<InputField>().text = "";
            int overtimeCheck = 0;
            while (DataBridge.player == null && overtimeCheck++ < 100)
            {
            } 
            SceneManager.LoadScene(0);
        }
        else
        {
            EditorUtility.DisplayDialog("Error", "Fill in all details", "OK");
        }
        
    }

}

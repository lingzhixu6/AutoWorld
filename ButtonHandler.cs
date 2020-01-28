using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hi");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ClickDashboard()
    {
        SceneManager.LoadScene(0);
    }

    public void ClickBank()
    {
        SceneManager.LoadScene(2);
    }

    public void ClickFactory()
    {
        SceneManager.LoadScene(1);
    }

    public void ClickNews()
    {
        SceneManager.LoadScene(3);
    }

    public void ClickStart()
    {
        SceneManager.LoadScene(4);
    }

    public void ClickMarket()
    {
        SceneManager.LoadScene(5);
    }

    public void ClickOffice()
    {
        SceneManager.LoadScene(8);

    }

    public void ClickEmployeeMarket()
    {
        SceneManager.LoadScene(6);

    }
    public void ClickSettings()
    {
        SceneManager.LoadScene(7);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimpleSideMenu : MonoBehaviour
{
  


    public void loadBankScene()
    {
        SceneManager.LoadScene(2);
    }
    public void loadFactoryScene()
    {
        SceneManager.LoadScene(1);

    }

    public void loadNewsScene()
    {
        SceneManager.LoadScene(3);

    }
    public void loadSettingsScene()
    {
        SceneManager.LoadScene(7);

    }
    public void loadDashboardScene()
    {
        SceneManager.LoadScene(0);

    }
    public void loadMarterialMarketScene()
    {
        SceneManager.LoadScene(5);

    }
    public void loadLabourMarketScene()
    {
        SceneManager.LoadScene(6);

    }

}

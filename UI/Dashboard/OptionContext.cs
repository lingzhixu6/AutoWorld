using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionContext : MonoBehaviour 
{
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
    
    
    
    public Dropdown myDropdown;
    public Text myTextBox; //public GameObject myTextBox2;
    
    public void ShowChosenOptionContext()
    {
        switch (myDropdown.value)
        {
            case 0:
                myTextBox.text = ""; //myTextBox2.GetComponent<Text>().text = "Data imported from 1";
                break;
            case 1:
                myTextBox.text = "Data imported from Material Market"; //myTextBox2.GetComponent<Text>().text = "Data imported from 1";
                break;
            case 2:
                myTextBox.text = "Data imported from Labour Market"; //myTextBox2.GetComponent<Text>().text = "Data imported from 1";
                break;
            case 3:
                myTextBox.text = "Data imported from Factory"; //myTextBox2.GetComponent<Text>().text = "Data imported from 1";
                break;
            case 4:
                myTextBox.text = "Data imported from Bank"; //myTextBox2.GetComponent<Text>().text = "Data imported from 1";
                break;
            case 5:
                myTextBox.text = "Data imported from News"; //myTextBox2.GetComponent<Text>().text = "Data imported from 1";
                break;
        }
    }
}

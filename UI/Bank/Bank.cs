using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public TextMeshProUGUI detailList;
    
    public void BarclaysBtnListener()
    {
        detailList.text = "Barclays\n      line 2";
    }
    public void HsbcBtnListener()
    {
        detailList.text = "HSBC";
    }
}

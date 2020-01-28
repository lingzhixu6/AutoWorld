using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class News : MonoBehaviour
{
    public TextMeshProUGUI detailList;
    
    public void BbcBtnListener()
    {
        detailList.text = "BBC";
    }
    public void AbcBtnListener()
    {
        detailList.text = "ABC";
    }
    
}

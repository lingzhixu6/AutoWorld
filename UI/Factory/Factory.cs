using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public TextMeshProUGUI detailList;

    public void plBtnListener()        //pl: Production Line
    {
        detailList.text = "Daily Production and daily productivity(influenced by )" + "data from DB"
                            + "\n"
                            + "Remaining car quantity(i.e. those that haven't been sold)" + "data from DB"
                            + "\n"
                            + "How many cars are being produced and how soon the manufacture will be complete"
                            + "\n"
                            ;
    }

    void updateCarYieldToDB()
    {
    }
}

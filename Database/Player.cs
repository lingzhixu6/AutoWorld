using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player {
    public string company;
    public string email;
    public string balance;
    
    private int steelQtyOwned;
    private int rubberQtyOwned;
    private int glassQtyOwned;
    private int aluminumQtyOwned;

    public static Player singletonPlayer;

    public Player(string company, string email, string balance)
    {
        this.company = company;
        this.email = email;
        this.balance = balance;
    }
    public static Player GetInstance()
    {
        return singletonPlayer;      
    }
    
}

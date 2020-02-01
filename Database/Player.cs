using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player {
    public string company;
    public string email;
    public int balance;
    
    private int steelQtyOwned;
    private int rubberQtyOwned;
    private int glassQtyOwned;
    private int aluminumQtyOwned;

    public static Player player;

    public Player(string company, string email, int balance)
    {
        this.company = company;
        this.email = email;
        this.balance = balance;
    }
    public static Player GetInstance()
    {
        return player;      
    }
    
}

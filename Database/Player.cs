using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player {
    public string company;
    public string email;
    public int balance;
    
    private int steelQuantityOwned;
    private int rubberQuantityOwned;
    private int glassQuantityOwned;
    private int aluminumQuantityOwned;

    public Player(string company, string email, int balance)
    {
        this.company = company;
        this.email = email;
        this.balance = balance;
    }
    
}

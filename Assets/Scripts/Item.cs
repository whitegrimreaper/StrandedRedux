using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour{
    //for armor, clothes, and weapons
    public bool equippable;

    //Locations
    //0: weapon
    //1: helmet
    //2: chest
    //3: leg
    //rn only one thing each
    public int equipLocation;

    //for armor and clothes
    public int armor;

    //for weapons
    public int damage;

    //value in currency
    public int value;
    
}

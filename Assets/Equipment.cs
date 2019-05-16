using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {

    //public Item weapon;
    //public Item helmet;
    //public Item chest;
    //public Item pants;

    //Locations
    //0: weapon
    //1: helmet
    //2: chest
    //3: leg
    public Item[] equips;

	// Use this for initialization
	void Start () {
        equips = new Item[4];
        for(int i = 0; i < 4; i++)
        {
            equips[i] = null;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

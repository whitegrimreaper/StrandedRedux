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
    public GameObject[] equips;
    public bool[] equipped;

	// Use this for initialization
	void Start () {
        equips = new GameObject[4];
        equipped = new bool[4];
        for(int i = 0; i < 4; i++)
        {
            equips[i] = null;
            equipped[i] = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

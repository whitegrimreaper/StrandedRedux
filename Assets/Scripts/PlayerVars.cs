using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVars : MonoBehaviour {

    public Body body;
    public Item[] items;
    //public Equipment[] equips;

	// Use this for initialization
	void Start () {
        body = new Body();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void takeDamage(int part, double damage, int type)
    {
        body.damagePart(part, damage, type);
        //update UI to reflect
        if(body.checkDead())
        {
            //geemu ova
        }
    }
}

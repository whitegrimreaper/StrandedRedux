using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVars : MonoBehaviour {

    public Body body;
    public Item[] items;
    //public Equipment[] equips;

	// Use this for initialization
	void Start () {
        body = new Body("Human");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

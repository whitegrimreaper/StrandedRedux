using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVars : MonoBehaviour {

    public Body body;

	// Use this for initialization
	void Start () {
        body = new Body("Human");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

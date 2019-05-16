using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVars : MonoBehaviour {

    public Body body;
    
    public int strength = 10;
    public int dexterity = 10;
    public int perception = 10;
    public int intelligence = 10;
    public Equipment equips;

    public double health;

	// Use this for initialization
	void Start () {
        body = new Body();
        equips = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Equipment>();
    }
	
	// Update is called once per frame
	void Update () {
        health = body.getBlood();
	}

    public void takeHit(int damage)
    {
        takeDamage(0, damage, 0);
        if(body.checkDead())
        {
            Destroy(this.gameObject);
        }
    }

    public int hitBack()
    {
        return doDamage();
    }

    void takeDamage(int part, double damage, int type)
    {
        //body.damagePart(part, damage, type);
        //update UI to reflect
        body.loseBlood(damage);
        if(body.checkDead())
        {
            //geemu ova
        }
    }

    int doDamage()
    {
        float damage = 0.0f;
        if(equips.equipped[0])
        {
            Debug.Log("Attacking with weapon");
            damage = (strength + equips.equips[0].GetComponent<Item>().damage) * (Random.Range(.25f, 4));
        }
        //if (has weapon) {do shit}
        //else
        damage = strength * (Random.Range(.25f, 4));
        return Mathf.FloorToInt(damage);
    }

    public void attack(PlayerVars other)
    {
        int deal = doDamage();
        other.takeHit(deal);
        takeHit(other.hitBack());
    }
}

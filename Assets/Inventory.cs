using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public GameObject[] items;
    private int numItems;

	// Use this for initialization
	void Start () {
        items = new GameObject[50];
        numItems = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //return true if all good
    //idk if this will ever need to return false
    public bool addToInventory(GameObject item)
    {
        Item itemScript = item.GetComponent<Item>();
        //GameObject copy = GameObject.Instantiate(item);
        if (itemScript.equippable)
        {
            Debug.Log("Found equippable item");
            Equipment equips = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Equipment>();
            //if we don't already have something in that slot
            //Debug.Log("got component");
            if (equips.equips[itemScript.equipLocation] == null)
            {
                //put er in there
                equips.equips[itemScript.equipLocation] = item;
                equips.equipped[itemScript.equipLocation] = true;
                //item.SetActive(false);
                item.GetComponent<SpriteRenderer>().enabled = false;
            }
            
        }
        else
        {
            items[numItems++] = item;
            item.SetActive(false);
        }
        //ameObject.Destroy(item);
        return true;
    }

    public bool removeFromInventory()
    {
        // move item to player's location (or specified location)
        // add to other inventory if necessary
        // reactivate GO
        // remove reference from inventory/equips
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

    public class Openable
    {
        public bool isOpenable;
        public bool isOpen;

        public Openable(bool able, bool open)
        {
            isOpenable = able;
            isOpen = open;
        }

        public void openOrClose()
        {
            if(!isOpenable)
            {
                Debug.Log("Attempted to open non-openable");
            }
            else if(isOpen){
                isOpen = false;
            }
            else
            {
                isOpen = true;
            }
        }
    }

    public bool passability;
    public bool isOpenable;
    public bool startsOpen;

    public Sprite closedSprite;
    public Sprite openSprite;

    private Openable openable;

	// Use this for initialization
	void Start () {
        if(isOpenable)
        {
            openable = new Openable(true, false);
        }
        else
        {
            openable = new Openable(false, false);
        }

        if(openable.isOpen && openSprite != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = openSprite;
        }
        else if(closedSprite != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = closedSprite;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(isOpenable && openable.isOpen) {
            passability = true;
        }
        else
        {
            passability = false;
        }
	}

    public bool isPassable()
    {
        return passability;
    }

    public void open()
    {
        openable.openOrClose();
        if(openable.isOpen)
        {
            this.GetComponent<SpriteRenderer>().sprite = openSprite;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = closedSprite;
        }
    }
}

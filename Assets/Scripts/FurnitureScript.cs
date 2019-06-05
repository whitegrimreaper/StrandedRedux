using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureScript : MonoBehaviour
{

    public bool passability;
    public bool isOpenable;
    public bool startsOpen;

    public Sprite closedSprite;
    public Sprite openSprite;

    private TileScript.Openable openable;

    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;

    // Use this for initialization
    void Start()
    {
        if (isOpenable)
        {
            openable = new TileScript.Openable(true, false);
        }
        else
        {
            openable = new TileScript.Openable(false, false);
        }

        if (openable.isOpen && openSprite != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = openSprite;
        }
        else if (closedSprite != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = closedSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpenable && openable.isOpen)
        {
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
        if (openable.isOpen)
        {
            this.GetComponent<SpriteRenderer>().sprite = openSprite;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = closedSprite;
        }
    }
}

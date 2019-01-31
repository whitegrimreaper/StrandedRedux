using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    class Dir
    {
        public int vert;
        public int horz;

        public Dir(string dir)
        {
            switch (dir)
            {
                case "up":
                    vert = 1;
                    horz = 0;
                    break;
                case "down":
                    vert = -1;
                    horz = 0;
                    break;
                case "left":
                    vert = 0;
                    horz = -1;
                    break;
                case "right":
                    vert = 0;
                    horz = 1;
                    break;
                default:
                    Debug.Log("you fucked up a dirs initialize, my man");
                    break;

            }
        }
    }

    Dir dirs;
    bool waitingOnInput;

    // Use this for initialization
    void Start () {
        waitingOnInput = false;
		
	}
	
	// Update is called once per frame
	void Update () {
        if(waitingOnInput)
        {
            playerDirectionInput();
        }
        else
        {
            playerMovementInput();
        }
        playerWorldInteraction();
		
    }

    bool checkMove(Dir dir)
    {
        //checkItems();
        return (checkTile(dir) && checkFurniture(dir));
    }

    bool checkDoor(GameObject tile)
    {
        //Vector2 loc = new Vector2(this.transform.position.x + dir.horz, this.transform.position.y + dir.vert);

        //GameObject[] objs;
        //objs = GameObject.FindGameObjectsWithTag("Tile");
        //GameObject tile = findTile(objs, loc);

        if (tile == null)
        {
            //Debug.Log("cri cD");
            return true;
        }
        else if (checkOpenable(tile))
        {
            return true;
        }
        return false;
    }

    //empty
    bool checkForItems(Vector2 loc)
    {
        return false;
    }

    bool checkFurniture(Dir dir)
    {
        Vector2 loc = new Vector2(this.transform.position.x + dir.horz, this.transform.position.y + dir.vert);

        GameObject[] objs;
        objs = GameObject.FindGameObjectsWithTag("Furn");
        GameObject furn = findInScene(objs, loc);

        //Debug.Log("Tile at loc: " + loc.x + ", " + loc.y);

        if (furn == null)
        {
            Debug.Log("CheckFurn Returned null");
            return true;
        }
        else if (checkPassable(furn, "furn"))
        {
            Debug.Log("Furniture found at loc: " + loc.x + ", " + loc.y + ", is passable");
            return true;
        }
        else
        {
            Debug.Log("Furniture found at loc: " + loc.x + ", " + loc.y + ", is NOT passable");
            return false;
        }
    }

    bool checkTile(Dir dir)
    {
        Vector2 loc = new Vector2(this.transform.position.x + dir.horz, this.transform.position.y + dir.vert);

        GameObject[] objs;
        objs = GameObject.FindGameObjectsWithTag("Tile");
        GameObject tile = findInScene(objs, loc);

        //Debug.Log("Tile at loc: " + loc.x + ", " + loc.y);

        if(tile == null)
        {
            Debug.Log("CheckTile Returned null");
            return true;
        }
        else if (checkPassable(tile, "tile"))
        {
            Debug.Log("Tile found at loc: " + loc.x + ", " + loc.y + ", is passable");
            return true;
        }
        else if(checkDoor(tile))
        {
            Debug.Log("Door found at loc: " + loc.x + ", " + loc.y + ", opening");
            tile.GetComponent<TileScript>().open();
            return false;
        }
        return false;
    }


    bool checkPassable(GameObject obj, string tag)
    {
        switch (tag) {
            case "tile":
                TileScript tile = obj.GetComponent<TileScript>();
                if (tile == null)
                {
                    Debug.Log("lol");
                    return false;
                }
                else
                {
                    return tile.isPassable();
                }
            case "furn":
                FurnitureScript furn = obj.GetComponent<FurnitureScript>();
                if (furn == null)
                {
                    Debug.Log("lol");
                    return false;
                }
                else
                {
                    return furn.isPassable();
                }
            default:
                return true;
        }
    }

    bool checkOpenable(GameObject obj)
    {
        TileScript scr = obj.GetComponent<TileScript>();
        if (scr == null)
        {
            Debug.Log("checkOpenable run with null script on tile");
            return false;
        }
        else
        {
            return scr.isOpenable;
        }
    }

    GameObject findInScene(GameObject[] objs, Vector2 loc)
    {
        for(int i = 0; i < objs.Length; i++)
        {
            if(objs[i].transform.position.x == loc.x && objs[i].transform.position.y == loc.y)
            {
                Debug.Log("Object found at location: " + loc.x + ", " + loc.y);
                return objs[i];
            }
        }
        return null;
    }

    void move(Dir dir)
    {
        this.transform.Translate(new Vector2(dir.horz, dir.vert));
    }

    void playerMovementInput()
    {
        bool moved = false;
        if (Input.GetKeyDown(KeyCode.W))
        {
            Dir dir = new Dir("up");
            if (checkMove(dir))
            {
                move(dir);
                moved = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Dir dir = new Dir("left");
            if (checkMove(dir))
            {
                move(dir);
                moved = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Dir dir = new Dir("down");
            if (checkMove(dir))
            {
                move(dir);
                moved = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Dir dir = new Dir("right");
            if (checkMove(dir))
            {
                move(dir);
                moved = true;
            }
        }

        if (moved)
        {
            Vector2 loc = new Vector2(this.transform.position.x, this.transform.position.y);
            if (checkForItems(loc))
            {
                //log items
            }
            
        }
    }

    //empty
    void playerCommandInput()
    {

    }

    //empty
    void playerDirectionInput()
    {

    }

    void playerWorldInteraction()
    {
        Vector2 pos = new Vector2(this.transform.position.x, this.transform.position.y);
        if (Input.GetKeyDown(KeyCode.G))
        {

            if(checkForItems(pos))
            {
                //if inv has space
                //addToInventory()
            }
        }
    }
}



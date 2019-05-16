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

    Inventory inventory;
    PlayerVars vars;

    // Use this for initialization
    void Start () {
        waitingOnInput = false;

        inventory = this.gameObject.GetComponentInChildren<Inventory>();
        vars = this.GetComponent<PlayerVars>();
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

    int checkMove(Dir dir)
    {
        //checkItems();
        //0 == good
        //1 == tile bad
        //2 == furniture bad
        //3 == NPC bad
        bool tile = checkTile(dir);
        bool furn = checkFurniture(dir);
        bool npc = checkNPC(dir);
        if(tile)
        {
            if(furn)
            {
                if(!npc)
                {
                    return 0;
                }
                return 3;
            }
            return 2;
        }
        return 1;
        //return (checkTile(dir) && checkFurniture(dir) && (!checkNPC(dir)));
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

    //uses loc because it checks the player's location, could probably change
    bool checkForItems(Vector2 loc)
    {
        GameObject found = findInScene(GameObject.FindGameObjectsWithTag("Item"), loc);
        if (found == null)
        {
            Debug.Log("CheckForItems Returned null");
            return false;
        }
        else 
        {
            Debug.Log("Item found!");
            return true;
        }
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

    //returns true if there is an NPC in Dir from the player
    bool checkNPC(Dir dir)
    {
        Vector2 loc = new Vector2(this.transform.position.x + dir.horz, this.transform.position.y + dir.vert);

        GameObject[] objs;
        objs = GameObject.FindGameObjectsWithTag("NPC");
        GameObject npc = findInScene(objs, loc);

        //Debug.Log("NPC at loc: " + loc.x + ", " + loc.y);

        if (npc == null)
        {
            Debug.Log("CheckNPC Returned null");
            return false;
        }
        else if (false /* gonna want to check and see if the NPC is hostile eventually*/)
        {
            Debug.Log("NPC at loc: " + loc.x + ", " + loc.y);
            //return true;
        }
        else
        {
            Debug.Log("NPC at loc: " + loc.x + ", " + loc.y);
            return true; ;
        }
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

    //checks for an object at location loc in the given list objs
    GameObject findInScene(GameObject[] objs, Vector2 loc)
    {
        for(int i = 0; i < objs.Length; i++)
        {
            if(objs[i].transform.position.x == loc.x && objs[i].transform.position.y == loc.y)
            {
                //Debug.Log("Object found at location: " + loc.x + ", " + loc.y);
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
        Dir dir = null;
        if (Input.GetKeyDown(KeyCode.W))
        {
            dir = new Dir("up");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            dir = new Dir("left");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            dir = new Dir("down");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            dir = new Dir("right");
        }
        if (dir != null)
        {
            int moveResult = checkMove(dir);
            //0 means we can move
            if(moveResult == 0)
            {
                move(dir);
                moved = true;
            }
            //1 means bad tile
            else if (moveResult == 1)
            {
                //do nothing now, might log later
            }
            //2 means furniture in way
            else if (moveResult == 2)
            {
                //should either log or maybe prompt for open
            }
            else if (moveResult == 3)
            {
                Vector2 loc = new Vector2(this.transform.position.x + dir.horz, this.transform.position.y + dir.vert);

                GameObject[] objs;
                objs = GameObject.FindGameObjectsWithTag("NPC");
                GameObject npc = findInScene(objs, loc);
                NPCScript npcScript = npc.GetComponent<NPCScript>();
                PlayerVars npcVars = npc.GetComponent<PlayerVars>();

                if(npcScript.hostile)
                {
                    Debug.Log("REEEEEEEEEEE");
                    vars.attack(npcVars);
                }
                
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
                GameObject found = findInScene(GameObject.FindGameObjectsWithTag("Item"), pos);
                //GameObject copy = GameObject.Instantiate(found);
                bool destroy = inventory.addToInventory(found);
                if(destroy)
                {
                    // not currently destroying the object because unity
                    //GameObject.Destroy(found);
                }
                //GameObject.Destroy(copy);
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            //drop item
        }
    }
}



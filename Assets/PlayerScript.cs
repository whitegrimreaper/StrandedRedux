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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        playerMovementInput();
		
    }

    bool checkMove(Dir dir)
    {
        //checkFurniture();
        //checkItems();
        return checkTile(dir);
    }

    bool checkTile(Dir dir)
    {
        Vector2 loc = new Vector2(this.transform.position.x + dir.horz, this.transform.position.y + dir.vert);

        GameObject[] objs;
        objs = GameObject.FindGameObjectsWithTag("Tile");
        GameObject tile = findTile(objs, loc);

        Debug.Log("Tile at loc: " + loc.x + ", " + loc.y);

        if(tile == null || checkPassable(tile))
        {
            if (tile == null)
            {
                Debug.Log("cri");
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    

    bool checkPassable(GameObject obj)
    {
        TileScript scr = obj.GetComponent<TileScript>();
        if(scr == null)
        {
            Debug.Log("lol");
            return false;
        }
        else
        {
            return scr.passability;
        }
    }

    GameObject findTile(GameObject[] objs, Vector2 loc)
    {
        for(int i = 0; i < objs.Length; i++)
        {
            if(objs[i].transform.position.x == loc.x && objs[i].transform.position.y == loc.y)
            {
                Debug.Log("Ladies and gentlemen,  we gotteeem");
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
        if (Input.GetKeyDown(KeyCode.W))
        {
            Dir dir = new Dir("up");
            if (checkMove(dir))
            {
                move(dir);
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Dir dir = new Dir("left");
            if (checkMove(dir))
            {
                move(dir);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Dir dir = new Dir("down");
            if (checkMove(dir))
            {
                move(dir);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Dir dir = new Dir("right");
            if (checkMove(dir))
            {
                move(dir);
            }
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        roundTilePositions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void roundTilePositions()
    {
        GameObject[] objs;
        objs = GameObject.FindGameObjectsWithTag("Tile");
        foreach(GameObject tile in objs)
        {
            tile.transform.position = new Vector3(Mathf.Round(tile.transform.position.x), Mathf.Round(tile.transform.position.y));
        }
    }
}

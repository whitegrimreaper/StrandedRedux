using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    public int boardRows, boardColumns;
    public int minRoomSize, maxRoomSize;

    public GameObject floor;
    public GameObject corridorT;

    private GameObject[,] floorPositions;

    public class SubShip
    {
        public SubShip left, right;
        public Rect rect;
        public Rect room = new Rect(-1, -1, 0, 0); // i.e null
        public int debugId;

        public List<Rect> corridors = new List<Rect>();


        private static int debugCounter = 0;

        public SubShip(Rect mrect)
        {
            rect = mrect;
            debugId = debugCounter;
            debugCounter++;
        }

        public bool IsLeaf()
        {
            return left == null && right == null;
        }

        public bool Split(int minRoomSize, int maxRoomSize)
        {
            if (!IsLeaf())
            {
                return false;
            }

            // choose a vertical or horizontal split depending on the proportions
            // i.e. if too wide split vertically, or too long horizontally,
            // or if nearly square choose vertical or horizontal at random
            bool splitH;
            if (rect.width / rect.height >= 1.25)
            {
                splitH = false;
            }
            else if (rect.height / rect.width >= 1.25)
            {
                splitH = true;
            }
            else
            {
                splitH = Random.Range(0.0f, 1.0f) > 0.5;
            }

            if (Mathf.Min(rect.height, rect.width) / 2 < minRoomSize)
            {
                Debug.Log("Sub-ship " + debugId + " will be a leaf");
                return false;
            }

            if (splitH)
            {
                // split so that the resulting sub-ships widths are not too small
                // (since we are splitting horizontally)
                int split = Random.Range(minRoomSize, (int)(rect.width - minRoomSize));

                left = new SubShip(new Rect(rect.x, rect.y, rect.width, split));
                right = new SubShip(
                  new Rect(rect.x, rect.y + split, rect.width, rect.height - split));
            }
            else
            {
                int split = Random.Range(minRoomSize, (int)(rect.height - minRoomSize));

                left = new SubShip(new Rect(rect.x, rect.y, split, rect.height));
                right = new SubShip(
                  new Rect(rect.x + split, rect.y, rect.width - split, rect.height));
            }

            return true;
        }

        public void CreateRoom()
        {
            if (left != null)
            {
                left.CreateRoom();
            }
            if (right != null)
            {
                right.CreateRoom();
            }
            if (IsLeaf())
            {
                int roomWidth = (int)Random.Range(rect.width / 2, rect.width - 2);
                int roomHeight = (int)Random.Range(rect.height / 2, rect.height - 2);
                int roomX = (int)Random.Range(1, rect.width - roomWidth - 1);
                int roomY = (int)Random.Range(1, rect.height - roomHeight - 1);

                // room position will be absolute in the board, not relative to the sub-ship
                room = new Rect(rect.x + roomX, rect.y + roomY, roomWidth, roomHeight);
                Debug.Log("Created room " + room + " in sub-ship " + debugId + " " + rect);
            }
        }

        public Rect GetRoom()
        {
            if (IsLeaf())
            {
                return room;
            }
            if (left != null)
            {
                Rect lroom = left.GetRoom();
                if (lroom.x != -1)
                {
                    return lroom;
                }
            }
            if (right != null)
            {
                Rect rroom = right.GetRoom();
                if (rroom.x != -1)
                {
                    return rroom;
                }
            }

            // workaround non nullable structs
            return new Rect(-1, -1, 0, 0);
        }

        public void CreateCorridorBetween(SubShip left, SubShip right)
        {
            Rect lroom = left.GetRoom();
            Rect rroom = right.GetRoom();

            Debug.Log("Creating corridor(s) between " + left.debugId + "(" + lroom + ") and " + right.debugId + " (" + rroom + ")");

            // attach the corridor to a random point in each room
            Vector2 lpoint = new Vector2((int)Random.Range(lroom.x + 1, lroom.xMax - 1), (int)Random.Range(lroom.y + 1, lroom.yMax - 1));
            Vector2 rpoint = new Vector2((int)Random.Range(rroom.x + 1, rroom.xMax - 1), (int)Random.Range(rroom.y + 1, rroom.yMax - 1));

            // always be sure that left point is on the left to simplify the code
            if (lpoint.x > rpoint.x)
            {
                Vector2 temp = lpoint;
                lpoint = rpoint;
                rpoint = temp;
            }

            int w = (int)(lpoint.x - rpoint.x);
            int h = (int)(lpoint.y - rpoint.y);

            Debug.Log("lpoint: " + lpoint + ", rpoint: " + rpoint + ", w: " + w + ", h: " + h);

            // if the points are not aligned horizontally
            if (w != 0)
            {
                // choose at random to go horizontal then vertical or the opposite
                if (Random.Range(0, 1) > 2)
                {
                    // add a corridor to the right
                    corridors.Add(new Rect(lpoint.x, lpoint.y, Mathf.Abs(w) + 1, 1));

                    // if left point is below right point go up
                    // otherwise go down
                    if (h < 0)
                    {
                        corridors.Add(new Rect(rpoint.x, lpoint.y, 1, Mathf.Abs(h)));
                    }
                    else
                    {
                        corridors.Add(new Rect(rpoint.x, lpoint.y, 1, -Mathf.Abs(h)));
                    }
                }
                else
                {
                    // go up or down
                    if (h < 0)
                    {
                        corridors.Add(new Rect(lpoint.x, lpoint.y, 1, Mathf.Abs(h)));
                    }
                    else
                    {
                        corridors.Add(new Rect(lpoint.x, rpoint.y, 1, Mathf.Abs(h)));
                    }

                    // then go right
                    corridors.Add(new Rect(lpoint.x, rpoint.y, Mathf.Abs(w) + 1, 1));
                }
            }
            else
            {
                // if the points are aligned horizontally
                // go up or down depending on the positions
                if (h < 0)
                {
                    corridors.Add(new Rect((int)lpoint.x, (int)lpoint.y, 1, Mathf.Abs(h)));
                }
                else
                {
                    corridors.Add(new Rect((int)rpoint.x, (int)rpoint.y, 1, Mathf.Abs(h)));
                }
            }

            Debug.Log("Corridors: ");
            foreach (Rect corridor in corridors)
            {
                Debug.Log("corridor: " + corridor);
            }
        }
    }

    public void CreateBSP(SubShip subShip)
    {
        Debug.Log("Splitting sub-ship " + subShip.debugId + ": " + subShip.rect);
        if (subShip.IsLeaf())
        {
            // if the sub-ship is too large
            if (subShip.rect.width > maxRoomSize
              || subShip.rect.height > maxRoomSize
              || Random.Range(0.0f, 1.0f) > 0.25)
            {

                if (subShip.Split(minRoomSize, maxRoomSize))
                {
                    Debug.Log("Splitted sub-ship " + subShip.debugId + " in "
                      + subShip.left.debugId + ": " + subShip.left.rect + ", "
                      + subShip.right.debugId + ": " + subShip.right.rect);

                    CreateBSP(subShip.left);
                    CreateBSP(subShip.right);
                    subShip.CreateCorridorBetween(subShip.left, subShip.right); // maybe
                }
            }
        }
    }

    public void DrawRooms(SubShip subShip)
    {
        if (subShip == null)
        {
            return;
        }
        if (subShip.IsLeaf())
        {
            for (int i = (int)subShip.room.x; i < subShip.room.xMax; i++)
            {
                for (int j = (int)subShip.room.y; j < subShip.room.yMax; j++)
                {
                    GameObject instance = Instantiate(floor, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(transform);
                    floorPositions[i, j] = instance;
                }
            }
        }
        else
        {
            DrawRooms(subShip.left);
            DrawRooms(subShip.right);
        }
    }

    void DrawCorridors(SubShip subShip)
    {
        if (subShip == null)
        {
            return;
        }

        DrawCorridors(subShip.left);
        DrawCorridors(subShip.right);

        foreach (Rect corridor in subShip.corridors)
        {
            for (int i = (int)corridor.x; i < corridor.xMax; i++)
            {
                for (int j = (int)corridor.y; j < corridor.yMax; j++)
                {
                    if (floorPositions[i, j] == null)
                    {
                        GameObject instance = Instantiate(corridorT, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                        instance.transform.SetParent(transform);
                        floorPositions[i, j] = instance;
                    }
                }
            }
        }
    }

    void Start()
    {
        SubShip rootsubShip = new SubShip(new Rect(0, 0, boardRows, boardColumns));
        CreateBSP(rootsubShip);
        rootsubShip.CreateRoom();

        floorPositions = new GameObject[boardRows, boardColumns];
        DrawRooms(rootsubShip);
    }

    // Update is called once per frame
    void Update () {
		
	}
}

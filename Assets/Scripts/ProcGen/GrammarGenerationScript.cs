using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrammarGenerationScript : MonoBehaviour
{
    public int boardRows, boardColumns;
    public int minRoomSize, maxRoomSize;
    public GameObject floorTile;
    public GameObject corridorTile;
    public GameObject wallTile;
    public GameObject player;

    public GameObject teleporter;
    public GameObject enemy;
    public GameObject key;

    private class BaseGrammar
    {
        Rule[] rules;

        public BaseGrammar()
        {
            rules = new Rule[5];
            rules[3] = new Rule("Barracks", null, true, null);
            rules[2] = new Rule("Medbay", null, true, null);
            rules[1] = new Rule("Cockpit", null, true, null);
            rules[0] = new Rule("initial", new Rule[3] { rules[1], rules[2], rules[3] }, false, null);
        }
    }

    private class GSubDungeon
    {
        public GSubDungeon sub1, sub2, sub3, sub4;
        public Rect rect;
        public Rect room = new Rect(-1, -1, 0, 0);

        public List<Rect> corridors = new List<Rect>();
        static System.Random rnd = new System.Random();
        public bool roomChosen = false;

        public GSubDungeon(Rect mrect)
        {
            rect = mrect;
            //debug maybe
        }

        public bool IAmLeaf()
        {
            return sub1 == null
                && sub2 == null
                && sub3 == null
                && sub4 == null;
        }
    }

    private class GrammarDungeon
    {
        public int floorNumber;
        public int floorType;
        public Vector3 floorOffset;

        //public List<List<GSubDungeon>> allFloorRooms;
        //public List<GSubDungeon> allFloorRoots;

        public GSubDungeon root;

        public GrammarDungeon()
        {
            floorNumber = 0;
            floorOffset = new Vector3(floorNumber*1000, 0.0f);
        }

        public GrammarDungeon(int number, int type)
        {
            floorNumber = number;
            floorOffset = new Vector3(floorNumber*1000, 0.0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        BaseGrammar gram = new BaseGrammar();
        GrammarDungeon dung = new GrammarDungeon();
        //dung.allFloorRooms = new List<List<GSubDungeon>>();
        GSubDungeon root = new GSubDungeon(new Rect(-1, -1, 0, 0));
        dung.root = root;
        GenerateGrammar(gram, root);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateGrammar(BaseGrammar baseG, GSubDungeon root)
    {
        Debug.Log("Splitting sub-dungeon");
    }

    public void GenerateRooms()
    {

    }

    public void GenerateCorridors()
    {

    }
}

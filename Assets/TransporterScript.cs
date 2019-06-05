using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransporterScript : MonoBehaviour
{
    public Vector2 destination;
    public bool accessible;
    public bool newScene;
    public string newSceneName;
    //public 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadNewScene()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if(SceneManager.GetActiveScene().name == "BSPScene")
        {
            SceneManager.LoadScene("BSPScene");
        }
        else
        {
            SceneManager.LoadScene("BSPScene");
        }
    }
}

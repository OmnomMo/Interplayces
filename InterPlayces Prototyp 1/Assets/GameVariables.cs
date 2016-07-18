using UnityEngine;
using System.Collections;

public class GameVariables : MonoBehaviour {

    public float highscore;

    public GameObject videoBackground;


    private static GameVariables instance;
    public static GameVariables Instance
    {
        get { return instance; }
    }



    void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateInterface : MonoBehaviour {

    public GameObject displayHeight;
    public GameObject displayMaxHeight;
    public GameObject GroundLevel;
    public GameObject displayGlobalHighscore;
   // public GameObject displayGameState;

        public GameObject JumpButton;
        public GameObject ResetButton;

    public float height;
    public float maxHeight;
   // private float globalHighscore = 0;


    public static UpdateInterface Instance { get { return instance; } }
    private static UpdateInterface instance;


    public void EnableReset()
    {
        JumpButton.GetComponent<Button>().enabled = false;
        ResetButton.GetComponent<Button>().enabled = true;
    }

    public void EnableJump()
    {
        JumpButton.GetComponent<Button>().enabled = true;
        ResetButton.GetComponent<Button>().enabled = false;
    }


    void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
	
	}

    public void ResetHighScoreDisplay()
    {
        //maxHeight = 0;
    }
	

    public void WriteGlobalHighscore()
    {
        displayGlobalHighscore.GetComponent<Text>().text = GameVariables.Instance.highscore.ToString("0.0") + " m";
    }

    public void UpdateGlobalHighscore (float score)
    {
        if (score > GameVariables.Instance.highscore)
        {
            GameVariables.Instance.highscore = score;

            displayGlobalHighscore.GetComponent<Text>().text = GameVariables.Instance.highscore.ToString("0.0") + " m";
        } else
        {
            displayGlobalHighscore.GetComponent<Text>().text = GameVariables.Instance.highscore.ToString("0.0") + " m";
        }
    }

	// Update is called once per frame
	void Update () {

        UpdateGlobalHighscore(maxHeight);

        height = Parts.Instance.gameObject.transform.position.y - GroundLevel.transform.position.y; 
        
        if (height > maxHeight)
        {
            maxHeight = height;
        } 

        displayHeight.GetComponent<Text>().text = height.ToString("0.0") + " m";
        displayMaxHeight.GetComponent<Text>().text = maxHeight.ToString("0.0") + " m";

        //if (GameState.Instance.IsInPlay())
        //{
        //    displayGameState.GetComponent<Text>().text = "Play Mode";
        //} else {
        //    displayGameState.GetComponent<Text>().text = "Setup Mode";
        //}


    }
}

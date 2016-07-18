using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour {



    
    private static GameState instance;
    public static GameState Instance
    {
        get { return instance; }
    }


    public GameObject interfaceCanvas;

    SetupState setup;
    public GameObject setupInterface;
    PlayState play;
    public GameObject playInterface;
    ParentState currentState;

    private List<GameObject> UI_Containers;

    void Awake()
    {
        instance = this;
    }




    //TODOOOO (Optional) Change class architecture. Create functions for reset and setup of robot in Parts-Class and just call these functions here




	// Use this for initialization
	void Start () {


        setup = new SetupState(setupInterface);
        play = new PlayState(playInterface);


        UI_Containers = new List<GameObject>();
        //Add Containers to list
        UI_Containers.Add(setupInterface);
        UI_Containers.Add(playInterface);

        UpdateInterface.Instance.WriteGlobalHighscore();

        StartCoroutine(GetBackgroundCam());




        currentState = setup;
        SwitchToSetup();

       // GameObject.Find("ARToolKit").GetComponent<ARController>().StartAR();
       

	}

    IEnumerator GetBackgroundCam()
    {
        yield return 0;

      

        if (GameVariables.Instance.videoBackground == null)
        {
            GameVariables.Instance.videoBackground = GameObject.Find("Video background");
        }
        Debug.Log("Found Video Background: " + GameVariables.Instance.videoBackground);
    }

    public void SwitchToPlay()
    {

        SwitchInterfaceTo(play.GetInterfaceObejct());

     

        Parts.Instance.TranslateRobot();
        Parts.Instance.ResetConnections();
        
        Parts.Instance.transform.rotation = Quaternion.identity;

        Parts.Instance.GetComponent<Rigidbody>().mass = Parts.Instance.GetRoboWeight();

        interfaceCanvas.GetComponent<UpdateInterface>().ResetHighScoreDisplay();
        UpdateInterface.Instance.EnableJump();


        StartCoroutine(delayedReaction());


        currentState = play;
    }


    public void SwitchToPlayScene()
    {
        

        //Reset Transformations
        Parts.Instance.transform.rotation = Quaternion.identity;
        Parts.Instance.GetComponent<Rigidbody>().mass = Parts.Instance.GetRoboWeight();
        Parts.Instance.transform.position = Vector3.zero;

        Parts.Instance.ResetConnections();
        Parts.Instance.Swap2D3D();

        

        //Disable tracking functionality
        PlayPiece[] pieces = Parts.Instance.GetComponentsInChildren<PlayPiece>(true);

        foreach (PlayPiece piece in pieces)
        {
            piece.enabled = false;
        }


        //Preserve MrRoboto (at all costs)!
        Parts.Instance.transform.parent = null;
        DontDestroyOnLoad(Parts.Instance);


        currentState = play;


        // GameObject.Find("ARToolKit").GetComponent<ARController>().StopAR();

        StartCoroutine(LoadNextLevel());
       

     

    }

    IEnumerator LoadNextLevel()
    {
        yield return 0;
        Application.LoadLevel("Spielfeld_MrRoboto_Play");
    }

    public void ResetJump()
    {
        //Reset scene
        Parts.Instance.FreezeParts();
        Parts.Instance.transform.position = new Vector3(0, 3, 0);
        
        Parts.Instance.transform.rotation = Quaternion.identity;
        Parts.Instance.ResetAllParts();
        UpdateInterface.Instance.UpdateGlobalHighscore(UpdateInterface.Instance.maxHeight);
        interfaceCanvas.GetComponent<UpdateInterface>().ResetHighScoreDisplay();

        Parts.Instance.TranslateRobot();

        StartCoroutine(delayedReaction());
    }

    public void SwitchToSetup()
    {

        //TODO: Track global Highscore

        if (currentState.Equals(play)) {
            

            //Reset scene
            Parts.Instance.FreezeParts();
            Parts.Instance.transform.position = new Vector3(0, 3, 0);
            Parts.Instance.transform.rotation = Quaternion.identity;
            Parts.Instance.ResetAllParts();

            UpdateInterface.Instance.UpdateGlobalHighscore(UpdateInterface.Instance.maxHeight);

            //Reset Positions
            Parts.Instance.ResetConnections();

        }

        SwitchInterfaceTo(setup.GetInterfaceObejct());
        currentState = setup;
    }
	

    public bool IsInPlay()
    {
        return currentState.Equals(play);
    }


    public bool IsInSetup()
    {
        return currentState.Equals(setup);
    }

    public void SwitchInterfaceTo (GameObject newInterface)
    {


        //then deactivate all but the next Interface
        foreach (GameObject UI_c in UI_Containers) 
        {
            //Debug.Log("___________________________________________________");
            if (UI_c == newInterface)
            {
                UI_c.SetActive(true);
            } else
            {
                UI_c.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update () {
        
	}

    public IEnumerator delayedReaction()
    {
        yield return 0;
        Parts.Instance.UnFreezeParts();
        //Time.timeScale = 0;


    }

}

class ParentState
{
    protected GameObject interfaceObject;
    public GameObject GetInterfaceObejct()
    {
        return interfaceObject;
    }
    
}

class SetupState : ParentState
{
    public SetupState(GameObject interfaceObject_) {
        interfaceObject = interfaceObject_;
    }
}

class PlayState : ParentState
{
    public PlayState(GameObject interfaceObject_)
    {
        interfaceObject = interfaceObject_;
    }
}

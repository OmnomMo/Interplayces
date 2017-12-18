using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class DataCollection : MonoBehaviour {



    private static DataCollection instance;

    public static DataCollection Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    public PlaySession currentSession;
    public SceneSession currentScene;

    public bool sessionActive;

    

    public string dataPath;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnStartScene;
        SceneManager.sceneUnloaded += OnEndScene;
    }

    void OnDisable()
    {
        Debug.Log("Disable DataCollection - write to file");
        currentSession.additionalInformation = "Game shutdown";
        EndSession();
        SceneManager.sceneLoaded -= OnStartScene;
        SceneManager.sceneUnloaded -= OnEndScene;
    }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);

        LoadedObjectManager.Instance.AddPersistenObject(gameObject);

        dataPath = Application.dataPath + "/DataCollection";

        if (!Directory.Exists(dataPath))
        {

            Directory.CreateDirectory(dataPath);
        }

        StartSession();
     
	}

    public void StartSession()
    {
        currentSession = new PlaySession(System.DateTime.Now);
        sessionActive = true;
    }

    public void EndSession()
    {
        currentSession.SetEndTime(System.DateTime.Now);
        currentSession.duration = currentSession.endTimeObj.Subtract(currentSession.startTimeObj).ToString();
        sessionActive = false;
        WriteToFile();
        //Serialize Session to file
    }

    public void EndSession(System.DateTime endTime)
    {
        currentSession.SetEndTime(endTime);
        currentSession.duration = currentSession.endTimeObj.Subtract(currentSession.startTimeObj).ToString();
        sessionActive = false;
        WriteToFile();
    }


    

    public void WriteToFile()
    {
        string json = JsonUtility.ToJson(currentSession);
        Debug.Log(json);

        System.IO.File.WriteAllText(dataPath + "/SessionData_" + System.DateTime.Now.ToString("yyyy_MM_dd__HH_mm_ss.txt"), json);
        //StreamWriter writer = new StreamWriter(sourceTextPath, true);
        //writer.Write(json);
        //writer.Close();
        //AssetDatabase.ImportAsset(sourceTextPath);



    }


    void OnEndScene(Scene scene)
    {

        //Debug.Log("EndScene " + scene.name);

        currentScene.SetEndTime(System.DateTime.Now);
        currentScene.duration = currentScene.endTimeObj.Subtract(currentScene.startTimeObj).ToString();

        
    }


    public void GetSceneProgress()
    {
        if (Score.Instance != null)
        {
            //Debug.Log("Document Score.");
            currentScene.planetsFound = Score.Instance.currentScore;
            currentScene.aliensCaught = Score.Instance.alienShipsCaught;
        }

        if (LevelProgress.Instance != null)
        {

            //Debug.Log("Document Objective.");

            Objective currentObj = LevelProgress.Instance.GetCurrentObjective();

            if (currentObj != null)
            {
                if (currentObj.completed)
                {
                    currentScene.objectiveCompleted = true;
                } else
                {
                    currentScene.objectiveCompleted = false;
                }
                currentScene.currentObjective = currentObj.startTtText;
            }
        }

        if (EndSceneDisplay.Instance != null)
        {
            currentScene.additionalInformation = "";
            currentScene.additionalInformation += EndSceneDisplay.Instance.reasonText.text;
            currentScene.additionalInformation += " - ";
            currentScene.additionalInformation += EndSceneDisplay.Instance.foundObjectsText.text;
        }
    }

    void OnStartScene(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("Start newScene " + scene.name);

    

        currentScene = new SceneSession(System.DateTime.Now);

        currentScene.sceneName = scene.name;
        currentSession.AddSceneSession(currentScene);
    }
	
	// Update is called once per frame
	void Update () {
        GetSceneProgress();
	}
}



[System.Serializable]
public class PlaySession
{
    public System.DateTime startTimeObj;
    public string startTime;
    public System.DateTime endTimeObj;
    public string endTime;

    public string duration;

    public string additionalInformation;

    public List<SceneSession> allSceneSessions;


    public PlaySession (System.DateTime _startTime)
    {
        startTimeObj = _startTime;
        startTime = _startTime.ToString("yyyy_MM_dd__HH_mm_ss");
        allSceneSessions = new List<SceneSession>();
    }

    public void SetEndTime(System.DateTime _endTime)
    {
        endTimeObj = _endTime;
        endTime = _endTime.ToString("yyyy_MM_dd__HH_mm_ss");
    }

    public void AddSceneSession(SceneSession _sceneSession)
    {
        allSceneSessions.Add(_sceneSession);
    }


}



[System.Serializable]
public class SceneSession
{

    public System.DateTime startTimeObj;
    public System.DateTime endTimeObj;

    public string startTime;
    public string endTime;

    public string duration; 

    public string sceneName;
    public int planetsFound;
    public string currentObjective;
    public bool objectiveCompleted;
    public int aliensCaught;

    public string additionalInformation;

    public SceneSession(System.DateTime _startTime)
    {
        startTimeObj = _startTime;
        startTime = _startTime.ToString("yyyy_MM_dd__HH_mm_ss");
    }

    public void SetEndTime(System.DateTime _endTime)
    {
        endTimeObj = _endTime;
        endTime = _endTime.ToString("yyyy_MM_dd__HH_mm_ss");
    }
}
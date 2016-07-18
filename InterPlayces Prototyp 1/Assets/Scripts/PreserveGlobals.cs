using UnityEngine;
using System.Collections;

public class PreserveGlobals : MonoBehaviour {

	// Use this for initialization
	void Start () {


        StartCoroutine("preserveStuff");



    }

    IEnumerator preserveStuff()
    {
        yield return 0;

        GameObject.Find("Video background").tag = "Global";

        GameVariables.Instance.videoBackground = GameObject.Find("Video background");

        GameObject.Find("Video source 0").tag = "Global";

        GameObject[] globals = GameObject.FindGameObjectsWithTag("Global");

        foreach (GameObject global in globals)
        {
            DontDestroyOnLoad(global);
        }

        Application.LoadLevel("Spielfeld_MrRoboto");
    }

    // Update is called once per frame
    void Update () {
	
	}
}

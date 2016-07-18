using UnityEngine;
using System.Collections;

public class DisableCameras : MonoBehaviour {


    public bool disable;
    public bool enable;

    public Rect camDimensions;

    //public GameObject videoBG;

	// Use this for initialization
	void Start () {

        StartCoroutine(DisableCams());

	}
	
    public void EnableCams()
    {
        if (enable)
        {
           GameVariables.Instance.videoBackground.SetActive(true);
        }
    }


    //This is an a abomination please dont look
    public IEnumerator DisableCams()
    {


        yield return 0;
        yield return 0;
        foreach (Camera c in GetComponentsInChildren<Camera>())
        {
           // Debug.Log("SCaling Cam!");
           //Debug.Log("CAMERADISABLEIMPORTANTIMPORTANT: " + c.gameObject.ToString());
           //c.gameObject.SetActive(false);
            c.rect = camDimensions;
            
        }

        //if (videoBG == null)
        //{

        //    Debug.Log("BEEp BOOP");
        //    videoBG = GameObject.Find("Video background");

        //}


        if (GameVariables.Instance.videoBackground != null)
        {
            if (enable)
            {
                GameVariables.Instance.videoBackground.SetActive(true);
            }

            GameVariables.Instance.videoBackground.GetComponent<Camera>().rect = camDimensions;
            GameVariables.Instance.videoBackground.transform.Rotate(new Vector3(0, 0, 180));

            if (disable)
            {
                GameVariables.Instance.videoBackground.SetActive(false);
            }
        }

   

        //GameObject.Find("Camera").transform.Rotate(new Vector3(0, 0, 180));
        //GameObject.Find("Camera").SetActive(false);
    }

	// Update is called once per frame
	void LateUpdate () {
        if (GameState.Instance.IsInSetup())
        {
           GameObject.Find("Camera").transform.Rotate(new Vector3(0, 0, 180));
        }
    }
}

using UnityEngine;
using System.Collections;

public class PlayStateManager : MonoBehaviour {


    public GameObject interfaceCanvas;
    public bool jumping;
    public GameObject obstacles;

    // Use this for initialization
    void Start () {

        SetupRoboPhysics();

    }


    void SetupRoboPhysics()
    {
        Parts.Instance.TranslateRobot();

        interfaceCanvas.GetComponent<UpdateInterface>().ResetHighScoreDisplay();
        UpdateInterface.Instance.EnableJump();

        CenterPivot();


        StartCoroutine(delayedReaction());
    }


    //Moves all parts of the robot so that the center of mass is correlating with pivot
    void CenterPivot()
    {
        Vector3 pivot = Parts.Instance.transform.position;
        Vector3 centerOfGravity = new Vector3();// = Parts.Instance.gameObject.GetComponent<Rigidbody>().centerOfMass;

        float nParts = 0;

        foreach (Robopart part in Parts.Instance.transform.GetComponentsInChildren<Robopart>())
        {
            nParts += 1f;
            centerOfGravity += part.transform.position;
        }

        centerOfGravity = centerOfGravity / nParts;



        Debug.Log("pivot: " + pivot);
        Debug.Log("Center of Mass: " + centerOfGravity);

        Vector3 diff = pivot - centerOfGravity;

       

        foreach (GameObject part in Parts.Instance.roboparts)
        {
            Debug.Log("translate for " + diff);
            part.transform.Translate(diff);


            //if (part.GetComponent<Robopart>().Connection != null)
            //{
            //    part.GetComponent<Robopart>().Connection.transform.Translate(diff);
            //}

            //if (part.GetComponent<Robopart>().Connection2 != null)
            //{
            //    part.GetComponent<Robopart>().Connection2.transform.Translate(diff);
            //}

            //if (part.GetComponent<Robopart>().ConnectionCube != null)
            //{
            //    part.GetComponent<Robopart>().ConnectionCube.transform.Translate(diff);
            //}

        }

        Parts.Instance._3DConnections = true;
        Parts.Instance.ResetConnections();



        //Time.timeScale = 0f;
    }

    public IEnumerator delayedReaction()
    {
        yield return 0;

        Debug.Log("Unfreeze Mr Roboto");
        Parts.Instance.GetComponent<Rigidbody>().isKinematic = false;
        Debug.Log("Is Kinematic: " + Parts.Instance.GetComponent<Rigidbody>().isKinematic);
        Parts.Instance.UnFreezeParts();
        //Time.timeScale = 0;


    }

    public void ResetJump()
    {
        //Reset scene
        Parts.Instance.FreezeParts();
        Parts.Instance.transform.position = new Vector3(0, 3, 0);
        //Parts.Instance.TranslateRobot();
        Parts.Instance.transform.rotation = Quaternion.identity;
        Parts.Instance.ResetAllParts();
        UpdateInterface.Instance.UpdateGlobalHighscore(UpdateInterface.Instance.maxHeight);
        interfaceCanvas.GetComponent<UpdateInterface>().ResetHighScoreDisplay();

        Parts.Instance.TranslateRobot();

        FlightControl.Instance.ResetBoosts();

        foreach (Obstacle o in obstacles.transform.GetComponentsInChildren<Obstacle>())
        {
            o.ResetPosition();
        }

        jumping = false;

        StartCoroutine(delayedReaction());
    }

    public void ToSetupScene()
    {
        GameObject.Destroy(Parts.Instance.gameObject);
        //GetComponent<DisableCameras>().EnableCams();
        Application.LoadLevel("Spielfeld_MrRoboto");
    }

    // Update is called once per frame
    void Update () {
	
	}
}

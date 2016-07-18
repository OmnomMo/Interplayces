using UnityEngine;
using System.Collections;

public class TrackCubePosition : MonoBehaviour {

    public GameObject trackedObject;
    ARTrackedObject tracker;
    ARMarker marker;

    public bool alwaysTrack;
    public bool trackZ;
    public bool trackRotation;
    //public bool inverted;

    public float heightAdjustment;
    public float widthAdjustment;
    public float depthAdjustment;

    public float transformationScale;

    public bool isVisible;

    public GameObject customOrigin;
    

	// Use this for initialization
	void Start () {
        tracker = trackedObject.GetComponent<ARTrackedObject>();
        marker = tracker.GetMarker();
	}
	
	// Update is called once per frame
	void LateUpdate () {

        Matrix4x4 pose;

        bool tracking = false;



        //check if tracking is active (is in setup mode, or is always tracking)
        if (alwaysTrack) {
            tracking = true;
        }
        else {
            if (GameState.Instance != null)
            {
                tracking = GameState.Instance.IsInSetup();
            }
            else
            {
                tracking = false;
            }
            
        }

        
        if (tracking)
        {

            //Hide Objects if they are not tracked
            //Only if some time has passed (all setups are complete)
            if (Time.time > 0.1f)
            {
                //Objects only visible if cube is tracked;
                if (!tracker.GetComponent<ARTrackedObject>().visible)
                {
                    //Parts.Instance.RemoveRobopart(transform.GetChild(0).gameObject);
                    transform.GetChild(0).gameObject.SetActive(false);
                    isVisible = false;
                }

                if (tracker.GetComponent<ARTrackedObject>().visible)
                {
                    //Parts.Instance.AddRobopart(transform.GetChild(0).gameObject);
                    transform.GetChild(0).gameObject.SetActive(true);
                    isVisible = true;
                }

            }

            pose = (customOrigin.transform.localToWorldMatrix /* baseMarker.TransformationMatrix.inverse*/ * marker.TransformationMatrix);
            transform.position = ARUtilityFunctions.PositionFromMatrix(pose) * transformationScale;

            if (trackRotation)
            {
                transform.rotation = ARUtilityFunctions.QuaternionFromMatrix(pose);
            }



            if (trackZ)
            {
                //Restrain transformations;
                transform.localPosition = new Vector3(transform.position.x + widthAdjustment,  transform.position.y + heightAdjustment, transform.position.z+ depthAdjustment);
                // transform.eulerAngles = new Vector3(0, transform.localRotation.y, 0);

            } else
            {
                transform.localPosition = new Vector3(-1 * transform.position.x + widthAdjustment, -1 * transform.position.y + heightAdjustment, depthAdjustment);
            }

        }

    }
}

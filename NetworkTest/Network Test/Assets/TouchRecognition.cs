using System.Collections;
using System.Collections.Generic;
using TouchScript;
using TouchScript.Gestures;
using UnityEngine;

public class TouchRecognition : MonoBehaviour {


    //private void OnEnable()
    //{
    //    if (TouchManager.Instance != null)

    //}

    //private void OnDisable()
    //{
    //    if (TouchManager.Instance != null)
    //        TouchManager.Instance.PointersPressed -= pointersPressedHandler;
    //}

    //private void pointersPressedHandler(object sender, PointerEventArgs e)
    //{
    //    foreach (var pointer in e.Pointers)
    //        Debug.Log(pointer.Id + " touched down at " + pointer.Position);
    //}

    //void OnTouchesBegan(string msgName, SendMessageOptions options)
    //{
    //    Debug.Log("Touch recognized!");
    //    NetworkActions.Instance.CmdRegisterInput();
    //}

        void OnTouchesBegan(IList<TouchPoint> touches)
    {
        Debug.Log("Touch recognized!");
        NetworkActions.Instance.CmdRegisterInput();
    }

    //void OnGestureStateChanged(Gesture sender)
    //{
    //    Debug.Log("Touch recognized!");
    //    NetworkActions.Instance.CmdRegisterInput();
    //}

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

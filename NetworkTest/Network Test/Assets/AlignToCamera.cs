using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlignToCamera : MonoBehaviour {

    public float scaleFactor;
    float parentScale;
	// Use this for initialization
	void Start () {
        GetComponent<RectTransform>().eulerAngles = new Vector3(-90, 0, 0);
        parentScale = GetComponent<RectTransform>().parent.gameObject.transform.lossyScale.x;

    }

    // Update is called once per frame
    void Update () {
        
        float scale = Camera.main.transform.position.y / parentScale * scaleFactor;
        GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
    }
}

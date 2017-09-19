using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

    string ttText;
    public Text ttTextObject;
    public Text ttTextObjectWithImage;
    Sprite ttImage;
    public Image ttImageObject;
    
    bool ttHasImage;
    bool ttShowCaptain;
    bool ttShowNavigator;


    bool ttHasArrow;
    Transform ttArrowTarget;
    public GameObject ttArrowObject;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (ttHasArrow)
        {
            PointTargetTowards(ttArrowTarget);
        }
	}
    
    public void PointTargetTowards(Transform targetPos)
    {
        if (GameObject.Find("navigationArrowDummy") != null)
        {
            GameObject.Find("navigationArrowDummy").transform.LookAt(targetPos);
            GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, GameObject.Find("navigationArrowDummy").transform.rotation.z));
        }

    }

    public void SetTTVisibility(bool captainVis, bool navVis)
    {
        ttShowCaptain = captainVis;
        ttShowNavigator = navVis;
    } 


    public void SetTTArrowTarget(Transform arrowTarget)
    {
        if (arrowTarget != null)
        {
            ttHasArrow = true;
            ttArrowTarget = arrowTarget;
        } else
        {
            ttHasArrow = false;
            ttArrowObject = null;
        }
    }

    public void Show()
    {
        Show(ttText, ttImage);
    }

    public void Show(string text)
    {
        Show(text, null);
    }



    public void Show(string text, Sprite newImage)
    {

        ttText = text;

        ttTextObject.text = ttText;
        ttTextObjectWithImage.text = ttText;

        ttImage = newImage;

        if (ttImage == null)
        {
            ttHasImage = false;
        } else
        {
            ttImageObject.sprite = ttImage;
        }


        if (ttHasImage)
        {
            ttTextObject.gameObject.SetActive(false);
            ttTextObjectWithImage.gameObject.SetActive(true);
            ttImageObject.gameObject.SetActive(true);
        } else
        {
            ttTextObject.gameObject.SetActive(true);
            ttTextObjectWithImage.gameObject.SetActive(false);
            ttImageObject.gameObject.SetActive(false);
        }

        if (ttHasArrow)
        {
            ttArrowObject.gameObject.SetActive(true);
        } else
        {
            ttArrowObject.gameObject.SetActive(false);
        }
    }
}

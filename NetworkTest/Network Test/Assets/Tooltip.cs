using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

    public string ttText;
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


    public bool timed;
    public float startTime;
    public float duration;


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

           // Debug.Log("Point Arrow towards target");
            GameObject.Find("navigationArrowDummy").transform.LookAt(targetPos);
            //ttArrowObject.GetComponent<RectTransform>().rotation = Quaternion.Euler();
            ttArrowObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, -1 * GameObject.Find("navigationArrowDummy").transform.eulerAngles.y);
        }
         else
        {
            Debug.Log("Navigation arrow dummy not found");
        }
    }

    public void SetTTVisibility(bool captainVis, bool navVis)
    {
        ttShowCaptain = captainVis;
        ttShowNavigator = navVis;
    } 


    public void SetTTArrowTarget(Transform arrowTarget)
    {

        //Debug.Log("Set Tooltiup Arrow Target");
        if (arrowTarget != null)
        {
            ttHasArrow = true;
            ttArrowTarget = arrowTarget;
        } else
        {
            ttHasArrow = false;
            ttArrowTarget = null;
        }
    }

    public void Hide()
    {

        TooltipManager.Instance.RemoveToolTipFromQueue(this);

        ttTextObject.gameObject.SetActive(false);
        ttTextObjectWithImage.gameObject.SetActive(false);
        ttImageObject.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }



    void OnDisable()
    {
        Hide();
    }

    public void SetContent(string text, Sprite newImage)
    {
        ttText = text;

        ttTextObject.text = ttText;
        ttTextObjectWithImage.text = ttText;


        ttImage = newImage;

        if (ttImage == null)
        {
            ttHasImage = false;
        }
        else
        {
            ttHasImage = true;
            ttImageObject.sprite = ttImage;
            ttImageObject.preserveAspect = true;
        }
    }

    public void Show()
    {
        //Debug.Log("Show Tooltip: " + ttText);
        



       

        if ((GameState.Instance.isPlayerCaptain() && ttShowCaptain) || (GameState.Instance.isPlayerNavigator() && ttShowNavigator))
        {
            if (ttHasImage)
            {
                ttTextObject.gameObject.SetActive(false);
                ttTextObjectWithImage.gameObject.SetActive(true);
                ttImageObject.gameObject.SetActive(true);
            }
            else
            {
                ttTextObject.gameObject.SetActive(true);
                ttTextObjectWithImage.gameObject.SetActive(false);
                ttImageObject.gameObject.SetActive(false);
            }

            if (ttHasArrow)
            {
                ttArrowObject.gameObject.SetActive(true);
            }
            else
            {
                ttArrowObject.gameObject.SetActive(false);
            }
        } else
        {
            ttTextObject.gameObject.SetActive(false);
            ttTextObjectWithImage.gameObject.SetActive(false);
            ttImageObject.gameObject.SetActive(false);
            ttArrowObject.gameObject.SetActive(false);
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Hides Object only for Navigator (or for capatin if bool is actice)

public class HideForNavigator : MonoBehaviour {


    public bool hideForCaptain;

	// Use this for initialization
	void Start () {

        if (!hideForCaptain)
        {
            
            if (GameState.Instance != null && GameState.Instance.isPlayerNavigator())
            {
                Debug.Log("Hide " + gameObject.ToString() + "for Navigator.");

                if (GetComponent<MeshRenderer>() != null)
                {
                    GetComponent<MeshRenderer>().enabled = false;
                }

                if (GetComponent<RectTransform>() != null)
                {
                    foreach (RectTransform child in GetComponentsInChildren<RectTransform>())
                    {
                        if (child.gameObject.GetComponent<Text>() != null)
                        {
                            child.gameObject.GetComponent<Text>().enabled = false;
                        }

                        if (child.gameObject.GetComponent<Canvas>() != null)
                        {
                            child.gameObject.GetComponent<Canvas>().enabled = false;
                        }

                        if (child.gameObject.GetComponent<Image>() != null)
                        {
                            child.gameObject.GetComponent<Image>().enabled = false;
                        }
                    }
                }

                if (GetComponent<Text>() != null)
                {
                    GetComponent<Text>().enabled = false;
                }

                if (GetComponent<Canvas>() != null)
                {
                    GetComponent<Canvas>().enabled = false;
                }

                if (GetComponent<Image>() != null)
                {
                    GetComponent<Image>().enabled = false;
                }
            }
        }else
        {

            if (GameState.Instance != null && GameState.Instance.isPlayerCaptain())
            {
               // Debug.Log("Hide " + gameObject.ToString() + "for Captain.");
                if (GetComponent<MeshRenderer>() != null)
                {
                    GetComponent<MeshRenderer>().enabled = false;
                }

                if (GetComponent<RectTransform>() != null)
                {
                    foreach (RectTransform child in GetComponentsInChildren<RectTransform>())
                    {
                        if (child.gameObject.GetComponent<Text>() != null)
                        {
                            child.gameObject.GetComponent<Text>().enabled = false;
                        }

                        if (child.gameObject.GetComponent<Canvas>() != null)
                        {
                            child.gameObject.GetComponent<Canvas>().enabled = false;
                        }

                        if (child.gameObject.GetComponent<Image>() != null)
                        {
                            child.gameObject.GetComponent<Image>().enabled = false;
                        }
                    }
                }

                if (GetComponent<Canvas>() != null)
                {
                    GetComponent<Canvas>().enabled = false;
                }


                if (GetComponent<Text>() != null)
                {
                    GetComponent<Text>().enabled = false;
                }

                if (GetComponent<Image>() != null)
                {
                    GetComponent<Image>().enabled = false;
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

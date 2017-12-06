using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SelectOnStart : MonoBehaviour {

    public Button firstSelection;

	// Use this for initialization
	void Start () {
        // firstSelection.Select();
        StartCoroutine(SelectContinueButtonDelayed());
	}

    IEnumerator SelectContinueButtonDelayed()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelection.gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}

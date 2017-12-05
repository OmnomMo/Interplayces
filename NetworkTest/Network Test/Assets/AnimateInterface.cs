using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AnimateInterface : NetworkBehaviour {

    public RectTransform animatedSlider1;

	// Use this for initialization
	void Start () {
		
	}

    [ClientRpc]
    public void RpcWobbleSliders()
    {

        if (NetworkActions.Instance.logActions)
        {
            Debug.Log(NetworkActions.Instance.nLocalActionsTaken++ + ". Wobble Sliders.");
        }

        Debug.Log("Wobblewoblle");
        if (GameState.Instance.isPlayerNavigator() && SpaceshipGameplay.Instance.thrustPower == 0 && !animatedSlider1.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Wobbling"))
        {
            animatedSlider1.GetComponent<Animator>().SetTrigger("WobbleTrigger");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

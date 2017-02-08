using UnityEngine;
using System.Collections;

public class NetworkPlayer : MonoBehaviour
{

    


    public Canvas dialogue;
    private static NetworkPlayer instance;
    public static NetworkPlayer Instance { get { return instance; } }

    // Use this for initialization
    void Start()
    {

        instance = this;
        Object.DontDestroyOnLoad(gameObject);
        

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerType + " is captain: " + (playerType == PlayerTypes.Captain) + " IsCaptain: " + isPlayerCaptain());
    }

   
    public GameState.PlayerTypes GetPlayerType()
    {
        return GameState.Instance.getPlayerType();
    }

    public void removeCanvas()
    {
        dialogue.gameObject.SetActive(false);
    }
}

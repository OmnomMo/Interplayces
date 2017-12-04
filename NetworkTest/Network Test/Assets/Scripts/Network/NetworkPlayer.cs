using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour
{

    


    public Canvas dialogue;
    private static NetworkPlayer instance;
    public static NetworkPlayer Instance { get { return instance; } }

    public GameObject navInterface;

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

    [ClientRpc]
    public void RpcRestartGame()
    {
        //GameObject.Destroy(Score.Instance.gameObject);

        MultiplayerSetup.Instance.ServerChangeScene("02_SpaceShipEditor_Tracking");
    }

    [ClientRpc] 
    public void RpcRegisterInput()
    {
        Sessionmanagement.Instance.GetNewInput();
    }

    [ClientRpc]
    public void RpcReturnToLevelSelect()
    {
        MultiplayerSetup.Instance.ServerChangeScene("03_Level_Select");
    }

    [ClientRpc]
    public void RpcRestartLevel()
    {
        //GameObject.Destroy(Score.Instance.gameObject);

        MultiplayerSetup.Instance.ServerChangeScene(Application.loadedLevelName);
    }

    [ClientRpc]
    public void RpcPauseGame()
    {
        GameObject.Find("Pause Menu Manager").GetComponent<GreatArcStudios.PauseManager>().PauseGame();
        MainSceneManager.Instance.DeactivateGameInterface();
      
    }

    [ClientRpc]
    public void RpcUnpauseGame()
    {
        GameObject.Find("Pause Menu Manager").GetComponent<GreatArcStudios.PauseManager>().UnPauseGame();
        MainSceneManager.Instance.ActivateGameInterface();
    }

    [ClientRpc]
    public void RpcShowEndMessage(int message)
    {
        ToEndScreen.Instance.showEndMessage(message);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInformation : NetworkBehaviour
{
    [SyncVar]
    public string playerName;
    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer) {
            return;
        }
        CmdSetName(GameObject.FindWithTag("PlayerNameInput").GetComponent<UnityEngine.UI.Text>().text);
        Debug.Log(playerName);
        foreach (var v in GameObject.FindGameObjectsWithTag("PreJoin")) {
            v.active = false;
        }
        GameObject readyButton = GameObject.Find("ReadyButton");
        readyButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(GetReady);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [Command]
    public void CmdSetName(string n) {
        playerName = n;
    }
    public void GetReady() {
        GetComponent<NetworkLobbyPlayer>().SendReadyToBeginMessage();
    }
}

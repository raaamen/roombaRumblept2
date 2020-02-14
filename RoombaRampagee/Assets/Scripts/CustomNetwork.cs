using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetwork : NetworkManager
{
    public GameManagerScript gm_script;
    public int playerCount = 0;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Transform startpos = GetStartPosition();
        var player = (GameObject)GameObject.Instantiate(playerPrefab, startpos.position,startpos.rotation);
        player.GetComponent<RoombaManager>().team = playerCount % 2;
        Debug.Log("Team set to " + playerCount % 2);
        playerCount++;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}

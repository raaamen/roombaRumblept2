using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetwork : NetworkManager
{
    public GameManagerScript gm_script;
    public int playerCount = 0;
    public GameObject textObj;

    public List<GameObject> roombas;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Transform startpos = GetStartPosition();
        var player = (GameObject)GameObject.Instantiate(playerPrefab, startpos.position,startpos.rotation);
        player.GetComponent<RoombaManager>().team = playerCount % 2;
        Debug.Log("Team set to " + playerCount % 2);
        playerCount++;
        player.GetComponent<RoombaManager>().playerNum = playerCount;

        roombas.Add(player);
        gm_script = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        gm_script.gameStarted = true;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
    public virtual void OnServerRemovePlayer(NetworkConnection conn, NetworkIdentity player)
    {
        if (player.gameObject != null)
        {
            NetworkServer.Destroy(player.gameObject.GetComponent<RoombaManager>().dustText);
            NetworkServer.Destroy(player.gameObject);
            roombas.Remove(player.gameObject);
        }
        playerCount--;

    }
}

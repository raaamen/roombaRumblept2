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
        textObj.GetComponent<showText>().playersInGame = playerCount;
        roombas.Add(player);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
    public virtual void OnServerRemovePlayer(NetworkConnection conn, NetworkIdentity player)
    {
        if (player.gameObject != null)
        {
            NetworkServer.Destroy(player.gameObject);
            roombas.Remove(player.gameObject);
        }
        playerCount--;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class CustomNetwork : NetworkLobbyManager
{
    public GameManagerScript gm_script;
    public int playerCount = 0;
    public GameObject textObj;
    public int readyCount;
    public GameObject playerCountText;
    public GameObject readyButton;

    public List<GameObject> roombas;

    // public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId){
    //     var spawnPos = startPositions[0];
    //     var player = Instantiate(playerPrefab, spawnPos.position, Quaternion.identity);
    //     NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    //     PlayerInformation pi = player.GetComponent<PlayerInformation>();
    //     pi.name = "Bob";
    // }
    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer,GameObject gamePlayer)
    {
        gamePlayer.GetComponent<RoombaManager>().team = playerCount % 2;
        Debug.Log("Team set to " + playerCount % 2);
        playerCount++;
        gamePlayer.GetComponent<RoombaManager>().playerNum = playerCount;
        gamePlayer.GetComponent<RoombaManager>().playerName = lobbyPlayer.GetComponent<PlayerInformation>().playerName;

        roombas.Add(gamePlayer);
        gm_script = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        gm_script.gameStarted = true;
        return true;
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

    public void quick_join() {
        StartMatchMaker();
        matchMaker.ListMatches(0,20,"",false,0,1,OnMatchList);
        Debug.Log("Searching");
        playerCountText.GetComponent<ReadyText>().nm = this;
        playerCountText.active = true;
        readyButton.active = true;
    }

    public virtual void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        if (success)
        {
            if (matchList.Count != 0)
            {
                Debug.Log("Matches Found");
                matchMaker.JoinMatch(matchList[0].networkId, "", "", "", 0, 1, OnMatchJoined);
            }
            else
            {
                Debug.Log("No Matches Found");
                Debug.Log("Creating Match");
                matchMaker.CreateMatch("Default Match", 2, true, "", "", "", 0, 1, OnMatchCreate);
            }
        }
        else
        {
            Debug.Log("ERROR : Match Search Failure");
        }
    }
    
    public virtual void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            Debug.Log("Match Joined");
            MatchInfo hostInfo = matchInfo;
            StartClient(hostInfo);
        }
        else
        {
            Debug.Log("ERROR : Match Join Failure");
        }

    }
    public virtual void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            Debug.Log("Match Created");

            MatchInfo hostInfo = matchInfo;
            NetworkServer.Listen(hostInfo, 9000);
            StartHost(hostInfo);
        }
        else
        {
            Debug.Log("ERROR : Match Create Failure");
        }
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoombaManager : NetworkBehaviour
{
    [SyncVar]
    public bool alive = true;

    [SyncVar]
    public int team = 0;

    [SyncVar]
    public int dust_collected;

    [SyncVar]
    public bool invulnerable = false;

    public int balloonRespawnTime;
    public int invulnerabilityTime;

    public GameObject playerIndicator;

    public GameObject balloon;

    public GameManagerScript gm_script;
    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer) {
            playerIndicator.active = false;
        }
        gm_script = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        RpcChangeBalloon(true,team*2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Command]
    public void CmdPopBalloon()
    {
        if (invulnerable || !alive) {
            return;
        }
        balloon.SetActive(false);
        alive = false;
        RpcChangeBalloon(false,-1);
        StartCoroutine("RespawnBalloon");
    }

    public IEnumerator RespawnBalloon()
    {
        if (alive) {
            yield return null;
        }
        yield return new WaitForSeconds(balloonRespawnTime);
        balloon.SetActive(true);
        RpcChangeBalloon(true,team*2+1);
        alive = true;
        invulnerable = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        RpcChangeBalloon(true,team*2);
        invulnerable = false;
    }

    [ClientRpc]
    public void RpcChangeBalloon(bool state,int colorIndex) {
        balloon.SetActive(state);
        if (colorIndex != -1) {
            balloon.GetComponent<MeshRenderer>().material = gm_script.balloonMats[colorIndex];
        }
    }
}

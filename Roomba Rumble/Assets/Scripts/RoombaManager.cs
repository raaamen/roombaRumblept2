using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoombaManager : NetworkBehaviour
{
    [SyncVar]
    public bool alive = true;

    [SyncVar]
    public int team = -1;

    [SyncVar]
    public int dust_collected;

    [SyncVar]
    public bool invulnerable = false;

    [SyncVar]
    public int playerNum;

    [SyncVar]
    public string playerName;

    public int balloonRespawnTime;
    public int invulnerabilityTime;

    public GameObject playerIndicator;

    public GameObject knife;
    public GameObject balloon;

    public GameObject dustText;
    public GameObject nameText;

    

    public GameManagerScript gm_script;
    // Start is called before the first frame update

    void Start()
    {
        if (!isLocalPlayer)
        {
            playerIndicator.active = false;
        }
        gm_script = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        ChangeBalloon(alive, team * 2);
        dustText = Instantiate(dustText,transform.position+new Vector3(3,0,0),Quaternion.Euler(90,180,90));
        dustText.GetComponent<roombaText>().rm = this;
        nameText = Instantiate(nameText,transform.position+new Vector3(-4,0,0),Quaternion.Euler(90,180,90));
        nameText.GetComponent<roombaNameText>().rm = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Command]
    public void CmdPopBalloon()
    {
        if (invulnerable)
        {
            return;
        }
        balloon.SetActive(false);
        alive = false;
        invulnerable = true;
        RpcChangeBalloon(false, -1);
        gm_script.CmdDropDust((int)(dust_collected*0.5),balloon.transform.position,transform.forward,transform.right);
        CmdSetDustCount((int)(0.25*dust_collected));
        StartCoroutine("RespawnBalloon");
    }

    public IEnumerator RespawnBalloon()
    {
        if (alive)
        {
            yield return null;
        }
        yield return new WaitForSeconds(balloonRespawnTime);
        balloon.SetActive(true);
        RpcChangeBalloon(true, team * 2 + 1);
        alive = true;
        invulnerable = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        RpcChangeBalloon(true, team * 2);
        invulnerable = false;
    }

    [ClientRpc]
    public void RpcChangeBalloon(bool state, int colorIndex)
    {
        if (state == false)
        {
            knife.GetComponent<AudioSource>().Play();
        }
        balloon.SetActive(state);
        if (colorIndex != -1)
        {
            balloon.GetComponent<MeshRenderer>().material = gm_script.balloonMats[colorIndex];
        }
    }

    [Command]
    public void CmdChangeBalloon(bool state, int colorIndex) {
        RpcChangeBalloon(state,colorIndex);
    }

    public void ChangeBalloon(bool state, int colorIndex)
    {
        if (state == false)
        {
            knife.GetComponent<AudioSource>().Play();
        }
        balloon.SetActive(state);
        if (colorIndex != -1)
        {
            balloon.GetComponent<MeshRenderer>().material = gm_script.balloonMats[colorIndex];
        }
    }

    [Command]
    public void CmdChangeDustCount(int delta_amount) {
        dust_collected += delta_amount;
    }

    [Command]
    public void CmdSetDustCount(int new_amount) {
        dust_collected = new_amount;
    }

    [Command]
    public void CmdDestroyObject(GameObject g) {
        NetworkServer.Destroy(g);
    }
}
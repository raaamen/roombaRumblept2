using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoombaManager : NetworkBehaviour
{
    [SyncVar]
    public bool alive = true;

    [SyncVar]
    public int team;

    [SyncVar]
    public int dust_collected;

    [SyncVar]
    public bool invulnerable = false;

    public int balloonRespawnTime;
    public int invulnerabilityTime;

    public GameObject knife;
    public GameObject balloon;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void popBalloon()
    {
        if (invulnerable) {
            return;
        }
        knife.GetComponent<AudioSource>().Play();
        balloon.SetActive(false);
        alive = false;
        StartCoroutine("respawnBalloon");
    }

    public IEnumerator respawnBalloon()
    {
        yield return new WaitForSeconds(balloonRespawnTime);
        balloon.SetActive(true);
        alive = true;
        invulnerable = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        invulnerable = false;
    }
}

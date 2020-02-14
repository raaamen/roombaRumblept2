using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManagerScript : NetworkBehaviour
{
    [SyncVar]
    public int team0score;
    [SyncVar]
    public int team1score;

    public bool endGame;

    [SyncVar]
    public bool gameStarted;

    public float timer = 0;

    public GameObject balloon;
    public Vector3 balloonOffset;

    public GameObject beginningimage;
    public GameObject dust;

    public Vector3 dustSpawn;

    public Material[] balloonMats;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    // void Update()
    // {
    //     while (gameStarted)
    //     {
    //         timer += Time.deltaTime;
    //         if (timer>=30f)
    //         {
    //             endGame = true;
    //         }
    //     }
    // }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 3)
        {
            CmdSpawnDust();
            timer = 0;
        }
    }

    void startGame()
    {
        beginningimage.SetActive(false);
        gameStarted = true;
    }

    [Command]
    void CmdSpawnDust()
    {
        //x coord upper = -4, x coord lower = -45
        //y coord = 0.2761
        //z coord upper = -5.3
        //z coord lower = -92
        if (gameStarted)
        {
            dustSpawn = new Vector3(Random.Range(-4, -45), 0.2761f, Random.Range(-5, -92));
            int mask = ~(LayerMask.GetMask("Floor"));
            Collider[] intersects = Physics.OverlapBox(dustSpawn, new Vector3(1.5f, 0.25f, 1.5f), Quaternion.identity, mask,
                QueryTriggerInteraction.Collide);
            if (intersects.Length > 0) return;
            GameObject new_dust = Instantiate(dust, dustSpawn, Quaternion.identity);
            NetworkServer.Spawn(new_dust);
        }
    }
    [Command]
    public void CmdDropDust(int amount,Vector3 location,Vector3 scatter_dir_long,Vector3 scatter_dir_wide) {
        for (int i = 0;i < amount;i++) {
            Vector3 randomfactor = scatter_dir_long * Random.value * 10 + scatter_dir_wide * (float)(Random.value-0.5) * 5;
            GameObject new_dust = Instantiate(dust,
                location+randomfactor,Quaternion.identity);
            NetworkServer.Spawn(new_dust);
        }
    }
}

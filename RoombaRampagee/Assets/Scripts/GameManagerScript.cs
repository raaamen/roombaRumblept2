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
        gameStarted = true;
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
            spawnDust();
            timer = 0;
        }
    }

    void startGame()
    {
        beginningimage.SetActive(false);
        gameStarted = true;
    }

    void spawnDust()
    {
        //x coord upper = -4, x coord lower = -45
        //y coord = 0.2761
        //z coord upper = -5.3
        //z coord lower = -92

        dustSpawn = new Vector3(Random.Range(-4, -45), 0.2761f, Random.Range(-5, -92));

        Instantiate(dust, dustSpawn, Quaternion.identity);
    }






}

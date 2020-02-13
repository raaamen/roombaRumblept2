using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManagerScript : NetworkBehaviour
{

    public int player1score;
    public int player2score;

    public bool endGame;
    public bool gameStarted;

    public float timer = 0;

    public GameObject balloon;
    public Vector3 balloonOffset;

    // Start is called before the first frame update
    void Start()
    {
        gameStarted = true;
    }

    [Command]
    void CmdRequestBalloon(GameObject roomba) {
        GameObject new_balloon = Instantiate(balloon,roomba.transform.position,roomba.transform.rotation);
        new_balloon.transform.Translate(balloonOffset);
        NetworkServer.Spawn(new_balloon);
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



}

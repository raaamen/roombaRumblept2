using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    public int player1score;
    public int player2score;

    public bool endGame;
    public bool gameStarted;

    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while (gameStarted)
        {
            timer += Time.deltaTime;
            if (timer>=30f)
            {
                endGame = true;
            }
        }
    }



}

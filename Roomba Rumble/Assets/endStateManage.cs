using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class endStateManage : MonoBehaviour
{

    public bool gameStarted;


    public float gameTimer = 180;

    public GameObject teamWinText;
    public GameObject restartingText;
    public GameObject gameOverText;
    public GameObject countdown;

    public GameManagerScript gm;
    public CustomNetwork nm;

    public string winningTeam;

    public int count = 3;



    // Start is called before the first frame update
    private void Awake()
    {
        gameStarted = true;
        teamWinText.SetActive(false);
        restartingText.SetActive(false);
        gameOverText.SetActive(false);
        gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        nm = GameObject.Find("NetworkManager").GetComponent<CustomNetwork>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        countdown.GetComponent<TMP_Text>().text = "Remaining game time: " + (int)gameTimer + " seconds";
        if (Input.GetKeyDown(KeyCode.H))
        {
            gameTimer = 1;
        }
        if (gameStarted) {
            gameTimer -= Time.deltaTime;
        }
        if (gameTimer <=0 && gameStarted)
        {
            gameStarted = false;
            gameOverText.SetActive(true);
            if (gm.team0score > gm.team1score)
            {
                winningTeam = "Red";
            }
            else if (gm.team1score > gm.team0score)
            {
                winningTeam = "Blue";
            }
            else if (gm.team0score == gm.team1score)
            {
                winningTeam = "No";
            }

            foreach (var item in nm.roombas)
            {
                item.GetComponent<RoombaManager>().alive = false;
                item.GetComponent<RoombaManager>().balloonRespawnTime = 10000000;
            }

            teamWinText.GetComponent<TMP_Text>().text = winningTeam+" Team has won!";
            teamWinText.SetActive(true);
            StartCoroutine("countDownText");
            
        }
    }

    public IEnumerator countDownText()
    {
        restartingText.SetActive(true);
        restartingText.GetComponent<TMP_Text>().text = "Game ending in "+count+"...";
        yield return new WaitForSeconds(1);
        count--;
        restartingText.GetComponent<TMP_Text>().text = "Game ending in " + count + "...";
        yield return new WaitForSeconds(1);
        count--;
        restartingText.GetComponent<TMP_Text>().text = "Game ending in " + count + "...";
        yield return new WaitForSeconds(1);
        Application.Quit();
        //insert scene reload here
    }
}

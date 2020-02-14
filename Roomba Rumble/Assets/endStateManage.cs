using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class endStateManage : MonoBehaviour
{

    public bool gameStarted;

    public float gameTimer = 0;

    public GameObject teamWinText;
    public GameObject restartingText;
    public GameObject gameOverText;

    public GameManagerScript gm;

    public string winningTeam;



    // Start is called before the first frame update
    private void Awake()
    {
        gameStarted = true;
        teamWinText.SetActive(false);
        restartingText.SetActive(false);
        gameOverText.SetActive(false);
        gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;
        if (gameTimer >=180)
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
            teamWinText.GetComponent<TMP_Text>().text = winningTeam+" Team has won!";
            teamWinText.SetActive(true);
            StartCoroutine("countDownText");
            
        }
    }

    public IEnumerator countDownText()
    {
        restartingText.SetActive(true);
        int count = 3;
        restartingText.GetComponent<TMP_Text>().text = "Game restarting in "+count+"...";
        yield return new WaitForSeconds(1);
        count--;
        restartingText.GetComponent<TMP_Text>().text = "Game restarting in " + count + "...";
        yield return new WaitForSeconds(1);
        count--;
        restartingText.GetComponent<TMP_Text>().text = "Game restarting in " + count + "...";
        yield return new WaitForSeconds(1);
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}

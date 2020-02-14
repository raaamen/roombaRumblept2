using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showText : MonoBehaviour
{

    public int playersInGame;

    public GameObject player1text;
    public GameObject player2text;
    public GameObject player3text;
    public GameObject player4text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateTextBoxes()
    {
        player1text.SetActive(false);
        player2text.SetActive(false);
        player3text.SetActive(false);
        player4text.SetActive(false);
        switch (playersInGame)
        {
            case 1:
                player1text.SetActive(true);
                break;
            case 2:
                player1text.SetActive(true);
                player2text.SetActive(true);
                break;
            case 3:
                player1text.SetActive(true);
                player2text.SetActive(true);
                player3text.SetActive(true);
                break;
            case 4:
                player1text.SetActive(true);
                player2text.SetActive(true);
                player3text.SetActive(true);
                player4text.SetActive(true);
                break;
            
        }
    }

}

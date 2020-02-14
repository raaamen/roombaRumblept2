using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class showText : MonoBehaviour
{


    public GameObject player1text;
    public GameObject player2text;
    public GameObject player3text;
    public GameObject player4text;

    public int player1dust;
    public int player2dust;
    public int player3dust;
    public int player4dust;

    public CustomNetwork network;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateTextBoxes(int playersInGame)
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

    public void updateTextNum()
    {
        foreach (var item in network.roombas)
        {
            switch (item.GetComponent<RoombaManager>().playerNum)
            {
                case 1:
                    player1text.GetComponent<TMP_Text>().text = "Player 1 Dust Collected: "+item.GetComponent<RoombaManager>().dust_collected;
                    break;
                case 2:
                    player2text.GetComponent<TMP_Text>().text = "Player 2 Dust Collected: " + item.GetComponent<RoombaManager>().dust_collected;
                    break;
                case 3:
                    player3text.GetComponent<TMP_Text>().text = "Player 3 Dust Collected: " + item.GetComponent<RoombaManager>().dust_collected;
                    break;
                case 4:
                    player4text.GetComponent<TMP_Text>().text = "Player 4 Dust Collected: " + item.GetComponent<RoombaManager>().dust_collected;
                    break;
            }
        }
    }

}

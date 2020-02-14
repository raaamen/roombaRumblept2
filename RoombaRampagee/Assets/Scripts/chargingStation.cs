using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class chargingStation : MonoBehaviour
{

    public GameObject text;

    public int team;
    //1 is blue 0 is red

    GameManagerScript gm_script;

    // Start is called before the first frame update
    void Start()
    {
        gm_script = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        updateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateText()
    {
        Debug.Log("text updated");
        switch (team)
        {
            case 1:
                text.GetComponent<TMP_Text>().text = "Blue Team Score: " + gm_script.team1score;
                break;
            case 0:
                text.GetComponent<TMP_Text>().text = "Red Team Score: " + gm_script.team0score;
                break;
        }
        
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class ReadyText : MonoBehaviour
{
    public CustomNetwork nm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (nm.numPlayers) {
            case 0:
            GetComponent<TMP_Text>().text = "Waiting for other players...";
            break;
            default:
            GetComponent<TMP_Text>().text = "Players in lobby: "+ nm.numPlayers;
            break;
        }
    }
}

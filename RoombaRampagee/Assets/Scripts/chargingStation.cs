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

    // Start is called before the first frame update
    void Start()
    {
        updateText(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateText(int num)
    {
        switch (team)
        {
            case 1:
                text.GetComponent<TMP_Text>().text = "Blue Team Score: " + num;
                break;
            case 0:
                text.GetComponent<TMP_Text>().text = "Red Team Score: " + num;
                break;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Roomba")
        {
            updateText(collision.gameObject.GetComponent<RoombaManager>().dust_collected);
        }
    
    }
}

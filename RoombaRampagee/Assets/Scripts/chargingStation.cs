using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class chargingStation : MonoBehaviour
{

    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateText(int teamnum, int num)
    {
        switch (teamnum)
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
        /**if (collision.gameObject.tag == "Roomba")
        {
            switch (collision.gameObject.GetComponent<RoombaManager>().)
            {
                default:
                    break;
            }
            updateText();
        }
    */
    }
}

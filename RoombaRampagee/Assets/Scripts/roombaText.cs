using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class roombaText : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        GetComponent<TMP_Text>().text = "Dust Collected: " + transform.parent.gameObject.GetComponent<RoombaManager>().dust_collected;
    }

    public void updateText()
    {
        
    }
}

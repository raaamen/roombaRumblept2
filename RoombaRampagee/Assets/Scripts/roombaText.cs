using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class roombaText : MonoBehaviour
{
    Quaternion startrotation;
    Vector3 offset;
    public RoombaManager rm;
    // Start is called before the first frame update
    void Start()
    {
        startrotation = transform.rotation;
        offset = transform.position - rm.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = startrotation;
        transform.position = rm.transform.position+offset;
        GetComponent<TMP_Text>().text = "Dust Collected: " + rm.dust_collected;
    }

    public void updateText()
    {
        
    }
}
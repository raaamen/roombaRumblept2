using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class roombaNameText : MonoBehaviour
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
        if (!rm) {
            Destroy(this.gameObject);
        }
        transform.rotation = startrotation;
        transform.position = rm.transform.position+offset;
        GetComponent<TMP_Text>().text = rm.playerName;
    }

    public void updateText()
    {
        
    }
}
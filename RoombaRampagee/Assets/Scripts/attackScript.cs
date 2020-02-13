using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackScript : MonoBehaviour
{

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Balloon")
        {
            collider.gameObject.transform.parent.gameObject.GetComponent<RoombaManager>().popBalloon();
            Debug.Log("bb");
        }
    }

}

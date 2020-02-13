using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{

    public RoombaMovement roombaScript;

    // Start is called before the first frame update

    private void Awake()
    {
        roombaScript = GetComponent<RoombaMovement>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Balloon")
        {
            roombaScript.collidingWithBalloon = true;
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Balloon")
        {
            roombaScript.collidingWithBalloon = false;
        }
    }

}

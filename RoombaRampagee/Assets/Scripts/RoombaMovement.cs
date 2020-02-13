﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaMovement : MonoBehaviour
{

    public bool gameStarted = false;


    /*keys for players are as following
     * 
     * player 1: wasd
     * w to move forward
     * a and d to rotate
     * s to turn backwards
     * 
     * 
     * player 2: arrow keys
     * up arrow to move forward
     * side arrows to rotate
     * down arrow to go backwards
     * 
     * */

    public KeyCode key;

    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;
    public KeyCode attackKey;


    public float moveSpeed;
    public float rotationSpeed;

    public Rigidbody rb;

    public GameObject knife;
    public GameObject target;
    public GameObject enemyBalloon;
    public GameObject gameManager;

    public GameManagerScript gameManagerScript;

    public bool collidingWithBalloon;

    // Start is called before the first frame update

    private void Awake()
    {
        gameStarted = true;
        rb = GetComponent<Rigidbody>();
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.gameStarted)
        {
            if (Input.GetKey(upKey))
            {
                rb.AddForce(transform.forward*-moveSpeed);
            }
            else if (Input.GetKey(downKey))
            {
                rb.AddForce(transform.forward * moveSpeed);
            }
            else if (Input.GetKey(rightKey))
            {
                //changing y rotation
                transform.Rotate(0, rotationSpeed*Time.deltaTime, 0);
            }
            else if (Input.GetKey(leftKey))
            {
                transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
            }

            if (collidingWithBalloon)
            {
                Destroy(enemyBalloon);
            }


        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoombaMovement : NetworkBehaviour
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

    public int balloonRespawnTime;

    public KeyCode key;

    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;
    public KeyCode attackKey;


    public float moveSpeed;
    public float rotationSpeed;

    public Rigidbody rb;

    // public GameObject knife;
    // public GameObject target;
    // public GameObject enemyBalloon;
    public GameObject gameManager;
    public GameObject balloon;

    // private GameObject balloon;

    public GameManagerScript gameManagerScript;

    public bool collidingWithBalloon;
    public bool canMove;
    public bool hasBalloon;

    // Start is called before the first frame update

    private void Awake()
    {
        if (!gameManager)
        {
            gameManager = GameObject.FindWithTag("GameController");
        }
        rb = GetComponent<Rigidbody>();
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();

    }

    void Start()
    {
        if (!gameManager)
        {
            gameManager = GameObject.FindWithTag("GameController");
        }
        rb = GetComponent<Rigidbody>();
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (gameManagerScript.gameStarted && canMove)
        {
            if (Input.GetKey(upKey))
            {
                rb.AddForce(transform.forward * -moveSpeed);
            }
            else if (Input.GetKey(downKey))
            {
                rb.AddForce(transform.forward * moveSpeed);
            }
            else if (Input.GetKey(rightKey))
            {
                //changing y rotation
                transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            }
            else if (Input.GetKey(leftKey))
            {
                transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
            }

            // if (collidingWithBalloon)
            // {
            //     Destroy(enemyBalloon);
            // }


        }
    }

    public void popBalloon()
    {
        balloon.SetActive(false);
        canMove = false;

    }

    public IEnumerator respawnBalloon()
    {
        yield return new WaitForSeconds(balloonRespawnTime);
        balloon.SetActive(true);
        canMove = true;
    }

}

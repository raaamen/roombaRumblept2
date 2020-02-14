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
    public bool hasBalloon;
    public RoombaManager r_man;

    public AudioSource audiosource;

    // Start is called before the first frame update

    private void Awake()
    {
        if (!gameManager)
        {
            gameManager = GameObject.FindWithTag("GameController");
        }
        rb = GetComponent<Rigidbody>();
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        r_man = GetComponent<RoombaManager>();
        audiosource = GetComponent<AudioSource>();
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

    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (gameManagerScript.gameStarted && r_man.alive)
        {
            if (Input.GetKey(upKey))
            {
                audiosource.Play();
                rb.AddForce(transform.forward * -moveSpeed);
            }
            else if (Input.GetKey(downKey))
            {
                audiosource.Play();
                rb.AddForce(transform.forward * moveSpeed);
            }
            else if (Input.GetKey(rightKey))
            {
                if (audiosource.isPlaying)
                {
                    audiosource.Stop();
                }
                //changing y rotation
                transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            }
            else if (Input.GetKey(leftKey))
            {
                if (audiosource.isPlaying)
                {
                    audiosource.Stop();
                }
                transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
            }

            // if (collidingWithBalloon)
            // {
            //     Destroy(enemyBalloon);
            // }


        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Charger")
        {
            Debug.Log("hit charger");
            if (GetComponent<RoombaManager>().dust_collected > 0)
            {
                other.gameObject.GetComponent<chargingStation>().updateText(GetComponent<RoombaManager>().dust_collected);
                GetComponent<RoombaManager>().dust_collected = 0;
            }
        }
        if (other.gameObject.tag == "Dust")
        {
            Debug.Log("dust collected");
            GetComponent<RoombaManager>().dust_collected++;
            Destroy(other.gameObject);
        }
    }
}

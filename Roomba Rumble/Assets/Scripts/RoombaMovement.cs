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
    public KeyCode dashKey;
    public int dust_per_dash;
    public float dash_cooldown;
    public bool dash_ready = false;
    public bool dash_cd_active = false;
    public float dash_force;
    public float dash_control_freeze_time;


    public float moveSpeed;
    public float rotationSpeed;

    public Rigidbody rb;

    // public GameObject knife;
    // public GameObject target;
    // public GameObject enemyBalloon;
    public GameObject gameManager;
    public GameObject balloon;
    public GameObject textobj;

    // private GameObject balloon;

    public GameManagerScript gameManagerScript;

    public bool collidingWithBalloon;
    public bool hasBalloon;
    public RoombaManager r_man;

    public AudioSource audiosource;

    public float slowDownPerDust;
    public float angularSlowDownPerDust;
    private bool currently_dashing;

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
        if (r_man.alive && !currently_dashing)
        {
            if (Input.GetKey(upKey))
            {
                audiosource.Play();
                rb.AddForce(transform.forward * -(moveSpeed-slowDownPerDust*r_man.dust_collected));
            }
            else if (Input.GetKey(downKey))
            {
                audiosource.Play();
                rb.AddForce(transform.forward * (moveSpeed-slowDownPerDust*r_man.dust_collected));
            }
            else if (Input.GetKey(rightKey))
            {
                if (audiosource.isPlaying)
                {
                    audiosource.Stop();
                }
                //changing y rotation
                transform.Rotate(0, (rotationSpeed-angularSlowDownPerDust*r_man.dust_collected) * Time.deltaTime, 0);
            }
            else if (Input.GetKey(leftKey))
            {
                if (audiosource.isPlaying)
                {
                    audiosource.Stop();
                }
                transform.Rotate(0, -(rotationSpeed-angularSlowDownPerDust*r_man.dust_collected) * Time.deltaTime, 0);
            } else {
                if (audiosource.isPlaying)
                {
                    audiosource.Stop();
                }
            }
            transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
        }
    }

    void Update () {
        if (!isLocalPlayer) {
            return;
        }
        dash_ready = r_man.dust_collected >= dust_per_dash && !dash_cd_active;
        if (dash_ready) {
            if (Input.GetKeyDown(dashKey)) {
                Dash();
            }
            r_man.playerIndicator.GetComponent<MeshRenderer>().material = gameManagerScript.indicatorMats[0];
        } else {
            r_man.playerIndicator.GetComponent<MeshRenderer>().material = gameManagerScript.indicatorMats[1];
        }
    }

    private void Dash () {
        rb.velocity = Vector3.zero;
        r_man.CmdChangeDustCount(r_man.dust_collected-dust_per_dash);
        dash_cd_active = true;
        currently_dashing = true;
        rb.AddForce(transform.forward * -dash_force);
        StartCoroutine("DashControlFreeze");
        StartCoroutine("DashCooldown");
    }

    public IEnumerator DashControlFreeze()
    {
        if (!currently_dashing)
        {
            yield return null;
        }
        yield return new WaitForSeconds(dash_control_freeze_time);
        currently_dashing = false;
    }

    public IEnumerator DashCooldown()
    {
        if (!dash_cd_active)
        {
            yield return null;
        }
        yield return new WaitForSeconds(dash_cooldown);
        dash_cd_active = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isLocalPlayer || !r_man.alive) {
            return;
        }
        if (other.gameObject.tag == "Charger")
        {
            Debug.Log("hit charger");
            if (r_man.dust_collected > 0)
            {
                int dust_transfer = r_man.dust_collected;
                r_man.dust_collected = 0;
                CmdDepositDust(other.gameObject.GetComponent<chargingStation>().team,
                    dust_transfer);
                r_man.CmdChangeDustCount(0);
            }
        }
        if (other.gameObject.tag == "Dust")
        {
            Debug.Log("dust collected");
            r_man.CmdChangeDustCount(r_man.dust_collected+1);
            r_man.CmdDestroyObject(other.gameObject);
            Destroy(other.gameObject);
        }
    }

    
    [Command]
    public void CmdDepositDust(int team,int amount) {
        switch (team) {
            case 0:
                gameManagerScript.team0score += amount;
                break;
            case 1:
                gameManagerScript.team1score += amount;
                break;
        }
    }
}

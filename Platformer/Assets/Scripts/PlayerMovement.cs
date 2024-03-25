using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private Vector3 velocity;
    private Quaternion targetRotation;
    private float forwardInput, sidewaysInput, turnInput, lookInput, jumpInput;
    private bool dashInput;

    public AdvancedEnemy enemy;
    public ParticleSystem dashAnimation;

    [SerializeField]
    private Transform currentSpawnPoint;

    bool counter;

    private void Start()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<Text>();
        UpdateStats();
        Spawn();
        counter = true;
        
    }

    // Update is called once per frame
    void Update()
    {
       
        GetInput();
        Turn();
        MoveWithPlatform();

    }

    IEnumerator count() {
        yield return new WaitForSeconds(1);
        counter = true;
    }
    private void GetInput()
    {
        if (inputSettings.FORWARD_AXIS.Length != 0)
            forwardInput = Input.GetAxis(inputSettings.FORWARD_AXIS);
        if (inputSettings.SIDEWAYS_AXIS.Length != 0)
            sidewaysInput = Input.GetAxis(inputSettings.SIDEWAYS_AXIS);
        if (inputSettings.TURN_AXIS.Length != 0)
            turnInput = Input.GetAxis(inputSettings.TURN_AXIS);      
        if (inputSettings.JUMP_AXIS.Length != 0)
            jumpInput = Input.GetAxisRaw(inputSettings.JUMP_AXIS);
        if(inputSettings.SHIFT.Length != 0)
            dashInput = Input.GetButton(inputSettings.SHIFT);
        
    }

    private void Run()
    {
        velocity.z = forwardInput * moveSettings.runVelocity;
        velocity.y = playerRigidbody.velocity.y;
        velocity.x = sidewaysInput * moveSettings.runVelocity;

        playerRigidbody.velocity = transform.TransformDirection(velocity);
    }

    private void Turn()
    {
        if (turnInput != 0)
        {
            targetRotation *= Quaternion.AngleAxis(moveSettings.rotateVelocity * turnInput* 5 * Time.deltaTime, Vector3.up);
        }
        transform.rotation = targetRotation;
    }


    private void Jump()
    {
        if (jumpInput != 0 && Grounded())
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x,moveSettings.jumpVelocity, playerRigidbody.velocity.z);
        }
    }
    
    
    private void FixedUpdate()
    {
        Run();
        Jump();
        Dash();
    }
    private bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, moveSettings.distanceToGround, moveSettings.ground);
    }


    private void Awake()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        velocity = Vector3.zero;
        forwardInput = sidewaysInput = turnInput = jumpInput = 0;
        targetRotation = transform.rotation;
    }

    public void Spawn()
    {
        UpdateStats();
        transform.position = currentSpawnPoint.position;

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag ==
        "DeathZone" || other.gameObject.tag == "Enemy")
        {
            if (counter)
            {
                lives--;
                counter = false;
                if(lives <= 0)
                {
                    SceneManager.LoadScene(3);
                }
            }
            StartCoroutine(count());
            Spawn();
        }
        if(other.gameObject.tag == "SpawnPoint")
        {
            currentSpawnPoint.position = other.transform.position;
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Coin")
        {
            if (counter)
            {
                coins++;
                counter = false;
            }
            StartCoroutine(count());
            Destroy(other.gameObject);
            UpdateStats();
        }
        if(other.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene(2);
        }
    }

    [System.Serializable]
    public class MoveSettings 
    {
        public float runVelocity = 12f;
        public float rotateVelocity = 100f;    
        public float jumpVelocity = 8f;
        public float distanceToGround = 1.3f;
        public LayerMask ground;
        public float dashSpeed = 5f;
    }

    [System.Serializable]
    public class InputSettings
    {
        public string FORWARD_AXIS = "Vertical";
        public string SIDEWAYS_AXIS = "Horizontal";
        public string TURN_AXIS = "Mouse X";
        public string JUMP_AXIS = "Jump";
        public string SHIFT = "Fire3";
    }

    public MoveSettings moveSettings;
    public InputSettings inputSettings;

   
    public LayerMask MovingPlatformLayer;
    public void MoveWithPlatform()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(transform.position,Vector3.down,out hitInfo, moveSettings.distanceToGround, MovingPlatformLayer))
        {
            if(jumpInput != 0 || forwardInput != 0 || sidewaysInput != 0)
            {
                transform.SetParent(null);
            }
            else
            {
                transform.SetParent(hitInfo.transform, true);
            }
        }
        else
        {
            transform.SetParent(null);
        }
    }


    public static int coins = 0;
    public static int lives = 8;
    public static Text playerStats;

    public static void UpdateStats()
    {
        playerStats.text = "Coins: " + coins.ToString()
        + "\nLives: " + lives.ToString();
    }


    float dashCooldown = 1f;

    float DashTime = 0.1f;

    private void Dash()
    {

        dashCooldown = dashCooldown - Time.deltaTime;
        if (dashCooldown <= 0)
        {
            DashTime = 0.1f;
            

        }

        if (dashInput)
        {

            if (DashTime > 0)
            {
                velocity.z = forwardInput * moveSettings.runVelocity * moveSettings.dashSpeed;
                velocity.x = sidewaysInput * moveSettings.runVelocity * moveSettings.dashSpeed;
                playerRigidbody.velocity = transform.TransformDirection(velocity);
                DashTime -= Time.deltaTime;
                dashCooldown = 1f;
                //Instantiate(dashAnimation,transform.position,transform.rotation);
                

            }
        }
            
           
       
    }
}


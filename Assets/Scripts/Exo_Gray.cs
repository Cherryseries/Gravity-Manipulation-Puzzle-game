using System;
using Unity.Mathematics;
using UnityEngine;

public class Exo_Gray : MonoBehaviour
{

    public static Exo_Gray instance; 
    
    [SerializeField] private float PlayerSpeed = 2f;
    [SerializeField] private float PlayerJumpSpeed = 3f;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;
    [SerializeField] private GameObject playerVisual;

    public GameObject Panel;

    private new Rigidbody rigidbody;
    private new ConstantForce constantForce;
    private int destroyCount;
    private float fallingtime;
    private bool isRunning;
    private bool isFalling;
    private bool playerIsIn = true;
    
    private void Awake()
    {
        instance = this;
        rigidbody = GetComponent<Rigidbody>();
        constantForce = GetComponent<ConstantForce>();
        inputHandler.OnJumpEvent += InputHandler_OnJumpEvent;
        inputHandler.OnManipulateEvent += InputHandler_OnManipulateEvent;
    }
    private void Start()
    {
        playerIsIn = true;
    }
    // manipulation of the player
    private void InputHandler_OnManipulateEvent(object sender, EventArgs e)
    {
        transform.rotation = Exo_Hologram.instance.transform.rotation;
        Exo_Hologram.instance.HologramHide();
        if (transform.rotation == Quaternion.Euler(0, 0, 90))
        {
            constantForce.force = new Vector3 (10,0, 0);
        }
        if(transform.rotation == Quaternion.Euler(0,0,180)|| transform.rotation == Quaternion.Euler(0,180, 180)) 
        {
            constantForce.force = new Vector3(0,10,0);
        }
        if (transform.rotation == Quaternion.Euler(0, 0, 270))
        {
            constantForce.force = new Vector3(-10,0,0);
        }
        if (transform.rotation == Quaternion.Euler(90, 0,0))
        {
            constantForce.force = new Vector3(0,0,-10);
        }
        if (transform.rotation == Quaternion.Euler(270 , 0, 0))
        {
            constantForce.force = new Vector3(0, 0, 10);
        }
        if (transform.rotation == Quaternion.Euler(0, 0, 0))
        {
            constantForce.force = new Vector3(0,-10,0);
        }
    }
    // adding and modifying the force force to jump
    private void InputHandler_OnJumpEvent(object sender, EventArgs e)
    {
        if (IsGrounded())
        {
            Vector2 inputVector = inputHandler.GetPlayerMomentNormalized();
            inputVector = inputVector.normalized;
            Vector3 movDir = new Vector3(0f, 0f, 0f);
            if (transform.rotation == Quaternion.Euler(0, 0, 90))
            {
                rigidbody.AddForce(new Vector3(-PlayerJumpSpeed,0, 0), ForceMode.Impulse);
            }
            if (transform.rotation == quaternion.identity || transform.rotation == Quaternion.Euler(0, 0, 180))
            {
                rigidbody.AddForce(new Vector3(0, PlayerJumpSpeed, 0), ForceMode.Impulse);
            }
            if (transform.rotation == Quaternion.Euler(180, 0, 0))
            {
                rigidbody.AddForce(new Vector3(0, -PlayerJumpSpeed, 0), ForceMode.Impulse);
            }
            //transform.rotation == Quaternion.Euler(270, 0, 0) is not necessary
            if (transform.rotation == Quaternion.Euler(90, 0, 0))
            {
                rigidbody.AddForce(new Vector3(0,0, PlayerJumpSpeed), ForceMode.Impulse);
            }
            if (transform.rotation == Quaternion.Euler(270, 0, 0))
            {
                rigidbody.AddForce(new Vector3(0,0,-PlayerJumpSpeed), ForceMode.Impulse);
            }
        }
    }

    void Update()
    {
        HandlePlayerMoments();
        if (playerIsIn)          // Check wheather the player is with in the boundary
        {
            if (IsGrounded())
            {
                isFalling = false;
                fallingtime = 0; // reset falling time into 0
            }
            if (!IsGrounded())
            {
                isFalling = true;
                fallingtime += Time.deltaTime; //Increase the falling time
                if (fallingtime > 2f && !IsGrounded())
                {
                    playerIsIn = false;
                    Panel.SetActive(true);
                    UI.instance.GameOverTxt("GAME OVER");
                    CheckCollected();
                }
            }
        }
        
    }
  
    private void OnTriggerEnter(Collider other)
    {    // using tag to detect the cube
        if (other.gameObject.CompareTag("Cube")) 
        {
            Destroy(other.gameObject);
            destroyCount++;
            if (destroyCount >= 5) // Count  the destroyed Cubes
            {
                UI.instance.PlayerStatusTxt("YOU WON");
                Panel.SetActive(true);
            }
        }
    }
    // Check number of cubes are destroyed  is less than the total count
    public void CheckCollected() 
    {
        if(destroyCount < 5) 
        {
            UI.instance.PlayerStatusTxt("YOU LOOSE");
            Panel.SetActive(true);
        }
    }
    private bool IsGrounded() 
    {
        return Physics.CheckSphere(groundCheck.position,.1f,ground);  // Check whether player is grounded
    }
    private void HandlePlayerMoments() //Handle the PlayerMoment
    {
        Vector2 inputVector = inputHandler.GetPlayerMomentNormalized();
        inputVector = inputVector.normalized;
        Vector3 movDir = new Vector3(0f, 0f,0f);
        if (transform.rotation == Quaternion.Euler(0, 0, 90))
        {
            movDir = new Vector3(0f,inputVector.x, inputVector.y);
        }
        if(transform.rotation == quaternion.identity || transform.rotation == Quaternion.Euler(0, 0, 180)) 
        {
            movDir = new Vector3(inputVector.x, 0f, inputVector.y);
        }
        if(transform.rotation == Quaternion.Euler(180, 0, 0))
        {
            movDir = new Vector3(-inputVector.x, 0f, -inputVector.y);
        }
        //transform.rotation == Quaternion.Euler(270, 0, 0) is not necessary
        if (transform.rotation == Quaternion.Euler(90,0,0))
        {
            movDir = new Vector3(inputVector.x, -inputVector.y,0);
        }
        if (transform.rotation == Quaternion.Euler(270, 0, 0))
        {
            movDir = new Vector3(inputVector.x,inputVector.y, 0);
        }
        if (movDir != Vector3.zero) 
        {
            isRunning = true;
        }
        else 
        {
            isRunning = false;
        }
        transform.position += movDir * PlayerSpeed * Time.deltaTime;

    }

    public bool IsRunning()
    {
        return isRunning;
    }
    
    public bool IsFalling()
    {
        return isFalling;
    }   
}

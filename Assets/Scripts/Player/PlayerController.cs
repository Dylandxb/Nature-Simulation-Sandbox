using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CamView camViewer;

    public enum CamView
    {
        Regular,
        NPCView,
        BinocularView,
    }

    [Header("Player Attributes")]
    //[Player Data]
    private Vector3 playerSlidingVel;
    [SerializeField] private GameObject headPos;
    [SerializeField] public float maximumSpeed = 4.0f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float gravity;
    [SerializeField] public float jumpSpeed = 5.0f;
    [SerializeField] public float jumpHeight;
    [SerializeField] private float gravMultiplier;
    [SerializeField] private float ySpeed;
    [SerializeField] private float rotationSpeed = 650.0f;
    [SerializeField] private float jumpPeriod = 0.4f;
    [SerializeField] private float jumpCooldown = 2.0f;
    //Speed at which player rotates to look at transform
    private float lookAtSpeed = 1.0f;
    private float baseStepOffset;
    private float?  prevGroundTime;
    private float?  nextGroundTime;
    private bool isJumping;
    private bool playerSliding;
    private bool canMove;
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canStand;
    [SerializeField] public bool canJump = false;
    private bool isCrouching = false;
    //[Components]
    private CharacterController crController;                 //Has automatic stepping
    public Transform npcTarget;
    private Animator anim;
    [SerializeField] private Transform camTransform;
    [SerializeField] private GameObject regularCam;
    [SerializeField] private GameObject npcCam;
    [SerializeField] private GameObject binocularCam;
    [SerializeField] private GameObject waypointMarker;
    [SerializeField] private CollectableData binoculars;
    [Header("Key Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    //[Singleton]
    public static PlayerController instance { get; set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

    }
    void Start()
    {
        anim = GetComponent<Animator>();
        crController = GetComponent<CharacterController>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        isGrounded = crController.isGrounded;
        baseStepOffset = crController.stepOffset;
        SwitchCamView(CamView.Regular);
        canJump = true;
        canMove = true;
        canSprint = true;
        EventManager.instance.EnterDialogue += FreezeInDialogue;
        EventManager.instance.StartSleep += FreezePlayer;
        EventManager.instance.EndSleep += UnFreezePlayer;
    }
    private void OnDisable()
    {
        EventManager.instance.EnterDialogue -= FreezeInDialogue;
        EventManager.instance.StartSleep -= FreezePlayer;
        EventManager.instance.EndSleep -= UnFreezePlayer;
    }
    void Update()
    {
        if(canMove == true)
        {
            Movement();
            PlayerStats.instance.RangeSpeed();
            PlayerStats.instance.RangeJumpHeight();
            if (Input.GetKey(KeyCode.P))
            {
                PlayerStats.instance.ResetPlayerPos();
            }
            Mathf.Lerp(maximumSpeed, 0, Time.deltaTime * 5.0f);
        }
        Interact();
        // Bored();
        UseBinoculars();
       if(transform.position.y > 6.0f)
       {
            PlayerStats.instance.DecreaseStamina();
       }
       //Stamina level affects sprint ability
       if (PlayerStats.instance.currentStaminaValue < 50.0f)
       {
           canSprint = false;
       }
       else
       {
           canSprint = true;
       }
       if (PlayerStats.instance.currentStrengthValue == 0)
       {
            canJump = false;
       }
       else
       {
            canJump = true;
       }

    }

    private void Movement()
    {
        anim.SetBool("isBored", false);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movementDir = new Vector3(horizontal, 0, vertical);
        float inputMagnitude = Mathf.Clamp01(movementDir.magnitude);

        if (Input.GetKey(sprintKey) && isCrouching == false && canSprint == true)                            //Increase input magnitude to run, disable sprint whilst crouching
        {
            inputMagnitude *= 2;
            PlayerStats.instance.LerpHunger();
            canSprint = true;
        }
        else
        {
            inputMagnitude /= 2;
            //canSprint = false;
        }

        anim.SetFloat("SpeedInput", inputMagnitude, 0.08f, Time.deltaTime);
        movementDir = Quaternion.AngleAxis(camTransform.eulerAngles.y, Vector3.up) * movementDir;
        float speed = inputMagnitude * maximumSpeed;
        movementDir.Normalize();

        gravity = Physics.gravity.y * gravMultiplier;

        //Increases gravitational pull on player while in jump mode, if space is pressed then jump is lower height
        if(isJumping && ySpeed > 0 && Input.GetKey(KeyCode.Space) == false)
        {
            gravity *= 4;
        }

        ySpeed += gravity * Time.deltaTime;

        isGrounded = crController.isGrounded;
        SetSlideVel();

        if (playerSlidingVel == Vector3.zero)
        {
            playerSliding = false;
        }


        if (isGrounded)
        {
            //Time since game start
            prevGroundTime = Time.time;
            //Check for jump in blend tree and play anim
        }
        if (Input.GetKeyDown(jumpKey) == true && canJump == true)
        {
            PlayerStats.instance.DecreaseStrength();
            nextGroundTime = Time.time;
            canJump = true;
        }

        //Checks if player has been grounded recently
        if (Time.time - prevGroundTime  <= jumpPeriod)
        {

            //Detects if the player is jumped on a slope and its vector is not 0
            if (playerSlidingVel != Vector3.zero)
            {
                playerSliding = true;
            }
            //Only increase the speed if player isnt sliding
            if (playerSliding == false)
            {
                ySpeed = -0.25f;
            }
            crController.stepOffset = baseStepOffset;
            
            anim.SetBool("isGrounded", true);
            isGrounded = true;
            anim.SetBool("isJumping", false); ;
            isJumping = false;
            anim.SetBool("isFalling", false);
            //Means player can jump even in crouch
            //canJump = true;

            //Checks if space bar has been pressed recently, check if player is allowed to jump
            //Add if statement to check for cooldown
            if (Time.time - nextGroundTime <= jumpPeriod && playerSliding == false && Time.time >= jumpCooldown /*&& canJump == true*/)
            {
                ySpeed = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
                anim.SetBool("isJumping", true);
                isJumping = true;
                isGrounded = false;
                isGrounded = false;
                prevGroundTime = null;
                nextGroundTime = null;
                jumpCooldown = 2.0f;
                canJump = true;
               // Debug.Log("can jump" + canJump);

            }

        }
        else
        {
            crController.stepOffset = 0;
            anim.SetBool("isGrounded", false);
            isGrounded = false;

            if ((isJumping && ySpeed < 0) || ySpeed < -2)
            {
                anim.SetBool("isFalling", true);
            }
        }
        //Moves the player down the slope by Y speed when its sliding
        //Sets a new vector

        if (playerSliding == true)
        {
            Vector3 vel = playerSlidingVel;
            vel.y = ySpeed;
            crController.Move(vel * Time.deltaTime);
        }

        if (camViewer == CamView.Regular || camViewer == CamView.NPCView || camViewer == CamView.BinocularView)
        {
            Vector3 playerVelocity = movementDir * speed;
            playerVelocity.y = ySpeed;
            //Move needs deltatime so char moves at same speed frame rate
            crController.Move(playerVelocity * Time.deltaTime);
            if (movementDir != Vector3.zero)
            {
                

                //if is moving then rotate direction its moving
                Quaternion faceRotation = Quaternion.LookRotation(movementDir, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, faceRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                //anim.SetBool("isMoving", false);
            }
        }

        Crouching();

    }

    public void SwitchCamView(CamView newView)
    {
        regularCam.SetActive(false);
        npcCam.SetActive(false);
        binocularCam.SetActive(false);

        if (newView == CamView.Regular)
        {
            regularCam.SetActive(true);
            waypointMarker.SetActive(true);
            binocularCam.SetActive(false);
        }
        if (newView == CamView.NPCView)
        {
            waypointMarker.SetActive(false);
            npcCam.SetActive(true);
        }
        if(newView == CamView.BinocularView)
        {
            waypointMarker.SetActive(false);
            binocularCam.SetActive(true);
        }

        camViewer = newView;
    }

    private void RotateCamera()
    {

    }
    private void SetSlideVel()
    {
        //Raycast distance from player to ground, with  max distance of 5
        //Check for a hit on the ground
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit info, 10))
        {
            //Calculate the angle between the hit and the slope, upwards
            float angle = Vector3.Angle(info.normal, Vector3.up);

            if (angle >= crController.slopeLimit)
            {
                //Vector poinnting down is the vertical speed of character
                //Moves it in direction of ground
                playerSlidingVel = Vector3.ProjectOnPlane(new Vector3(0, ySpeed ,0 ), info.normal);
                return;
            }
          
        }
        if (playerSliding)
        {
            //Gradually decreases player sliding velocity vector to 0
            //Checks if the magnitude of the vector is larger then 1 then return its still sliding
            playerSlidingVel -= playerSlidingVel * Time.deltaTime * 2;
            if (playerSlidingVel.magnitude > 1)
            {
                //Sets velocity back to 0
                return;
            }
            //else the velocity is set to 0
        }
        playerSlidingVel = Vector3.zero;
    }

    private void Crouching()
    {
        if (Physics.Raycast(headPos.transform.position, Vector3.up, 0.5f))
        {
            canStand = false;
            Debug.DrawRay(headPos.transform.position, Vector3.up, Color.green);
            //Do same function as this to draw the ray
            //private void OnDrawGizmos()
            //{
            //    // Cast a ray from the center of the screen
            //    Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            //    // Draw a gizmo to represent the ray
            //    Gizmos.color = gizmoColor;
            //    Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance);
            //}
        }
        else
        {
            canStand = true;
        }

        if(Input.GetKeyDown(crouchKey))
        {
            isCrouching = !isCrouching;
            if (!isCrouching && canStand == true)
            {
                anim.SetBool("isCrouching", false);
                crController.height = 1.8f;
                crController.center = new Vector3(0, 0.92f, 0);
                isGrounded = true;
                canJump = true;
                maximumSpeed = 2.0f;
            }
            
            if (isCrouching)
            {
                anim.SetBool("isCrouching", true);
                crController.height = 1.05f;
                crController.center = new Vector3(0, 0.55f, 0);
                maximumSpeed = 1.0f;
                anim.SetBool("isGrounded", true);
                isGrounded = true;
                anim.SetBool("isJumping", false); ;
                isJumping = false;
                canJump = false;
                anim.SetBool("isFalling", false);

            }

        }
    }
    private void Bored()
    {
        StartCoroutine(TimeForBored());

    }
    void Interact()
    {
        //Check for key press
        //Check if player can interact with NPC UI
        if(Input.GetKeyDown(interactKey) && InteractPrompt.cantInteract == false)
        {
            //Maximum range from player to collider to interact with
            float interactRange = 2.0f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            //For each collider
            foreach(Collider collider in colliderArray)
            {
                //Interact with all Interactables with key press E
                if (collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact();
                    interactable.DisplayUI();
                }
            }
        }
    }

    private void FreezeInDialogue()
    {
        StartCoroutine(PlayerInDialogue());
        StartCoroutine(LookAtNPC());
    }
    
    public void FreezePlayer()
    {
        anim.SetBool("isMoving", false);
        anim.SetBool("isBored", true);
        //Sleeping anim??
        canMove = false;
    }
    public void UnFreezePlayer()
    {
        //Re-enables player movement
        anim.SetBool("isBored", false);
        //anim.SetBool("isMoving", true);
        canMove = true;
    }
    IEnumerator PlayerInDialogue()
    {
        //Trigger dialogue animation -> Talking when facing player
        //Call with interact npc event
        canMove = false;
        anim.SetBool("isDialogue", true);
        yield return new WaitForSeconds(6.0f);
        //Shows two cases of debug because npc has 2 colliders attached, by removing one it works correct
        anim.SetBool("isDialogue", false);
        UnFreezePlayer();
        yield return null;
    }

    private IEnumerator LookAtNPC()
    {
        //Current rotation of player
        Quaternion currentRotate = transform.rotation;
        //Direction vector between the position of player to npc
        Quaternion lookRotate = Quaternion.LookRotation(npcTarget.position - transform.position);
        float timeToTurn = 0;
        //1 second to turn smoothly
        while (timeToTurn < 1.0f)
        {
            timeToTurn += Time.deltaTime * lookAtSpeed;
            //Lerp towards the vector between NPC and player
            transform.rotation = Quaternion.Lerp(currentRotate, lookRotate, timeToTurn);
            yield return null;
        }
        
    }


    IEnumerator TimeForBored()
    {
        anim.SetBool("isBored", true);
        Debug.Log("is bored");
        yield return new WaitForSeconds(6.0f);
        anim.SetBool("isBored", false);
        Debug.Log("isnt bored");

    }

    private void UseBinoculars()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Inventory.instance.HasItem(binoculars))
            {
                //Change FOV
                //Flip to first person VIEW
                //Look at a POI
                //Find a target with its tag and look towards the POI with this cam view
                SwitchCamView(CamView.BinocularView);
                FreezePlayer();
            }

        }
        //If player has binoculars and X is pressed, it takes them out of the view
        if (Input.GetKeyDown(KeyCode.X) && Inventory.instance.HasItem(binoculars))
        {
            SwitchCamView(CamView.Regular);
            UnFreezePlayer();
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trail"))
        {
            Debug.Log("Colliding");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheMove : MonoBehaviour
{
    LjusStamina staminaScript;

    /*public float Stamina = 100;
    public float MaxStamina = 100f;
    public bool staminaD = false;*/

    [Header("Movement")]
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float dashSpeed;

    public float groundDrag;

    //  [SerializeField] private TrailRenderer tr;

    private bool ActivateAnimation = false;
    private bool isMovingForward = false;
    private bool isMovingBackwards = false;

    public Transform cam;
    public Transform playerObj;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

   
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;



    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Crouching")]
    public float crouchSpeed;
    /* public float crouchYScale;
     private float startYScale;*/

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [SerializeField]
    KeyCode Forward;

    [SerializeField]
    KeyCode Left;

    [SerializeField]
    KeyCode Right;

    [SerializeField]
    KeyCode Backward;

    [SerializeField]
    KeyCode Kick;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    bool nogravity;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    public Vector3 velocity;

    public bool isGrounded;

    public float jumpForce;
    public float counter1Force;
    public float counter2Force;
    public float jumpForceDown;

    [SerializeField]
    private float jumpTimeCounter;
    [SerializeField]
    public float jumpTime = 0.3f;

    private bool isJumping;

    private float coyoteTimeCounter;

    public float coyoteTime;

    private float jumpBufferTimeCounter;

    public float jumpBufferTime;

    public float jumpHeight = 10f;

    public float jumpTimeLimit = 0.35f; // Time limit for variable jump

    public float maxJumpTime = 0.35f; // Max jump duration

    private bool jumpButtonHeld = false;

    public float wallrunSpeed;

    private bool dasher = true;
    private float dashingPower = 60f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 5f;

    public Transform groundCheck;

    public float groundDistance = 0.4f;

    public LayerMask groundMask;

     Animator m_Animator;
     public Animator animator;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        wallrunning,
        climbing,
        dashing,
        air,
        crouching,
    }

    public bool dashing;

    public bool wallrunning;

    private void Start()
    {
        staminaScript = GetComponentInParent<LjusStamina>();
        rb = GetComponent<Rigidbody>();
        // Tr = GetComponent<TrailRenderer>();
        rb.freezeRotation = true;
        m_Animator = GetComponentInChildren<Animator>();
        readyToJump = true;
        // tr.emitting = false;
       // m_Animator.SetBool("IsJumping", false);
        isJumping = false;
       // m_Animator.SetBool("IsFalling", false);
        // startYScale = transform.localScale.y;

    }

    private void Update()
    {

       /* if (Input.GetKey(jumpKey) && )
        {
            staminaD = true;

            print("Working");
        }
        if (staminaD == true)
        {
            Stamina = Stamina -= 1 * Time.deltaTime * 5;
        }

        if (Stamina < 1f)
        {
            staminaD = false;
        }*/


        // ground check
         grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        // isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

       // grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
       // nogravity = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
        {
           // m_Animator.SetBool("IsGrounded", true);
            isGrounded = true;


            rb.drag = groundDrag;
          //  m_Animator.SetBool("IsJumping", false);
            isJumping = false;
           // m_Animator.SetBool("IsFalling", false);

           // print("Grounded");
        }

        else
        {
            rb.drag = 0;

          //  m_Animator.SetBool("IsGrounded", false);
            isGrounded = false;


            if ((isJumping && rb.velocity.y < 2) || rb.velocity.y < -2)
            {
               // m_Animator.SetBool("IsFalling", true);
                // m_Animator.SetBool("IsIdle", false);

            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dasher && !isGrounded)
        {
            StartCoroutine(Dash());
        }

        // Jump();


        /* if (grounded)
         {
             coyoteTimeCounter = coyoteTime;
         }
         else
         {
             coyoteTimeCounter -= Time.deltaTime;
         }
         if (Input.GetButtonDown("Jump"))
         {
             jumpBufferTimeCounter = jumpBufferTime;
         }
         else
         {
             jumpBufferTimeCounter -= Time.deltaTime;
         }


         if (coyoteTimeCounter > 0f && jumpBufferTimeCounter > 0f) // Den h�r kollar om spelaren �r p� marken om den �r s� ska man kunna man kunna trycka p� space f�r att hoppa.
         {
             print("ground jump");
             isJumping = true;
             isGrounded = false;
             jumpTimeCounter = jumpTime;
             // rb.velocity = Vector2.up * jumpForce;
             rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
             jumpBufferTimeCounter = 0f;
             //m_Animator.SetBool("IsJumping", true);
         }

         else if (Input.GetButton("Jump") && isJumping == true) /* Den h�r koden ser till s� att n�r spelaren trycker p� space och inte h�ller ner space
                                                                s� blir det ett kortare hopp och den ser ocks� till s� att det inte funkar i luften.*/
        /*  {


              if (jumpTimeCounter > 0)
              {
                  print("continue jump");

                  //  rb.velocity = Vector2.up * jumpForce;
                  rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                  jumpTimeCounter -= Time.deltaTime;
                  // m_Animator.SetBool("IsJumping", true);
              }
              else
              {
                  print("nej jump");
                  isJumping = false;
                  isGrounded = true;
                 // rb.AddForce(-transform.up * jumpForceDown, ForceMode.Impulse);
                  // m_Animator.SetBool("IsJumping", false);
              }

          }
          else
          {
              isJumping = false;
              // m_Animator.SetBool("IsJumping", false);
          }
          if (Input.GetButtonUp("Jump"))
          {
              coyoteTimeCounter = 0f;
          }*/

        // Refined version of your jumping logic




        /*  if (grounded == true && Input.GetButtonDown("Jump")) // Den h�r kollar om spelaren �r p� marken om den �r s� ska man kunna man kunna trycka p� space f�r att hoppa.
          {
              print("ground jump");
              //isJumping = true;
              //jumpTimeCounter = jumpTime;
              rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
              exitingSlope = true;


              // m_Animator.SetBool("IsJumping", true);
              isJumping = true;


              // rb.AddForce(velocity = Vector3.up * jumpHeight);
              // rb.AddForce(transform.up * jumpHeight);
          }
          else if (grounded == false)
          {
              // m_Animator.SetBool("IsFalling", true);

          }
        */


        if (grounded == true && Input.GetButtonDown("Jump")) // Den h�r kollar om spelaren �r p� marken om den �r s� ska man kunna man kunna trycka p� space f�r att hoppa.
        {
            print("ground jump");
            isJumping = true;
            jumpTimeCounter = jumpTime;
             // velocity = Vector3.up * jumpHeight;
           rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);

        }

      /*  if (Input.GetButton("Jump") && isJumping == true) /* Den h�r koden ser till s� att n�r spelaren trycker p� space och inte h�ller ner space
                                                               s� blir det ett kortare hopp och den ser ocks� till s� att det inte funkar i luften.*/
      /*  {


            if (jumpTimeCounter > 0)
            {
                print("continue jump");
                //  velocity = Vector3.up * jumpHeight;
                rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                print("nej jump");
                isJumping = false;
            }

        }*/
        else if (Input.GetButtonUp("Jump") && !grounded && isJumping == true)
        {
            jumpHeight = 0f;
        }
        else
        {
            isJumping = false;
            jumpHeight = 10f;
        }

        // Fly thingaruni
       
        
            if (grounded == false && isJumping == false && Input.GetKeyDown(jumpKey) && staminaScript.staminaD == true)
            {

                // Physics.gravity = new Vector3(0f, 0f, 0f);
                print("Nogravity");
                // velocity = new Vector3(transform.forward.x * jumpForceDown, 0f, transform.forward.z * jumpForceDown);
                //  Dash();
                jumpHeight = 0f;
                isJumping = false;
                // AirMove();
                // rb.AddForce(transform.up * counter1Force, ForceMode.Impulse);
                // rb.AddForce(-transform.up * counter2Force, ForceMode.Impulse);
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                

                m_Animator.SetBool("IsFlying", true);
                // nogravity = true;
                moveSpeed = 15f;
            }

            else if ((rb.constraints & RigidbodyConstraints.FreezePositionY) == RigidbodyConstraints.FreezePositionY && Input.GetKeyUp(jumpKey))
            {
                rb.constraints = ~RigidbodyConstraints.FreezePosition;
                rb.AddForce(transform.up * counter1Force, ForceMode.Impulse);
                moveSpeed = 8f;
                m_Animator.SetBool("IsFlying", false);
            }
            else if (grounded == true)
            {
                moveSpeed = 8f;
                m_Animator.SetBool("IsFlying", false);
                
            }
                
            if (grounded == false && isJumping == false && staminaScript.staminaD == false)
            {
                m_Animator.SetBool("IsFlying", false);
            }



        if (staminaScript.staminaD == false)
        {
            rb.constraints = ~RigidbodyConstraints.FreezePosition;
        }
          





    }



    private void FixedUpdate()
    {
        MovePlayer();

       

        if (!grounded && !Input.GetButton("Jump"))
        {
            rb.AddForce(-transform.up * jumpForceDown);
            exitingSlope = false;
            //  m_Animator.SetBool("IsJumping", false);
            //  m_Animator.SetBool("IsFalling", true);
            isJumping = false;

        }
   


        rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);




        /*  else if (!grounded && Input.GetKeyDown(jumpKey))
          {
             jumpForceDown = 0f;
          }
          else
          {
             jumpForceDown = 20f;
          }

         /*  // Reset jump variables when the player is grounded
           if (grounded)
           {
               isJumping = false;
               jumpTimeCounter = 0f; // Reset jump timer only after player lands
           }

         /* if (isJumping && jumpTimeCounter > 0)
          {
              rb.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime, ForceMode.Impulse);
              jumpTimeCounter -= Time.fixedDeltaTime; // Decrease the jump time counter
          }
          else if (!grounded && !Input.GetButton("Jump")) // If in the air and not holding the button
          {
              rb.AddForce(Vector3.down * jumpForceDown, ForceMode.Acceleration); // Apply downward force
              isJumping = false;
          }*/


    }


    private void StartJump()
    {
        print("ground jump");
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        isJumping = true;
        jumpTimeCounter = maxJumpTime;
        grounded = false; // Player has left the ground
        jumpForce = 10f;
    }

    private void HoldJump()
    {
        if (jumpTimeCounter > 0)
        {
            rb.AddForce(transform.up * jumpForce * Time.deltaTime, ForceMode.VelocityChange);
            jumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            EndJump();
        }
    }

    private void EndJump()
    {
        isJumping = false;
       
    }

    private void ApplyGravity()
    {
        rb.AddForce(-transform.up * jumpForceDown);
       
    }

    // Optional: If you want to re-enable jumping when player touches the ground
   /* private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WhatIsGround"))
        {
            grounded = true;
            isJumping = false; // Reset jump state
        }
    }*/



    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


        // m_Animator.transform.localPosition = Vector3.zero;
        //m_Animator.transform.localEulerAngles = Vector3.zero;
        // grounded &&

        if (Input.GetKey(Forward))
        {
            m_Animator.SetFloat("Run", moveSpeed, 0.05f, Time.deltaTime);
            if (Input.GetKey(crouchKey))
            {
               // m_Animator.SetFloat("CrouchWalk", moveSpeed);
            }
        }

        else if (Input.GetKey(Left))
        {
            m_Animator.SetFloat("Run", moveSpeed, 0.05f, Time.deltaTime);
            if (Input.GetKey(crouchKey))
            {
               // m_Animator.SetFloat("CrouchWalk", moveSpeed);
            }
        }

        else if (Input.GetKey(Right))
        {
            m_Animator.SetFloat("Run", moveSpeed, 0.05f, Time.deltaTime);
            if (Input.GetKey(crouchKey))
            {
               // m_Animator.SetFloat("CrouchWalk", moveSpeed);
            }
        }

        else if (Input.GetKey(Backward))
        {
            m_Animator.SetFloat("Run", moveSpeed, 0.05f, Time.deltaTime);
            if (Input.GetKey(crouchKey))
            {
               // m_Animator.SetFloat("CrouchWalk", moveSpeed);
            }
        }
        else
        {
            m_Animator.SetFloat("Run", 0, 0.05f, Time.deltaTime);
            // m_Animator.SetBool("IsFalling", true);
            m_Animator.SetFloat("CrouchWalk", 0);
            //  m_Animator.SetBool("IsCrouching", false);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //m_Animator.SetFloat("Sprint", moveSpeed);
            isJumping = true;
           // m_Animator.SetBool("IsJumping", true);
        }

        if (Input.GetKey(Forward))
        {
           // m_Animator.SetFloat("Sprint", moveSpeed);
        }

        else if (Input.GetKey(Left))
        {
           // m_Animator.SetFloat("Sprint", moveSpeed);
        }

        else if (Input.GetKey(Right))
        {
           // m_Animator.SetFloat("Sprint", moveSpeed);
        }

        else if (Input.GetKey(Backward))
        {
          //  m_Animator.SetFloat("Sprint", moveSpeed);
        }
        else
        {
           // m_Animator.SetFloat("Sprint", 0);
            // m_Animator.SetBool("IsFalling", true);

        }



        if (Input.GetKey(crouchKey) && grounded == true)
        {
            // rb.AddForce(moveDirection.normalized * crouchSpeed, ForceMode.Force);
            moveSpeed = crouchSpeed;
            m_Animator.SetFloat("IsCrouching", moveSpeed, 1f, Time.deltaTime);
            // m_Animator.SetFloat("CrouchWalk", moveSpeed);
            jumpHeight = 0f;
            m_Animator.SetBool("IsJumping", false);
        }
        else if (Input.GetKey(crouchKey) && grounded == false)
        {
            rb.AddForce(-transform.up * counter2Force, ForceMode.Impulse);
        }


        // stop crouch
        if (Input.GetKeyUp(crouchKey) && grounded == true)
        {
            moveSpeed = 8f;
            m_Animator.SetFloat("IsCrouching", 0);
            jumpHeight = 10f;
            // m_Animator.SetFloat("CrouchWalk", 0);

        }

        if (Input.GetKeyDown(sprintKey))
        {
            // rb.AddForce(moveDirection.normalized * crouchSpeed, ForceMode.Force);
            moveSpeed = sprintSpeed;
        }


        // stop crouch
        if (Input.GetKeyUp(sprintKey))
        {
            moveSpeed = 8f;
        }

        if (Input.GetKey(Kick))
        {
          //  m_Animator.SetBool("IsPunching", true);
        }

        else
        {
          //  m_Animator.SetBool("IsPunching", false);
        }

        // when to jump
        /*  if (Input.GetButtonDown("Jump") && readyToJump && grounded)
          {
              readyToJump = false;

              Jump();

              Invoke(nameof(ResetJump), jumpCooldown);
          }
          /* if (grounded == true && Input.GetButtonDown("Jump")) // Den h�r kollar om spelaren �r p� marken om den �r s� ska man kunna man kunna trycka p� space f�r att hoppa.
           {
               print("ground jump");
               isJumping = true;
               jumpTimeCounter = jumpTime;
               rb.velocity = Vector3.up * jumpForce;
           }

           if (Input.GetButton("Jump") && isJumping == true) /* Den h�r koden ser till s� att n�r spelaren trycker p� space och inte h�ller ner space
                                                                  s� blir det ett kortare hopp och den ser ocks� till s� att det inte funkar i luften.*/
        /* {


             if (jumpTimeCounter > 0)
             {
                 print("continue jump");
                 rb.velocity = Vector3.up * jumpForce;
                 jumpTimeCounter -= Time.deltaTime;
             }
             else
             {
                 print("nej jump");
                 isJumping = false;
             }
         }
         else
         {
             isJumping = false;
         }*/
        // start crouch
        /* if (Input.GetKeyDown(crouchKey))
         {
             transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
             rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
         }

         // stop crouch
         if (Input.GetKeyUp(crouchKey))
         {
             transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
         }*/


        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            m_Animator.SetBool("IsJumping", true);
        }
        else if (!Input.GetButtonDown("Jump") && grounded == false)
        {
            m_Animator.SetBool("IsJumping", false);
        }
        if (grounded == false)
        { 
            m_Animator.SetBool("IsFalling", true);
        }
        else if (grounded == true)
        {
            m_Animator.SetBool("IsFalling", false);
        }

    }
    private void AirMove()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded == false)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
            
    }


    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 12f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        else if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
        //    rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        //  Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        /*if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(30f, angle, 0f);


            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //(moveDir.normalized * speed * Time.deltaTime);
            // calculate movement direction
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        }
        else if (direction.magnitude >= 0f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);




        }*/

        // turn gravity off while on slope
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        // limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

   /* private void Jump()
    {
        exitingSlope = true;

        // reset y velocity
        /* if (grounded == true && Input.GetButtonDown("Jump"))
         {
             rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

             rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
         }*/



        //m_Animator.SetTrigger("Jump");

     /*   if (grounded == true && Input.GetButtonDown("Jump")) // Den h�r kollar om spelaren �r p� marken om den �r s� ska man kunna man kunna trycka p� space f�r att hoppa.
        {
            print("ground jump");
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            // rb.AddForce(velocity = Vector3.up * jumpHeight);
            // rb.AddForce(transform.up * jumpHeight);
        }

        if (Input.GetButton("Jump") && isJumping == true) /* Den h�r koden ser till s� att n�r spelaren trycker p� space och inte h�ller ner space
                                                                s� blir det ett kortare hopp och den ser ocks� till s� att det inte funkar i luften.*/
     /*   {


            if (jumpTimeCounter > 0)
            {
                print("continue jump");
                // rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

                //rb.AddForce(velocity = Vector3.up * jumpHeight);

                // velocity = Vector3.up * jumpHeight;

                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                print("nej jump");
                isJumping = false;
            }
        }
        else if (grounded == false && (Input.GetButton("Jump") == false || isJumping == false))
        {
            isJumping = false;
            //rb.AddForce(-transform.up * jumpForce, ForceMode.Impulse);
            //  rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }


    }
    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }*/
    private void StateHandler()
    {
        /* if (Input.GetKey(crouchKey))
         {
             state = MovementState.crouching;
             moveSpeed = crouchSpeed;
         }*/

        if (dasher)
        {
            state = MovementState.dashing;
            moveSpeed = dashSpeed;
        }

        // Mode - Sprinting
        else if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        // Mode - wallrunning
        else if (wallrunning)
        {
            state = MovementState.wallrunning;
            moveSpeed = wallrunSpeed;
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    private IEnumerator Dash()
    {
        dasher = true;
        // tr.emitting = true;
        // velocity = new Vector3(transform.forward.x * dashingPower, 0f, transform.forward.z * dashingPower);
        yield return new WaitForSeconds(dashingTime);
        velocity = Vector3.zero;
        // tr.emitting = false;
        yield return new WaitForSeconds(dashingCooldown);
        dasher = true;
        velocity = new Vector3(transform.forward.x * jumpForceDown, 0f, transform.forward.z * jumpForceDown);
    }
}

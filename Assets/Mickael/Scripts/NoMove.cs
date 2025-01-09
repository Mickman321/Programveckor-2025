using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMove : MonoBehaviour
{
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float sprintSpeed;

    bool isGrounded = true;
    [SerializeField]
    private float jumpForce;

    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    // Dem ovanför här är för att säga vad som är mark hur starkt hoppet ska vara och positionen på gubben som marken kommer i kontakt med.
    [SerializeField]
    private float jumpTimeCounter;
    [SerializeField]
    public float jumpTime;

    private float coyoteTimeCounter;

    public float coyoteTime;

    private float jumpBufferTimeCounter;

    public float jumpBufferTime;

    private bool isJumping;


    [SerializeField]
    KeyCode Jump;

    [SerializeField]
    KeyCode Left;

    [SerializeField]
    KeyCode Right;

    [SerializeField]
    KeyCode Backward;


    public float moveSpeed = 10f;
    public float maxSpeed = 350f;
    public float accSpeed;

    float horizontalInput;
    float verticalInput;

    Vector2 moveDirection;

    Rigidbody2D rb;

    Animator m_Animator;
    public Animator animator;
    // public Transform orientation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 6f;
       // m_Animator = GetComponentInChildren<Animator>();
      //  m_Animator.SetBool("IsJumping", false);
      //  m_Animator.SetBool("IsFalling", false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);



        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferTimeCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferTimeCounter -= Time.deltaTime;
        }


        if (coyoteTimeCounter > 0f && jumpBufferTimeCounter > 0f) // Den här kollar om spelaren är på marken om den är så ska man kunna man kunna trycka på space för att hoppa.
        {
            print("ground jump");
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            jumpBufferTimeCounter = 0f;
            //m_Animator.SetBool("IsJumping", true);
        }

        else if (Input.GetKey(KeyCode.Space) && isJumping == true) /* Den här koden ser till så att när spelaren trycker på space och inte håller ner space
                                                               så blir det ett kortare hopp och den ser också till så att det inte funkar i luften.*/
        {


            if (jumpTimeCounter > 0)
            {
                print("continue jump");

                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
                // m_Animator.SetBool("IsJumping", true);
            }
            else
            {
                print("nej jump");
                isJumping = false;
                // m_Animator.SetBool("IsJumping", false);
            }

        }
        else
        {
            isJumping = false;
            // m_Animator.SetBool("IsJumping", false);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            coyoteTimeCounter = 0f;
        }

        if (Input.GetKeyDown(sprintKey))
        {
            // rb.AddForce(moveDirection.normalized * crouchSpeed, ForceMode.Force);
            moveSpeed = sprintSpeed;
            if (moveSpeed == 20f)
            {
                print("Sprinting");
            }
        }



        else if (Input.GetKeyUp(sprintKey))
        {
            moveSpeed = 6f;
        }


        /* if (Input.GetKey(Jump))
         {
             m_Animator.SetBool("IsJumping", true);

         }*/
        else if (isGrounded == true)
        {
           // m_Animator.SetBool("IsJumping", false);
            //  m_Animator.SetFloat("Run", moveSpeed);
        }


        if (Input.GetKey(Left))
        {
            // m_Animator.SetFloat("Run", moveSpeed);
            //transform.eulerAngles = new Vector3(0, 180, 0);

        }

        else if (Input.GetKey(Right))
        {
            //  m_Animator.SetFloat("Run", moveSpeed);

        }

        else if (Input.GetKey(Backward))
        {
           // m_Animator.SetFloat("Run", moveSpeed);

        }
        else
        {
            // m_Animator.SetFloat("Run", 0);
            // m_Animator.SetBool("IsFalling", true);
            // m_Animator.SetFloat("CrouchWalk", 0);
            //  m_Animator.SetBool("IsCrouching", false);


        }
        if (isGrounded == false && isJumping == false)
        {
           // m_Animator.SetBool("IsFalling", true);
        }
        else
        {
           // m_Animator.SetBool("IsFalling", false);
        }

        if (horizontalInput != 0f && moveSpeed < maxSpeed)
        {
            moveSpeed += Time.deltaTime * accSpeed;
        }

        if (horizontalInput == 0f)
        {
            moveSpeed = 10f;
        }


    }

    void FixedUpdate()
    {
        if (horizontalInput > 0.1f || horizontalInput < -0.1f)
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }

        if (horizontalInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
          ///  m_Animator.SetFloat("Run", moveSpeed);
        }
        else if (horizontalInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
          //  m_Animator.SetFloat("Run", moveSpeed);
        }
        else
        {
           // m_Animator.SetFloat("Run", 0);
        }
        if (verticalInput > -1)
        {
          //  m_Animator.SetBool("IsJumping", true);
        }
        else if (verticalInput < -1)
        {
           // m_Animator.SetBool("IsJumping", false);
        }
        /* else
         {
             m_Animator.SetBool("IsJumping", false);
         }*/

    }
}

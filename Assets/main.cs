using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public float moveSpeed = 1000;
    public float moveInput = 0;
    public float jumpForce = 300;

    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public GroundChecker groundChecker;
    public bool isJump = false;
    private float speedMultiplier = 1f;
    private bool isRolling = false;
    private float rotationSpeed = 310f;
    private float currentRotation = 0f;
    public Animator anim;
    public bool isOpen = false;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        moveInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
        
        if (moveInput != 0)
        {
        anim.SetFloat("IsMove", 1);
        }
        else{
        anim.SetFloat("IsMove", -1);
        }

        if (moveInput < 0)
        {
        sprite.flipX = true;
        }
        else{
        sprite.flipX = false;
        }
        
        if (isJump)
        {
            anim.SetBool("IsJump", true);
        }
        else
        {
            anim.SetBool("IsJump", false);
        }

         if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speedMultiplier = 2f;
        }
        else
        {
            speedMultiplier = 1f;
        }

         if (isRolling)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, rotationStep);
            currentRotation += rotationStep;

            if (currentRotation >= 360f)
            {
                isRolling = false;
                currentRotation = 0f;
                transform.rotation = Quaternion.identity;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput *
             moveSpeed *
             speedMultiplier * 
             Time.fixedDeltaTime,
             rb.velocity.y);

        if (isJump && groundChecker.isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
            isJump = false;
            isRolling = true;
            currentRotation = 0f;
        }
    }


  
}


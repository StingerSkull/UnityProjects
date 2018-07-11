using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
     public bool jump = false;

    public float jumpTimeCounter;
    public float jumpTime;

    public float maxSpeed = 4f;
    public float jumpForce = 5f;
    public Transform groundCheck;


    public bool grounded = false;
    private Rigidbody2D rb2d;


    // Use this for initialization
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded)
        {           
            jump = true;
            jumpTimeCounter = jumpTime;
        }

        if(Input.GetButton("Jump") && jump)
        {
            if(jumpTimeCounter > 0)
            {
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                jump = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            jump = false;
        }
    }

    void FixedUpdate()
    {
        if (jump)
        {
            rb2d.velocity = Vector2.up * jumpForce;
        }

        float h = Input.GetAxis("Horizontal");

        rb2d.velocity = new Vector2(h * maxSpeed, rb2d.velocity.y);

        if (h > 0 && !facingRight)
        {
            facingRight = !facingRight;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (h < 0 && facingRight)
        {
            facingRight = !facingRight;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
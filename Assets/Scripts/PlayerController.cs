using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{


    // Variables en inspector
    public float speed = 5;
    public float jumpForce = 3;
    public Animator animator;
    public AudioSource cAudio;
   


    // Variable no inspector
    private Vector2 move;
    private bool grounded;
    private bool crouch;
    private bool hurt;
    private Rigidbody2D Rbody2D;
    private SpriteRenderer FRender;
    private CapsuleCollider2D Collider;
    private PlayerHealth Vidas;
    internal object cAnimator;

    // Start is called before the first frame update
    void Start()
    {
        Rbody2D = GetComponent<Rigidbody2D>();
        FRender = GetComponentInChildren<SpriteRenderer>();
        Collider = GetComponent<CapsuleCollider2D>();
        Vidas = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerJump();
        PlayerOrientation();
        PlayerGrounded();
        PlayerCrouch();

        animator.SetFloat("Speed", Mathf.Abs(move.x));

        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
    }

    void PlayerCrouch()
    {
        crouch = Input.GetKey(KeyCode.S);
        animator.SetBool("IsCrouching", crouch);

    }
    void PlayerJump()
    {
        bool jump = Input.GetKeyDown(KeyCode.W);

        if (jump && grounded)
        {
            Rbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Vector2.up == new Vector2(0,1)
            
        }

        if (jump)
        {
            PlayerAudioController pAudio = GetComponent<PlayerAudioController>();
            pAudio.Jump();
        }

        animator.SetBool("IsJumping", grounded);

        
    }

    void FixedUpdate()
    {
        
        if (crouch)
        {
            Rbody2D.velocity = new Vector2(0, Rbody2D.velocity.y);
        }
        else 
        {
            Rbody2D.velocity = new Vector2(move.x * speed, Rbody2D.velocity.y);
        }

        
    }
    void PlayerOrientation()
    {
        

        if (move.x < 0)
            FRender.flipX = true;
        else if (move.x > 0)
            FRender.flipX = false;
    }

    void PlayerGrounded()   
    {
        grounded = Physics2D.Raycast(transform.position, Vector2.down, 0.15f, LayerMask.GetMask("Environment"));
        //grounded = false;

        //if (hit) 
        //{
        //    grounded = true;
        //}
    }




}


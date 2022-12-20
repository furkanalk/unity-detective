using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Animator playerAnimator;

    private float horizontalInput;
    private float horizontalMove;
    private bool isGrounded;

    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private float gravityMod;

    Rigidbody2D rbPlayer;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        Physics.gravity *= gravityMod;
        isGrounded = true;
    }

    void Update()
    {
        giveSpeedtoPlayer(); // Hýz verir
        checkifPlayerisWalking(); // Yurumeye basladi mi kontrol eder
        checkifPlayerJumped(); // Zýpladý mý diye kontrol eder
        checkifPlayerCanSprint(); // Depar atabilir mi kontrol eder
        
        checkifPlayerCanSmoke(); // Cutscenelere koyulabilir?
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void checkifPlayerisWalking()
    {
        horizontalMove = Input.GetAxis("Horizontal") * walkSpeed;
        playerAnimator.SetFloat("playerSpeed", Mathf.Abs(horizontalMove));
    }

    void giveSpeedtoPlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        switch(playerAnimator.GetBool("playerSprint"))
        {
            case true:
                transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * (float)(walkSpeed * 1.5));
                break;
            case false:              
                transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * walkSpeed);
                break;
        }    
    }

    void checkifPlayerJumped()
    {
        if(!isGrounded)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            isGrounded = false;
            rbPlayer.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void checkifPlayerCanSprint()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerAnimator.SetBool("playerSprint", false);
            return;
        }
            
        if (!isGrounded)
        {
            playerAnimator.SetBool("playerSprint", false);
            return;
        }

        if (Mathf.Abs(horizontalMove) < 0.01)
        {
            playerAnimator.SetBool("playerSprint", false);
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
            playerAnimator.SetBool("playerSprint", true);
    }

    void checkifPlayerCanSmoke()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            playerAnimator.Play("Player_smoking");
        }
    }
}

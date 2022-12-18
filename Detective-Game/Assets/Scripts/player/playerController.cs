using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private float horizontalInput;
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

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        //horizontalInput *= walkSpeed * Time.deltaTime;

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * walkSpeed);

        checkifPlayerJumped();
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void checkifPlayerJumped()
    {
        if(!isGrounded)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isGrounded = false;
            rbPlayer.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }

    }
}

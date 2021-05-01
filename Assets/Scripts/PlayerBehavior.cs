using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool isJumping = false;
    private float moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        FixRotation();
        Move();
    }

    private void ProcessInputs(){
        moveDirection = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump")){
            isJumping = true;
        }
    }

    private void FixRotation(){
        if(moveDirection > 0 && !facingRight){
            FlipCharacter();
        }else if(moveDirection <0 && facingRight){
            FlipCharacter();
        }
    }

    private void Move(){
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        if(isJumping){
            rb.AddForce(new Vector2(0f, jumpForce));
        }
        isJumping = false;
    }

    private void FlipCharacter(){
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    private float moveSpeed = 25f;
    private float jumpForce = 2000f;
    Component deathCollider;
    private Rigidbody2D rb;
    private Animator anim;
    private bool facingRight = true;
    private bool isJumping = false;
    private float moveDirection;
    private float lowJumpMultiplier = 10f;
    private float fallMultiplier = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Component[] colliders = GetComponents(typeof(CapsuleCollider2D));
        deathCollider = colliders[0];
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        FixRotation();
        PlayAnimation();
    }

    private void FixedUpdate(){
        Move();
    }

    private void ProcessInputs(){
        moveDirection = Input.GetAxis("Horizontal");
        isJumping = Input.GetButton("Jump");
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
        if(isJumping && rb.velocity.y == 0){
            rb.AddForce(new Vector2(0f, jumpForce));
        } else if(rb.velocity.y < 0){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if(rb.velocity.y > 0 && !isJumping){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void FlipCharacter(){
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void PlayAnimation(){
        if(rb.velocity.y > 0f){
            anim.Play("Player_Jump");
        }
        else if(rb.velocity.y < 0f){
            anim.Play("Player_Fall");
        }
        else if(Mathf.Abs(rb.velocity.x) > 0.2f){
            anim.Play("Player_Run");
        }
        else{
            anim.Play("Player_Idle");
        }
    }
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.name == "Monster"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
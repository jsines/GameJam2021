using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 3;
    private float jumpForce = 400;
    Component deathCollider;
    private Rigidbody2D rb;
    private Animator anim;
    private bool facingRight = true;
    private bool isJumping = false;
    private float moveDirection;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Component[] colliders = GetComponents(typeof(CapsuleCollider2D));
        deathCollider = colliders[1];
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

    private void PlayAnimation(){
        if(rb.velocity.y > 0f && isJumping){
            anim.Play("Player_Jump");
        }
        else if(rb.velocity.y <= 0f && isJumping){
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
            SceneManager.LoadScene("GameScene");
        }
    }
}
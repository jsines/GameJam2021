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
    
    public GameObject monster;
    public Vector3[] lastCheckpoint = new Vector3[2];

    // Start is called before the first frame update
    void Start()
    {
        if(lastCheckpoint[0] == new Vector3(0,0,0)){
            lastCheckpoint[0] = new Vector3(-33.5f, 35.9f, 0f);
            lastCheckpoint[1] = new Vector3(4f, 3.3f, 0f);
        }
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Component[] colliders = GetComponents(typeof(CapsuleCollider2D));
        deathCollider = colliders[0];

        MoveToCheckpoint();
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
        if(Input.inputString.Contains("r")){
            MoveToCheckpoint();
        }
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

    void MoveToCheckpoint(){
        transform.localPosition = lastCheckpoint[0];
        monster.transform.localPosition = lastCheckpoint[1];
        monster.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.name == "Monster"){
            MoveToCheckpoint();
        }
    }
}
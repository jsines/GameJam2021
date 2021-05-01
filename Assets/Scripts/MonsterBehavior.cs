using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform playerPos;
    public Transform enemyPos;
    public float movementSpeed;
    private bool facingRight;

    void Update()
    {
        if(playerPos.localPosition.x > enemyPos.localPosition.x){
            rb.AddForce(new Vector2(movementSpeed, 0f));
        }
        else if(playerPos.localPosition.x < enemyPos.localPosition.x){
            rb.AddForce(new Vector2(-movementSpeed, 0f));
        }
        FixRotation();
    }

    private void FixRotation(){
        if(rb.velocity.x < 0 && !facingRight){
            FlipCharacter();
        }else if(rb.velocity.x > 0 && facingRight){
            FlipCharacter();
        }
    }

    private void FlipCharacter(){
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}

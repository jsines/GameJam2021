using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform playerPos;
    public Transform enemyPos;
    public float movementSpeed;

    void Update()
    {
        if(playerPos.localPosition.x > enemyPos.localPosition.x){
            rb.AddForce(new Vector2(movementSpeed, 0f));
        }
        else if(playerPos.localPosition.x < enemyPos.localPosition.x){
            rb.AddForce(new Vector2(-movementSpeed, 0f));
        }
    }
}

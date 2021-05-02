using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableFloorBehavior : MonoBehaviour
{
    Component collision;
    Component monsterBreaker;
    Vector2 velHolder;

    void Start()
    {
        Component[] colliders = GetComponents(typeof(BoxCollider2D));
        collision = colliders[0];
        monsterBreaker = colliders[1];
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.name == "Monster"){
            MonsterBehavior monster = collision.gameObject.GetComponent<MonsterBehavior>();
            Destroy(gameObject);
        }
    }

}

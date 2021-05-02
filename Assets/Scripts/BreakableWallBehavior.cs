using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWallBehavior : MonoBehaviour
{
    Component collision;
    Component monsterBreaker;
    Vector2 velHolder;
    private AudioSource aSrc;

    void Start()
    {
        Component[] colliders = GetComponents(typeof(BoxCollider2D));
        collision = colliders[0];
        monsterBreaker = colliders[1];
        aSrc = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.name == "Monster"){
            aSrc.Play();
            Destroy(gameObject);
        }
    }

}



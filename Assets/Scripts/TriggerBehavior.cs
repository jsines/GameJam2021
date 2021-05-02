using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBehavior : MonoBehaviour
{
    public Vector3[] checkpointLocations = new Vector3[2];

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.name == "Player"){
            PlayerBehavior player = collision.gameObject.GetComponent<PlayerBehavior>();
            player.lastCheckpoint[0] = checkpointLocations[0];
            player.lastCheckpoint[1] = checkpointLocations[1];
        }
    }
}

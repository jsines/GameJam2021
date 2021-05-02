using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    Component buttonPressure;
    private bool buttonPressed;
    public GameObject associatedDoor;

    //Sprite
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    // Start is called before the first frame update
    void Start()
    {
        buttonPressure = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.name == "Monster" && !buttonPressed){
            buttonPressed = true;
            DoorBehavior doorScript = associatedDoor.GetComponent<DoorBehavior>();
            doorScript.TriggerDoor();
            ChangeSprite();
        }
    }

    void ChangeSprite(){
        spriteRenderer.sprite = newSprite;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    Component buttonPressure;
    [SerializeField] bool buttonPressed;
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
        if(collision.name == "Monster"){
            buttonPressed = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        if(collision.name == "Monster"){
            buttonPressed = false;
        }
    }
}

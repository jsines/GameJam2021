using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    private int buttonsPressed = 0;
    private int totalButtons;
    public Transform buttonHolder;
    // Start is called before the first frame update
    void Start()
    {
        totalButtons = buttonHolder.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerDoor(){
        buttonsPressed += 1;
        if(totalButtons == buttonsPressed){
            Destroy(gameObject);
        }
    }
}

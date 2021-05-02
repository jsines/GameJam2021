using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private float flashInterval = 1f;
    private float flashTimer = 0f;
    private float offset = 5f;
    public Transform titlePress;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        flashTimer += Time.deltaTime;
        if(flashTimer >= flashInterval){
            flashTimer -= flashInterval;
            titlePress.position = new Vector3(titlePress.position.x, titlePress.position.y, offset);
            offset *= -1;
        }
        if(Input.anyKey){
            SceneManager.LoadScene("Level1");
        }
    }
}

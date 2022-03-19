using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressableObject : MonoBehaviour
{
    public GameLogic gameLogic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        gameLogic.pressableObject = this;   
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        gameLogic.pressableObject = null;
    }

    public void press()
    {
        Debug.Log(gameObject.name + " is pressed");
    }
}

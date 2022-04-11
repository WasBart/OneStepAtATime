using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorPressableObject : PressableObject
{
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

    override public void Press()
    {
        Debug.Log(gameObject.name + " is pressed");
    }

    override public void Restore()
    {
      //do nothing
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllowedPressableObject : PressableObject { 
    private RectTransform rect;
    private Image image;
    private Color initialColor;
    private bool pressed;
    public Vector2 currentSize;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        initialColor = image.color;
        currentSize = rect.sizeDelta;
    }

    override public void Press()
    {
        Debug.Log(gameObject.name + " is pressed");
        image.color = Color.green;
        rect.sizeDelta = currentSize;
        pressed = true;
    }

    override public void Restore()
    {
        Debug.Log(gameObject.name + " is restored");
        image.color = initialColor;
        rect.sizeDelta = currentSize;
        pressed = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       //Debug.Log(gameObject.name + " enter");
        if (!pressed)
        {
            currentSize = rect.sizeDelta;
            gameLogic.pressableObject = this;
            rect.sizeDelta += new Vector2(0,5f);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(gameObject.name + " exit");
        if (!pressed)
        {
            if (gameLogic.pressableObject == this)
            {
                gameLogic.pressableObject = null;
            }
            rect.sizeDelta = currentSize;
        }
    }
}

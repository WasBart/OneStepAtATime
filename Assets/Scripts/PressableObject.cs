using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressableObject : MonoBehaviour
{
    public GameLogic gameLogic;
    private RectTransform rect;
    private Image image;
    private Color initialColor;
    private bool pressed;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        initialColor = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!pressed)
        {
            gameLogic.pressableObject = this;
            rect.sizeDelta = new Vector2(65, 65);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!pressed)
        {
            gameLogic.pressableObject = null;
            rect.sizeDelta = new Vector2(60, 60);
        }
    }

    public void Press()
    {
        Debug.Log(gameObject.name + " is pressed");
        image.color = Color.green;
        rect.sizeDelta = new Vector2(40, 40);
        pressed = true;
    }

    public void Restore()
    {
        Debug.Log(gameObject.name + " is restored");
        image.color = initialColor;
        rect.sizeDelta = new Vector2(60, 60);
        pressed = false;
    }
}

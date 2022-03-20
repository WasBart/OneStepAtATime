using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobblyMovement : MonoBehaviour
{
    public Rigidbody rb_body;
    public Rigidbody rb_head;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            rb_body.AddForce(new Vector3(0, 4.0f, 4.0f), ForceMode.Impulse);
            //this.transform.Translate(new Vector3(0, 1.0f, 0));
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            rb_head.velocity = Vector3.zero;
            //this.transform.Translate(new Vector3(0, 1.0f, 0));
        }
    }
}

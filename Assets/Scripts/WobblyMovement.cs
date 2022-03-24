using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobblyMovement : MonoBehaviour
{
    public Rigidbody rb_body;
    public Rigidbody rb_head;
    public Animator animator;

    public float upForce;
    public float forwardForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            rb_body.AddForce(new Vector3(0, upForce, forwardForce), ForceMode.Impulse);
            animator.SetBool("isJumping", true);
            animator.speed = 0.5f;
        }
        else if (rb_body.velocity.sqrMagnitude <= 0.01f)
        {
            animator.SetBool("isJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            rb_head.velocity = Vector3.zero;
            //this.transform.Translate(new Vector3(0, 1.0f, 0));
        }

        Debug.Log(rb_body.velocity.sqrMagnitude);
       
    }
}

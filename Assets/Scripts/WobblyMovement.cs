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
    private Vector3 offset = new Vector3(0, 0.05f, 0);
    public float time;
    public float barAnimSpeed = 0.75f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //if (animator.GetBool("isJumping") && rb_body.velocity.y <= 0.0f)
        //{
        //    RaycastHit hit;
        //    if (Physics.Raycast(rb_body.transform.position + offset, Vector3.down, out hit, 0.1f))
        //    {
        //        if (hit.collider.CompareTag("Stairs"))
        //        {
        //            animator.SetBool("isJumping", false);
        //            //rb_body.velocity = Vector3.zero;
        //            time = Time.time - time;
        //        }
        //    }
        //}


        if (Input.GetKeyDown(KeyCode.S))
        {
            rb_head.velocity = Vector3.zero;
            //this.transform.Translate(new Vector3(0, 1.0f, 0));
        }

        //Debug.Log(rb_body.velocity.sqrMagnitude);

    }

    public void Jump()
    {
        rb_body.AddForce(new Vector3(0, upForce, forwardForce), ForceMode.Impulse);
        animator.SetBool("prepareJump", false);
        animator.SetBool("landed", false);
        animator.SetBool("isJumping", true);
        time = Time.time;
    }

    public void PrepareJump()
    {
        Debug.Log("Prepare");
        animator.SetBool("prepareJump", true);
    }

    public void Miss()
    {
        Debug.Log("Miss");
        animator.SetBool("prepareJump", false);
        //animator.SetBool("landed", true);
    }

    public void Fail()
    {
        Debug.Log("Fail");
        animator.SetBool("prepareJump", false);
        animator.SetBool("fail", true);
        animator.SetBool("landed", true);
    }
}




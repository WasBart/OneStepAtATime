using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour { 

    public WobblyMovement wobblyMovement;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.transform.tag == "Stairs" && wobblyMovement.animator.GetBool("isJumping") && wobblyMovement.rb_body.velocity.y <= 0.001)
        {
            wobblyMovement.animator.SetBool("isJumping", false);
            wobblyMovement.animator.SetBool("landed", true);
            //animator.SetBool("isJumping", false);
        }
    }
}

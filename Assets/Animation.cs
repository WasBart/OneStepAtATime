using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private Animator animator;
    public int input  = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        if(input < 5){
        if(Input.GetKeyDown("space")){
  
            animator.Play("Rightstep", 0, 1.0f);
            animator.SetBool("leftStep", true);
             animator.SetBool("rightStep", false);
             input++;
    
             
        }
        else if(Input.GetKeyDown("return")){
         
                 animator.Play("LeftStep1", 0, 1.0f);
                animator.Play("LeftStep2", 0, 1.0f);
            animator.SetBool("rightStep", true);
             animator.SetBool("leftStep", false);
             input++;
      
        }
        
        }
    }


    
}

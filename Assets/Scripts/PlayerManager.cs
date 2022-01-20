using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public GameManager gameManager;

 

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.name == "GoalMesh") 
        {
            Debug.Log("You have won the game!");
            gameManager.NextLevel();
        }
    }



    // public void ResetGame(){
    //     this.transform.position = startPosition;
    //     firstMovement = true;
    //     InitializeLevelSequence();
    //     rend.material.color = Color.white;
    //     mistakeCounter = 0;   
    //     animator.SetBool("leftStep", false);
    //     animator.SetBool("rightStep", false);     
    //     animator.SetBool("mistake", false); 
    //     animator.Play("idle");         
    // }


    // IEnumerator WrongInput() 
    // {
    //     rend.material.color = Color.gray;
    //     movementPenalty = true;
    //     animator.SetBool("mistake", movementPenalty);
    //     yield return new WaitForSeconds(1);
        
    //     if (mistakeCounter == 1){
    //         rend.material.color = new Color32(255,170,170,0);
    //     }
    //     else if (mistakeCounter == 2){
    //         rend.material.color = new Color32(255,83,72,0);
    //     }
    //     else
    //     {
    //         rend.material.color = Color.red;
    //     }
        
    //     movementPenalty = false;
    //     animator.SetBool("mistake", movementPenalty);
    // }


    // void InitializeLevelSequence() 
    // {
    //     if(levelNumber == 1)
    //     {
    //         this.levelSequence.Clear();
    //         this.levelSequence.AddLast("left");
    //         this.levelSequence.AddLast("right");
    //         this.levelSequence.AddLast("left");
    //         this.levelSequence.AddLast("right");
    //         this.levelSequence.AddLast("left");
    //         this.levelSequence.AddLast("right");
    //         this.levelSequence.AddLast("left");
    //         this.levelSequence.AddLast("right");
    //         this.levelSequence.AddLast("left");
    //         this.levelSequence.AddLast("right");
    //         this.levelSequence.AddLast("left");
    //         this.levelSequence.AddLast("right");
    //     }
    //     else if(levelNumber == 2)
    //     {
    //         this.levelSequence.Clear();
    //         this.levelSequence.AddLast("left");
    //         this.levelSequence.AddLast("half-right");
    //         this.levelSequence.AddLast("half-right");
    //         this.levelSequence.AddLast("left");
    //         this.levelSequence.AddLast("right");
    //         this.levelSequence.AddLast("half-left");
    //         this.levelSequence.AddLast("half-left");
    //         this.levelSequence.AddLast("right");
    //         this.levelSequence.AddLast("left");
    //         this.levelSequence.AddLast("right");
    //         this.levelSequence.AddLast("left");
    //         this.levelSequence.AddLast("right");
    //         // Section 2
    //         this.levelSequence.AddLast("left");
    //         this.levelSequence.AddLast("half-right");
    //         this.levelSequence.AddLast("half-right");
    //         this.levelSequence.AddLast("left");
    //         this.levelSequence.AddLast("right");
    //         this.levelSequence.AddLast("left");
    //         this.levelSequence.AddLast("half-right");
    //         this.levelSequence.AddLast("half-right");
    //         this.levelSequence.AddLast("left");
    //         this.levelSequence.AddLast("right");
    //         this.levelSequence.AddLast("half-left");
    //         this.levelSequence.AddLast("half-left");
    //         this.levelSequence.AddLast("right");
    //         this.levelSequence.AddLast("left");
    //     }
    // }
}

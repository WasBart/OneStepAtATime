using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 startPosition;
    public float stepSizeY = 0.5f;
    public float stepSizeZ = 1.0f;
    private bool movementPenalty = false;
    private bool firstMovement = true;
    private LinkedList<string> levelSequence = new LinkedList<string>();
    public AudioSource leftLegAS;
    public AudioSource rightLegAS;

    public int levelNumber = 0;

    private int mistakeCounter;
    private static int MAX_MISTAKE = 3;

    public Renderer rend;

    public Animator animator;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.gameObject.transform.position;
        if(rend == null){
        rend = this.GetComponentInChildren<Renderer>();
        }
        InitializeLevelSequence();
    }

    // Update is called once per frame
    void Update()
    {
        if (movementPenalty) 
        {
            return;
        }
        bool moved = false;
        bool madeMistake = false;
        string correctInput = levelSequence.First.Value;
        if (Input.GetKeyDown("space")) 
        {
            Debug.Log("Input space recognized");
            Debug.Log("Correct Input: " + correctInput);
            Debug.Log("Current Input: space");
            if (correctInput == "left")
            {
                if(!firstMovement)
                {
                    this.transform.Translate(0, stepSizeY, stepSizeZ);
                }
                animator.SetBool("leftStep", true);
                animator.SetBool("rightStep", false);
                //leftLegAS.Play();
              
                moved = true;
            }
            else if (correctInput == "half-left")
            {
                this.transform.Translate(0, stepSizeY/2, stepSizeZ/2);
                animator.SetBool("leftStep", true);
                animator.SetBool("rightStep", false);
                moved = true;
            }
            else 
            {
                madeMistake = true;
            }
        }
        else if (Input.GetKeyDown("return"))
        {
            Debug.Log("Input enter recognized");
            Debug.Log("Correct Input: " + correctInput);
            Debug.Log("Current Input: return");
            if (correctInput == "right") 
            {
                this.transform.Translate(0, stepSizeY, stepSizeZ);
                animator.SetBool("leftStep", false);
                animator.SetBool("rightStep", true);
                
                //rightLegAS.Play();
                moved = true;
            }
            else if (correctInput == "half-right")
            {
                this.transform.Translate(0, stepSizeY/2, stepSizeZ/2);
                animator.SetBool("leftStep", false);
                animator.SetBool("rightStep", true);
                moved = true;
            }
            else 
            {
                madeMistake = true;
            }
        }
        if (moved) 
        {
            levelSequence.RemoveFirst();
            firstMovement = false;
        }
        else if (madeMistake) 
        {
            if(mistakeCounter < MAX_MISTAKE){
                mistakeCounter ++;
                StartCoroutine(WrongInput());
            }
            else{
                //game over code
                gameManager.showGameOverUI();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.name == "GoalMesh") 
        {
            Debug.Log("You have won the game!");
            ResetGame();
            if(levelNumber == 1){
                gameManager.ChangeToLevel2();
            }
            else{
                gameManager.ReturnToMenu();
            }
        }
    }

    public void ResetGame(){
        this.transform.position = startPosition;
        firstMovement = true;
        InitializeLevelSequence();
        rend.material.color = Color.white;
        mistakeCounter = 0;   
        animator.SetBool("leftStep", false);
        animator.SetBool("rightStep", false);     
        animator.SetBool("mistake", false); 
        animator.Play("idle");         
    }


    IEnumerator WrongInput() 
    {
        rend.material.color = Color.gray;
        movementPenalty = true;
        animator.SetBool("mistake", movementPenalty);
        yield return new WaitForSeconds(1);
        
        if (mistakeCounter == 1){
            rend.material.color = new Color32(255,170,170,0);
        }
        else if (mistakeCounter == 2){
            rend.material.color = new Color32(255,83,72,0);
        }
        else
        {
            rend.material.color = Color.red;
        }
        
        movementPenalty = false;
        animator.SetBool("mistake", movementPenalty);
    }


    void InitializeLevelSequence() 
    {
        if(levelNumber == 1)
        {
            this.levelSequence.Clear();
            this.levelSequence.AddLast("left");
            this.levelSequence.AddLast("right");
            this.levelSequence.AddLast("left");
            this.levelSequence.AddLast("right");
            this.levelSequence.AddLast("left");
            this.levelSequence.AddLast("right");
            this.levelSequence.AddLast("left");
            this.levelSequence.AddLast("right");
            this.levelSequence.AddLast("left");
            this.levelSequence.AddLast("right");
            this.levelSequence.AddLast("left");
            this.levelSequence.AddLast("right");
        }
        else if(levelNumber == 2)
        {
            this.levelSequence.Clear();
            this.levelSequence.AddLast("left");
            this.levelSequence.AddLast("half-right");
            this.levelSequence.AddLast("half-right");
            this.levelSequence.AddLast("left");
            this.levelSequence.AddLast("right");
            this.levelSequence.AddLast("half-left");
            this.levelSequence.AddLast("half-left");
            this.levelSequence.AddLast("right");
            this.levelSequence.AddLast("left");
            this.levelSequence.AddLast("right");
            this.levelSequence.AddLast("left");
            this.levelSequence.AddLast("right");
            // Section 2
            this.levelSequence.AddLast("left");
            this.levelSequence.AddLast("half-right");
            this.levelSequence.AddLast("half-right");
            this.levelSequence.AddLast("left");
            this.levelSequence.AddLast("right");
            this.levelSequence.AddLast("left");
            this.levelSequence.AddLast("half-right");
            this.levelSequence.AddLast("half-right");
            this.levelSequence.AddLast("left");
            this.levelSequence.AddLast("right");
            this.levelSequence.AddLast("half-left");
            this.levelSequence.AddLast("half-left");
            this.levelSequence.AddLast("right");
            this.levelSequence.AddLast("left");
        }
    }
}

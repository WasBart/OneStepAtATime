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
    private LinkedList<KeyCode> levelSequence = new LinkedList<KeyCode>();
    public AudioSource leftLegAS;
    public AudioSource rightLegAS;

    private int mistakeCounter;
    private static int MAX_MISTAKE = 3;

    public Renderer rend;

    public Animator animator;

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
        KeyCode correctInput = levelSequence.First.Value;
        if (Input.GetKeyDown("space")) 
        {
            Debug.Log("Input space recognized");
            Debug.Log("Correct Input: " + correctInput);
            Debug.Log("Current Input: " + KeyCode.Space);
            if (correctInput == KeyCode.Space)
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
            else 
            {
                madeMistake = true;
            }
        }
        else if (Input.GetKeyDown("return"))
        {
            if (correctInput == KeyCode.Return) 
            {
                this.transform.Translate(0, stepSizeY, stepSizeZ);
                animator.SetBool("leftStep", false);
                animator.SetBool("rightStep", true);
                
                //rightLegAS.Play();
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
                Debug.Log("Game Over!");
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
        }
    }

    private void ResetGame(){
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
        
        if(mistakeCounter == 1){
            rend.material.color = new Color32(255,170,170,0);
        }
        else if (mistakeCounter== 2){
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
        this.levelSequence.Clear();
        this.levelSequence.AddLast(KeyCode.Space);
        this.levelSequence.AddLast(KeyCode.Return);
        this.levelSequence.AddLast(KeyCode.Space);
        this.levelSequence.AddLast(KeyCode.Return);
        this.levelSequence.AddLast(KeyCode.Space);
        this.levelSequence.AddLast(KeyCode.Return);
        this.levelSequence.AddLast(KeyCode.Space);
        this.levelSequence.AddLast(KeyCode.Return);
        this.levelSequence.AddLast(KeyCode.Space);
        this.levelSequence.AddLast(KeyCode.Return);
        this.levelSequence.AddLast(KeyCode.Space);
        this.levelSequence.AddLast(KeyCode.Return);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(0.3f, 0.5f, -0.3f);
    public float stepSizeY = 0.5f;
    public float StepSizeZ = 1.0f;
    private bool movementPenalty = false;
    private bool firstMovement = true;
    private LinkedList<KeyCode> levelSequence = new LinkedList<KeyCode>();
    private Renderer[] legRenderers;
    public GameObject leftLeg;
    public GameObject rightLeg;

    // Start is called before the first frame update
    void Start()
    {
        InitializeLevelSequence();
        legRenderers = this.GetComponentsInChildren<Renderer>();
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
                if(firstMovement)
                {
                    leftLeg.transform.Translate(0, stepSizeY / 2, StepSizeZ / 2);
                }
                else 
                {
                    leftLeg.transform.Translate(0, stepSizeY, StepSizeZ);
                }
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
                rightLeg.transform.Translate(0, stepSizeY, StepSizeZ);
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
            StartCoroutine(WrongInput());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.name == "GoalMesh") 
        {
            Debug.Log("You have won the game!");
            this.transform.position = startPosition;
            leftLeg.transform.localPosition = new Vector3(-0.5f,0,0);
            rightLeg.transform.localPosition = new Vector3(0.5f,0,0);
            InitializeLevelSequence();
            foreach (Renderer r in this.legRenderers) 
            {
                r.material.color = Color.white;
            }
        }
    }

    IEnumerator WrongInput() 
    {
        foreach (Renderer r in this.legRenderers) 
        {
            r.material.color = Color.red;
        }
        movementPenalty = true;
        yield return new WaitForSeconds(1);
        foreach (Renderer r in this.legRenderers) 
        {
            r.material.color = Color.white;
        }
        movementPenalty = false;
    }

    void InitializeLevelSequence() 
    {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
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
    private static int MAX_MISTAKE = 2;

    public Renderer rend;

    public GameManager gameManager;

    public Vector3 currentLeft;
    public Vector3 currentRight;

    private Vector3 startingRight;
    private Vector3 startingLeft;

    private string lastMovement;

    private int inputCounter;
    private float lastInputTime;
    private float hintTime = 1.0f;
    private bool hintVisible = false;

    public bool gameOver = false;



    void Start()
    {
        startPosition = this.gameObject.transform.parent.transform.position;
        if (rend == null)
        {
            rend = this.GetComponentInChildren<Renderer>();
        }
        InitializeLevelSequence();
        startingRight = animator.GetBoneTransform(HumanBodyBones.RightFoot).position;
        startingLeft = animator.GetBoneTransform(HumanBodyBones.LeftFoot).position;
        currentLeft = animator.GetBoneTransform(HumanBodyBones.LeftFoot).position;
        currentRight = animator.GetBoneTransform(HumanBodyBones.RightFoot).position;
        gameManager.UpdateInputCanvas(ConvertInput(levelSequence.First.Value));
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ResetGame()
    {
        this.transform.parent.transform.position = startPosition;
        firstMovement = true;
        InitializeLevelSequence();
        rend.material.color = Color.white;
        mistakeCounter = 0;
        inputCounter = 0;
        hintVisible = false;
        currentLeft = startingLeft;
        currentRight = startingRight;
        gameManager.UpdateInputCanvas(ConvertInput(levelSequence.First.Value));
        gameManager.restoreHealth();
        gameOver = false;
    }


    IEnumerator WrongInput()
    {
        rend.material.color = Color.gray;
        movementPenalty = true;
        yield return new WaitForSeconds(1);
        rend.material.color = Color.white;
        movementPenalty = false;
    }


    void InitializeLevelSequence()
    {
        if (levelNumber == 1)
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
            this.levelSequence.AddLast("EOL");
        }

        else if (levelNumber == 2)
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
            this.levelSequence.AddLast("EOL");
        }
    }


    private void OnAnimatorIK(int layerIndex)

    {
        if (!gameOver)
        {
            bool moved = false;
            bool madeMistake = false;
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);

            if (movementPenalty)
            {
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, currentLeft);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.Euler(0, 0, 0));
                animator.SetIKPosition(AvatarIKGoal.RightFoot, currentRight);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.Euler(0, 0, 0));
                return;
            }

            string correctInput = levelSequence.First.Value;


            if (Time.time - lastInputTime > hintTime && inputCounter >= 3)
            {
                gameManager.UpdateInputCanvas(ConvertInput(correctInput));
                hintVisible = true;
                lastInputTime = float.MaxValue;
            }

            if (Input.GetKeyDown("space"))
            {
                if (correctInput == "left")
                {

                    if (firstMovement)
                    {
                        currentLeft += new Vector3(0, stepSizeY / 2, stepSizeZ / 2);

                        //this.transform.parent.transformTranslate(0, 0, stepSizeZ / 8);
                    }
                    else
                    {

                        this.transform.parent.transform.Translate(0, stepSizeY / 2, stepSizeZ / 2);
                        currentLeft += new Vector3(0, stepSizeY, stepSizeZ);
                    }


                    //leftLegAS.Play();

                    moved = true;
                }
                else if (correctInput == "half-left")
                {
                    Debug.Log(lastMovement);
                    if (lastMovement == "half-left")
                    {
                        currentLeft += new Vector3(0, stepSizeY * 0.25f, stepSizeZ * 0.5f);
                    }
                    else
                    {
                        currentLeft += new Vector3(0, stepSizeY * 0.75f, stepSizeZ * 0.5f);
                    }
                    this.transform.parent.transform.Translate(0, stepSizeY / 4, stepSizeZ / 4);

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
                    this.transform.parent.transform.Translate(0, stepSizeY / 2, stepSizeZ / 2);
                    currentRight += new Vector3(0, stepSizeY, stepSizeZ);
                    //rightLegAS.Play();
                    moved = true;
                }
                else if (correctInput == "half-right")
                {
                    Debug.Log(lastMovement);
                    if (lastMovement == "half-right")
                    {
                        currentRight += new Vector3(0, stepSizeY * 0.25f, stepSizeZ * 0.5f);
                        this.transform.parent.transform.Translate(0, stepSizeY / 4, stepSizeZ * 1 / 8);
                    }
                    else
                    {
                        currentRight += new Vector3(0, stepSizeY * 0.75f, stepSizeZ * 0.5f);
                        this.transform.parent.transform.Translate(0, stepSizeY / 4, stepSizeZ * 3 / 8);
                    }

                    moved = true;
                }
                else
                {
                    madeMistake = true;
                }
            }
            if (moved)
            {
                if (firstMovement)
                {
                    gameManager.startTime = Time.time;
                }
                levelSequence.RemoveFirst();
                lastMovement = correctInput;
                firstMovement = false;
                inputCounter++;
                lastInputTime = Time.time;
                if (hintVisible || inputCounter >= 3)
                {
                    gameManager.UpdateInputCanvas(null);
                }
                else
                {
                    gameManager.UpdateInputCanvas(ConvertInput(levelSequence.First.Value));
                }
            }
            else if (madeMistake)
            {
                if (mistakeCounter < MAX_MISTAKE)
                {
                    gameManager.UpdateHealth(mistakeCounter);
                    mistakeCounter++;
                    StartCoroutine(WrongInput());
                }
                else
                {
                    //game over code
                    gameManager.UpdateHealth(mistakeCounter);
                    gameManager.showGameOverUI();
                    gameOver = true;
                }
            }
        }

        animator.SetIKPosition(AvatarIKGoal.LeftFoot, currentLeft);
        animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.Euler(0, 0, 0));
        animator.SetIKPosition(AvatarIKGoal.RightFoot, currentRight);
        animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.Euler(0, 0, 0));


    }


    private string ConvertInput(string input)
    {
        if (input.Contains("-"))
        {
            return input.Substring(input.LastIndexOf("-") + 1);
        }
        return input;
    }
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public PressableObject pressableObject;
    public ErrorPressableObject errordPressableObject;
    public List<PressableObject> pressableObjects;
    public List<PressableObject> errorPressableObjects;
    public List<PressableObject> pressableObjectsCopy;
    public GameObject phaseContainer;
    public WobblyMovement wobblyMovement;
    public Animator rhythmAnimator;
    public Animator pubertyAnimator;
    private float windupNeeded = 0.5f;
    private float curTime = 0;
    private float targetTime = float.MaxValue;
    private int stepCount;
    public Phase[] phases;
    public int phaseChangeCount = 15;
    private int currentPhase = 0;
    private bool initJump = false;
    public GameObject mainCam;
    public GameObject moving;
    private bool jumpAllowed;

    void Start()
    {
        phases = phaseContainer.GetComponentsInChildren<Phase>(true);
        pressableObjects = new List<PressableObject>(phases[currentPhase].GetAllowedPressableObjects());
        pressableObjectsCopy = new List<PressableObject>();
        rhythmAnimator.SetFloat("speed", wobblyMovement.barAnimSpeed);
        wobblyMovement.animator.SetBool("landed", true);
        rhythmAnimator.SetBool("moving", true);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !wobblyMovement.animator.GetCurrentAnimatorStateInfo(0).IsName("fail"))
        {
            Debug.Log("Down Triggered");
            curTime = Time.time;
            targetTime = curTime + windupNeeded;
            pressableObject = null;
            rhythmAnimator.SetFloat("speed", wobblyMovement.barAnimSpeed);
            initJump = true;
        }
        if (initJump)
        {
            if (wobblyMovement.animator.GetBool("landed") && (wobblyMovement.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || wobblyMovement.animator.GetCurrentAnimatorStateInfo(0).IsName("Landed")))
            {
                Debug.Log("prpeare jump triggered");
                wobblyMovement.PrepareJump();
                initJump = false;
            }
        }

        if(Time.time >= targetTime && !wobblyMovement.animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") && !wobblyMovement.animator.GetCurrentAnimatorStateInfo(0).IsName("fail") && !initJump)
        {
            pressableObjects.ForEach(c => c.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f,1.0f));
            jumpAllowed = true;
        }
        else
        {
            pressableObjects.ForEach(c => c.GetComponent<Image>().color = Color.gray);
            jumpAllowed = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            initJump = false;
            rhythmAnimator.SetFloat("speed", 0f);
            if (jumpAllowed)
            {
                jumpAllowed = false;
                if (Time.time >= targetTime)
                {
                    targetTime = float.MaxValue;
                  
                    if (pressableObject != null)
                    {
                        Debug.Log(pressableObject.gameObject.name);
                        if (pressableObjects.Contains(pressableObject))
                        {
                            pressableObject.Press();
                            pressableObjectsCopy.Add(pressableObject);
                            pressableObjects.Remove(pressableObject);
                            wobblyMovement.Jump();
                            stepCount++;
                            Debug.Log(stepCount);
                            if (pressableObjects.Count == 0)
                            {
                                
                                Debug.Log("trigger step");
                                pressableObject = null;
                                pressableObjects = new List<PressableObject>(pressableObjectsCopy);
                                pressableObjectsCopy.Clear();
                                pressableObjects.ForEach(p => p.Restore());
                               

                                if (currentPhase == 1)
                                {
                                    moving.GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(-250.0f, 250.0f), phases[currentPhase].GetComponent<RectTransform>().anchoredPosition.y);
                                }

                                if(currentPhase == 3)
                                {

                                    if (pressableObjects[0].GetComponent<RectTransform>().sizeDelta.x > 20)
                                    {
                                        Debug.Log(pressableObjects[0].GetComponent<RectTransform>().sizeDelta);
                                        pressableObjects.ForEach(c => c.GetComponent<RectTransform>().sizeDelta -= new Vector2(20, 0));
                                        pressableObjects.ForEach(c => c.GetComponent<BoxCollider2D>().size -= new Vector2(20, 0));
                                        pressableObjects.ForEach(c => c.GetComponent<AllowedPressableObject>().currentSize = c.GetComponent<RectTransform>().sizeDelta);
                                        errorPressableObjects.ForEach(c => c.GetComponent<RectTransform>().sizeDelta += new Vector2(35, 0));
                                        errorPressableObjects.ForEach(c => c.GetComponent<BoxCollider2D>().size += new Vector2(35, 0));
                                    }
                                    else
                                    {
                                        if (wobblyMovement.barAnimSpeed < 1.2f)
                                        {
                                            wobblyMovement.barAnimSpeed += 0.1f;
                                        }
                                    }
                                }

                                if (stepCount % phaseChangeCount == 0)
                                {
                                    ChangePhase();
                                }
                            }
                        }
                        //must be error pressable
                        else
                        {
                            targetTime = float.MaxValue;
                            Debug.Log("remove Life");
                            wobblyMovement.Fail();
                            pressableObject = null;
                        }
                    }
                    else
                    {
                        Debug.Log("pressableOject null");
                        targetTime = float.MaxValue;
                        wobblyMovement.Miss();
                        pressableObject = null;
                    }

                }
                else
                {
                    targetTime = float.MaxValue;
                    Debug.Log("Not Enough time!");
                    wobblyMovement.Miss();
                    pressableObject = null;
                }
            }
            else
            {
                Debug.Log("wrong state");
                targetTime = float.MaxValue;
                wobblyMovement.Miss();
                pressableObject = null;
            }

        }
    }

    private void ChangePhase()
    {
        Debug.Log("changePhaseTriggered");
        if (currentPhase < phases.Length - 1)
        {
            phases[currentPhase].gameObject.SetActive(false);
            currentPhase++;
            phases[currentPhase].gameObject.SetActive(true);
            pressableObjects = new List<PressableObject>(phases[currentPhase].GetAllowedPressableObjects());
            errorPressableObjects = new List<PressableObject>(phases[currentPhase].GetErrorPressableObjects());

            if (currentPhase == 1)
            {
                wobblyMovement.barAnimSpeed = 0.0f;
                rhythmAnimator.enabled = false;
                //mainCam.transform.rotation = Quaternion.Euler(0, 0, 180);
            }

            if (currentPhase == 2)
            {
                rhythmAnimator.enabled = true;
                wobblyMovement.barAnimSpeed = 0.7f;
                //mainCam.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (currentPhase == 3)
            {
                rhythmAnimator.enabled = true;
                wobblyMovement.barAnimSpeed = 0.5f;
                //mainCam.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

}





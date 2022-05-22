using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public PressableObject pressableObject;
    public ErrorPressableObject errordPressableObject;
    public List<PressableObject> pressableObjects;
    public List<PressableObject> pressableObjectsCopy;
    public GameObject phaseContainer;
    public WobblyMovement wobblyMovement;
    public Animator rhythmAnimator;
    private float windupNeeded = 0.5f;
    private float curTime = 0;
    private float targetTime = 0;
    private float speed = 0.75f;
    private int stepCount;
    public Phase[] phases;
    public int phaseChangeCount = 10;
    private int currentPhase = 0;
    private bool initJump = false;

    void Start()
    {
        phases = phaseContainer.GetComponentsInChildren<Phase>(true);
        pressableObjects = new List<PressableObject>(phases[currentPhase].GetAllowedPressableObjects());
        pressableObjectsCopy = new List<PressableObject>();
        rhythmAnimator.SetFloat("speed", speed);
        wobblyMovement.animator.SetBool("landed", true);
        rhythmAnimator.SetBool("moving", true);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Down Triggered");
            curTime = Time.time;
            targetTime = curTime + windupNeeded;
            pressableObject = null;
            rhythmAnimator.SetFloat("speed", speed);
            initJump = true;
        }
        if (initJump)
        {
            if ((wobblyMovement.animator.GetBool("landed") && wobblyMovement.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || wobblyMovement.animator.GetCurrentAnimatorStateInfo(0).IsName("Landed")))
            {
                Debug.Log("prpeare jump triggered");
                wobblyMovement.PrepareJump();
                initJump = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && (wobblyMovement.animator.GetBool("prepareJump")))
        {
            if (Time.time >= targetTime)
            {
                rhythmAnimator.SetFloat("speed", 0f);
                if (pressableObject != null)
                {
                    Debug.Log(pressableObject.gameObject.name);
                    if (pressableObjects.Contains(pressableObject))
                    {
                        pressableObject.Press();
                        pressableObjectsCopy.Add(pressableObject);
                        pressableObjects.Remove(pressableObject);
                        if (pressableObjects.Count == 0)
                        {
                            wobblyMovement.Jump();
                            Debug.Log("trigger step");
                            pressableObject = null;
                            pressableObjects = new List<PressableObject>(pressableObjectsCopy);
                            pressableObjectsCopy.Clear();
                            pressableObjects.ForEach(p => p.Restore());
                            stepCount++;
                            Debug.Log(stepCount);

                            if (currentPhase == 1)
                            {
                                phases[currentPhase].GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(-243.0f, 243.0f), phases[currentPhase].GetComponent<RectTransform>().anchoredPosition.y);
                            }

                            if(stepCount % phaseChangeCount == 0)
                            {
                                ChangePhase();
                            }
                        }
                    }
                    //must be error pressable
                    else
                    {
                        Debug.Log("remove Life");
                        wobblyMovement.Fail();
                        pressableObject = null;
                    }
                }
                else
                {
                    wobblyMovement.Miss();
                    pressableObject = null;
                }

            }
            else
            {
                Debug.Log("Not Enough time!");
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
        }
    }

}





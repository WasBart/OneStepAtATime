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
    public Phase phase;
    public WobblyMovement wobblyMovement;
    public Animator rhythmAnimator;

    void Start()
    {
        pressableObjects = new List<PressableObject>(phase.GetAllowedPressableObjects());
        pressableObjectsCopy = new List<PressableObject>();
        rhythmAnimator.SetFloat("speed", 0.75f);
        wobblyMovement.animator.SetBool("landed", true);
        rhythmAnimator.SetBool("moving", true);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (wobblyMovement.animator.GetBool("landed") && wobblyMovement.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || wobblyMovement.animator.GetCurrentAnimatorStateInfo(0).IsName("Landed")))
        { 
            wobblyMovement.PrepareJump();
           
            pressableObject = null;
            rhythmAnimator.SetFloat("speed", 0.75f);
        }

        if (Input.GetKeyUp(KeyCode.Space) && wobblyMovement.animator.GetBool("landed"))
        {
            rhythmAnimator.SetFloat("speed", 0f);
            if (pressableObject != null)
            {
                Debug.Log(pressableObject.gameObject.name);
                if (pressableObjects.Contains(pressableObject)){
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
                    }
                }
                //must be error pressable
                else {
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
    }

    
}

 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public PressableObject pressableObject;
    public List<PressableObject> pressableObjects;
    public List<PressableObject> pressableObjectsCopy;
    public Phase phase;
    public WobblyMovement wobblyMovement;
    public Animator rhythmAnimator;
    void Start()
    {
        pressableObjects = new List<PressableObject>(phase.GetPressableObjects());
        pressableObjectsCopy = new List<PressableObject>();
        rhythmAnimator.SetFloat("speed", 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            wobblyMovement.animator.SetBool("landed", false);
            wobblyMovement.prepareJump();       
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            wobblyMovement.Jump();
            //    if (pressableObject != null && pressableObjects.Contains(pressableObject))
            //    {
            //        pressableObject.Press();
            //        pressableObjectsCopy.Add(pressableObject);
            //        pressableObjects.Remove(pressableObject);
            //        if(pressableObjects.Count == 0)
            //        {
            //            Debug.Log("trigger step");

            //            pressableObjects = new List<PressableObject>(pressableObjectsCopy);
            //            pressableObjectsCopy.Clear();
            //            pressableObjects.ForEach(p => p.Restore());
            //        }
            //    }
            //    else
            //    {
            //        Debug.Log("remove Life");
            //    }
            //
        }
    }

    
}

 

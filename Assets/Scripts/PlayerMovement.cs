using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(0.3f, 0.5f, -0.3f);
    public float stepSizeY = 0.25f;
    public float StepSizeZ = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) 
        {
            this.transform.Translate(0, stepSizeY, StepSizeZ);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.name == "GoalMesh") 
        {
            Debug.Log("You have won the game!");
            this.transform.position = startPosition;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
    public AllowedPressableObject[] GetAllowedPressableObjects()
    {
        return this.GetComponentsInChildren<AllowedPressableObject>();
    }

    public ErrorPressableObject[] GetErrorPressableObjects()
    {
        return this.GetComponentsInChildren<ErrorPressableObject>();
    }
}

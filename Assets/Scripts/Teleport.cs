using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.GetComponentInParent<WobblyMovement>().transform.position = other.transform.GetComponentInParent<WobblyMovement>().transform.position - new Vector3(0, 3*2.5f, 3*4.96f);
        mainCam.transform.position = mainCam.transform.position - new Vector3(0, 3*2.5f, 3*4.96f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public float zOffset;
    public float yOffset;

    public float interpolation;
    private Vector3 prevPos;
    // Start is called before the first frame update
    void Start()
    {
        prevPos = player.transform.position + new Vector3(0, yOffset, -zOffset);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Vector3.Lerp(prevPos, player.transform.position + new Vector3(0, yOffset, -zOffset), interpolation);
        //newPos = player.transform.position + new Vector3(0, yOffset, -zOffset);
        transform.position = newPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Code for camera to follow the player at the given values on X, Y and Z.

    public float  XVal, YVal, ZVal;
    Camera Camera;
    GameObject Player;
   
    void Start()
    {
        Camera = GetComponent<Camera>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        Vector3 playerInfo = Player.transform.transform.position;
        Camera.transform.position = new Vector3(playerInfo.x + XVal, playerInfo.y + YVal, playerInfo.z + ZVal);
    }
}

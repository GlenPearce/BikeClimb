using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


//This code is for player movement and control, including checkpoints.
//Also deals with the character falling and the fallen screen

public class PlayerMvmt : MonoBehaviour
{

    public float mvmt, rotate, maxRpm;
    

    public WheelCollider frWheel, bkWheel;
    public Transform frWheelT, bkWheelT;
    public Rigidbody bike, c1Sign, c2Sign, c3Sign;
    public GameObject c0, c1, c2, c3, fallenTxt;
    public gameManager gameManager;
    public Menu menu;
    public int mostRecentChk = 0;
    bool inMenu = true, fall = false;

    public void Update()
    {

        //Space takes player to most recent check point, R restarts the track.
        //Checks to see if player is in a menu first.
        //Also contains restart button for pause menu.

        
        if (inMenu == false)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                bike.transform.position = c0.transform.position;
                bike.transform.rotation = c0.transform.rotation;

                gameManager.resetTime();
                gameManager.resetFault();

                bike.velocity = Vector3.zero;
                bike.angularVelocity = Vector3.zero;

                mostRecentChk = 0;

                fall = false;
                fallenTxt.SetActive(false);
            }

            if (Input.GetKeyDown("space") | Input.GetKeyDown("r"))
            {

                if (mostRecentChk == 0)
                {
                    bike.transform.position = c0.transform.position;
                    bike.transform.rotation = c0.transform.rotation;
                    gameManager.resetTime();

                }

                if (mostRecentChk == 1)
                {
                    bike.transform.position = c1.transform.position;
                    bike.transform.rotation = c1.transform.rotation;
                    gameManager.faultAdd();
                }

                if (mostRecentChk == 2)
                {
                    bike.transform.position = c2.transform.position;
                    bike.transform.rotation = c2.transform.rotation;
                    gameManager.faultAdd();
                }

                if (mostRecentChk == 3)
                {
                    bike.transform.position = c3.transform.position;
                    bike.transform.rotation = c3.transform.rotation;
                    gameManager.faultAdd();
                }

                bike.velocity = Vector3.zero;
                bike.angularVelocity = Vector3.zero;

                fall = false;
                fallenTxt.SetActive(false);
            }
        }

    }

    public void FixedUpdate()
    {
        //Acceleration + Max Speed
        if (fall == false)
        {
            
            if (bkWheel.rpm > maxRpm)
            {
                bkWheel.motorTorque = 0;
            }
            else
            {
                float motor = (mvmt * 100) * Input.GetAxis("Vertical");
                bkWheel.motorTorque = motor;
            }

            //Rotation
            float RotateFrce = (rotate * 100) * Input.GetAxis("Horizontal");
            bike.AddTorque(transform.right * RotateFrce);
        }

        //Wheel Spinning
        wheelUpdate(frWheel, frWheelT);
        wheelUpdate(bkWheel, bkWheelT);

    }

    //Method called to allow disabling of inputs when in menu.

    public void inAMenu(bool i)
    {
        if (i == true)
        {
            inMenu = true;
            gameManager.stopTime(true);
        }
        if (i == false)
        {
            inMenu = false;
            gameManager.stopTime(false);
        }
    }
    

    //Wheel Spinning Updater

    public void wheelUpdate (WheelCollider collider, Transform transform)
    {
        Vector3 Pos = transform.position;
        Quaternion Quat = transform.rotation;

        collider.GetWorldPose(out Pos, out Quat);

        transform.position = Pos;
        transform.rotation = Quat;
    }

    //Code that brings information back on which checkpoint is entered.
    //Checkpoint 4 is the finish line

    public void checkpointEntered(int whichCheckpoint)
    {
        Debug.Log("You Entered: " + whichCheckpoint);
    }

    void OnTriggerEnter(Collider checkPoint)
    {
        if (checkPoint.name == "Checkpoint1")
        {
            checkpointEntered(1);
            mostRecentChk = 1;
            c1Sign.AddTorque(transform.right * -900);            
        }

        if (checkPoint.name == "Checkpoint2")
        {
            checkpointEntered(2);
            mostRecentChk = 2;
            c2Sign.AddTorque(transform.right * -900);
        }

        if (checkPoint.name == "Checkpoint3")
        {
            checkpointEntered(3);
            mostRecentChk = 3;
            c3Sign.AddTorque(transform.right * -900);
        }

        //Finish Line
        if (checkPoint.name == "Checkpoint4")
        {
            checkpointEntered(4);
            bike.isKinematic = true;
            menu.finish();
            gameManager.finishLine();
            gameManager.stopTime(true);
            inMenu = true;
        }
    }

    public void fallen()
    {
        fall = true;
        fallenTxt.SetActive(true);
    }
    
    public void Restart()
    {
        bike.transform.position = c0.transform.position;
        bike.transform.rotation = c0.transform.rotation;

        gameManager.resetTime();
        gameManager.resetFault();

        bike.velocity = Vector3.zero;
        bike.angularVelocity = Vector3.zero;

        mostRecentChk = 0;

        fall = false;
        fallenTxt.SetActive(false);
        inMenu = false;
    }

}
    



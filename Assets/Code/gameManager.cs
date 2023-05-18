using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This code deals with time and faults.
public class gameManager : MonoBehaviour
{
    
    float time = 0;
    public Text timer, fault, HSTime, HSFaults, finishHSTime, finishHSFaults, finishTimeHSHL, finishFaultsHSHL;
    public Text currentTime, currentFaults, currentTimeHL, currentFaultsHL;
    int minutes, seconds, finalmins, totalScore, finalfaults;
    string HScore, currentScore;
    public PlayerMvmt playerScript;
    public int faults;
    bool stopTimer = false;

    

    void Start()
    {
        if (PlayerPrefs.GetInt("highScore") == 0)
        {
            PlayerPrefs.SetInt("highScore", 999999);
        }

        HScore = PlayerPrefs.GetInt("highScore").ToString("D6");
        HSTime.text = HScore.Substring(2, 2) + ":" + HScore.Substring(4,2);
        HSFaults.text = HScore.Substring(0, 2);
        Debug.Log(HScore);

    }

    public void faultAdd()
    {
        faults++;
        fault.text = faults.ToString();
    }

    public void resetFault()
    {
        faults = 0;
        fault.text = faults.ToString();
    }

    public void resetTime()
    {
        time = 0;
        minutes = 0;
    }
    

    void Update()
    {
        time += Time.deltaTime;

        //Converting time to int
        seconds = (int)time;  
        
        //When 60 seconds is reached, reset timer and add 1 to minutes
        if (seconds == 60)
        {
            minutes++;
            seconds = 0;
            time = 0;
        }
        if (stopTimer == false)
        {
            timer.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");  //String shows 2 digits each
        }
        if (stopTimer == true)
        {
            time = seconds;
        }

    }

    public void stopTime(bool i)
    {
        stopTimer = i;
    }


    public void finishLine()
    {
        finalfaults = faults * 10000;
        finalmins = minutes * 100;
        totalScore = seconds + finalmins + finalfaults;
        currentScore = totalScore.ToString("D6");

        if (totalScore <= int.Parse(HScore))
        {
            HScore = totalScore.ToString("D6");
            PlayerPrefs.SetInt("highScore", totalScore);

        }

        //Display current run score at finish

        currentTime.text = currentScore.Substring(2, 2) + ":" + currentScore.Substring(4, 2);
        currentFaults.text = currentScore.Substring(0, 2);
        currentTimeHL.text = currentScore.Substring(2, 2) + ":" + currentScore.Substring(4, 2);
        currentFaultsHL.text = currentScore.Substring(0, 2);

        //Display high score at the finish

        finishHSTime.text = HScore.Substring(2, 2) + ":" + HScore.Substring(4, 2);
        finishHSFaults.text = HScore.Substring(0, 2);
        finishTimeHSHL.text = HScore.Substring(2, 2) + ":" + HScore.Substring(4, 2);
        finishFaultsHSHL.text = HScore.Substring(0, 2);

    }

}

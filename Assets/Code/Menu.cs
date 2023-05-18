using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    //This script is for menu and pause menu buttons and allowing the user to use escape to pause.
    //This script also containts countdown timer to start.

    public GameObject pauseCan, mainMenuCan, star, starText, bkWheel, pOptionsPan, mOptionsPan, mainMenuPanel, countText, gameCan, finishCan, quitConfirmPan;
    public Rigidbody bikeRb;
    bool paused = false;
    public Animation camAnim;
    public Text count, countHL;
    public CameraFollow camFol;
    public gameManager gameManager;
    public PlayerMvmt playerMvmt;
    public bool inMainMenu = true;
    public AudioSource bikeEngine;

    
    //Pressing escape allows the user to bring up and close the pause menu
    void Update()
    {
        if (inMainMenu == false)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (paused == false)
                {
                    bikeRb.isKinematic = true;
                    bkWheel.SetActive(false);
                    bikeRb.velocity = Vector3.zero;
                    bikeRb.angularVelocity = Vector3.zero;
                    pauseCan.SetActive(true);
                    paused = true;
                    bikeEngine.mute = true;
                    gameManager.stopTime(true);
                }

               
            }
        }
    }

    //Pause menu Buttons
    public void pContinueBtn()
    {
        bikeRb.isKinematic = false;
        bkWheel.SetActive(true);
        pauseCan.SetActive(false);
        pOptionsPan.SetActive(false);
        paused = false;
        bikeEngine.mute = false;
        gameManager.stopTime(false);
    }

    public void optionBtn()
    {
        pOptionsPan.SetActive(true);
        mOptionsPan.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void bkOptBtn()
    {
        pOptionsPan.SetActive(false);
        mOptionsPan.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    //Choosing to quit from the pause menu, restarts the scene
    public void quitToMainBtn()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);

    }


    //Main Menu Buttons
    //Start button with countdown to begin race
    public void mStartBtn()
    {
        camAnim.Play("CameraMvmt");
        StartCoroutine(menuCanvasVanish());
    }
    
    IEnumerator menuCanvasVanish()
    {
        
        mainMenuPanel.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        countText.SetActive(true);
        count.text = "3";
        countHL.text = "3";
        yield return new WaitForSeconds(1.0f);
        count.text = "2";
        countHL.text = "2";
        yield return new WaitForSeconds(1.0f);
        count.text = "1";
        countHL.text = "1";
        yield return new WaitForSeconds(1.0f);
        count.text = "GO!";
        countHL.text = "GO!";
        camFol.enabled = true;
        bikeRb.isKinematic = false;
        yield return new WaitForSeconds(1.0f);
        inMainMenu = false;
        if(PlayerPrefs.GetInt("Star") == 1)
        {
            star.SetActive(true);
        }
        playerMvmt.inAMenu(false);
        countText.SetActive(false);
        mainMenuCan.SetActive(false);
        gameCan.SetActive(true);
        gameManager.resetTime();
    }

    public void mQuit()
    {
        quitConfirmPan.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void ConfQuit()
    {
        Application.Quit();
    }

    public void ConfBack()
    {
        quitConfirmPan.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void finish()
    {
        finishCan.SetActive(true);
        inMainMenu = true;
        
    }

    //SecretStarCode
    private void OnTriggerEnter(Collider other)
    {
        if (PlayerPrefs.GetInt("Star") == 0)
            {
            PlayerPrefs.SetInt("Star", 1);
            star.SetActive(true);
            }

        starText.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        starText.SetActive(false);
    }

    //Calls on playerMvmt to restart in the same way that pressing Enter would.
    public void RestartBtn()
    {
        playerMvmt.Restart();
        pauseCan.SetActive(false);
        bikeRb.isKinematic = false;
        bkWheel.SetActive(true);
        pauseCan.SetActive(false);
        pOptionsPan.SetActive(false);
        paused = false;
        bikeEngine.mute = false;
        gameManager.stopTime(false);
    }
}

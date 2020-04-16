using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour
{

    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject volumeSliders;

  
    //Enables the buttons in the options menu
    public void GoToOptions()
    {
        buttons.SetActive(false);
        volumeSliders.gameObject.SetActive(true);

    }

    //Goes back to the main menu screen
    public void backToMenu()
    {
        buttons.SetActive(true);
        volumeSliders.SetActive(false);

    }


    //Changes the game volume
    public void SetMasterVolume(float val)
    {
        Debug.Log("Master volume: " + val);
    }

    //Sets the sound effect volume 
    public void setSFXVolume(float val)
    {
        Debug.Log("SFX volume: " + val);
    }

    //Sets the music volume
    public void setMusicVolume(float val)
    {
        Debug.Log("Music volume: " + val);
    }
    public void playGame()
    {
        Application.LoadLevel(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

}

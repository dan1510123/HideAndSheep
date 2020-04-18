using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Components;
using Unity.Entities;
using Unity;
public class MainMenuController : MonoBehaviour
{

    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject volumeSliders;
    [SerializeField] private Image background;

    private Vector2 backgroundDelta = new Vector2(300, 400);
    private Vector3 backgroundOffset = new Vector2(0, 75);
    //Enables the buttons in the options menu
    public void GoToOptions()
    {
        buttons.SetActive(false);
        volumeSliders.gameObject.SetActive(true);
        background.rectTransform.sizeDelta += backgroundDelta;
        background.rectTransform.localPosition -= backgroundOffset;
    }

    //Goes back to the main menu screen
    public void backToMenu()
    {
        buttons.SetActive(true);
        volumeSliders.SetActive(false);
        background.rectTransform.sizeDelta += -backgroundDelta;
        background.rectTransform.localPosition += backgroundOffset;

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

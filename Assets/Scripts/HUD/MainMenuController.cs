﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Components;
using Unity.Entities;
using Unity;
using UnityEngine.Networking;
using Assets.Scripts.HUD;
using System.Windows;
public class MainMenuController : MonoBehaviour
{

    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject volumeSliders;
    [SerializeField] private Image background;
    private Vector2 backgroundDelta = new Vector2(300, 400);
    private Vector3 backgroundOffset = new Vector2(0, 75);
    private IpRequestManager requestManager;


    //Enables the buttons in the options menu
    public void GoToOptions()
    {
        buttons.SetActive(false);
        volumeSliders.gameObject.SetActive(true);
        background.rectTransform.sizeDelta += backgroundDelta;
        background.rectTransform.localPosition -= backgroundOffset;
    }



    //Goes back to the main menu screen
    public void BackToMenuFromVolumeSliders()
    {
        buttons.SetActive(true);
        volumeSliders.SetActive(false);
        background.rectTransform.sizeDelta += -backgroundDelta;
        background.rectTransform.localPosition += backgroundOffset;

    }

    #region SoundOptions
    //Changes the game volume
    public void SetMasterVolume(float val)
    {
        Debug.Log("Master volume: " + val);
    }

    //Sets the sound effect volume 
    public void SetSFXVolume(float val)
    {
        Debug.Log("SFX volume: " + val);
    }

    //Sets the music volume
    public void SetMusicVolume(float val)
    {
        Debug.Log("Music volume: " + val);
    }

    #endregion

    public void PlayGame()
    {
        Application.LoadLevel(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}

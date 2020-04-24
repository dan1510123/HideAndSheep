using System.Collections;
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
    [SerializeField] private GameObject playOptions;
    [SerializeField] private GameObject HostOptions;
    [SerializeField] private GameObject JoinOptions;
    private readonly string getURL = "https://api.ipify.org";
    private string publicIp;
    private Vector2 backgroundDelta = new Vector2(300, 400);
    private Vector3 backgroundOffset = new Vector2(0, 75);
    private IpRequestManager requestManager;
    private Text hostIpText;

    public void Start()
    {
        requestManager = transform.gameObject.AddComponent<IpRequestManager>();
        hostIpText = HostOptions.GetComponentInChildren<Text>();
        StartCoroutine(GetHostIp());

    }

    #region GoTos
    //Enables the buttons in the options menu
    public void GoToOptions()
    {
        buttons.SetActive(false);
        volumeSliders.gameObject.SetActive(true);
        background.rectTransform.sizeDelta += backgroundDelta;
        background.rectTransform.localPosition -= backgroundOffset;
    }

    //Go to the singleplayer & multiplayer menu
    public void GoToPlayOptions()
    {
        playOptions.SetActive(true);
        buttons.SetActive(false);
    }

    public void GoToHostOptions()
    {
        background.rectTransform.sizeDelta += backgroundDelta * .83f;
        playOptions.SetActive(false);
        HostOptions.SetActive(true);
    }

    public void GotToJoinOptions()
    {
        background.rectTransform.sizeDelta += backgroundDelta * .83f;
        playOptions.SetActive(false);
        JoinOptions.SetActive(true);
    }
    #endregion

    #region BackTos
    //Goes back to the main menu screen
    public void BackToMenuFromVolumeSliders()
    {
        buttons.SetActive(true);
        volumeSliders.SetActive(false);
        background.rectTransform.sizeDelta += -backgroundDelta;
        background.rectTransform.localPosition += backgroundOffset;

    }

    //Goes back to main menu from play options menu
    public void BackToMenuFromPlayOptions()
    {
        buttons.SetActive(true);
        playOptions.SetActive(false);
    }

    //Goes back to the play options menu
    public void BackToPlayOptionsFromHost()
    {
        background.rectTransform.sizeDelta -= backgroundDelta * .83f;
        playOptions.SetActive(true);
        HostOptions.SetActive(false);
    }

    public void BackToPlayOptionsFromJoin()
    {
        background.rectTransform.sizeDelta -= backgroundDelta * .83f;
        playOptions.SetActive(true);
        JoinOptions.SetActive(false);
    }
    #endregion

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

    #region PlayOptions
    /// <summary>
    /// Plays the game
    /// </summary>
    public void PlayGame()
    {
        Application.LoadLevel(1);
    }

    /// <summary>
    /// Plays the game
    /// </summary>
    /// <param name="modifier">Value is 0 if host, 1 if client</param>
    public void PlayGame(int modifier)
    {
        Application.LoadLevel(1);
    }


    public void quitGame()
    {
        Application.Quit();
    }
    #endregion

    #region Helpers
    private IEnumerator GetHostIp()
    {
        float loopTime = 0;
        yield return StartCoroutine(requestManager.GetHostIp(getURL));

        //while (!requestManager.requestFinished && loopTime < 2)
        //{
        //    loopTime += Time.deltaTime;
        //}
        //yield return new WaitForSeconds(.5f);
        publicIp = requestManager.HostIp;

        hostIpText.text = "Your public IP is " + publicIp + "\n Send this to your friend so they can join.";
        yield return null;
    }
    #endregion
}

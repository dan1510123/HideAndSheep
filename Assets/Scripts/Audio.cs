using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private static AudioSource backgroundMusic;
    private static AudioSource projectileSound;
    private static AudioSource deathSound;
    private static AudioSource enemySound;
    private static AudioSource shootingSound;
    private static AudioSource itemSound;
    private static AudioSource menuSound;
    public static bool isMusicPlaying = false;
    public static float sfxVol = .5f;
    public static float musicVol = .5f;
    public static float masterVol = .5f;

    // Start is called before the first frame update

    //private void Awake()
    //{
    //    if (!isMusicPlaying)
    //    {
    //        backgroundMusic.playOnAwake = true;
    //    }
    //}

    void Start()
    {
        Debug.Log(isMusicPlaying);
        AudioSource[] sources = GetComponents<AudioSource>();
        backgroundMusic = sources[0];
        projectileSound = sources[1];
        deathSound = sources[2];
        enemySound = sources[3];
        shootingSound = sources[4];
        itemSound = sources[5];
        menuSound = sources[6];

        DontDestroyOnLoad(this.gameObject);

        Application.LoadLevel(1);
    }

    public static void PlayProjectileSound()
    {
        projectileSound.Play();
    }

    public static void PlayEnemySound()
    {
        enemySound.Play();
    }

    public static void PlayShootingSound()
    {
        shootingSound.Play();
    }

    public static void PlayDeathSound()
    {
        deathSound.Play();
    }

    public static void PlayItemSound()
    {
        itemSound.Play();
    }

    public static void PlayMenuSound()
    {
        menuSound.Play();
    }

    public static void SetMasterVolume(float val)
    {
        masterVol = val;
        UpdateVolumes();
        //SetSFXVolume(val);
        //SetMusicVolume(val);
    }
    public static void SetSFXVolume(float val)
    {
        sfxVol = val;
        projectileSound.volume = sfxVol * masterVol;
        enemySound.volume = sfxVol * masterVol;
        shootingSound.volume = sfxVol * masterVol;
        deathSound.volume = sfxVol * masterVol;
        itemSound.volume = sfxVol * masterVol;
        menuSound.volume = sfxVol * masterVol;
    }

    private static void UpdateVolumes()
    {
        projectileSound.volume = sfxVol * masterVol;
        enemySound.volume = sfxVol * masterVol;
        shootingSound.volume = sfxVol * masterVol;
        deathSound.volume = sfxVol * masterVol;
        itemSound.volume = sfxVol * masterVol;
        menuSound.volume = sfxVol * masterVol;
        backgroundMusic.volume = musicVol * masterVol;
    }

    public static void SetMusicVolume(float val)
    {
        musicVol = val;
        backgroundMusic.volume = musicVol * masterVol;
    }


}

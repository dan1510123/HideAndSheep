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

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("STARTING");
        AudioSource[] sources = GetComponents<AudioSource>();
        backgroundMusic = sources[0];
        projectileSound = sources[1];
        deathSound = sources[2];
        enemySound = sources[3];
        shootingSound = sources[4];
        itemSound = sources[5];
        menuSound = sources[6];

        DontDestroyOnLoad(this.gameObject);
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

    public static void SetSFXVolume(float val)
    {
        projectileSound.volume = val;
        enemySound.volume = val;
        shootingSound.volume = val;
        deathSound.volume = val;
        itemSound.volume = val;
        menuSound.volume = val;
    }

    public static void SetMusicVolume(float val)
    {
        backgroundMusic.volume = val;
    }


}

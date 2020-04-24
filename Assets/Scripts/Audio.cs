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

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        backgroundMusic = sources[0];
        projectileSound = sources[1];
        deathSound = sources[2];
        enemySound = sources[3];
        shootingSound = sources[4];
        itemSound = sources[5];
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

}

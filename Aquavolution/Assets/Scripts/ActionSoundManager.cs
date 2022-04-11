using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSoundManager : MonoBehaviour
{
    public static AudioClip PlayerDeathSound, EatingSound, PlayerDamageSound, PlayerEnemyEqualSizeSound;

    static AudioSource AudioSrc;

    // Start is called before the first frame update
    void Start()
    {
        PlayerDeathSound = Resources.Load<AudioClip>("death-sound2");
        EatingSound = Resources.Load<AudioClip>("eating-sound");
        PlayerDamageSound = Resources.Load<AudioClip>("damage-sound3");
        PlayerEnemyEqualSizeSound = Resources.Load<AudioClip>("boing-sound");

        AudioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound (string Clip)
    {
        switch (Clip) {
            case "eat": 
                AudioSrc.PlayOneShot(EatingSound);
                break;
            case "die": 
                AudioSrc.PlayOneShot(PlayerDeathSound);
                break;
            case "damage":
                AudioSrc.PlayOneShot(PlayerDamageSound);
                break;
            case "boing":
                AudioSrc.PlayOneShot(PlayerEnemyEqualSizeSound);
                break;
        }

    }
}

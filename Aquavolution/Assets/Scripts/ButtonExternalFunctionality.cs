using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExternalFunctionality : MonoBehaviour
{
    public AudioSource MySounds;
    public AudioClip ClickSound;

    public void ButtonClickSound() 
    {
        MySounds.PlayOneShot(ClickSound);
    }
}

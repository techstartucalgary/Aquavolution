using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusicManager : MonoBehaviour
{
    [SerializeField] 
    Image SoundOnIcon;
    [SerializeField] 
    Image SoundOffIcon;
    private bool Muted = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Muted"))
        {
            PlayerPrefs.SetInt("Muted", 0);
            Load();
        } 
        else 
        {
            Load();
        }
        
        UpdateButtonIcon();
        AudioListener.pause = Muted;
    }

    public void OnButtonPress()
    {
        if(Muted == false)
        {
            Muted = true;
            AudioListener.pause = true;
        } 
        else 
        {
            Muted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        if (Muted == false)
        {
            SoundOnIcon.enabled = true;
            SoundOffIcon.enabled = false;
        }
        else 
        {
            SoundOnIcon.enabled = false;
            SoundOffIcon.enabled = true;
        }
    }

    private void Load()
    {
        Muted = PlayerPrefs.GetInt("Muted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("Muted", Muted ? 1 : 0);
    }
}

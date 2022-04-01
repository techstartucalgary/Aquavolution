using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UserInterface : MonoBehaviour
{
    public GameObject LevelUpButton;
    
    public static void UpdateHealthBar() 
    {
        string HeartName = "Heart (" + PlayerStats.Health + ")";
        GameObject TargetHeart = GameObject.Find(HeartName);
        TargetHeart.GetComponent<Image>().enabled = false;
    }

    public void DisplayLevelUp(bool state)
    {
        LevelUpButton.SetActive(state);
    }
}

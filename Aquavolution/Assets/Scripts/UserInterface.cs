using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    public GameObject LevelUpButton;
    
    public static void UpdateHealthBar() 
    {
        string HeartName = "Heart (" + PlayerStats.Health + ")";
        GameObject TargetHeart = GameObject.Find(HeartName);
        TargetHeart.SetActive(false);
    }

    public void DisplayLevelUp(bool state)
    {
        LevelUpButton.SetActive(state);
    }
}

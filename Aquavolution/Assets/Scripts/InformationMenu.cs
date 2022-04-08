using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InformationMenu : MonoBehaviour
{
    public GameObject InformationMenuScreen;

    public void ShowInformationMenu()
    {
        InformationMenuScreen.SetActive(true);
    }

    public void Back()
    {
        SceneManager.LoadScene("SplashScene");
    }
}

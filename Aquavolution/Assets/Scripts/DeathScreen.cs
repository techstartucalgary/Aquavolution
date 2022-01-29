using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public Text ScoreText;

    public void Setup(int FinalScore)
    {
        gameObject.SetActive(true);
        ScoreText.text = "Final Score: " + FinalScore.ToString();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("SplashScene");
    }
}

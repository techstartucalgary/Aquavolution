using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] 
    private Text ScoreCount;
    public static int FoodCount;
    public static int Health;
    private static float SizeChange = 0.05F;
    private Vector3 ScaleIncrease = new Vector3(SizeChange, SizeChange, 0);
    GameObject Player;
    GameController GameController;

    // Start is called before the first frame update
    void Start()
    {
        //Set up initial values when the scene starts
        FoodCount = 0;
        Health = 5;
        Player = gameObject;
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        //set initial transform scale for player
        Player.transform.localScale = new Vector3(1, 1, 1);
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.tag == "Food") 
        {
            IncreaseFood(1);        
        }

        if (Col.gameObject.tag == "Waste")
        {
            DecreaseHealth();
        }
    }

    //method is called whenever a collision is detected
    void OnCollisionEnter2D(Collision2D Col)
    {
        if (Col.gameObject.tag == "Enemy")
        {
            // Get behavior script of enemy we touch
            EnemyBehavior EnemyScript = Col.gameObject.GetComponent<EnemyBehavior>();

            // Lose health if player hits an enemy larger than them
            if (FoodCount < EnemyScript.Size)
            {
                DecreaseHealth();
            }
            // Eat the enemy and gain their size as food if player is larger than them
            if (FoodCount > EnemyScript.Size)
            {
                EnemyScript.GetEaten();
                IncreaseFood(EnemyScript.Size);
            }
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
        GameController.GameOver(FoodCount);
    }

    void DecreaseHealth() {
        
        UserInterface.UpdateHealthBar(); //update heath bar UI

        Health -= 1; //decrease player health

        if (Health <= 0)
        {
            Die();
        } 
    }

    void IncreaseFood(int IncreaseVal)
    {
        FoodCount += IncreaseVal;
        DisplayScoreToScreen(FoodCount);
    }

    void DisplayScoreToScreen(int FoodCount){
        ScoreCount.text = "Score: " + FoodCount; //display score to screen
        Player.transform.localScale += ScaleIncrease; //increases size by ScaleIncrease
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static int FoodCount = 0;
    public static int Health = 5;
    private string FoodTag = "Food";
    [SerializeField] private Text ScoreCount;
    private string EnemyTag = "Enemy";
    private static float SizeChange = 0.2F;
    private Vector3 ScaleIncrease = new Vector3(SizeChange, SizeChange, 0);

    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject;
        //set initial transform scale for player
        Player.transform.localScale = new Vector3(1, 1, 1);
    }

    void Update()
    {
        
    }

    //method is called whenever a collision is detected
    void OnCollisionEnter2D(Collision2D Col)
    {
        //on collision with an object of type food
        if (Col.gameObject.tag == FoodTag) 
        {
            FoodCount++;

            ScoreCount.text = "Score: " + FoodCount; //display score to screen
            //Debug.Log("New food count: " + FoodCount);

            Player.transform.localScale += ScaleIncrease; //increases size by ScaleIncrease
        }

        if (Col.gameObject.tag == EnemyTag)
        {
            DecreaseHealth();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

    void DecreaseHealth() {
        
        UserInterface.UpdateHealthBar(); //update heath bar UI

        Health -= 1; //decrease player health

        if (Health <= 0)
        {
            Die();
        } 
    }
}

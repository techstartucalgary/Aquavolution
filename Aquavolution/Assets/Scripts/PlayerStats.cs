using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] 
    private Text ScoreCount;
    public int FoodCount;
    public int BossThreshold;
    public static int Health;
    private static float SizeChange = 0.05F;
    private Vector3 ScaleIncrease = new Vector3(SizeChange, SizeChange, 0);
    GameObject Player;
    GameController GameController;
    private SpawnBoss SpawnBoss;
    public GameObject Canvas;
    private UserInterface UI;
    public float KnockbackDuration = 1;
    public float KnockbackPower = 4;

    public static int PlayerLevel = 1;

    static Animator animator;

    private static string CurrentSkin;

    void Start()
    {
        // Had to put a delay so we can find game objects 
        StartCoroutine("SetupPlayer");
        UpdateAnimator();
    }

    IEnumerator SetupPlayer()
    {
        Health = 5;
        Player = gameObject;
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        UI = Canvas.GetComponent<UserInterface>();
        Player.transform.localScale = new Vector3(1, 1, 1);
        yield return new WaitForSeconds(3);
        SpawnBoss = GameObject.Find("Room4(Clone)").GetComponent<SpawnBoss>();
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.tag == "Food") 
        {
            IncreaseFood(1); 
            animator.SetTrigger("eating");
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
            if (FoodCount <= EnemyScript.Size)
            {
                StartCoroutine(MouseFollow.instance.Knockback(KnockbackDuration, KnockbackPower, Col.transform));
                DecreaseHealth();
            }
            // Eat the enemy and gain their size as food if player is larger than them
            if (FoodCount > EnemyScript.Size)
            {
                EnemyScript.GetEaten();
                IncreaseFood(EnemyScript.Size);
                animator.SetTrigger("eating");
            }
        }

        if (Col.gameObject.tag == "Waste")
        {
            DecreaseHealth();
        }
    }

    void Die()
    {
        animator.SetTrigger("dead");
        GameController.GameOver(FoodCount);
    }

    public void DecreaseHealth() {
        
        if (Health > 0)
        {
            UserInterface.UpdateHealthBar(); //update heath bar UI
            Health -= 1; //decrease player health
        }
        if (Health <= 0)
        {
            Die();
        } 
    }

    void IncreaseFood(int IncreaseVal)
    {
        FoodCount += IncreaseVal;
        DisplayScoreToScreen(FoodCount);

        if (FoodCount % 10 == 0)
            UI.DisplayLevelUp(true);

        if (FoodCount == BossThreshold)
            SpawnBoss.ThresholdMet();
    }

    void DisplayScoreToScreen(int FoodCount){
        ScoreCount.text = "Score: " + FoodCount; //display score to screen
        Player.transform.localScale += ScaleIncrease; //increases size by ScaleIncrease
    }

    public static void IncreaseLevel() {
        PlayerLevel += 1;
        UpdateAnimator();
    }

    static void UpdateAnimator() {
        CurrentSkin = "Skin " + PlayerLevel;
        animator = GameObject.Find(CurrentSkin).GetComponent<Animator>();    
    }
}
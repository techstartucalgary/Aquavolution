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
    private GameObject CurrSkin;

    void Start()
    {
        PlayerLevel = 1;
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
        if (Health > 0)
        {
            if (Col.gameObject.tag == "Enemy")
            {
                // Get behavior script of enemy we touch
                EnemyBehavior EnemyScript = Col.gameObject.GetComponent<EnemyBehavior>();

                // Only get knocked back if same size as enemy
                if (FoodCount == EnemyScript.Size)
                {
                    ActionSoundManager.PlaySound("boing");
                    StartCoroutine(MouseFollow.instance.Knockback(KnockbackDuration, KnockbackPower, Col.transform));
                }
                // Lose health if player hits an enemy larger than them
                if (FoodCount < EnemyScript.Size)
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
    }

    void Die()
    {
        ActionSoundManager.PlaySound("die");
        animator.SetTrigger("dead");
        GameController.GameOver(FoodCount);
    }

    void ChangePlayerSkinColor()
    {
        CurrSkin = GameObject.Find(CurrentSkin);
        CurrSkin.GetComponent<SkinTakesDamage>().ChangeSkinColor();
    } 

    public void DecreaseHealth() 
    {
        ActionSoundManager.PlaySound("damage");
        ChangePlayerSkinColor();

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

    bool CheckBetween(int FoodCount)
    {
        switch (FoodCount)
        {
            case int n when (n >= 10 && n < 20) && PlayerLevel == 1:
                return true;
            case int n when (n >= 20 && n < 30) && PlayerLevel == 2:
                return true;
            case int n when (n >= 30 && n < 40) && PlayerLevel == 3:
                return true;
            case int n when (n >= 40 && n < 50) && PlayerLevel == 4:
                return true;
            case int n when (n >= 50 && n < 60) && PlayerLevel == 5:
                return true;
            case int n when (n >= 60 && n < 70) && PlayerLevel == 6:
                return true;
            default: return false;    
        }
    }

    void IncreaseFood(int IncreaseVal)
    {
        if (Health > 0)
        {
            ActionSoundManager.PlaySound("eat");
            FoodCount += IncreaseVal;
        }
        
        DisplayScoreToScreen(FoodCount);

        if (CheckBetween(FoodCount))
            UI.DisplayLevelUp(true);

        if (FoodCount == BossThreshold)
            SpawnBoss.ThresholdMet();
    }

    void DisplayScoreToScreen(int FoodCount){
        ScoreCount.text = "Score: " + FoodCount; //display score to screen
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

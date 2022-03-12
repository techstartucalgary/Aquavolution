using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{   
    private Rigidbody2D RB;
    public float MoveSpeed;
    public int Size;
    private bool Patrolling;
    private static float SizeChange = 0.05F;
    private Vector3 ScaleIncrease = new Vector3(SizeChange, SizeChange, 0);
    
    public Transform SightStart, SightEnd;
    private bool Collision;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        RunBehavior();
        Size = 1;
    }

    void Update() 
    {
        // Tilemaps are set to Layer Solid, which makes Collision True if enemy hits a Solid layer
        Collision = Physics2D.Linecast(SightStart.position, SightEnd.position, 1 << LayerMask.NameToLayer("Solid"));

        if (Collision) 
        {
            FlipEnemy();
        }
    }

    void FixedUpdate()
    {        
        if (Patrolling)
        {
            RB.velocity = new Vector2(transform.localScale.x, 0) * MoveSpeed;
        }
    }

    public void FlipEnemy() 
    {
        transform.localScale = new Vector3(transform.localScale.x == gameObject.transform.localScale.x ? -gameObject.transform.localScale.x : gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }

    void RunBehavior()
    {
        Patrolling = true;
    }

    public void GetEaten()
    {
        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D Col)
    {
        //on collision with an object of type food
        if (Col.gameObject.tag == "Food") 
        {
            IncreaseFood(1);            
        }
        if (Col.gameObject.tag == "Enemy")
        {
            FlipEnemy();
        }
        if (Col.gameObject.tag == "Waste")
        {
            // Size--;
            // if (Size < 0){
            //     GetEaten();
            // }
            FlipEnemy();
        }
    }

    void IncreaseFood(int IncreaseVal)
    {
        Size += IncreaseVal;
        gameObject.transform.localScale += ScaleIncrease; //increases size by ScaleIncrease
    }
}
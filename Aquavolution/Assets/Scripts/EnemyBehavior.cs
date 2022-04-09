using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{   
    private Rigidbody2D RB;
    public float MoveSpeed;
    public int Size;
    private int MaxSize = 15;
    private static float SizeChange = 0.05F;
    private Vector3 ScaleIncrease = new Vector3(SizeChange, SizeChange, 0);
    
    public Transform SightStart, SightEnd;
    private bool Collision;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
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
        RB.velocity = new Vector2(transform.localScale.x, 0) * MoveSpeed;
    }

    public void FlipEnemy() 
    {
        transform.localScale = new Vector3(transform.localScale.x == gameObject.transform.localScale.x ? -gameObject.transform.localScale.x : gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }

    public void GetEaten()
    {
        if (gameObject.name == "Enemy_Shark")
        {
            gameObject.transform.GetComponentInParent<Victory>().SharkEaten();
        }
        
        Animator AnimController = gameObject.GetComponent<Animator>();
        AnimController.SetTrigger("Eaten");
        gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, .5f);
    }

    void OnCollisionEnter2D(Collision2D Col)
    {
        if (Col.gameObject.tag == "Enemy")
        {
            FlipEnemy();
        }
        if (Col.gameObject.tag == "Waste")
        {
            FlipEnemy();
        }
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.tag == "Food")
        {
            IncreaseFood(1);
        }
    }

    void IncreaseFood(int IncreaseVal)
    {
        Size += IncreaseVal;
        if (gameObject.transform.localScale.x < MaxSize && gameObject.transform.localScale.y < MaxSize && gameObject.transform.localScale.z < MaxSize)
            gameObject.transform.localScale += ScaleIncrease; //increases size by ScaleIncrease
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // TODO: Why do we need these?

    // private Transform Transform;
    // private SpriteRenderer SR;
    // private float StartPosition;
    // public float PatrolLength;
    // private Vector3 OldPos = Vector3.zero;
    
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
        // SR = GetComponent<SpriteRenderer>();
        RB = GetComponent<Rigidbody2D>();
        RunBehavior();
        // StartPosition = RB.position.x;
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
            // RB.MovePosition(new Vector2((Mathf.Sin((2 * Mathf.PI * (Time.time*MoveSpeed/PatrolLength)) - (Mathf.PI / 2)) * (PatrolLength/2) + (PatrolLength/2))+StartPosition,RB.position.y));
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
    }

    void IncreaseFood(int IncreaseVal)
    {
        Size += IncreaseVal;
        gameObject.transform.localScale += ScaleIncrease; //increases size by ScaleIncrease
    }
}
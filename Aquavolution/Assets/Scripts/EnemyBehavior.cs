using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Transform Transform;
    private SpriteRenderer SR;
    private Rigidbody2D RB;
    public float PatrolLength;
    public int MoveSpeed;
    private bool Patrolling;
    private float StartPosition;
    private Vector3 OldPos = Vector3.zero;
    private static float SizeChange = 0.2F;
    private Vector3 ScaleIncrease = new Vector3(SizeChange, SizeChange, 0);
    public int Size;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        RB = GetComponent<Rigidbody2D>();
        RunBehavior();
        StartPosition = RB.position.x;
    }

    void RunBehavior()
    {
        Patrolling = true;
    }

    void FixedUpdate()
    {        
        if (Patrolling)
        {
            RB.MovePosition(new Vector2((Mathf.Sin((2 * Mathf.PI * (Time.time*MoveSpeed/PatrolLength)) - (Mathf.PI / 2)) * (PatrolLength/2) + (PatrolLength/2))+StartPosition,RB.position.y));
        }
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
    }

    void IncreaseFood(int IncreaseVal)
    {
        Size += IncreaseVal;
        gameObject.transform.localScale += ScaleIncrease; //increases size by ScaleIncrease
    }
}

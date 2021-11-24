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

    // Start is called before the first frame update
    void Start()
    {
        Transform = GetComponent<Transform>();
        SR = GetComponent<SpriteRenderer>();
        RB = GetComponent<Rigidbody2D>();
        RunBehavior();
        StartPosition = RB.position.x;
    }

    void RunBehavior()
    {
        Patrolling = true;
    }

    void Update()
    {        
        if (Patrolling)
        {
            RB.MovePosition(new Vector2((Mathf.Sin((2 * Mathf.PI * (Time.time*MoveSpeed/PatrolLength)) - (Mathf.PI / 2)) * (PatrolLength/2) + (PatrolLength/2))+StartPosition,RB.position.y));
        }
    }
}

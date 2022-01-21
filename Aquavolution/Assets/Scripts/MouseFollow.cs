using UnityEngine;
using System.Collections;
using System;

public class MouseFollow : MonoBehaviour
{
    Vector2 MousePosition;
    public float StartingMoveSpeed;

    public float SurfaceHeight;
    public float Gravity;
    public float MoveDisabledTime;

    private float JumpTime;
    
    [SerializeField]
    private float MinSpeed;

    [SerializeField]
    private float SlowdownFactor;

    private Rigidbody2D Rb;
    private Transform Transform;
    private Vector2 Position = new Vector2(0f, 0f);

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        if (transform.position.y >= SurfaceHeight)
            Rb.gravityScale = Gravity;
        else
        {
            Rb.gravityScale = 0;
            Rb.AddForce((MousePosition - (Vector2)transform.position).normalized * GetMoveSpeed());
        }
        transform.up = MousePosition - (Vector2)transform.position;

        if (transform.localEulerAngles.z < 180)
            GetComponent<SpriteRenderer>().flipX = true;
        else
            GetComponent<SpriteRenderer>().flipX = false;
    }

    // Returns move speed, which gets lower as scale increases, to a minimum speed
    private float GetMoveSpeed()
    {
        float Scale = transform.localScale.x;

        float Speed = 0.0f;

        if (Scale <= 1.5f)
        {
            Speed = StartingMoveSpeed;
        }
        else 
        {
            float Slowdown = Scale * SlowdownFactor;
            if (StartingMoveSpeed - Slowdown <= MinSpeed) 
            {
                Speed = MinSpeed;                
            }
            else 
            {
                Speed = StartingMoveSpeed - Slowdown;
            }
        }

        float SpeedRatio = Math.Max(GetMouseRatio().x, GetMouseRatio().y);

        return Speed * SpeedRatio;
    }

    // Gets ratio of mousepos:screenheight, where 0 is center, 1.0 is max height/width
    private Vector2 GetMouseRatio()
    {
        float YRatio = Math.Abs(2*Input.mousePosition.y/Screen.height - 1);
        float XRatio = Math.Abs(2*Input.mousePosition.x/Screen.width - 1);

        if ((Input.mousePosition.y > Screen.height) || (Input.mousePosition.y < 0))
            YRatio = 1;
        
        if ((Input.mousePosition.x > Screen.width) || (Input.mousePosition.x < 0))
            XRatio = 1;

        // Debug.Log("Mouse Ratio: X, Y:" + XRatio + ", " + YRatio);

        return new Vector2(XRatio, YRatio);
    }
}
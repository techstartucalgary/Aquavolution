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
        // gets in pixels
        MousePosition = Input.mousePosition;
        
        // convert to world
        MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        if (transform.position.y >= SurfaceHeight)
            Rb.gravityScale = Gravity;
        else
        {
            Rb.gravityScale = 0;
            float SpeedRatio = Math.Max(GetMouseRatio().x, GetMouseRatio().y);
            // transform.position = Vector2.MoveTowards(transform.position, MousePosition, GetMoveSpeed() * Time.fixedDeltaTime * SpeedRatio);
            Rb.AddForce((MousePosition - (Vector2)transform.position).normalized * GetMoveSpeed() * SpeedRatio);
            
            transform.up = MousePosition - (Vector2)transform.position;
        }
    }

    // Returns move speed, which gets lower as scale increases, to a minimum speed
    private float GetMoveSpeed()
    {
        float Scale = transform.localScale.x;

        if (Scale <= 1.5f)
        {
            return StartingMoveSpeed;
        }
        else 
        {
            float Slowdown = Scale * SlowdownFactor;
            if (StartingMoveSpeed - Slowdown <= MinSpeed) 
            {
                return MinSpeed;                
            }
            else 
            {
                return StartingMoveSpeed - Slowdown;
            }
        }
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

using UnityEngine;
using System.Collections;
using System;

public class MouseFollow : MonoBehaviour
{
    public static MouseFollow instance;

    Vector2 MousePosition;
    public float StartingMoveSpeed;
    public float SurfaceHeight;
    public float Gravity;
    [SerializeField]
    private float MinSpeed;

    [SerializeField]
    private float SlowdownFactor;
    private Rigidbody2D Rb;

    private void Awake() 
    {
        instance = this;   
    }

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

        return new Vector2(XRatio, YRatio);
    }

    public IEnumerator Knockback(float KnockbackDuration, float KnockbackPower, Transform obj)
    {
        float Timer = 0;

        while (KnockbackDuration > Timer)
        {
            Timer += Time.deltaTime;
            Vector2 Direction = (obj.transform.position - this.transform.position).normalized;
            Rb.AddForce(-Direction * KnockbackPower);
        }

        yield return 0;
    }
}
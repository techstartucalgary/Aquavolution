using UnityEngine;
using System.Collections;
using System;

public class MouseFollow : MonoBehaviour
{
    public static MouseFollow instance;
    Vector2 MousePosition;
    private Vector2 OldMousePos;
    public float Speed;
    public float MaxVelocity;
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
        if (PlayerStats.Health == 0) { return; }
        if (Vector2.SqrMagnitude(Rb.velocity) < MaxVelocity)
        {
            Vector2 Direction = (MousePosition - (Vector2)transform.position).normalized;            
            Rb.AddForce(Direction * GetMoveSpeed());

            OldMousePos = MousePosition;
        }
        else
        {
            Vector2 Direction = ((Vector2)transform.position - OldMousePos).normalized;
            Rb.AddForce(Direction * 1);
        }
        transform.up = MousePosition - (Vector2)transform.position;
    }

    private float GetMoveSpeed()
    {
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

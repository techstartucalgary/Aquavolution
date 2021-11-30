using UnityEngine;
using System.Collections;

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
        
        if (MousePosition.x < 0)
            MousePosition.x = 0;
        if (MousePosition.x > Screen.width)
            MousePosition.x = Screen.width;
        if (MousePosition.y < 0)
            MousePosition.y = 0;
        if (MousePosition.y > Screen.height)
            MousePosition.y = Screen.height;
        
        // convert to world
        MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);
        StartCoroutine("Move");
    }

    IEnumerator Move()
    {
        // If we're above the surface, we're affected by gravity and fall down
        if (transform.position.y >= SurfaceHeight)
        {
            Rb.gravityScale = Gravity;
            yield return new WaitForSeconds(MoveDisabledTime);
            Rb.gravityScale = 0;
        }
        else if (Rb.gravityScale == 0)
        {
            Position = Vector2.Lerp(transform.position, MousePosition, GetMoveSpeed() * Time.fixedDeltaTime);
            Rb.MovePosition(Position);
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
}

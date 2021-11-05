using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public int foodCount = 0;
    private double size = 1;
    private Vector3 scale = new Vector3(1, 1, 1);

    GameObject player;

    void OnCollisionEnter2D(Collision2D Col)        // This method is called whenever a collision is detected, and passes the Component which Food collided with
    {
        Debug.Log("collided with " + Col.collider.ToString().Substring(0, 4));
        if (Col.collider.ToString().Substring(0, 4) == "Food") {
            foodCount++;
            size += 0.2;
            scale.Set((float)size, (float)size, 1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        player.transform.localScale = scale;
    }
}

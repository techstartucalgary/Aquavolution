using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D Col)        // This method is called whenever a collision is detected, and passes the Component which Food collided with
    {
        // Destroy food only if collides with player or enemy
        if (Col.gameObject.tag == "Player" || Col.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);                // Sets Food to inactive, so it does not show up. Could also delete entirely
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public int FoodCount = 0;
    private static float SizeChange = 0.2F;
    private Vector3 ScaleIncrease = new Vector3(SizeChange, SizeChange, 0);

    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject;
        Player.transform.localScale = new Vector3(1, 1, 1); //set initial transform scale fro player
    }

    void OnCollisionEnter2D(Collision2D Col) //method is called whenever a collision is detected
    {
        //print statement
        Debug.Log("collided with " + Col.gameObject.tag);

        //on collision with an object of type food
        if (Col.gameObject.tag == "Food") {
            FoodCount++;
            Player.transform.localScale += ScaleIncrease; //increases size by ScaleIncrease
        }
    }
}
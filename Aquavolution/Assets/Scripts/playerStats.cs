using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int FoodCount = 0;
    private string FoodTag = "Food";
    private static float SizeChange = 0.2F;
    private Vector3 ScaleIncrease = new Vector3(SizeChange, SizeChange, 0);

    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject;
        //set initial transform scale for player
        Player.transform.localScale = new Vector3(1, 1, 1);
    }

    //method is called whenever a collision is detected
    void OnCollisionEnter2D(Collision2D Col)
    {
        //on collision with an object of type food
        if (Col.gameObject.tag == FoodTag) {
            FoodCount++;
            Debug.Log("New food count: " + FoodCount);
            Player.transform.localScale += ScaleIncrease; //increases size by ScaleIncrease
        }
    }
}

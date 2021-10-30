using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    public GameObject FoodPrefab;
    public Camera Cam;
    private int ScreenWidth;
    private int ScreenHeight;
    private System.Random RNG;
    private Vector3 SpawnPos;
    // Start is called before the first frame update
    void Start()
    {
        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;
        RNG = new System.Random();

        InvokeRepeating("randomSpawn", 0.5f, 0.5f);
    }

    // Gets x, y integers randomly between 0 and screen width and height in pixels
    // Then, turns the pixel values into world space units, and instantiates a food prefab at that location
    void randomSpawn()
    {
        int SpawnX = RNG.Next(0, ScreenWidth);
        int SpawnY = RNG.Next(0, ScreenHeight);
        
        SpawnPos = Cam.ScreenToWorldPoint(new Vector3(SpawnX, SpawnY, Cam.nearClipPlane));      // Converts pixel values to world-space 
        
        GameObject FoodObject = Instantiate(FoodPrefab, SpawnPos, Quaternion.identity);         // Instantiates Prefab
        FoodObject.SetActive(true);     // Enables prefab
    }
}

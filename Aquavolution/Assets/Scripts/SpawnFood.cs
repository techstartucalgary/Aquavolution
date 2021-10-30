using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    public GameObject FoodPrefab;
    public Camera Cam;
    public float Speed = 0.5f;
    private int ScreenWidth;
    private int ScreenHeight;
    private System.Random RNG;
    private Vector3 SpawnPos;
    
    void Start()
    {
        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;
        RNG = new System.Random();

        InvokeRepeating("randomSpawn", 0.5f, Speed);
    }

    // Gets x, y integers randomly between 0 and screen width and height in pixels,
    // then, turns the pixel values into world space units, and instantiates a food prefab at that location
    void randomSpawn()
    {
        int SpawnX = RNG.Next(0, ScreenWidth);
        int SpawnY = RNG.Next(0, ScreenHeight);
        
        // Converts pixel values to world-space
        SpawnPos = Cam.ScreenToWorldPoint(new Vector3(SpawnX, SpawnY, Cam.nearClipPlane));
        // Instantiates Prefab
        GameObject FoodObject = Instantiate(FoodPrefab, SpawnPos, Quaternion.identity);
        // Enables prefab
        FoodObject.SetActive(true);
    }
}

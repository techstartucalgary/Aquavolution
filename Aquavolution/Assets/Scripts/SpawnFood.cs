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

    // Update is called once per frame
    void Update()
    {

    }

    void randomSpawn()
    {
        int SpawnX = RNG.Next(0, ScreenWidth);
        int SpawnY = RNG.Next(0, ScreenHeight);
        Debug.Log("PX X: " + SpawnX + " PX Y: " + SpawnY);
        SpawnPos = Cam.ScreenToWorldPoint(new Vector3(SpawnX, SpawnY, Cam.nearClipPlane));
        Debug.Log("\nWorldSpace: " + SpawnPos);
        GameObject FoodObject = Instantiate(FoodPrefab, SpawnPos, Quaternion.identity);
        FoodObject.SetActive(true);
    }
}

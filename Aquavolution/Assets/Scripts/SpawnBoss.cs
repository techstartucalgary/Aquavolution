using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public float DistanceThreshold;
    public GameObject Player;
    public GameObject Shark;
    public GameObject SharkAnim;
    private bool IsThresholdMet = false;
    private bool SharkSpawned = false;

    void Update()
    {
        if ((Vector2.Distance(Player.transform.position, this.transform.position) < DistanceThreshold) && (IsThresholdMet) && (!SharkSpawned))
        {
            OpenDoor();
            Shark.SetActive(true);
            SharkSpawned = true;
        }        
    }

    // Player will call this method when they reach the score threshold
    public void ThresholdMet()
    {
        IsThresholdMet = true;
    }

    private void OpenDoor()
    {
        Destroy(this.transform.GetChild(1).Find("TopDoorGrid").gameObject);
    }
}
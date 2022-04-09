using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject Bubble;
    private LevelGeneration LevelGen;

    void Start()
    {
        LevelGen = gameObject.GetComponent<LevelGeneration>();
        InvokeRepeating("SpawnBubbles", 0, 0.6f);
    }

    void SpawnBubbles()
    {
        foreach (GameObject R in LevelGen.InstantiatedRooms)
        {
            if (R != null)
            {
                Instantiate(Bubble, GetPos(R), Quaternion.identity);
            }
        }
    }

    private Vector2 GetPos(GameObject _R)
    {
        Vector2 CenterPos = _R.transform.GetChild(0).position;
        float RandValue = Random.Range(-20, 20);
        return new Vector2(CenterPos.x + RandValue, CenterPos.y - 5);
    }
}

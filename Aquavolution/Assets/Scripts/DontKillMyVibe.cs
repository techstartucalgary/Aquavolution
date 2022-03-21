using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontKillMyVibe : MonoBehaviour
{
    private static DontKillMyVibe Instance = null;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        } 
        else 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}

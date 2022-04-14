using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public DeathScreen DeathScreen;

    public void GameOver(int FoodCount, int HighestCount)
    {
        DeathScreen.Setup(FoodCount, HighestCount);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

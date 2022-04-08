using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    public GameObject VictoryPopUp;

    public void SharkEaten()
    {
        VictoryPopUp.SetActive(true);
    }
}

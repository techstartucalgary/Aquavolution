using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePopper : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3);
    }
}

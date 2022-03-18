using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoomControl : MonoBehaviour
{
    public float ZoomSize = 5;
    private int PlayerSize;
    private int MaximumZoomSize = 8;

    void Update()
    {
        PlayerSize = PlayerStats.FoodCount + 1;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (ZoomSize > 5)
                ZoomSize -= 0.1f;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (ZoomSize < PlayerSize && ZoomSize < MaximumZoomSize)
                ZoomSize += 0.1f;
        }

        GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = ZoomSize;
        
    }
}

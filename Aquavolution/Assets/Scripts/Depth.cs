using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Depth : MonoBehaviour {
    [SerializeField]
    private Text DepthYText;
    private GameObject FinalRoom;

    void Start()
    {
        StartCoroutine("FindFinalRoom");
    }

    IEnumerator FindFinalRoom()
    {
        yield return new WaitForSeconds(.1f);
        FinalRoom = GameObject.Find("Room4(Clone)").transform.GetChild(0).gameObject;
    }

    private void Update ()
    {
        if (FinalRoom != null)
        {
            // Calculate distance value for x and y from surface origin
            float Distance = Vector2.SqrMagnitude((FinalRoom.transform.position - transform.position));
            Distance *= 0.001f;

            // to 2 decimal places "F2"
            if (Distance > 0.6f)
                DepthYText.text = Distance.ToString("F2") + " m away from the boss!";
            else
                DepthYText.text = "";
            
        }
	}
}

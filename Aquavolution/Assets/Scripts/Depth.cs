using UnityEngine;
using UnityEngine.UI;

public class Depth : MonoBehaviour {

    [SerializeField]
    private Transform oceanFloorX;

    [SerializeField]
    private Transform oceanFloorY;

    [SerializeField]
    private Text depthxText;

    [SerializeField]
    private Text depthyText;

    private float depthx;
    private float depthy;

    private void Update () {

        // Calculate distance value by x and y axis
        depthx = (transform.position.x - oceanFloorX.transform.position.x);

        depthy = (transform.position.y - oceanFloorY.transform.position.y);

        // to 2 decimal places "F2"
        depthxText.text = "x: " + depthx.ToString("F2") + " meters";
        depthyText.text = "y: " + depthy.ToString("F2") + " meters";

	}
}

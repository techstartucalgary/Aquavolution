using UnityEngine;
using UnityEngine.UI;

public class Depth : MonoBehaviour {

    [SerializeField]
    private Text DepthXText;

    [SerializeField]
    private Text DepthYText;

    private float DepthX;
    private float DepthY;

    [SerializeField]
    private GameObject SurfaceOrigin;

    private void Update ()
    {
        // Calculate distance value for x and y from surface origin
        DepthX = (SurfaceOrigin.transform.position.x - transform.position.x);
        DepthY = (SurfaceOrigin.transform.position.y - transform.position.y);

        // to 2 decimal places "F2"
        DepthXText.text = " "; //removed temporarily
        DepthYText.text = "Depth: " + DepthY.ToString("F2") + " meters";
	}
}

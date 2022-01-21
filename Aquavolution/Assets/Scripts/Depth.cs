using UnityEngine;
using UnityEngine.UI;

public class Depth : MonoBehaviour {

    [SerializeField]
    private Transform SurfaceOrigin;

    [SerializeField]
    private Text DepthXText;

    [SerializeField]
    private Text DepthYText;

    private float DepthX;
    private float DepthY;

    private void Update () {
        // X is taken out tempoarily, to be replaced with minimap UI

        // Calculate distance value by x and y axis
        //60 is hardcoded to current map, will be changed once map is tiled
        DepthX = (60 - transform.position.x - SurfaceOrigin.transform.position.x);
        DepthY = (60 - transform.position.y - SurfaceOrigin.transform.position.y);

        // to 2 decimal places "F2"
        //DepthXText.text = "x: " + depthx.ToString("F2") + " meters";
        DepthXText.text = "";
        DepthYText.text = "Depth: " + DepthY.ToString("F2") + " meters";
        
	}
}
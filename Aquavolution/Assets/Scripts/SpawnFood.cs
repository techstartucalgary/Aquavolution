using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    public GameObject Food;
    public Camera Cam;
    [SerializeField]
    private int MaxFood = 10;
    
    void Start()
    {
        randomSpawn();
    }

    // Gets x, y integers randomly between 0 and screen width and height in pixels,
    // then, turns the pixel values into world space units, and instantiates a food prefab at that location
    void randomSpawn()
    {
        for (int i = 0; i < MaxFood; i++){
            int SpawnX = Random.Range(0, Screen.width);
            int SpawnY = Random.Range(0, Screen.height);
            
            // Converts pixel values to world-space
            Vector3 SpawnPos = Cam.ScreenToWorldPoint(new Vector3(SpawnX, SpawnY, Cam.nearClipPlane));
            // Instantiates Prefab
            GameObject FoodObject = Instantiate(Food, SpawnPos, Quaternion.identity);
            // Enables prefab
            FoodObject.SetActive(true);
        }
    }
}

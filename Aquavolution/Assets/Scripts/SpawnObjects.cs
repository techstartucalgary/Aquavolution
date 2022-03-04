using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnObjects : MonoBehaviour
{
    public GameObject Food;
    public GameObject Enemy;
    public Camera Cam;
    public int MaxEnemy = 5;
    public float SpawnRate;

    private LevelGeneration LevelGenerator;

    void Start()
    {
        LevelGenerator = gameObject.GetComponent<LevelGeneration>();
        RandomSpawn(Enemy, MaxEnemy); 
        InvokeRepeating("GenerateFood", 0, SpawnRate);
    }

    // Gets x, y integers randomly between 0 and screen width and height in pixels,
    // then, turns the pixel values into world space units, and instantiates the given prefab at that location
    // Repeats this MaxCount amount of times
    void RandomSpawn(GameObject SpawnObj, int MaxCount)
    {
        foreach (GameObject R in LevelGenerator.InstantiatedRooms)
        {
            if (R == null)
                continue;

            for (int i = 0; i < MaxCount; i++)
            {
                // This code can be used to spawn different fish in different room types
                /*
                switch (R.name)
                {
                    case "Room0":
                        SpawnObj = Fish0;
                    case "Room1":
                        SpawnObj = Fish1;
                    case "Room2":
                        SpawnObj = Fish2;
                    case "Room3":
                        SpawnObj = Fish4;
                    default:
                        break;
                }
                */
                
                GameObject SpawnedObject = Instantiate(SpawnObj, GetLocation(R), Quaternion.identity);
                SpawnedObject.SetActive(true);
            }
        }
    }

    void GenerateFood()
    {
        foreach (GameObject R in LevelGenerator.InstantiatedRooms)
        {
            if (R == null)
                continue;

            GameObject SpawnedObject = Instantiate(Food, GetLocation(R), Quaternion.identity);
            SpawnedObject.SetActive(true);
        }
    }

    Vector3 GetLocation(GameObject R)
    {
        // Get Diamond icon in the room. This could be an empty game object instead
        Vector3 RCenter = R.transform.GetChild(0).position;

        float SpawnX = Random.Range(RCenter.x-10, RCenter.x+10);
        float SpawnY = Random.Range(RCenter.y-5, RCenter.y+5);

        return new Vector3(SpawnX, SpawnY, Cam.nearClipPlane);
    }
}

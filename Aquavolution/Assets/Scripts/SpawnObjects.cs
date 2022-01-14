using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject Food;
    public GameObject Enemy;
    public Camera Cam;
    public int MaxFood = 10;
    public int MaxEnemy = 5;

    public float Speed;

    void Start()
    {
        RandomSpawn(Enemy, MaxEnemy);

        // Repeatedly generate food objects at Speed 
        InvokeRepeating("Generate", 0, Speed);

    }

    // Gets x, y integers randomly between 0 and screen width and height in pixels,
    // then, turns the pixel values into world space units, and instantiates the given prefab at that location
    // Repeats this MaxCount amount of times
    void RandomSpawn(GameObject SpawnObj, int MaxCount)
    {
        for (int i = 0; i < MaxCount; i++)
        {
            int SpawnX = Random.Range(0, Screen.width);
            int SpawnY = Random.Range(0, Screen.height);

            // Converts pixel values to world-space
            Vector3 SpawnPos = Cam.ScreenToWorldPoint(new Vector3(SpawnX, SpawnY, Cam.nearClipPlane));
            // Instantiates Prefab
            GameObject SpawnedObject = Instantiate(SpawnObj, SpawnPos, Quaternion.identity);
            // Enables prefab
            SpawnedObject.SetActive(true);
        }
    }

    void Generate()
    {
        if (GameObject.FindGameObjectsWithTag("Food").Length < MaxFood)
        {
            RoomData CurrentRoom = GetPlayerRoom();
            float SpawnX = Random.Range((float)CurrentRoom.XZero, (float)CurrentRoom.XMax);
            float SpawnY = Random.Range((float)CurrentRoom.YZero, (float)CurrentRoom.YMax);

            Vector3 SpawnPos = new Vector3(SpawnX, SpawnY, Cam.nearClipPlane);

            // Instantiates Prefab
            Instantiate(Food, SpawnPos, Quaternion.identity).SetActive(true);
        }
    }

    // We'll improve this later once we have more rooms, for now it just returns room 1
    RoomData GetPlayerRoom()
    {
        return(Room1);
    }
}

using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnObjects : MonoBehaviour
{
    public GameObject Food;
    public GameObject PlasticBottle;
    public GameObject PlasticStraw;
    public int MaxPlastic = 3;

    public GameObject Enemy;

    public GameObject Enemy0;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;

    public Camera Cam;
    public int MaxEnemy = 5;
    public float SpawnRate;

    private LevelGeneration LevelGenerator;

    void Start()
    {
        LevelGenerator = gameObject.GetComponent<LevelGeneration>();
        RandomSpawn(Enemy1, MaxEnemy);
        GeneratePlastic(MaxPlastic);

        // Repeatedly generate food objects at Speed 
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
                string RoomNumber = R.name.Substring(0, 5);
                switch (RoomNumber) {
                    case "Room0":
                        SpawnObj = Enemy0;
                        break;
                    case "Room1":
                        SpawnObj = Enemy1;
                        break;
                    case "Room2":
                        SpawnObj = Enemy2;
                        break;
                    case "Room3":
                        SpawnObj = Enemy3;
                        break;
                    case "Room4":
                        SpawnObj = Enemy4;
                        break;
                    default:
                        SpawnObj = Enemy;
                        break;
                }

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

    void GeneratePlastic(int MaxCount)
    {
        foreach (GameObject R in LevelGenerator.InstantiatedRooms)
        {
            if (R == null)
                continue;

            for (int i = 0; i < MaxCount; i++)
            {
                GameObject SpawnedObject = Instantiate(PlasticBottle, GetLocation(R), Quaternion.identity);
                SpawnedObject.SetActive(true);
                SpawnedObject = Instantiate(PlasticStraw, GetLocation(R), Quaternion.identity);
                SpawnedObject.SetActive(true);
            }
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
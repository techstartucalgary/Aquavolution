using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnObjects : MonoBehaviour
{
    public GameObject Food;
    public GameObject PlasticBottle;
    public GameObject PlasticStraw;
    public int MaxPlastic = 3;
    public int MaxFood;
    public int FoodCount = 0;
    public GameObject Enemy0;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public Camera Cam;
    public float SpawnRate;

    private LevelGeneration LevelGenerator;

    void Start()
    {
        LevelGenerator = gameObject.GetComponent<LevelGeneration>();
        SpawnEnemies();
        GeneratePlastic(MaxPlastic);

        // Repeatedly generate food objects at Speed 
        InvokeRepeating("GenerateFood", 0, SpawnRate);
    }

    void SpawnEnemies()
    {
        GameObject SpawnObj = Enemy0;
        foreach (GameObject R in LevelGenerator.InstantiatedRooms)
        {
            int MaxCount = 4;            

            if (R == null)
                continue;            

            switch (R.name.Substring(0, 5)) 
            {
                case "Room0":
                    SpawnObj = Enemy0;
                    MaxCount = 2;
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
                    continue;
                default:
                    break;
            }

            for (int i = 0; i < MaxCount; i++)
            {                
                
                GameObject SpawnedObject = Instantiate(SpawnObj, GetLocation(R), Quaternion.identity);
                SpawnedObject.SetActive(true);
            }
        }
    }

    void GenerateFood()
    {
        foreach (GameObject R in LevelGenerator.InstantiatedRooms)
        {
            if ((R == null) || (R.name == "Room4(Clone)") || !(FoodCount < MaxFood))
                continue;

            GameObject SpawnedObject = Instantiate(Food, GetLocation(R), Quaternion.identity);            
            SpawnedObject.SetActive(true);

            FoodCount++;
        }
    }

    void GeneratePlastic(int MaxCount)
    {
        foreach (GameObject R in LevelGenerator.InstantiatedRooms)
        {
            if ((R == null) || (R.name == "Room4(Clone)") || (R.transform.position.x == 0 && R.transform.position.y == 0))
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

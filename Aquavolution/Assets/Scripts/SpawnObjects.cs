using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject Food;
    public GameObject Enemy;
    public Camera Cam;
    public int MaxFood = 10;
    public int MaxEnemy = 5;
    public float Speed;

    public LevelGeneration LevelGenerator;

    // Magic numbers
    public float XMinOffset = -25.414432f;
    public float XMaxOffset = 17.665568f;
    public float YMinOffset = 15.5339f;
    public float YMaxOffset = 35.4139f;

    void Start()
    {
        LevelGenerator = gameObject.GetComponent<LevelGeneration>();
        RandomSpawn(Enemy, MaxEnemy);

        // Repeatedly generate food objects at Speed 
        InvokeRepeating("GenerateFood", 0, Speed);

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
                float SpawnX = Random.Range(R.transform.position.x + XMinOffset, R.transform.position.x + XMaxOffset);
                float SpawnY = Random.Range(R.transform.position.y + YMinOffset, R.transform.position.y + YMaxOffset);

                // This code will be used to spawn different fish in different room types
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

                // Gets random points between bottom left and top right corner of the room
                Vector3 SpawnPos = new Vector3(SpawnX, SpawnY, Cam.nearClipPlane);
                GameObject SpawnedObject = Instantiate(SpawnObj, SpawnPos, Quaternion.identity);
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

            // Gets random points between bottom left and top right corner of the room
            float SpawnX = Random.Range(R.transform.position.x + XMinOffset, R.transform.position.x + XMaxOffset);
            float SpawnY = Random.Range(R.transform.position.y + YMinOffset, R.transform.position.y + YMaxOffset);

            Vector3 SpawnPos = new Vector3(SpawnX, SpawnY, Cam.nearClipPlane);
            GameObject SpawnedObject = Instantiate(Food, SpawnPos, Quaternion.identity);
            SpawnedObject.SetActive(true);
        }
    }
}

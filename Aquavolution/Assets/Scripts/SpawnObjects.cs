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
        InvokeRepeating("Generate", 0, Speed);

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

                // Converts pixel values to world-space
                //Vector3 SpawnPos = Cam.ScreenToWorldPoint(new Vector3(SpawnX, SpawnY, Cam.nearClipPlane));
                Vector3 SpawnPos = new Vector3(SpawnX, SpawnY, Cam.nearClipPlane);
                // Instantiates Prefab
                GameObject SpawnedObject = Instantiate(SpawnObj, SpawnPos, Quaternion.identity);
                // Enables prefab
                SpawnedObject.SetActive(true);
            }
        }
    }

    void Generate()
    {
        int SpawnX = Random.Range(0, Screen.width);
        int SpawnY = Random.Range(0, Screen.height);

        // Converts pixel values to world-space
        Vector3 SpawnPos = Cam.ScreenToWorldPoint(new Vector3(SpawnX, SpawnY, Cam.nearClipPlane));
        // Instantiates Prefab
        GameObject SpawnedObject = Instantiate(Food, SpawnPos, Quaternion.identity);
        // Enables prefab
        SpawnedObject.SetActive(true);
    }
}

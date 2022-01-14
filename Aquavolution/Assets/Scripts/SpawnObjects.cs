using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject Food;
    public GameObject Enemy;
    public Camera Cam;
    public int MaxFood = 10;
    public int MaxEnemy = 5;
    public float Speed;
    public struct RoomData
    {
        public double RoomNum;
        public double XZero;
        public double YZero;
        public double XMax;
        public double YMax;
        public RoomData(int ConstRoomNum, double ConstXZero, double ConstYZero, double ConstXMax, double ConstYMax)
        {
            this.RoomNum = ConstRoomNum;
            this.XZero = ConstXZero;
            this.YZero = ConstYZero;
            this.XMax = ConstXMax;
            this.YMax = ConstYMax;
        }
    };
    public RoomData Room1;

    void Start()
    {
        Room1 = new RoomData(0, -45.9, -21.6, 25.3, 33.5);
        RandomSpawn(Enemy, MaxEnemy);
        RandomSpawn(Food, MaxFood/2);

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
            RoomData CurrentRoom = GetPlayerRoom();
            float SpawnX = Random.Range((float)CurrentRoom.XZero, (float)CurrentRoom.XMax);
            float SpawnY = Random.Range((float)CurrentRoom.YZero, (float)CurrentRoom.YMax);

            // Converts pixel values to world-space
            Vector3 SpawnPos = new Vector3(SpawnX, SpawnY, Cam.nearClipPlane);

            // Instantiates Prefab
            Instantiate(SpawnObj, SpawnPos, Quaternion.identity).SetActive(true);
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

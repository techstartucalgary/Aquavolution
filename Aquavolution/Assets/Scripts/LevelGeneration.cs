 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    Vector2 WorldSize = new Vector2(100, 100);
    Room[,] Rooms;
    public GameObject[,] InstantiatedRooms;
    List<Vector2> TakenPositions = new List<Vector2>();
    int GridSizeX, GridSizeY;
    public int NumRooms;
    public GameObject MapSprite;
    public float MapRoomGap;
    public float RoomGapX;
    public float RoomGapY;
    private Vector2 index;

    void Start()
    {
        if (NumRooms < Math.Abs(BossDepth))
            Debug.LogWarning("There are less rooms than the specified boss depth, no boss room will spawn.");

        if (NumRooms >= ((WorldSize.x * 2) * (WorldSize.y * 2)) )
            NumRooms = Mathf.RoundToInt((WorldSize.x * 2) * WorldSize.y * 2);
        GridSizeX = Mathf.RoundToInt(WorldSize.x);
        GridSizeY = Mathf.RoundToInt(WorldSize.y);

        CreateRooms();
        SetRoomDoors();
        DrawMap();
        CreateTunnels();
    }

    void CreateRooms()
    {
        // Keeps track of all the information we need to know about where our rooms are
        InstantiatedRooms = new GameObject[GridSizeX * 2, GridSizeY * 2];
        Rooms = new Room[GridSizeX * 2, GridSizeY * 2];
        Rooms[GridSizeX, GridSizeY] = new Room(Vector2.zero, 1);
        TakenPositions.Insert(0, Vector2.zero);
        Vector2 CheckPos = Vector2.zero;

        // Magic Numbers for random generation
        float RandomCompare = 0.2f, RandomCompareStart = 0.2f, RandomCompareEnd = 0.01f;
    
        // Creates 2D array of Rooms according to randomly decided adjacent locations
        for (int i = 0; i < NumRooms-1; i++)
        {
            float RandomPerc = ((float) i / ((float) NumRooms-1));
            RandomCompare = Mathf.Lerp(RandomCompareStart, RandomCompareEnd, RandomPerc);
            CheckPos = FindNewValidRoomPos();
        
            // If a room has > 1 neighbors, there is a chance for it to get another neighbor
            if (NumberOfNeighbors(CheckPos, TakenPositions) > 1 && Random.value > RandomCompare)
            {
                int Iterations = 0;
                do
                {
                    CheckPos = FindNewValidRoomPos();
                    Iterations++;
                }
                while (NumberOfNeighbors(CheckPos, TakenPositions) > 1 && Iterations < 50);

                if (Iterations >= 50)
                    Debug.LogError("Error: Could not create with fewer neighbors than: " + NumberOfNeighbors(CheckPos, TakenPositions));            
            }

            // Add created room to arrays
            Rooms[(int)CheckPos.x + GridSizeX, (int)CheckPos.y + GridSizeY] = new Room(CheckPos, 0);
            TakenPositions.Insert(0, CheckPos);
        }
    }

    Vector2 FindNewValidRoomPos()
    {
        int x = 0, y = 0;
        Vector2 CheckingPos = Vector2.zero;
        do
        {
            do
            {
                int Index = Mathf.RoundToInt(UnityEngine.Random.value * (TakenPositions.Count - 1));
                
                x = (int) TakenPositions[Index].x;
                y = (int) TakenPositions[Index].y;                    
            }
            while ((y == BossDepth) && (GetDeepestRoom().y == BossDepth));


            bool Down, Right;

            Down = (UnityEngine.Random.value < 0.9f);
            Right = (UnityEngine.Random.value < 0.5f);
            
            if (Down)
                y -= 1;
            else
            {
                if (Right)
                    x += 1;
                else
                    x -= 1;
            }
            CheckingPos = new Vector2(x,y);
        }

        return CheckingPos;
    }

    // Counts how many neighbors a given position has
    int NumberOfNeighbors (Vector2 CheckingPos, List<Vector2> UsedPositions)
    {
        int NumNeighbors = 0;
        if (UsedPositions.Contains(CheckingPos + Vector2.right))
            NumNeighbors++;
        if (UsedPositions.Contains(CheckingPos + Vector2.left))
            NumNeighbors++;
        if (UsedPositions.Contains(CheckingPos + Vector2.up))
            NumNeighbors++;
        if (UsedPositions.Contains(CheckingPos + Vector2.down))
            NumNeighbors++;

        return NumNeighbors;
    }

    // Checks if a room has a neighbor, then sets the door boolean to enable a door between rooms
    void SetRoomDoors()
    {
        for (int x = 0; x < (GridSizeX * 2); x++)
            for (int y = 0; y < (GridSizeY * 2); y++)
            {
                if (Rooms[x,y] == null)
                    continue;

                Vector2 GridPosition = new Vector2(x,y);

                // Check Above
                if (y - 1 < 0)
                    Rooms[x,y].DoorBot = false;
                else
                    Rooms[x,y].DoorBot = (Rooms[x, y-1] != null);

                // Check Below
                if (y + 1 >= GridSizeY * 2)
                    Rooms[x,y].DoorTop = false;
                else
                    Rooms[x,y].DoorTop = (Rooms[x, y + 1] != null);

                //  Check Left
                if (x - 1 < 0)
                    Rooms[x,y].DoorLeft = false;
                else
                    Rooms[x,y].DoorLeft = (Rooms[x - 1, y] != null);

                // Check Right
                if (x + 1 >= GridSizeX * 2)
                    Rooms[x,y].DoorRight = false;
                else   
                    Rooms[x,y].DoorRight = (Rooms[x + 1, y] != null);
            }
    }

    void DrawMap()
    {
        foreach (Room R in Rooms)
        {
            if (R == null)
                continue;

            
            // Instantiates the Sprites. This does nothing right now, but can be used to make a minimap
            /*
            Vector2 DrawPos = R.GridPos;
            DrawPos.x *= MapRoomGap/10;
            DrawPos.y *= MapRoomGap/20;
            GameObject MapSpriteObj = Object.Instantiate(MapSprite, DrawPos, Quaternion.identity);
            MapSpriteObj.SetActive(true);
            MapSpriteSelector Mapper = MapSpriteObj.GetComponent<MapSpriteSelector>();
            Mapper.Type = R.Type;
            Mapper.Up = R.DoorTop;
            Mapper.Down = R.DoorBot;
            Mapper.Right = R.DoorRight;
            Mapper.Left = R.DoorLeft;
            */

            // Creates room objects and adds them to the array of Instantiated Rooms
            Vector2 DrawPos = R.GridPos;
            DrawPos.x *= RoomGapX/100;
            DrawPos.y *= RoomGapY/100;

            R.Type = (int)Mathf.Abs(R.GridPos.y/10);

            GameObject RoomPrefab = Instantiate(GameObject.Find("Room" + R.Type.ToString()), DrawPos, Quaternion.identity);

            index = ExtensionMethods.CoordinatesOf<Room>(Rooms, R);
            InstantiatedRooms[(int)index.x, (int)index.y] = RoomPrefab;
        }
    }

    // Creates holes in rooms to allow movement through them
    void CreateTunnels()
    {
        foreach (Room R in Rooms)
        {
            if (R == null)
                continue;

            index = ExtensionMethods.CoordinatesOf<Room>(Rooms, R);
            GameObject CurrentRoom = InstantiatedRooms[(int)index.x, (int)index.y];

            // Deletes corresponding door of room
            if (R.DoorBot)
                Destroy(CurrentRoom.transform.GetChild(1).Find("BotDoorGrid").gameObject);
            if (R.DoorTop)
                Destroy(CurrentRoom.transform.GetChild(1).Find("TopDoorGrid").gameObject);
            if (R.DoorLeft)
                Destroy(CurrentRoom.transform.GetChild(1).Find("LeftDoorGrid").gameObject);
            if (R.DoorRight)
                Destroy(CurrentRoom.transform.GetChild(1).Find("RightDoorGrid").gameObject);
        }
    }

    private Vector2 GetDeepestRoom()
    {
        Vector2 MaxDepth = Vector2.zero;
        foreach (Vector2 Pos in TakenPositions)
        {
            if (Pos.y < MaxDepth.y)
                MaxDepth = Pos;
        }
        return MaxDepth;
    }

    private bool ApproachingRoomLimit()
    {
        int DeepestRoomDepth = (int) GetDeepestRoom().y;
        
        if (NumRooms - TakenPositions.Count <= Math.Abs(BossDepth) - Math.Abs(DeepestRoomDepth))
            return true;
        else if (DeepestRoomDepth >= BossDepth)
            return false;
        else
            return false;
    }
}
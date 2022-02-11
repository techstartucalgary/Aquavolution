using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    Vector2 WorldSize = new Vector2(4,4);
    Room[,] Rooms;
    public GameObject[,] InstantiatedRooms;
    List<Vector2> TakenPositions = new List<Vector2>();
    int GridSizeX, GridSizeY;
    public int NumRooms;
    public GameObject MapSprite;
    public float MapRoomGap;
    public float RoomGap;
    private Vector2 index;

    
    void Start()
    {
        if (NumRooms >= ((WorldSize.x * 2) * (WorldSize.y * 2)) )
            NumRooms = Mathf.RoundToInt((WorldSize.x * 2) * WorldSize.y * 2);
        GridSizeX = Mathf.RoundToInt(WorldSize.x);
        GridSizeY = Mathf.RoundToInt(WorldSize.y);

        CreateRooms();
        SetRoomDoors();
        DrawMap();
    }

    void CreateRooms()
    {
        InstantiatedRooms = new GameObject[GridSizeX * 2, GridSizeY * 2];
        Rooms = new Room[GridSizeX * 2, GridSizeY * 2];
        Rooms[GridSizeX, GridSizeY] = new Room(Vector2.zero, 1);
        TakenPositions.Insert(0, Vector2.zero);
        Vector2 CheckPos = Vector2.zero;

        // Magic Numbers
        float RandomCompare = 0.2f, RandomCompareStart = 0.2f, RandomCompareEnd = 0.01f;

        for (int i = 0; i < NumRooms-1; i++)
        {
            float RandomPerc = ((float)i / ((float)NumRooms-1));
            RandomCompare = Mathf.Lerp(RandomCompareStart, RandomCompareEnd, RandomPerc);
            CheckPos = NewPosition();
        
            if (NumberOfNeighbors(CheckPos, TakenPositions) > 1 && Random.value > RandomCompare)
            {
                int Iterations = 0;
                do
                {
                    CheckPos = NewPosition();
                    Iterations++;
                }
                while (NumberOfNeighbors(CheckPos, TakenPositions) > 1 && Iterations < 100);

                if (Iterations >= 50)
                    Debug.LogError("Error: Could not create with fewer neighbors than: " + NumberOfNeighbors(CheckPos, TakenPositions));            
            }

            Rooms[(int)CheckPos.x + GridSizeX, (int)CheckPos.y + GridSizeY] = new Room(CheckPos, 0);
            TakenPositions.Insert(0, CheckPos);
        }
    }

    Vector2 NewPosition()
    {
        int x = 0, y = 0;
        Vector2 CheckingPos = Vector2.zero;
        do
        {
            int Index = Mathf.RoundToInt(Random.value * (TakenPositions.Count - 1));
            x = (int) TakenPositions[Index].x;
            y = (int) TakenPositions[Index].y;
            bool UpDown = (Random.value < 0.5f);
            bool Positive = (Random.value < 0.5f);
            if (UpDown)
                y -= 1;
            else
            {
                if (Positive)
                    x += 1;
                else
                    x -= 1;
            }
            CheckingPos = new Vector2(x,y);
        } 
        while (TakenPositions.Contains(CheckingPos) || x >= GridSizeX || x < - GridSizeX || y >= GridSizeY || y < -GridSizeY);

        return CheckingPos;
    }

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

            // Creates room objects and adds them to the array of Instantiated Rooms
            DrawPos.x *= RoomGap/10;
            DrawPos.y *= RoomGap/10;
            R.Type = (int)Mathf.Abs(R.GridPos.y);

            GameObject RoomPrefab = Instantiate(GameObject.Find("Room" + R.Type.ToString()), DrawPos, Quaternion.identity);

            index = ExtensionMethods.CoordinatesOf<Room>(Rooms, R);
            InstantiatedRooms[(int)index.x, (int)index.y] = RoomPrefab;
        }
    }
}
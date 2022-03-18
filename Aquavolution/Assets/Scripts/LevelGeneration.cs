using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelGeneration : MonoBehaviour
{
<<<<<<< HEAD
    Vector2 WorldSize = new Vector2(100, 100);
=======
    Vector2 WorldSize = new Vector2(10, 10);
>>>>>>> 130b881cf9fe47d0f2a25add06eb37170ea7e35e
    Room[,] Rooms;
    public GameObject[,] InstantiatedRooms;
    List<Vector2> TakenPositions = new List<Vector2>();
    int GridSizeX, GridSizeY;
    public int NumRooms;
    //public GameObject MapSprite;
    //public float MapRoomGap;
    public float RoomGapX;
    public float RoomGapY;
    private Vector2 index;
    public int BossDepth;

    void Start()
    {
<<<<<<< HEAD
        if (NumRooms < Math.Abs(BossDepth))
=======
        // Makes sure we don't have too many rooms in our world
        if (NumRooms < BossDepth)
>>>>>>> 130b881cf9fe47d0f2a25add06eb37170ea7e35e
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

            if (!ApproachingRoomLimit() || GetDeepestRoom().y == BossDepth)
            {
                RandomCompare = Mathf.Lerp(RandomCompareStart, RandomCompareEnd, RandomPerc);
                CheckPos = FindNewValidRoomPos();

                // If a room already has multiple neighbors, it's less likely to gain another one
                if (NumberOfNeighbors(CheckPos, TakenPositions) > 1 && UnityEngine.Random.value > RandomCompare)
                {
                    int Iterations = 0;

                    while (NumberOfNeighbors(CheckPos, TakenPositions) > 1 && Iterations < 50)
                    {
                        CheckPos = FindNewValidRoomPos();
                        Iterations++;
                    }

                    //if (Iterations >= 50)
                        //Debug.LogError("Error: Could not create with fewer neighbors than: " + NumberOfNeighbors(CheckPos, TakenPositions));            
                }
            }
            else
            {
                CheckPos = new Vector2(GetDeepestRoom().x, GetDeepestRoom().y - 1);
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
        
        while (TakenPositions.Contains(CheckingPos) || x >= GridSizeX || x < -GridSizeX || y >= GridSizeY || y < -GridSizeY)
        {
<<<<<<< HEAD
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
=======
            int Index = Mathf.RoundToInt(UnityEngine.Random.value * (TakenPositions.Count - 1));
            
            x = (int) TakenPositions[Index].x;
            y = (int) TakenPositions[Index].y;
            
            bool Down, Right;
            int DeepestRoom = GetDeepestRoom();

            // Make sure we're only going down if there aren't enough rooms left to go left or right
            /*
            if (TakenPositions.Count - NumRooms == DeepestRoom)
                Down = true;
            else if (DeepestRoom < BossDepth)
                Down = true;
                //Down = (UnityEngine.Random.value < Math.Abs( (DeepestRoom+1)/BossDepth ));
            else
                Down = false;
            */
            
            // TODO: Infinite loop happens when Down is set to false for some reason
            Down = (UnityEngine.Random.value < 0.5f);
            //Down = false;
            Right = (UnityEngine.Random.value < 0.5f);

>>>>>>> 130b881cf9fe47d0f2a25add06eb37170ea7e35e
            
            if (Down)
                y -= 1;
            else if (Right)
                x += 1;
            else
                x -= 1;

            CheckingPos = new Vector2(x,y);
        }
<<<<<<< HEAD
=======
        while (TakenPositions.Contains(CheckingPos) || x >= GridSizeX || x < -GridSizeX || y >= GridSizeY || y < -GridSizeY);
>>>>>>> 130b881cf9fe47d0f2a25add06eb37170ea7e35e

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
<<<<<<< HEAD
=======
            R.Type = Mathf.Abs((int)R.GridPos.y/10);
>>>>>>> 130b881cf9fe47d0f2a25add06eb37170ea7e35e

            R.Type = Mathf.Abs((int) R.GridPos.y / 10);
            GameObject RoomPrefab = Instantiate(GameObject.Find("Room" + R.Type.ToString()), DrawPos, Quaternion.identity);

            index = ExtensionMethods.CoordinatesOf<Room>(Rooms, R);
            InstantiatedRooms[(int)index.x, (int)index.y] = RoomPrefab;
        }
    }

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

<<<<<<< HEAD
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
=======
    int GetDeepestRoom()
    {
        int MaxDepth = 0;
        foreach (Vector2 Pos in TakenPositions)
        {
            if (Pos.y < MaxDepth)
                MaxDepth = (int)Pos.y;
        }
        return MaxDepth;
    }
>>>>>>> 130b881cf9fe47d0f2a25add06eb37170ea7e35e
}
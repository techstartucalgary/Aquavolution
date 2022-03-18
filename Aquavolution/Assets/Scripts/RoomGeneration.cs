using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomGeneration : MonoBehaviour
{
    private Tilemap[,] TMap;
    public TileBase Wall;
    public TileBase Blank;
    public TileBase DebugTile;
    private GameObject[,] RoomArray;
    private Rect StandardRoom;

    public int DoorYStart;
    public int DoorYEnd;
    public int DoorXStart;
    public int DoorXEnd;

    struct Rect
    {
        public TileBase Tile;
        public int XOffset;
        public int YOffset;
        public int Width;
        public int Height;
    };

    void Start()
    {
        RoomArray = gameObject.GetComponent<LevelGeneration>().InstantiatedRooms;
        TMap = new Tilemap[RoomArray.GetLength(0), RoomArray.GetLength(1)];
        CreateTMapArray(RoomArray);

        CreateStandardRoomStruct();
        DrawRooms();
    }

    void CreateTMapArray(GameObject[,] _RoomArray)
    {
        for (int x = 0; x < RoomArray.GetLength(1); x++)
            for (int y = 0; y < RoomArray.GetLength(0); y++)
                {
                    if (RoomArray[x,y] != null)
                        {
                            TMap[x,y] = RoomArray[x,y].transform.Find
                                ("TileMaps/WallGrid/Collisions").gameObject.GetComponent<Tilemap>();
                        }
                }
    }

    void DrawRooms()
    {
        foreach (Tilemap T in TMap)
            {
                if (T != null)
                {
                    DrawRectWithDoorGaps(StandardRoom, T);
                    CreateVariation(T);
                    ClearRectOutside(StandardRoom, 15, T);
                }
            }
    }

    void DrawRectWithDoorGaps(Rect _Rect, Tilemap _TMap)
    {
        for (int x = 0; x < _Rect.Width; x++)
        {
            for (int y = 0; y < _Rect.Height; y++)
            {
                if (   
                    ((x == 0) && ( (y < DoorYStart) || (y > DoorYEnd) ) )
                    || ((x == _Rect.Width - 1)  && ( (y < DoorYStart) || (y > DoorYEnd) ) )
                    || ((y == 0) && ( (x < DoorXStart) || (x > DoorXEnd) ) )
                    || ((y == _Rect.Height - 1) && ( (x < DoorXStart) || (x > DoorXEnd)) )
                    )
                        _TMap.SetTile(new Vector3Int(x + _Rect.XOffset, y + _Rect.YOffset, 0), _Rect.Tile);
            }
        }
    }

    void DrawRect(Rect _Rect, Tilemap _TMap)
    {
        for (int x = 0; x < _Rect.Width; x++)
        {
            for (int y = 0; y < _Rect.Height; y++)
            {
                if ( (x == 0) || (x == _Rect.Width - 1) || (y == 0) || (y == _Rect.Height - 1) )
                    _TMap.SetTile(new Vector3Int(x + _Rect.XOffset, y + _Rect.YOffset, 0), _Rect.Tile);
            }
        }
    }

    void CreateVariation(Tilemap _T)
    {
        bool XLow = false;
        bool XMax = false;
        bool YLow = false;
        bool YMax = false;
        for (int i = 0; i < Random.Range(0, 5); i++)
        {
            Rect rect;
            rect.Tile = Wall;
            rect.Height = Random.Range(7, 10);
            rect.Width = Random.Range(8, 14);

            if ((Random.Range(0,2) == 0) && (XLow == false))
            {
                rect.XOffset = StandardRoom.XOffset + Random.Range(-3, 5);
                XLow = true;
            }
            else if (XMax == false)
            {
                rect.XOffset = StandardRoom.XOffset + StandardRoom.Width + Random.Range(-5, -1);
                XMax = true;
            }
            else
                rect.XOffset = 0;

            if ((Random.Range(0,2) == 0) && (YLow == false))
            {
                rect.YOffset = StandardRoom.YOffset + Random.Range(-3, -1);
                YLow = true;
            }
            else if (YMax == false)
            {
                rect.YOffset = StandardRoom.YOffset + StandardRoom.Height + Random.Range (-3, -1);
                YMax = true;
            }
            else
                rect.YOffset = 0;

            DrawRect(rect, _T);
            ClearRectCenter(rect, _T);
        }        
    }

    // Deletes center of rect passed
     void ClearRectCenter(Rect _Rect, Tilemap _TMap)
    {
        for (int x = _Rect.XOffset; x < (_Rect.Width + _Rect.XOffset); x++)
        {
            for (int y = _Rect.YOffset; y < (_Rect.Height + _Rect.YOffset); y++)
            {
                if ((x != _Rect.XOffset) && (x != _Rect.Width + _Rect.XOffset - 1) && (y != _Rect.YOffset) && (y != _Rect.Height + _Rect.YOffset - 1))
                    _TMap.SetTile(new Vector3Int(x, y, 0), Blank);
            }
        }
    }

    // Deletes in a range outside of rect passed
    void ClearRectOutside(Rect _Rect, int Radius, Tilemap _TMap)
    {
        for (int x = _Rect.XOffset - Radius; x < (_Rect.Width + _Rect.XOffset + Radius); x++)
        {
            for (int y = _Rect.YOffset - Radius; y < (_Rect.Height + _Rect.YOffset + Radius); y++)
            {
                if ((x < _Rect.XOffset) || (x >= _Rect.Width + _Rect.XOffset) || (y < _Rect.YOffset) || (y >= _Rect.Height + _Rect.YOffset))
                    _TMap.SetTile(new Vector3Int(x, y, 0), DebugTile);
            }
        }
    }

    void CreateStandardRoomStruct()
    {
        StandardRoom.Tile = Wall;
        StandardRoom.XOffset = -25;
        StandardRoom.YOffset = 14;
        StandardRoom.Width = 45;
        StandardRoom.Height = 22;
    }
}
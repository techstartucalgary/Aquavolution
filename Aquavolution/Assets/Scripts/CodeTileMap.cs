using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Threading;

public class CodeTileMap : MonoBehaviour
{
    private Tilemap[,] TMap;
    public TileBase Wall;
    public TileBase Blank;
    public TileBase DebugTile;
    public GameObject[,] RoomArray;
    Rect standardRoom;
    public Tilemap testTilemap;
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

        CreateRects();
        CreateWalls();
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

    void CreateWalls()
    {
        foreach (Tilemap T in TMap)
            {
                if (T != null)
                {
                    GenerateRoom(standardRoom, T);
                    CreateVariation(T);
                    ClearOutside(standardRoom, 15, T);
                }
            }
    }

    // Generates a rectangle border with gaps for doors
    void GenerateRoom(Rect _Rect, Tilemap _TMap)
    {
        for (int x = 0; x < _Rect.Width; x++)
        {
            for (int y = 0; y < _Rect.Height; y++)
            {
                if (   
                    ((x == 0) && ( (y < 6) || (y > 15) ) )
                    || ((x == _Rect.Width - 1)  && ( (y < 6) || (y > 15) ) )
                    || ((y == 0) && ( (x < 19) || (x > 29) ) )
                    || ((y == _Rect.Height - 1) && ( (x < 19) || (x > 29)) )
                    )
                        _TMap.SetTile(new Vector3Int(x + _Rect.XOffset, y + _Rect.YOffset, 0), _Rect.Tile);
            }
        }
    }

    // Generates a rectangle border
    void GenerateRect(Rect _Rect, Tilemap _TMap)
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
                rect.XOffset = standardRoom.XOffset + Random.Range(-3, 5);
                XLow = true;
            }
            else if (XMax == false)
            {
                rect.XOffset = standardRoom.XOffset + standardRoom.Width + Random.Range(-5, -1);
                XMax = true;
            }
            else
                rect.XOffset = 0;

            if ((Random.Range(0,2) == 0) && (YLow == false))
            {
                rect.YOffset = standardRoom.YOffset + Random.Range(-3, -1);
                YLow = true;
            }
            else if (YMax == false)
            {
                rect.YOffset = standardRoom.YOffset + standardRoom.Height + Random.Range (-3, -1);
                YMax = true;
            }
            else
                rect.YOffset = 0;

            GenerateRect(rect, _T);
            DeleteCenter(rect, _T);
        }        
    }

    // Deletes center of rect passed
     void DeleteCenter(Rect _Rect, Tilemap _TMap)
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
    void ClearOutside(Rect _Rect, int Radius, Tilemap _TMap)
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

    void CreateRects()
    {
        standardRoom.Tile = Wall;
        standardRoom.XOffset = -25;
        standardRoom.YOffset = 14;
        standardRoom.Width = 45;
        standardRoom.Height = 22;
    }

    
    // Delete overlapping tiles. This isn't used in this script but might be useful at some point in future
    /*
    void NANDTileMap(Rect __Rect0, Rect __Rect1)
    {
        int Offset0 = __Rect0.Offset;
        int Width0 = __Rect0.Width;
        int Height0 = __Rect0.Height;

        int Offset1 = __Rect1.Offset;
        int Width1 = __Rect1.Width;
        int Height1 = __Rect1.Height;

        for (int x = Offset0; x < (Width0 + Offset0); x++)
        {
            for (int y = Offset0; y < (Height0 + Offset0); y++)
            {
                if ((x != Offset0) && (x != Width0+Offset0-1) && (y != Offset0) && (y != Height0+Offset0-1))
                    TMap.SetTile(new Vector3Int(x, y, 0), Blank);
            }
        }

        for (int x1 = Offset1; x1 < (Width1 + Offset1); x1++)
        {
            for (int y1 = Offset1; y1 < (Height1 + Offset1); y1++)
            {
                if ((x1 != Offset1) && (x1 != Width1+Offset1-1) && (y1 != Offset1) && (y1 != Height1+Offset1-1))
                    TMap.SetTile(new Vector3Int(x1, y1, 0), Blank);
            }
        }
    } */
}
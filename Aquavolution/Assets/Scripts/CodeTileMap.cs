using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Threading;

public class CodeTileMap : MonoBehaviour
{
    public Tilemap TMap;
    public TileBase Wall;
    public TileBase Blank;
    public TileBase Debug;
    Rect rect1;
    Rect rect2;
    Rect rect3;
    Rect rect4;
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
        CreateRects();
        StartCoroutine("Test");
    }



    IEnumerator Test()
    {
        GenerateRect(rect1);
        GenerateRect(rect2);
        GenerateRect(rect3);
        GenerateRect(rect4);
        yield return new WaitForSeconds(1);
        DeleteCenter(rect2);
        DeleteCenter(rect3);
        DeleteCenter(rect4);
        yield return new WaitForSeconds(1);
        ClearOutside(rect1, 10);
    }

    // Generates a rectangle border
    void GenerateRect(Rect __Rect)
    {
        for (int x = __Rect.XOffset; x < (__Rect.Width + __Rect.XOffset); x++)
        {
            for (int y = __Rect.YOffset; y < (__Rect.Height + __Rect.YOffset); y++)
            {
                if ((x == __Rect.XOffset) || (x == __Rect.Width + __Rect.XOffset - 1) || (y == __Rect.YOffset) || (y == __Rect.Height + __Rect.YOffset - 1))
                    TMap.SetTile(new Vector3Int(x, y, 0), __Rect.Tile);
            }
        }
    }

    // Deletes center of rect passed
     void DeleteCenter(Rect __Rect)
    {
        for (int x = __Rect.XOffset; x < (__Rect.Width + __Rect.XOffset); x++)
        {
            for (int y = __Rect.YOffset; y < (__Rect.Height + __Rect.YOffset); y++)
            {
                if ((x != __Rect.XOffset) && (x != __Rect.Width + __Rect.XOffset - 1) && (y != __Rect.YOffset) && (y != __Rect.Height + __Rect.YOffset - 1))
                    TMap.SetTile(new Vector3Int(x, y, 0), Blank);
            }
        }
    }

    // Deletes in a range outside of rect passed
    void ClearOutside(Rect __Rect, int Radius)
    {
        for (int x = __Rect.XOffset - Radius; x < (__Rect.Width + __Rect.XOffset + Radius); x++)
        {
            for (int y = __Rect.YOffset - Radius; y < (__Rect.Height + __Rect.YOffset + Radius); y++)
            {
                if ((x < __Rect.XOffset) || (x >= __Rect.Width + __Rect.XOffset) || (y < __Rect.YOffset) || (y >= __Rect.Height + __Rect.YOffset))
                    TMap.SetTile(new Vector3Int(x, y, 0), Debug);
            }
        }
    }

    void CreateRects()
    {
        rect1.Tile = Wall;
        rect1.XOffset = 0;
        rect1.YOffset = 0;
        rect1.Width = 20;
        rect1.Height = 20;

        rect2.Tile = Wall;
        rect2.XOffset = (int)Random.Range(16, 20);
        rect2.YOffset = (int)Random.Range(16, 20);
        rect2.Width = (int)Random.Range(5, 8);
        rect2.Height = (int)Random.Range(5, 8);

        rect3.Tile = Wall;
        rect3.XOffset = (int)Random.Range(-4, 4);
        rect3.YOffset = (int)Random.Range(16, 20);
        rect3.Width = (int)Random.Range(5, 8);
        rect3.Height = (int)Random.Range(5, 8);

        rect4.Tile = Wall;
        rect4.XOffset = (int)Random.Range(16, 20);
        rect4.YOffset = (int)Random.Range(-4, -4);
        rect4.Width = (int)Random.Range(5, 8);
        rect4.Height = (int)Random.Range(5, 8);
    }

    
    /* // Delete overlapping tiles
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
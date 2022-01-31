using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Vector2 GridPos;
    public int Type;
    public bool DoorTop, DoorBot, DoorLeft, DoorRight;
    public Room(Vector2 _GridPos, int _Type)
    {
        GridPos = _GridPos;
        Type = _Type;
    }
}

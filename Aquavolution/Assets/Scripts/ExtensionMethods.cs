using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static Vector2 CoordinatesOf<T>(T[,] ArrayInput, T Value)
    {
        int w = ArrayInput.GetLength(0); // width
        int h = ArrayInput.GetLength(1); // height

        // Check every element in 2D array, if it is null, ignore, else, if it is the Value we want, return the index the value is stored in
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                if (ArrayInput[x, y] == null)
                    continue;
                else if (ArrayInput[x, y].Equals(Value))
                    return new Vector2(x,y);
            }
        }
        return new Vector2(-1,-1);
    }
}
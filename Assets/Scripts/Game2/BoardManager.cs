using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public int columns = 10;
    public int rows = 4;
    public GameObject[] floorTiles;
    public Sprite[] sprites;

    private Transform boardHolder;

    public void BoardUpdate(List<Sprite> sprite1, List<Sprite> sprite2)
    {
        if (boardHolder != null)
        {
            Destroy(boardHolder.gameObject);
        }
        boardHolder = new GameObject("Board").transform;
        for (int x = 0; x < columns; x++)
        {
            if (x != 4 && x != 5)
            {
                for (int y = 0; y < rows; y++)
                {
                    List<Sprite> sprite = null;
                    int index = 0;
                    GameObject toInstantiate = null;
                    if (x < 4)
                    {
                        index = x * 4 + y;
                        sprite = sprite1;
                    }
                    else if (x > 5)
                    {
                        index = (x - 6) * 4 + y;
                        sprite = sprite2;
                    }
                    if (sprite[index] == sprites[0])
                    {
                        toInstantiate = floorTiles[0];
                    }
                    else if (sprite[index] == sprites[1])
                    {
                        toInstantiate = floorTiles[1];
                    }
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }
            }
        }
    }
}
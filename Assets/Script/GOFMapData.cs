using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GOFMapData : MonoBehaviour
{
    public int[,,] MakeMapRandom(int height, int width, int depth) //random
    {
        int[,,] map = new int[width, height, depth];
        for (int y = 0; y < height; y++) //create graph
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < depth; z++)
                {
                    System.Random random = new System.Random(); //creating rng
                    int randomNumber = random.Next(0, 3);   // Generate a random integer, 0, 1, or 2
                    if (randomNumber == 2) //if two, make live. 2/3 chance dead for better patterns
                    {
                        map[x, y, z] = 1;
                    }
                }
            }
        }
        return map;
    }

    public int[,,] MakeMapBlinker(int height, int width, int depth)
    {
        int[,,] map = new int[width, height, depth];
        for (int y = 0; y < height; y++) //create graph
        {
            for (int x = 0; x < width; x++)
            {
                for(int z = 0; z < depth; z++)
                    map[x, y, z] = 0;
            }
        }

        int middleX = width / 2; //finding middle
        int middleY = height / 2;
        int middleZ = depth / 2;

        map[middleX, middleY, middleZ] = 1; //make shape
        map[middleX - 1, middleY, middleZ] = 1;
        map[middleX, middleY - 1, middleZ] = 1;
        map[middleX - 1, middleY - 1, middleZ-1] = 1;

        return map;
    }

    public int[,,] MakeMapTube(int height, int width, int depth)
    {
        int[,,] map = new int[width, height, depth];
        for (int y = 0; y < height; y++) //create graph
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < depth; z++)
                    map[x, y, z] = 0;
            }
        }

        int middleX = width / 2; //finding middle
        int middleY = height / 2;
        int middleZ = depth / 2;

        map[middleX - 2, middleY + 1, middleZ] = 1;
        map[middleX - 2, middleY, middleZ - 1] = 1;

        map[middleX - 1, middleY, middleZ] = 1;
        map[middleX - 1, middleY + 1, middleZ - 1] = 1;

        map[middleX, middleY + 1, middleZ] = 1;
        map[middleX, middleY, middleZ - 1] = 1;

        map[middleX + 1, middleY + 1, middleZ] = 1;
        map[middleX + 1, middleY, middleZ - 1] = 1;

        return map;
    }

    public int[,,] MakeMapPulsar(int height, int width, int depth)
    {
        int[,,] map = new int[width, height, depth];
        for (int y = 0; y < height; y++) //create graph
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < depth; z++)
                    map[x, y, z] = 0;
            }
        }

        int middleX = width / 2; //finding middle
        int middleY = height / 2;
        int middleZ = depth / 2;

        map[middleX - 1, middleY + 1, middleZ] = 1;
        map[middleX - 1, middleY + 1, middleZ + 1] = 1;
        map[middleX - 1, middleY + 1, middleZ + 2] = 1;

        map[middleX - 1, middleY - 1, middleZ] = 1;
        map[middleX - 1, middleY - 1, middleZ + 1] = 1;
        map[middleX - 1, middleY - 1, middleZ + 2] = 1;

        map[middleX + 1, middleY + 1, middleZ] = 1;
        map[middleX + 1, middleY + 1, middleZ + 1] = 1;
        map[middleX + 1, middleY + 1, middleZ + 2] = 1;

        map[middleX + 1, middleY - 1, middleZ] = 1;
        map[middleX + 1, middleY - 1, middleZ + 1] = 1;
        map[middleX + 1, middleY - 1, middleZ + 2] = 1;

        return map;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Node[,,] nodes;
    public List<Node> walls = new List<Node>();
    int[,,] m_mapData;
    int m_width;
    int m_height;
    int m_depth;

    public static readonly Vector3[] allDirections =
    {
        // Top Layer
        new Vector3(0f, 1f, 0f),     // Up
        new Vector3(1f, 1f, 0f),     // Up-Right
        new Vector3(1f, 0f, 0f),     // Right
        new Vector3(1f, -1f, 0f),    // Down-Right
        new Vector3(0f, -1f, 0f),    // Down
        new Vector3(-1f, -1f, 0f),   // Down-Left
        new Vector3(-1f, 0f, 0f),    // Left
        new Vector3(-1f, 1f, 0f),    // Up-Left
        new Vector3(0f, 0f, 1f),     // Forward

        // Middle Layer
        new Vector3(0f, 1f, 1f),     // Up-Forward
        new Vector3(1f, 1f, 1f),     // Up-Right-Forward
        new Vector3(1f, 0f, 1f),     // Right-Forward
        new Vector3(1f, -1f, 1f),    // Down-Right-Forward
        new Vector3(0f, -1f, 1f),    // Down-Forward
        new Vector3(-1f, -1f, 1f),   // Down-Left-Forward
        new Vector3(-1f, 0f, 1f),    // Left-Forward
        new Vector3(-1f, 1f, 1f),    // Up-Left-Forward
        //new Vector3(0f, 0f, 0f),     // Center 

        // Bottom Layer
        new Vector3(0f, 1f, -1f),    // Up-Backward
        new Vector3(1f, 1f, -1f),    // Up-Right-Backward
        new Vector3(1f, 0f, -1f),    // Right-Backward
        new Vector3(1f, -1f, -1f),   // Down-Right-Backward
        new Vector3(0f, -1f, -1f),   // Down-Backward
        new Vector3(-1f, -1f, -1f),  // Down-Left-Backward
        new Vector3(-1f, 0f, -1f),   // Left-Backward
        new Vector3(-1f, 1f, -1f),   // Up-Left-Backward
        new Vector3(0f, 0f, -1f)     // Backward
    };


    public void Init(int[,,] mapData)
    {
        m_mapData = mapData;
        m_width = mapData.GetLength(0);
        m_height = mapData.GetLength(1);
        m_depth = mapData.GetLength(2);
        //Debug.Log("");
        nodes = new Node[m_width, m_height, m_depth];    //Creating the array of nodes

        for (int y = 0; y < m_height; y++)
        {
            for (int x = 0; x < m_width; x++)
            {
                for (int z = 0; z < m_depth; z++)
                {
                    NodeType type = (NodeType)mapData[x, y, z]; //creating node on map
                    if (type == NodeType.Live)
                    {
                        Debug.Log("node at " + x + ", " + y + ", " + z + "is live");
                    }
                    Node newNode = new Node(x, y, z, type);
                    nodes[x, y, z] = newNode;
                    newNode.position = new Vector3(x, z, y); //might be in wrong order, refer to 2d for reference
                }
            }
        }
        for (int y = 0; y < m_height; y++)
        {
            for (int x = 0; x < m_width; x++)
            {
                for (int z = 0; z < m_width; z++)
                {
                    nodes[x, y, z].neighbors = GetNeighbors(x, y, z); //saving neighbor array for each node
                }
            }
        }
    }

    public int getWidth() 
    {
        return m_width;
    }

    public int getHeight()
    {
        return m_height;
    }

    public int getDepth()
    {
        return m_depth;
    }

    public bool IsWithinBounds(int x, int y, int z) //checks for if it is in bounds
    {
        return (x >= 0 && x < m_width && y >= 0 && y < m_height && z >= 0 && z < m_depth);
    }

    List<Node> GetNeighbors(int x, int y, int z, Node[,,] nodeArray, Vector3[] directions)
    {
        List<Node> neighborNodes = new List<Node>();
        foreach (Vector3 dir in directions) //checks when creating neighbors
        {
            int newX = x + (int)dir.x;
            int newY = y + (int)dir.y;
            int newZ = z + (int)dir.z;
            if (IsWithinBounds(newX, newY, newZ))
            {
                neighborNodes.Add(nodeArray[newX, newY, newZ]);
            }
            else
            {
                if ((newX == -1 && newY == -1 && newZ == -1) ||   // Bottom-Left-Back
                    (newX == m_width && newY == m_height && newZ == m_depth) ||   // Top-Right-Front
                    (newX == m_width && newY == -1 && newZ == m_depth) ||   // Top-Left-Front
                    (newX == -1 && newY == m_height && newZ == -1) ||   // Bottom-Right-Back
                    (newX == -1 && newY == -1 && newZ == m_depth) ||   // Bottom-Left-Front
                    (newX == m_width && newY == m_height && newZ == -1) ||   // Top-Right-Back
                    (newX == m_width && newY == -1 && newZ == -1) ||   // Top-Left-Back
                    (newX == -1 && newY == m_height && newZ == m_depth) ||  // Bottom-Right-Front


                    (newX == -1 && newY == -1) ||
                    (newX == -1 && newY == m_height) ||
                    (newX == -1 && newZ == -1) ||
                    (newX == -1 && newZ == m_depth) ||
                    (newX == m_width && newY == -1) ||
                    (newX == m_width && newY == m_height) ||
                    (newX == m_width && newZ == -1) ||
                    (newX == m_width && newZ == m_depth) ||
                    (newY == -1 && newZ == -1) ||
                    (newY == -1 && newZ == m_depth) ||
                    (newY == m_height && newZ == -1) ||
                    (newY == m_height && newZ == m_depth)) { }

                else if (newX == -1) //wraps around x axis backward
                {
                    neighborNodes.Add(nodeArray[m_width - 1, newY, newZ]);
                }
                else if (newY == -1) //wraps around y axis backward
                {
                    neighborNodes.Add(nodeArray[newX, m_height - 1, newZ]);
                }
                else if (newZ == -1) //wraps around z axis backward
                {
                    neighborNodes.Add(nodeArray[newX, newY, m_depth - 1]);
                }
                else if (newX == m_width) //wraps around x axis forward
                {
                    neighborNodes.Add(nodeArray[0, newY, newZ]);
                }
                else if (newY == m_height) //wraps around y axis forward
                {
                    neighborNodes.Add(nodeArray[newX, 0, newZ]);
                }
                else if (newZ == m_depth) //wraps around z axis forward
                {
                    neighborNodes.Add(nodeArray[newX, newY, 0]);
                }
            }
        }
        return neighborNodes;
    }
    List<Node> GetNeighbors(int x, int y, int z) 
    {
        return GetNeighbors(x, y, z, nodes, allDirections);
    }
}

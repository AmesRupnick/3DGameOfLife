using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType //list of values that NodeType could possibly be
{
    Dead = 0,
    Live = 1,
    PreDead = 2, //in-between states used for processing
    PreLive = 3  //''
}
public class Node
{
    public NodeType nodeType = NodeType.Dead; //starts at dead
    public int xIndex = -1;
    public int yIndex = -1;
    public int zIndex = -1;
    public Vector3 position; //Only available in unity, has components x, y, and z, called a complex primative
    public List<Node> liveNodes = new List<Node>(); //3 lists, one to keep live nodes, one deadnodes, and one neighbors of current node
    public List<Node> deadNodes = new List<Node>();
    public List<Node> neighbors = new List<Node>();
    public Node previous = null;

    public Node(int xIndex, int yIndex, int zIndex, NodeType nodeType) //creation
    {
        this.xIndex = xIndex;
        this.yIndex = yIndex;
        this.zIndex = zIndex;
        this.nodeType = nodeType;
    }

    public void Reset() //removes node
    {
        previous = null;
    }
}

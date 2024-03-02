using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    Node m_startNode;
    Node m_goalNode;
    Graph m_graph;
    GraphView m_graphView;

    Queue<Node> m_frontierNodes;
    List<Node> m_exploredNodes;
    List<Node> m_pathNodes;
    List<Node> m_deadNodes;
    List<Node> m_liveNodes;
    Queue<Node> m_psuedoNodes;

    NodeView startNodeView;
    NodeView goalNodeView;
    int num = 0;

    public Color liveNodeColor = Color.green; //color of live nodes
    public Color deadNodeColor = Color.clear;   //color of dead nodes
    private SpriteRenderer spriteRenderer;

    public bool isComplete = false;
    int m_iterations = 0;

    public void Init(Graph graph, GraphView graphView, Node start, Node goal)
    {
        if (start == null || goal == null || graph == null || graphView == null) //checks for missing pieces
        {
            Debug.LogWarning("Missing Components");
            return;
        }

        //initialization
        m_graph = graph; 
        m_graphView = graphView;
        m_startNode = start;
        m_goalNode = goal;

        m_liveNodes = new List<Node>();
        m_deadNodes = new List<Node>();
        m_psuedoNodes = new Queue<Node>();
        m_frontierNodes = new Queue<Node>();
        m_frontierNodes.Enqueue(start);
        m_exploredNodes = new List<Node>();

        deadNodeColor = Color.clear;

        

        startNodeView = graphView.nodeViews[start.xIndex, start.yIndex, start.zIndex];
        goalNodeView = graphView.nodeViews[goal.xIndex, goal.yIndex, goal.zIndex];

        Node currentNode = m_startNode;

        while (m_frontierNodes.Count > 0) //if all nodes haven't been exhausted
        {
            currentNode = m_frontierNodes.Dequeue(); //get the next node
            if (!m_exploredNodes.Contains(currentNode))
            {
                m_exploredNodes.Add(currentNode); //move to explored nodes
            }

            if (currentNode.nodeType == NodeType.Live) //figure out if nodes initially are alive or dead
            {
                m_liveNodes.Add(currentNode);
            }
            if (currentNode.nodeType == NodeType.Dead)
            {
                m_deadNodes.Add(currentNode);
            }
            ExpandCurrentNode(currentNode);
        } 
        //gets to end of all nodes
        graphView.ColorNodes(m_deadNodes, Color.red); //color the nodes
        graphView.ColorNodes(m_liveNodes, Color.green); //''
        Debug.Log("Working in intialization");

        m_deadNodes.Clear(); //clean up for first run
        m_liveNodes.Clear();
        m_frontierNodes.Clear();
        m_exploredNodes.Clear();

        m_frontierNodes.Enqueue(m_startNode); //enqueue start again for first run
        
    }

    public IEnumerator SearchRoutine(GraphView graphView, float timeStep)
    {

        while (isComplete == false)
        {
            Node currentNode = m_startNode;

            if (m_frontierNodes.Count > 0) //if all nodes haven't been exhausted
            {
                currentNode = m_frontierNodes.Dequeue(); //get the next node
                if (!m_exploredNodes.Contains(currentNode))
                {
                    m_exploredNodes.Add(currentNode); //move to explored nodes
                }

                int liveCellCount = SumLiveCells(currentNode); //gets the number of live neighbors
                if (liveCellCount < 5 && currentNode.nodeType == NodeType.Live) //dies of starvation
                {
                    currentNode.nodeType = NodeType.PreDead; //predead and prelive allow the graph to change everything at once as to not interfere with alogorithm
                    m_psuedoNodes.Enqueue(currentNode);
                }
                if (liveCellCount == 4 && currentNode.nodeType == NodeType.Dead) //lives
                {
                    currentNode.nodeType = NodeType.PreLive;
                    m_psuedoNodes.Enqueue(currentNode);

                }
                if (liveCellCount > 6 && currentNode.nodeType == NodeType.Live) //dies of overcrowding
                {
                    currentNode.nodeType = NodeType.PreDead;
                    m_psuedoNodes.Enqueue(currentNode);
                }

                if (currentNode.nodeType == NodeType.Dead || currentNode.nodeType == NodeType.PreDead)
                {
                    m_deadNodes.Add(currentNode); //for coloring later
                }

                if (currentNode.nodeType == NodeType.Live || currentNode.nodeType == NodeType.PreLive)
                {
                    m_liveNodes.Add(currentNode); //for coloring later
                }
                ExpandCurrentNode(currentNode);
            }
            else //gets to end of all nodes
            {
                yield return new WaitForSeconds(timeStep); //use the pause
                //graphView.AlphaNodes(m_deadNodes, 0f); //color the nodes
                //graphView.AlphaNodes(m_liveNodes, 1f); //''
                graphView.ColorNodes(m_deadNodes, Color.red); //color the nodes
                graphView.ColorNodes(m_liveNodes, Color.green); //''
                Debug.Log("Working");

                m_deadNodes.Clear(); //clean up for next run
                m_liveNodes.Clear();
                m_frontierNodes.Clear();
                m_exploredNodes.Clear();

                m_frontierNodes.Enqueue(m_startNode); //enqueue start again for next run

                foreach (Node item in m_psuedoNodes) //changes nodes to actual states after coloring
                {
                    if (item.nodeType == NodeType.PreDead)
                    {
                        item.nodeType = NodeType.Dead;
                    }
                    if (item.nodeType == NodeType.PreLive)
                    {
                        item.nodeType = NodeType.Live;
                    }
                }
            }
        }
    }

   
    void ExpandCurrentNode(Node node) //find the neighbors of the node, like in class
    {
        for (int i = 0; i < node.neighbors.Count; i++)
        {
            if (!m_exploredNodes.Contains(node.neighbors[i]) && !m_frontierNodes.Contains(node.neighbors[i]))
            {
                node.neighbors[i].previous = node;
                m_frontierNodes.Enqueue(node.neighbors[i]);
            }
        }
    }

    int SumLiveCells(Node node)
    {
        int sum = 0;
        for (int i = 0; i < node.neighbors.Count; i++)
        {
            if (node.neighbors[i].nodeType == NodeType.Live || node.neighbors[i].nodeType == NodeType.PreDead) 
            {
                sum++; //if nodes are live, add one to sum
            }
        }
        return sum;
    }
}

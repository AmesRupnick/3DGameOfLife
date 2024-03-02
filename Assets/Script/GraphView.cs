using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Graph))]

public class GraphView : MonoBehaviour
{

    public GameObject nodeViewPrefab;
    public NodeView[,,] nodeViews;
    public Color liveNodeColor = Color.green;
    public Color deadNodeColor = Color.red;
    public void Init(Graph graph)
    {
        if (graph == null)
        {
            Debug.LogWarning("No graph to initialize");
            return;
        } 
        nodeViews = new NodeView[graph.getWidth(), graph.getHeight(), graph.getDepth()];
        //Debug.LogWarning(nodeViews);

        foreach (Node n in graph.nodes)
        {
            GameObject instance = Instantiate(nodeViewPrefab, Vector3.zero, Quaternion.identity);
            NodeView nodeView = instance.GetComponent<NodeView>();
            if (nodeView != null)
            {
                nodeView.Init(n);
                nodeViews[n.xIndex, n.yIndex, n.zIndex] = nodeView;
                if (n.nodeType == NodeType.Live)
                {
                    nodeView.ColorNode(liveNodeColor);
                }
                else
                {
                    nodeView.ColorNode(deadNodeColor);
                }
            }
        }
    }

    public void ColorNodes(List<Node> nodes, Color color)
    {
        foreach (Node n in nodes)
        {
            if (n != null)
            {
                NodeView nodeView = nodeViews[n.xIndex, n.yIndex, n.zIndex];
                if (nodeView != null)
                {
                    nodeView.ColorNode(color);
                }
            }
        }
    }

    public void AlphaNodes(List<Node> nodes, float alpha)
    {
        foreach (Node n in nodes)
        {
            if (n != null)
            {
                NodeView nodeView = nodeViews[n.xIndex, n.yIndex, n.zIndex];
                if (nodeView != null)
                {
                    nodeView.AlphaNode(alpha);
                }
            }
        }
    }
}

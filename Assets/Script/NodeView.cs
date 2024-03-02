using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeView : MonoBehaviour
{
    public GameObject tile; //actual image of object
    [Range (0,0.5f)] //sizing
    public float borderSize = 0.15f; //sizing

    public void Init(Node node) //creating node
    {
        if (tile != null) 
        {
            tile.name = "Node (" + node.xIndex + "," + node.yIndex + "," + node.zIndex + ")";
            tile.transform.position = node.position;
            tile.transform.localScale = new Vector3(1f - borderSize, 1f - borderSize, 1f - borderSize);
            
        }
    }

    public void ColorNode(Color color, GameObject go) //coloring the node
    {
        if (go != null)
        {
            Renderer goRenederer = go.GetComponent<Renderer>();
            if (goRenederer != null)
            {
                goRenederer.material.color = color;
            }
        }
    }

    public void ColorNode(Color color) //simplified coloring
    {
        ColorNode(color, tile);
    }

    public void AlphaNode(float alpha, GameObject go) //method for changing transparency, wont work with default shader
    {
        if (go != null)
        {
            Renderer goRenderer = go.GetComponent<Renderer>();
            if (goRenderer != null)
            {
                // Print statements for debugging
                //Debug.Log("Changing alpha of " + go.name + " to " + alpha);

                Color objectColor = goRenderer.material.color;
                objectColor.a = alpha;
                goRenderer.material.color = objectColor;
            }
            else
            {
                Debug.LogWarning("Renderer not found on " + go.name);
            }
        }
        else
        {
            Debug.LogWarning("GameObject is null");
        }
    }


    public void AlphaNode(float alpha) //simplified transparency
    {
        AlphaNode(alpha, tile);
    }
}

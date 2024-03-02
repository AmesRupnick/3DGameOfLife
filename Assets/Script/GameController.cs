using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameOfLife pathfinder;
    public GOFMapData mapData;
    public Graph graph;
    public InputHandler inputField;
    public Input input1;
    public Input input2;
    public float pause;
    public int sizeX = 0;
    public int sizeY = 0;
    public int sizeZ = 0;
    public int startX = 0; //relic for initializing "search"
    public int startY = 0;
    public int startZ = 0;
    public int goalX = 0; //relic for initializing "search"
    public int goalY = 6;
    public int goalZ = 6;

    public void StartGame() //called by the start sim button
    {
        int sizeX = inputField.GetSize(); //get values from text boxes
        int sizeY = sizeX;
        int sizeZ = sizeX;
        float pause = inputField.GetSpeed();
        string mapDesign = inputField.GetDesign();

        if (mapData!=null && graph != null)
        {
            int[,,] mapInstance;
            if (mapDesign == "pulsar" || mapDesign == "Pulsar") //make if pulsar
            {
                mapInstance = mapData.MakeMapPulsar(sizeX, sizeY, sizeZ);
            }
            else if (mapDesign == "blinker" || mapDesign == "Blinker") //make if blinker
            {
                mapInstance = mapData.MakeMapBlinker(sizeX, sizeY, sizeZ);
            }
            else if (mapDesign == "tube" || mapDesign == "Tube") //make if glider
            {
                mapInstance = mapData.MakeMapTube(sizeX, sizeY, sizeZ);
            }
            else //default is random
            {
                mapInstance = mapData.MakeMapRandom(sizeX, sizeY, sizeZ);
            }
            graph.Init(mapInstance);
            GraphView graphView = graph.gameObject.GetComponent<GraphView>(); //create view
            if (graphView != null)
            {
                graphView.Init(graph);
            }
            if (graph.IsWithinBounds(startX, startY, startZ) && graph.IsWithinBounds(goalX, goalY, goalZ)) //create start
            {
                Node startNode = graph.nodes[startX, startY, startZ]; //relic
                Node goalNode = graph.nodes[goalX, goalY, goalZ]; //relic
                pathfinder.Init(graph, graphView, startNode, goalNode); //initialize the game of life path
                StartCoroutine(pathfinder.SearchRoutine(graphView, pause)); //start it
            }
        }
    }
}

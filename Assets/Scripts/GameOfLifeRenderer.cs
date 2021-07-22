using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameOfLife;

public class GameOfLifeRenderer : MonoBehaviour
{
     public int[,] emptyPattern = new int[16, 16]{
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
        };

    public GameObject Prefab;
    public int upwardHight = 64;
    // GameOfLifeClass game;
    GameObject cells;
    List<GameObject> objectPool; 
    public bool isRendering;
    int aliveCells = 0;
    // Start is called before the first frame update
    // void Start()=>StartSimulation(RandomPatternGenerator(size,size));
    int[][,] playArea;
   
    public void StartSimulation(int[,] inputGrid)
    {
        int width = inputGrid.GetLength(0);
        int hight = inputGrid.GetLength(1);
        playArea = new int[upwardHight][,];

        playArea[0] = inputGrid;
        // game = new GameOfLifeClass(inputGrid);

        objectPool = new List<GameObject>();
        cells = new GameObject("cells");


        var main = Camera.main;
        main.orthographicSize = Mathf.Sqrt(width*hight)*0.6f;
        main.transform.position = new Vector3(width/2,hight/2,main.transform.position.z);
        
        StartCoroutine("StartCycles");
    }
    
    IEnumerator StartCycles(){
        RenderGeneration(playArea[0],0,false);
        yield return new WaitForSeconds(0.20f); //trick the user into seeing their pattern more time
        
        // while (enabled){
        for (int z = 1; z < upwardHight; z++){
            playArea[z] = GameOfLifeClass.CalculateNextGen(playArea[z-1]);
            yield return new WaitForSeconds(0.020f);
            RenderGeneration(playArea[z],z,false);
        }
    }
  
    void RenderGeneration(int[,] grid,int z,bool redraw = true){
        int width = grid.GetLength(0);
        int hight = grid.GetLength(1);
        

        // if (redraw){
        //     aliveCells = 0;
        //     foreach (var cell in objectPool)
        //     {
        //         cell.SetActive(false);
        //     }
        // }

        for (int Y = 0; Y < hight; Y++)
        {
            for (int X = 0; X < width; X++)
            {
                if (grid[X,Y] >= 1){
                    // if (objectPool.Count <= aliveCells){
                        var cell = Instantiate(Prefab,cells.transform);
                        objectPool.Add(cell);
                    // if (redraw) objectPool[aliveCells].SetActive(true);
                    objectPool[aliveCells].transform.position = new Vector3(width - X,hight - Y,z);
                    aliveCells ++;

                }
            }
        }
        
    }
    public int[,] RandomPatternGenerator(int width, int hight){
        int[,] output = new int[width,hight];
        for (int Y = 0; Y < hight; Y++)
        {
            for (int X = 0; X < width; X++)
            {
                output[X,Y] = Random.value >= 0.5f? 1:0;
            }
        }
        return output;
    }
}

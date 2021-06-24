using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameOfLife;

public class GameOfLifeRenderer : MonoBehaviour
{
     public int[,] startPattern = new int[16, 16]{
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
    public int size = 64;
    GameOfLifeClass game;
    GameObject cells;
    List<GameObject> objectPool; 
    public bool isRendering;
    int aliveCells = 0;
    // Start is called before the first frame update
    // void Start()=>StartSimulation(RandomPatternGenerator(size,size));
    public void StartSimulation(int[,] inputGrid)
    {
        game = new GameOfLifeClass(inputGrid);

        objectPool = new List<GameObject>();
        cells = new GameObject("cells");

        int width = game.gridPattern.GetLength(0);
        int hight = game.gridPattern.GetLength(1);

        var main = Camera.main;
        main.orthographicSize = Mathf.Sqrt(width*hight)*0.6f;
        main.transform.position = new Vector3(width/2,hight/2,main.transform.position.z);
        
        StartCoroutine("StartCycles");
    }
    
    IEnumerator StartCycles(){
        RenderGeneration(game.gridPattern);
        yield return new WaitForSeconds(0.20f); //trick the user into seeing their pattern more time
        
        while (enabled){
            game.CalculateNextGen();
            yield return new WaitForSeconds(0.020f);
            RenderGeneration(game.gridPattern);
        }
    }
  
    void RenderGeneration(int[,] grid,bool redraw = true){
        int width = grid.GetLength(0);
        int hight = grid.GetLength(1);
        

        if (redraw){
            aliveCells = 0;
            foreach (var cell in objectPool)
            {
                cell.SetActive(false);
            }
        }

        for (int Y = 0; Y < hight; Y++)
        {
            for (int X = 0; X < width; X++)
            {
                if (grid[X,Y] >= 1){
                    if (objectPool.Count <= aliveCells){
                        objectPool.Add(Instantiate(Prefab,cells.transform));
                    }
                    if (redraw) objectPool[aliveCells].SetActive(true);
                    objectPool[aliveCells].transform.position = new Vector3(width - X,hight - Y);
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

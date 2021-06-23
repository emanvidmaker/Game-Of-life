using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameOfLife;

public class GameOfLifeRenderer : MonoBehaviour
{
     public static int[,] startPattern = new int[16, 16]{
        {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,1,0,0,0,0,1,1,0,0,0,0,0,0,0},
        {0,0,1,0,0,0,1,0,1,0,0,0,0,0,0,0},
        {1,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0}
        };
    GameOfLifeClass game;

    public GameObject Prefab;
    public int size = 64;

    GameObject cells;
    List<GameObject> objectPool; 
    // Start is called before the first frame update
    void Start()=>StartSimulation(RandomPatternGenerator(size,size));
    void StartSimulation(int[,] Size)
    {
        game = new GameOfLifeClass(Size);

        objectPool = new List<GameObject>();
        cells = new GameObject("cells");

        int width = game.gridPattern.GetLength(0);
        int hight = game.gridPattern.GetLength(1);

        var main = Camera.main;
        main.orthographicSize = Mathf.Sqrt(width*hight)*0.6f;
        main.transform.position = new Vector3(width/2,hight/2,main.transform.position.z);
        // RenderGeneration(game.gridPattern);
        
        StartCoroutine("StartCycles");
    }
    IEnumerator StartCycles(){
        while (enabled){
            RenderGeneration(game.gridPattern);
            game.CalculateNextGen();
            yield return new WaitForSeconds(0.020f);
        }
    }
  
    void RenderGeneration(int[,] grid,bool redraw = true){
        int width = grid.GetLength(0);
        int hight = grid.GetLength(1);
        int aliveCells = 0;

        if (redraw) foreach (var cell in objectPool)
        {
            cell.SetActive(false);
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
                    objectPool[aliveCells].transform.position = new Vector3(hight - Y,width - X);
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

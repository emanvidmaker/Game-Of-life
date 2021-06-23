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
    List<int[,]> iterations;

    GameOfLifeClass game; 
    Camera Camera;
    // Start is called before the first frame update
    void Start()
    {
        int size = 16;
        game = new GameOfLifeClass(RandomPatternGenerator(size,size));



        objectPool = new List<GameObject>();
        iterations = new List<int[,]>(); 
        cells = new GameObject("cells");
        int width = game.gridPattern.GetLength(0);
        int hight = game.gridPattern.GetLength(1);

        Camera = Camera.main;
        
        Camera.orthographicSize = Mathf.Sqrt(width*hight)*0.55f;
        Camera.transform.position = new Vector3(width/2,hight/2,Camera.transform.position.z);
        // RenderGeneration(game.gridPattern);
        
        StartCoroutine("StartCycles");
    }
      public GameObject Prefab;
    GameObject cells;
    List<GameObject> objectPool;
    IEnumerator StartCycles(){
        for (int i = 0; i < 100; i++)
        {
            iterations.Add(game.gridPattern);
            RenderGeneration(game.gridPattern,i,false);
            game.CalculateNextGen();
            Camera.transform.position += new Vector3(0,0,1);
            yield return new WaitForSeconds(0.020f);
        }
    }
    int aliveCells = 0;

    void RenderGeneration(int[,] grid, int Z = 0 , bool redraw = true){
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
                    objectPool[aliveCells].transform.position = new Vector3(hight - Y,width - X,Z);
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

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
    // Start is called before the first frame update
    void Start()
    {
        int size = 64;
        game = new GameOfLifeClass(RandomPatternGenerator(size,size));



        ObjectPool = new List<GameObject>();
        cells = new GameObject("cells");
        int width = game.gridPattern.GetLength(0);
        int hight = game.gridPattern.GetLength(1);

        var main = Camera.main;
        main.orthographicSize = Mathf.Sqrt(width*hight)*0.6f;
        main.transform.position = new Vector3(width/2,hight/2,main.transform.position.z);
        // RenderGeneration(game.gridPattern);
        
        StartCoroutine("StartCycles");
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
    public GameObject Prefab;
    GameObject cells;
    List<GameObject> ObjectPool;
    IEnumerator StartCycles(){
        while (enabled){
            RenderGeneration(game.gridPattern);
            game.CalculateNextGen();
            yield return new WaitForSeconds(0.020f);
        }
    }
    void Update()
    {
       
    }

    void RenderGeneration(int[,] grid,bool redraw = true){
        int width = grid.GetLength(0);
        int hight = grid.GetLength(1);
        int aliveCells = 0;

        if (redraw) foreach (var cell in ObjectPool)
        {
            cell.SetActive(false);
        }

        for (int Y = 0; Y < hight; Y++)
        {
            for (int X = 0; X < width; X++)
            {
                if (grid[X,Y] >= 1){
                    if (ObjectPool.Count <= aliveCells){
                        ObjectPool.Add(Instantiate(Prefab,cells.transform));
                    }
                    if (redraw) ObjectPool[aliveCells].SetActive(true);
                    ObjectPool[aliveCells].transform.position = new Vector3(hight - Y,width - X);
                    aliveCells ++;

                }
            }
        }
        
    }
}

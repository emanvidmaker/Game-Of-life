using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridObject : MonoBehaviour
{
    public  TextMeshProUGUI uGUI;
    public const int WIDTH = 16;
    public const int HIGHT = 16;
    [SerializeField()]
    public int[,] neighborsGrid = new int[WIDTH,HIGHT];
    public int[,] grid = new int[WIDTH,HIGHT]{
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
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
        {0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0}
    };
    private void Start() {
        uGUI.text = PrintGrid(grid);
    }
   
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            grid = CalculateNextGen(grid);
            uGUI.text = PrintGrid(grid);
        }

    }
    string PrintGrid(int[,] grid){
        string output = "";
        for (int X = 0; X < WIDTH; X++)
        {
            for (int Y = 0; Y < HIGHT; Y++)
            {
                if (grid[X,Y] == 1)
                    output += "<color=green>"+"•"+"</color>";
                else
                    output += "-";
                output += "";
            }
            output += System.Environment.NewLine;
        }

        return output;
    }

    int[,] CalculateNextGen(int[,] grid)
    {
        neighborsGrid = CalculateNeighbors(neighborsGrid);
        var localGrid = new int[WIDTH,HIGHT];
        for (int Y = 0; Y < HIGHT; Y++)
        {
            for (int X = 0; X < WIDTH; X++)
            {
                int currentCell = grid[X,Y];
                int neighbors = neighborsGrid[X,Y];
                
                if (currentCell == 1){ //if on
                    if (neighbors == 1){ currentCell = 0;} //has only 1 neighbors then it turns OFF in the next turn. (SOLITUDE)
                    if (neighbors >= 4) {currentCell = 0;} //has 4 or more neighbors then it turns OFF in the next turn. (OVERPOPULATION)
                    if ((neighbors == 2)||(neighbors == 3)) {currentCell = 1;}// has 2 or 3 neighbors then it remains ON in the next turn.
                }
                else if (currentCell == 0){ //if off
                    if (neighbors == 3) currentCell = 1; //has exactly 3 neighbors then it turns ON in the next turn. (REPRODUCTION)
                }

                localGrid[X,Y] = currentCell;

            }
        }
        return localGrid;
    }

    int[,] CalculateNeighbors(int[,] Ngrid){
        for (int Y = 0; Y < HIGHT; Y++)
        {
            for (int X = 0; X < WIDTH; X++)
            {
                Ngrid[X,Y] = 0;
                for (int nY = -1; nY <= 1; nY++)
                {
                    for (int nX = -1; nX <= 1; nX++)
                    {
                        if (!(nX == 0 && nY == 0))
                            {Ngrid[X,Y] += grid[(nX+X+WIDTH)%WIDTH,(nY+Y+HIGHT)%HIGHT];}
                    } 
                }
            }
        }
        return Ngrid;
    }
}

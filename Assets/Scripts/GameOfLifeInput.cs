using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOfLifeInput : MonoBehaviour
{
    public static int[,] storedPattern = new int[16, 16]{
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

    public static UnityEvent<int[,]> PlayOutStart = new UnityEvent<int[,]>();
    
    public Slider sizeSlider;
    public GameObject buttonPrefab;
    public GameObject buttonParentObject;
    RectTransform buttonParentRect;
    public GridLayoutGroup uiGrid;
    public GameOfLifeRenderer _renderer;
    List<GameObject> buttonPool;

    public Color colorDead,colorDeadAlt,colorAlive;
    
    void Start()
    {
        buttonParentRect =  buttonParentObject.GetComponent<RectTransform>();
        buttonPool = new List<GameObject>();
        RenderUiGrid();
        sizeSlider.onValueChanged.AddListener(SetSize);
    }
    void RenderUiGrid(){
        var width = storedPattern.GetLength(0);
        var hight = storedPattern.GetLength(1);
        // uiGrid.constraintCount = width;
        
        
        foreach (var bnt in buttonPool)
        {
            bnt.SetActive(false);
        }

        uiGrid.cellSize = new Vector2((1f/(float)width)*buttonParentRect.rect.width, (1f/(float)hight)*buttonParentRect.rect.height);
        
        int pos = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < hight; y++)
            {
                if (buttonPool.Count <= pos){
                    buttonPool.Add(Instantiate(buttonPrefab,buttonParentObject.transform));
                }
                buttonPool[pos].SetActive(true);
                buttonPool[pos].transform.localPosition = new Vector3 (x,y,0);
                var bnt = buttonPool[pos].GetComponent<Button>();
                var func = bnt.GetComponent<GameOfLifePatternButton>();

                func.pos = new Vector2Int(x,y);
                func.UpdateCell();
                bnt.onClick.RemoveAllListeners();
                bnt.onClick.AddListener(func.SetCell);
                pos ++;

            }
        }
    }
     
    // Update is called once per frame
    public void ResetScene() {
     SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }
    public void StartPlayOut(){
        PlayOutStart.Invoke(storedPattern);
        _renderer.StartSimulation(storedPattern);

    }
    public void SetSize(float value){

        int valueInt = (int)Mathf.Floor(value);

        var prevPattern = storedPattern;

        var newPattern = new int[valueInt,valueInt];

        // //lossless transfer of values (does not work)
        // for (int x = 0; x < Mathf.Min(valueInt,prevPattern.GetLength(0)); x++)
        // {
        //     for (int y = 0; y < Mathf.Min(valueInt,prevPattern.GetLength(1)); y++)
        //     {
        //         newPattern[x,y] = prevPattern[x,y];
        //     }
        // }
        storedPattern = newPattern;
        RenderUiGrid();
    }
    

}

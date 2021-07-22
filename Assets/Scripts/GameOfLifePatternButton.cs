using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOfLifePatternButton : MonoBehaviour
{   
    public Vector2Int pos {
        get{return _pos;}
        set{
            _pos = value;
            UpdateCell();
            }
        }
    private Vector2Int _pos ;
    static GameOfLifeInput mannager;

    void Start(){
    }

   public void SetCell(){
        GameOfLifeInput.storedPattern[pos.x,pos.y] = (GameOfLifeInput.storedPattern[pos.x,pos.y] == 1)? 0 : 1;
        UpdateCell();
   }
    public void UpdateCell(){
        if (!mannager) mannager = FindObjectOfType<GameOfLifeInput>();

        GameOfLifeInput.storedPattern[pos.x,pos.y] = (GameOfLifeInput.storedPattern[pos.x,pos.y] == 0)? 0 : 1;
        GetComponent<Image>().color = 
        (GameOfLifeInput.storedPattern[pos.x,pos.y] == 0)? 
        (((pos.x+pos.y)%2 == 0)?mannager.colorDead : mannager.colorDeadAlt)
         : mannager.colorAlive;
    }
}

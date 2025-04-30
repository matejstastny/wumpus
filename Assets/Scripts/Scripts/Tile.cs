using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Tile : MonoBehaviour
{
    private GameObject gcobj;
    private gamecontroller gc;
    public bool[] doors;
    public Tile[] adjacentTiles;
    public int x;
    public int y;
    // Start is called before the first frame update
    void Start()
    {
        doors= new bool[6];
        adjacentTiles= new Tile[6];
        gcobj=GameObject.FindWithTag("GameController");
        gc=gcobj.GetComponent<gamecontroller>();
        HexCodeX=gc.coordToHexCode(transform.position.x,transform.position.y)[0];
        HexCodeY=gc.coordToHexCode(transform.position.x,transform.position.y)[1];
        adjacentTiles[1]=gc.grid[x,(y>gc.maxY)?0:y+1];
        adjacentTiles[4]=gc.grid[x,(y==0)?gc.maxY:y-1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    int HexCodeX;
    int HexCodeY;
    public Tile[] Ajecenttiles(int x,int y){
        return new Tile[0];
    }
    GameObject[] things;
    public void Addthing(){

    }
    public void Removething(){

    }

}

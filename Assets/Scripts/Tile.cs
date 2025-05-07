using UnityEngine;

public class Tile : MonoBehaviour
{
    public Tile[] adjacentTiles;
    public bool[] doors;
    public int x;
    public int y;

    private new GameObject gameObject;
    private GameController gc;
    private int hexCodeX;
    private int hexCodeY;
    private GameObject[] things;

    // Main ---------------------------------------------------------------------

    void Start()
    {
        doors= new bool[6];
        gameObject=GameObject.FindWithTag("GameController");
        gc=gameObject.GetComponent<GameController>();
        InitializeAdjecentTiles();
    }
    // Accesors -----------------------------------------------------------------

    public Tile[] GetAjecentTiles(int x, int y)
    {
        return new Tile[0];
    }

    // Private -----------------------------------------------------------------

    private void InitializeAdjecentTiles() {
        adjacentTiles= new Tile[6];
        hexCodeX=(int)gc.WorldPositionToHexPosition(transform.position.x,transform.position.y)[0];
        hexCodeY=(int)gc.WorldPositionToHexPosition(transform.position.x,transform.position.y)[1];
        adjacentTiles[1]=gc.grid[(x>gc.gridWidth-2)?0:x+1,y];
        adjacentTiles[4]=gc.grid[(x==0)?gc.gridWidth-1:x-1,y];
        adjacentTiles[0]=gc.grid[(y%2==0)?(x>gc.gridWidth-2)?0:x+1:x,(y>gc.gridHeight-2)?0:y+1];
        adjacentTiles[2]=gc.grid[(y%2==0)?(x==0)?gc.gridWidth-1:x-1:x,(y==0)?gc.gridHeight-1:y-1];
        adjacentTiles[3]=gc.grid[(y%2==0)?x:(x>gc.gridWidth-2)?0:x+1,(y==0)?gc.gridHeight-1:y-1];
        adjacentTiles[5]=gc.grid[(y%2==0)?x:(x==0)?gc.gridWidth-1:x-1,(y>gc.gridHeight-2)?0:y+1];
    }
}

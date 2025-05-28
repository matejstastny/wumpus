using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Tile[] adjacentTiles;
    public bool[] doors;
    public int x;
    public int y;

    private new GameObject gameObject;
    private GameController gc;
    public int hexCodeX;
    public int hexCodeY;
    private GameObject[] things;
    public GameObject doorPrefab;

    // Main ---------------------------------------------------------------------

    void Start()
    {
        doors = new bool[6];
        gameObject = GameObject.FindWithTag("GameController");
        gc = gameObject.GetComponent<GameController>();
        InitializeAdjecentTiles();
        FinializeMaze();
        //if (hexCodeX==hexCodeY)RecursiveInitializeMaze();
        //for(int n=0; n<4; n++) RenderDoors(n);// RenderDoors Tester
    }
    // Accesors -----------------------------------------------------------------

    public Tile[] GetAdjecentTiles(int x, int y)
    {
        return new Tile[0];
    }
    public int DoorNum()
    {
        int ret = 0;
        for (int n = 0; n < 5; n++) if (doors[n]) ret++;
        return ret;
    }

    // Private -----------------------------------------------------------------

    private void InitializeAdjecentTiles()
    {
        adjacentTiles = new Tile[6];
        hexCodeX = (int)gc.WorldPositionToHexPosition(transform.position.x, transform.position.y)[0];
        hexCodeY = (int)gc.WorldPositionToHexPosition(transform.position.x, transform.position.y)[1];
        adjacentTiles[1] = gc.grid[(x > gc.gridWidth - 2) ? 0 : x + 1, y];
        adjacentTiles[4] = gc.grid[(x == 0) ? gc.gridWidth - 1 : x - 1, y];
        adjacentTiles[0] = gc.grid[(y % 2 == 0) ? (x > gc.gridWidth - 2) ? 0 : x + 1 : x, (y > gc.gridHeight - 2) ? 0 : y + 1];
        adjacentTiles[2] = gc.grid[(y % 2 == 0) ? (x == 0) ? gc.gridWidth - 1 : x - 1 : x, (y == 0) ? gc.gridHeight - 1 : y - 1];
        adjacentTiles[3] = gc.grid[(y % 2 == 0) ? x : (x > gc.gridWidth - 2) ? 0 : x + 1, (y == 0) ? gc.gridHeight - 1 : y - 1];
        adjacentTiles[5] = gc.grid[(y % 2 == 0) ? x : (x == 0) ? gc.gridWidth - 1 : x - 1, (y > gc.gridHeight - 2) ? 0 : y + 1];
    }
    private void RenderDoors(int direction)
    {//broken
        float[] hexPosition = gc.HexCodeToWorldPosition(x - gc.gridWidth / 2, y - gc.gridHeight / 2);
        Vector3 tilePosition = new(hexPosition[0], hexPosition[1], 0);
        float[] adjHexPosition = gc.HexCodeToWorldPosition(adjacentTiles[direction].x - gc.gridWidth / 2, adjacentTiles[direction].y - gc.gridHeight / 2);
        Vector3 adjTilePosition = new(adjHexPosition[0], adjHexPosition[1], 0);
        Instantiate(doorPrefab, new Vector3((x > gc.gridWidth - 1 || x == 0) ? 100 : (tilePosition.x - ((tilePosition.x - adjTilePosition.x) / 2)),
        (y > gc.gridHeight - 1 || y == 0) ? ((direction == 0 || direction == 5) ? tilePosition.y + 0.5f //check if normal and hanle top if not
        : (direction == 2 || direction == 3) ? tilePosition.y - 0.5f : tilePosition.y) ://cases for botton and sides
        (tilePosition.y - ((tilePosition.y - adjTilePosition.y) / 2)), 0)//default case
        , transform.rotation);
    }
    // Initilize Maze -------------------------------------------------------------
    public void RecursiveInitializeMaze()
    {
        bool finialize=CheckForMazeFinalization();
        int doorToConsider = Random.Range(0, 6);
        while (adjacentTiles[doorToConsider].DoorNum() < 1&&!finialize)
        {
            doorToConsider = Random.Range(0, 6);
            if (adjacentTiles[doorToConsider].DoorNum() < 1)
            {
                doors[doorToConsider] = true;
                adjacentTiles[doorToConsider].doors[doorToConsider>2?doorToConsider-3:doorToConsider+3] = true;
                adjacentTiles[doorToConsider].RecursiveInitializeMaze();
                return;
            }
        }

    }
    private bool CheckForMazeFinalization(){
        bool finialize = true;
        foreach (Tile adjTile in adjacentTiles)
        {
            if (adjTile.DoorNum() < 1) finialize = false;
        }
        if (finialize)
        {
            foreach (Tile tile in gc.grid)
            {
                tile.FinializeMaze();
            }
        }
        return finialize;
    }
    public void FinializeMaze()
    {
        int doorToConsider = Random.Range(0, 6);
        doors[doorToConsider] = true;
        adjacentTiles[doorToConsider].doors[doorToConsider>2?doorToConsider-3:doorToConsider+3] = true;
    }

}
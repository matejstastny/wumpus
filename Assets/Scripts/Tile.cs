using UnityEngine;

public class Tile : MonoBehaviour
{
    public Tile[] adjacentTiles;
    public bool[] doors;
    public int x;
    public int y;

    private new GameObject gameObject;
    private GameController gameController;
    private int hexCodeX;
    private int hexCodeY;

    // Main ---------------------------------------------------------------------

    void Start()
    {
        doors = new bool[6];
        adjacentTiles = new Tile[6];
        gameObject = GameObject.FindWithTag("GameController");
        gameController = gameObject.GetComponent<GameController>();
        hexCodeX = (int)gameController.WorldPositionToHexPosition(transform.position.x, transform.position.y)[0];
        hexCodeY = (int)gameController.WorldPositionToHexPosition(transform.position.x, transform.position.y)[1];
        adjacentTiles[1] = gameController.grid[x, (y > gameController.gridWidth) ? 0 : y + 1];
        adjacentTiles[4] = gameController.grid[x, (y == 0) ? gameController.gridHeight : y - 1];
    }

    // Accesors -----------------------------------------------------------------

    public Tile[] GetAjecentTiles(int x, int y)
    {
        return new Tile[0];
    }
}

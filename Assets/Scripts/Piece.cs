using UnityEngine;

public class Piece : MonoBehaviour
{
    public int reach = 1;
    public int side = 1;
    public int startRow;
    public int startColumn;

    public Material sideOne;
    public Material sideTwo;
    public Material sideThree;

    private new SpriteRenderer renderer;
    public bool hasAttacked;
    private bool hasMoved;
    public int id;
    public new string name;

    // Constructor --------------------------------------------------------------

    public Piece(string name, int reach, int side, int hp, int row, int col)
    {
        this.name = name;
        this.reach = reach;
        this.side = side;
        startRow = row;
        startColumn = col;
    }

    // Main ---------------------------------------------------------------------

    void Start()
    {
        id = UnityEngine.Random.Range(1, 100000000);
        Debug.Log(id);
        if (Mathf.Round(startColumn / (Mathf.Sqrt(3) / 2.0f)) * (Mathf.Sqrt(3) / 2.0f) % Mathf.Sqrt(3) == 0)
        {
            transform.position = new Vector2(
            startRow,
            Mathf.Round(startColumn / (Mathf.Sqrt(3) / 2.0f)) * (Mathf.Sqrt(3) / 2.0f)
        );
        }
        else
        {
            transform.position = new Vector2(
            startRow - 0.5f,
            Mathf.Round(startColumn / (Mathf.Sqrt(3) / 2.0f)) * (Mathf.Sqrt(3) / 2.0f)
        );
        }
        renderer = GetComponent<SpriteRenderer>();
        if (side == 1)
        {
            renderer.material = sideOne;
        }
        else if (side == 2)
        {
            renderer.material = sideTwo;
        }
        else
        {
            renderer.material = sideThree;
        }
    }

    // Accesors -----------------------------------------------------------------

    public int Getreach()
    {
        return reach;
    }
    public bool HasMoved()
    {
        return hasMoved;
    }
    public void Move()
    {
        hasMoved = true;
    }
    public void Reset()
    {
        hasMoved = false;
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public int GetID()
    {
        return id;
    }
}

using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject piecePrefab;
    public GameObject tilePrefab;

    public int currentTurn = 1;
    public int gridWidth = 5;
    public int gridHeight = 6;
    public Tile[,] grid;

    private Collider2D targetCollider;
    private GameObject selectedPiece;
    private Vector3 mousePosition;
    private Vector3 targetPosition;
    private Vector3 dragOffset;
    private Vector2 originalPosition;
    private Piece selectedPieceData;
    private Piece pieceData;
    private Piece[][] sideTracker;

    // Main ---------------------------------------------------------------------

    void Start()
    {
        InitializeGrid();
        InitializePieces();
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        HandleMouseInput();
        CheckTurnChange();
    }

    // Initialization -----------------------------------------------------------

    private void InitializeGrid()
    {
        grid = new Tile[gridWidth, gridHeight];
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                float[] hexPosition = HexCodeToWorldPosition(x - gridWidth / 2, y - gridHeight / 2);
                Vector3 tilePosition = new(hexPosition[0], hexPosition[1], 0);
                GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
                grid[x, y] = tile.GetComponent<Tile>();
                grid[x, y].x = x;
                grid[x, y].y = y;
            }
        }
    }

    private void InitializePieces()
    {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("Piece");
        foreach (GameObject piece in pieces)
        {
            pieceData = piece.GetComponent<Piece>();
            if (pieceData != null)
            {
                pieceData.Move();
                if (pieceData.side == currentTurn)
                {
                    pieceData.Reset();
                }
            }
        }
    }

    // Mouse --------------------------------------------------------------------

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
        else if (Input.GetMouseButton(0))
        {
            OnMouseDrag();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
        }
    }

    private void OnMouseDown()
    {
        targetCollider = Physics2D.OverlapPoint(mousePosition);
        if (targetCollider != null && targetCollider.GetComponent<Piece>() != null)
        {
            selectedPiece = targetCollider.gameObject;
            originalPosition = selectedPiece.transform.position;
            dragOffset = selectedPiece.transform.position - mousePosition;
            selectedPieceData = selectedPiece.GetComponent<Piece>();
        }
    }

    private void OnMouseDrag()
    {
        if (selectedPiece != null && !selectedPieceData.HasMoved())
        {
            selectedPiece.transform.position = mousePosition + dragOffset;
            targetPosition = selectedPiece.transform.position;
            float[] nearestHex = WorldPositionToHexPosition(targetPosition.x, targetPosition.y);
            selectedPiece.transform.position = new Vector3(nearestHex[0], nearestHex[1], Mathf.Round(targetPosition.z + 0.5f) - 0.5f);
        }
    }

    private void OnMouseUp()
    {
        if (selectedPiece != null)
        {
            int movementRange = selectedPieceData.GetSpeed();
            if (!IsMoveValid(movementRange))
            {
                UndoMove();
            }
            else
            {
                HandlePieceInteraction(movementRange);
                selectedPieceData.Move();
                selectedPiece = null;
            }
        }
    }

    // Checks -------------------------------------------------------------------

    private bool IsMoveValid(int range)
    {
        if (IsOutOfRange(range) || IsMoveIllegal())
        {
            return false;
        }
        return true;
    }

    private bool IsOutOfRange(int range)
    {
        float distance = Vector2.Distance(selectedPiece.transform.position, originalPosition);
        return distance > range || distance == 0;
    }

    private bool IsMoveIllegal()
    {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("Piece");
        foreach (GameObject piece in pieces)
        {
            pieceData = piece.GetComponent<Piece>();
            if (pieceData != null && pieceData != selectedPieceData && pieceData.side == currentTurn &&
                selectedPiece.transform.position == piece.transform.position)
            {
                return true;
            }
        }
        return false;
    }

    // Actions ------------------------------------------------------------------

    private void HandlePieceInteraction(int range)
    {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("Piece");
        foreach (GameObject piece in pieces)
        {
            pieceData = piece.GetComponent<Piece>();
            if (pieceData != null && pieceData != selectedPieceData && pieceData.side != currentTurn &&
                selectedPiece.transform.position == piece.transform.position && !IsOutOfRange(range))
            {
                pieceData.Die();
                selectedPieceData.Move();
                ResetSelection();
            }
        }
    }

    private void UndoMove()
    {
        selectedPiece.transform.position = originalPosition;
        ResetSelection();
    }

    private void ResetSelection()
    {
        selectedPiece = null;
        selectedPieceData = null;
        pieceData = null;
    }

    private void CheckTurnChange()
    {
        if (IsTurnComplete())
        {
            AdvanceTurn();
            ResetPiecesForCurrentTurn();
        }
    }

    private bool IsTurnComplete()
    {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("Piece");
        foreach (GameObject piece in pieces)
        {
            pieceData = piece.GetComponent<Piece>();
            if (pieceData != null && !pieceData.HasMoved() && pieceData.side == currentTurn)
            {
                return false;
            }
        }
        return true;
    }

    private void AdvanceTurn()
    {
        currentTurn = currentTurn < 2 ? currentTurn + 1 : 1;
    }

    private void ResetPiecesForCurrentTurn()
    {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("Piece");
        foreach (GameObject piece in pieces)
        {
            pieceData = piece.GetComponent<Piece>();
            if (pieceData != null && pieceData.side == currentTurn)
            {
                pieceData.Reset();
            }
        }
    }

    // Conversion ---------------------------------------------------------------

    public float[] WorldPositionToHexPosition(float x, float y)
    {
        float[] result = new float[2];
        if (Mathf.Round(y / (Mathf.Sqrt(3) / 2.0f)) * (Mathf.Sqrt(3) / 2.0f) % Mathf.Sqrt(3) == 0)
        {
            result[0] = Mathf.Round(x);
        }
        else
        {
            result[0] = Mathf.Round(x + 0.5f) - 0.5f;
        }
        result[1] = Mathf.Round(y / (Mathf.Sqrt(3) / 2.0f)) * (Mathf.Sqrt(3) / 2.0f);
        return result;
    }

    public float[] HexCodeToWorldPosition(float x, float y)
    {
        float[] result = new float[2];
        result[0] = y % 2 == 0 ? x : x + 0.5f;
        result[1] = y * (Mathf.Sqrt(3) / 2.0f);
        return result;
    }

    // Accesors -----------------------------------------------------------------

    public Piece GetPieceData()
    {
        return selectedPieceData;
    }
}

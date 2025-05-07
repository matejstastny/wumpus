/*
This file skips to the next "player's" turn when a button is pressed. Therefore I don't think this script does anything at the moment.



*/
using UnityEngine;
using UnityEngine.UI;

public class EndTurn : MonoBehaviour
{

    public Button button;

    private GameController gameController;
    private new SpriteRenderer renderer;
    private new GameObject gameObject;
    private Piece pieceData;

    // Main ---------------------------------------------------------------------

    void Start()
    {
        gameObject = GameObject.FindWithTag("GameController");
        gameController = gameObject.GetComponent<GameController>();
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(OnClickTask);
    }

    void OnClickTask()
    {
        if (gameController.currentTurn <= 2)
        {
            gameController.currentTurn++;
            GameObject[] localpeices = GameObject.FindGameObjectsWithTag("Peice");
            foreach (GameObject peice in localpeices)
            {
                pieceData = peice.GetComponent<Piece>();
                if (pieceData != null && pieceData.side == gameController.currentTurn)
                {
                    pieceData.Reset();
                }
                else pieceData.Move();
            }
        }
        else if (gameController.currentTurn > 2)
        {
            gameController.currentTurn = 1;
            GameObject[] localpeices = GameObject.FindGameObjectsWithTag("Peice");
            foreach (GameObject peice in localpeices)
            {
                pieceData = peice.GetComponent<Piece>();
                if (pieceData != null && pieceData.side == gameController.currentTurn)
                {
                    pieceData.Reset();
                }
                else pieceData.Move();
            }
        }
    }

    // Materials ----------------------------------------------------------------

    public Material sideOne;
    public Material sideTwo;
    public Material sideThree;

    void Update()
    {
        renderer = GetComponent<SpriteRenderer>();
        if (gameController.currentTurn == 1)
        {
            renderer.material = sideOne;
        }
        else if (gameController.currentTurn == 2)
        {
            renderer.material = sideTwo;
        }
        else
        {
            renderer.material = sideThree;
        }
    }
}

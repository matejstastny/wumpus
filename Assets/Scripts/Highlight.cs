//enables/disables highlight based on selection.
using UnityEngine;

public class Highlight : MonoBehaviour
{
    private new GameObject gameObject;
    private GameController gameController;
    private SpriteRenderer spriteRenderer;
    public int parentID;

    void Start()
    {
        gameObject = GameObject.FindWithTag("GameController");
        gameController = gameObject.GetComponent<GameController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    void Update()
    {
        Piece parentdata = GetComponentInParent<Piece>();
        if (parentdata != null && parentID == 0)
        {
            parentID = parentdata.GetID();
        }
        if (gameController.GetPieceData().id == parentID)
            spriteRenderer.enabled = true;
        else
        {
            spriteRenderer.enabled = false;
        }
    }
}

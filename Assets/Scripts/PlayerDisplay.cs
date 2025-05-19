using UnityEngine;
using UnityEngine.UI;

// Indicator for who's turn it is
public class PlayerDisplay : MonoBehaviour
{
    public Material sideOne;
    public Material sideTwo;
    public Material sideThree;

    private SpriteRenderer spriteRenderer;
    private GameController gameController;
    private new GameObject gameObject;

    // Main ---------------------------------------------------------------------

    void Start()
    {
        gameObject = GameObject.FindWithTag("GameController");
        gameController = gameObject.GetComponent<GameController>();
    }

    void Update()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (gameController.currentTurn == 1)
        {
            spriteRenderer.material = sideOne;
        }
        else if (gameController.currentTurn == 2)
        {
            spriteRenderer.material = sideTwo;
        }
        else
        {
            spriteRenderer.material = sideThree;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    public Button button;

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
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
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

    // Events -------------------------------------------------------------------

    private void TaskOnClick()
    {
        //transform.Translate(10,10,0);
        Debug.Log("You have clicked the button!");
    }
}

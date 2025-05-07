using UnityEngine;
using TMPro;
// shows info on selected "unit" on corrisopnding object in text form
public class UsnitInfo : MonoBehaviour
{
    [SerializeField] TMP_Text info;
    private GameController gameController;
    private new GameObject gameObject;

    void Start()
    {
        gameObject = GameObject.FindWithTag("GameController");
        gameController = gameObject.GetComponent<GameController>();

    }

    void Update()
    {
        if (gameController != null)
        {
            info.text = "Unit: " + gameController.GetPieceData().name + "    Range: 0\nSpeed: " +
            gameController.GetPieceData().reach + "	Damage: null\nSide: " +
            gameController.GetPieceData().side + "\nHP: 1";
        }
    }
}

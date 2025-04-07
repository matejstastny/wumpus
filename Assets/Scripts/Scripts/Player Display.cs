using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    private SpriteRenderer renderer;
    public Button yourbutton;
    private GameObject gcobj;
    private gamecontroller gc;
    public Material sideOne;
    public Material sideTwo;
    public Material sideThree;
    // Start is called before the first frame update
    void Start()
    {
        gcobj=GameObject.FindWithTag("GameController");
        gc=gcobj.GetComponent<gamecontroller>();
        Button btn=yourbutton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        renderer=GetComponent<SpriteRenderer>();
        if(gc.GetTurn()==1){
            renderer.material=sideOne;
        }
        else if(gc.GetTurn()==2){
            renderer.material=sideTwo;
        }
        else{
            renderer.material=sideThree;
        }
    }
    void TaskOnClick(){
        //transform.Translate(10,10,0);
        Debug.Log ("You have clicked the button!");
    }
}

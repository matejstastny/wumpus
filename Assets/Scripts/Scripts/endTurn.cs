using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class endTurn : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject gcobj;
    private gamecontroller gc;
    private Peice pData;
    private SpriteRenderer renderer;

    public Button yourbutton;

    void Start()
    {
        gcobj=GameObject.FindWithTag("GameController");
        gc=gcobj.GetComponent<gamecontroller>();
        Button btn=yourbutton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    //void Update()
   // {
        
    //}
    void TaskOnClick(){
        if(gc.turnTrack<=2){
            gc.turnTrack++;
            GameObject[] localpeices=GameObject.FindGameObjectsWithTag("Peice");
            foreach(GameObject peice in localpeices){
                pData=peice.GetComponent<Peice>();
                if(pData!=null&&pData.side==gc.turnTrack){
                    pData.Reset();
                }
                else pData.Move();
            }
        }else if(gc.turnTrack>2){
            gc.turnTrack=1;
            GameObject[] localpeices=GameObject.FindGameObjectsWithTag("Peice");
            foreach(GameObject peice in localpeices){
                pData=peice.GetComponent<Peice>();
                if(pData!=null&&pData.side==gc.turnTrack){
                    pData.Reset();
                }
                else pData.Move();
            }
        }
    }
    //color change
    public Material sideOne;
    public Material sideTwo;
    public Material sideThree;
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
}

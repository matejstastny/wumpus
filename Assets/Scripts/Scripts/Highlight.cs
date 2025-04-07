using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject gcobj;
    private gamecontroller gc;
    private SpriteRenderer rend;
    public int parentID;
    // Start is called before the first frame update
    void Start()
    {
        gcobj=GameObject.FindWithTag("GameController");
        gc=gcobj.GetComponent<gamecontroller>();
        rend=GetComponent<SpriteRenderer>();
        rend.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        Peice parentdata = GetComponentInParent<Peice>();
        if (parentdata != null&&parentID==0)
        {
            parentID=parentdata.getID();
        }
        if(gc.getPeiceData().ID==parentID)
            rend.enabled=true;
        else{
            rend.enabled=false;
        }
    }
}
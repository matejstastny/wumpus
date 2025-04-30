using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class unitInfo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMP_Text info;
    private GameObject gcobj;
    private gamecontroller gc;
    // Start is called before the first frame update
    void Start()
    {
        gcobj=GameObject.FindWithTag("GameController");
        gc=gcobj.GetComponent<gamecontroller>();
        
    }

    // Update is called once per frame
    void Update()
    {
        info.text="Unit: "+gc.getPeiceData().name+"    Range: 0\nSpeed: "+
        gc.getPeiceData().speed+"	Damage: null\nSide: "+
        gc.getPeiceData().side+"\nHP: 1";
        }
}

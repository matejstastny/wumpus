using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Peice : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed=1;
    public int side=1;
    public int startRow;
    public int startColumn;
    private SpriteRenderer renderer;
    public Material sideOne;
    public Material sideTwo;
    public Material sideThree;
    private bool moved;
    public bool attacked;
    public int ID;
    public string name;
    public Peice(String name, int speed, int side, int HP, int row, int col){
        this.name=name;
        this.speed=speed;
        this.side=side;
        //HP
        startRow=row;
        startColumn=col;
    }
    void Start()
    {
        ID=UnityEngine.Random.Range(1,100000000);
        Debug.Log (ID);
        if(Mathf.Round(startColumn/(Mathf.Sqrt(3)/2.0f))*(Mathf.Sqrt(3)/2.0f)%Mathf.Sqrt(3)==0){
                transform.position = new Vector2(
                startRow, 
                Mathf.Round(startColumn/(Mathf.Sqrt(3)/2.0f))*(Mathf.Sqrt(3)/2.0f)
            );}else{
                transform.position = new Vector2(
                startRow-0.5f, 
                Mathf.Round(startColumn/(Mathf.Sqrt(3)/2.0f))*(Mathf.Sqrt(3)/2.0f)
            );
            }
        renderer=GetComponent<SpriteRenderer>();
        if(side==1){
            renderer.material=sideOne;
        }
        else if(side==2){
            renderer.material=sideTwo;
        }
        else{
            renderer.material=sideThree;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int getSpeed(){
        return speed;
    }
    public bool getMoved(){
        return moved;
    }
    public void Move(){
        moved=true;
    }
    public void Reset(){
        moved=false;
    }
    public void die(){
        Destroy(this.gameObject);
    }
    public int getID(){
        return ID;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class gamecontroller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject selectedObject;
    public GameObject peicePrefab;
    public GameObject TilePrefab;
    Vector3 offset;
    private Vector3 targetPos;
    private Peice peiceData;
    private Vector2 orgpos;
    private Peice[][] sideTracker; //interior peices, exterior sides
    public int turnTrack=1;
    private Peice pData;
    Vector3 mousePosition;
    Collider2D targetObject;
    public int maxX;
    public int maxY;
    public Tile[,] grid;
    void Start(){
        maxX=5;
        maxY=6;
        grid=new Tile[maxX,maxY];
        for (int x=0; x<maxX; x++){
            for (int y=0; y<maxY; y++){
                GameObject tile=Instantiate(TilePrefab,new Vector3(hexCodeToCoord(x-maxX/2,y-maxY/2)[0],hexCodeToCoord(x-maxX/2,y-maxY/2)[1],0),transform.rotation);
                grid[x, y]=tile.GetComponent<Tile>();
                grid[x, y].x=x;
                grid[x, y].y=y;
            }
        }
        Debug.Log(grid);
        GameObject[] localpeices=GameObject.FindGameObjectsWithTag("Peice");
        foreach(GameObject peice in localpeices){
            pData=peice.GetComponent<Peice>();
            if(pData!=null){
                pData.Move();
            }
            if(pData.side==turnTrack){
                pData.Reset();
            }
        }
    }
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseDown();
        mouseMove();
        MouseUp();
        checkTurnChange();
    }
    public int GetTurn(){
        return turnTrack;
    }
    public void MouseUp(){
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            int speed=peiceData.getSpeed();
            GameObject[] localpeices=GameObject.FindGameObjectsWithTag("Peice");
            foreach(GameObject peice in localpeices){
                pData=peice.GetComponent<Peice>();
                if(pData!=null&&pData!=peiceData&&pData.side!=turnTrack
                &&selectedObject.transform.position.x==peice.transform.position.x
                &&selectedObject.transform.position.y==peice.transform.position.y&&!checkRange(speed)){
                    pData.die();
                    peiceData.Move();
                    peiceData=null;
                    selectedObject = null;
                    pData=null;
                }
            }
            if(checkRange(speed)||!isLeagalMove()){
                undoMove();
            }
            else{
                peiceData.Move();
                selectedObject = null;
            }
        }
    }
    public bool isLeagalMove(){
        int speed=peiceData.getSpeed();
        GameObject[] localpeices=GameObject.FindGameObjectsWithTag("Peice");
        foreach(GameObject peice in localpeices){
            pData=peice.GetComponent<Peice>();
            if(pData!=null&&pData!=peiceData&&pData.side==turnTrack
            &&selectedObject.transform.position.x==peice.transform.position.x
            &&selectedObject.transform.position.y==peice.transform.position.y){
                return false;
            }
        }
        return true;
    }
    public void mouseMove(){
        if (selectedObject&&!peiceData.getMoved())
        {
            selectedObject.transform.position = mousePosition + offset;
            targetPos=selectedObject.transform.position;//15:12 board
            float[] nearestHex = posToHexPos(targetPos.x,targetPos.y);
            selectedObject.transform.position= new Vector3(
                nearestHex[0],nearestHex[1],Mathf.Round(targetPos.z+0.5f)-0.5f);
        }else{
            selectedObject=null;
        }
    }
    public void mouseDown(){
        if (Input.GetMouseButtonDown(0))
        {
            targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject!=null&&targetObject.GetComponent<Peice>()!=null)
            {
                selectedObject = targetObject.transform.gameObject;
                orgpos=selectedObject.transform.position;
                offset = selectedObject.transform.position - mousePosition;
                peiceData=selectedObject.GetComponent<Peice>();
            }
        }
    }
    private Boolean checkRange(int range){
        return range<Vector2.Distance((Vector2)selectedObject.transform.position,orgpos)
            ||Vector2.Distance((Vector2)selectedObject.transform.position,orgpos)==0;
    }
    private void undoMove(){
        selectedObject.transform.position=orgpos;
        selectedObject = null;
    }
    private void checkTurnChange(){
        bool turnchange=true;
        GameObject[] peices=GameObject.FindGameObjectsWithTag("Peice");
        foreach(GameObject peice in peices){
            pData=peice.GetComponent<Peice>();
            if(pData!=null&&!pData.getMoved()&&pData.side==turnTrack){
                turnchange=false;
            }
        }
        if(turnTrack<=2&&turnchange){
            turnTrack++;
            GameObject[] localpeices=GameObject.FindGameObjectsWithTag("Peice");
            foreach(GameObject peice in localpeices){
                pData=peice.GetComponent<Peice>();
                if(pData!=null&&pData.side==turnTrack){
                    pData.Reset();
                }
            }
        }else if(turnTrack>2&&turnchange){
            turnTrack=1;
            GameObject[] localpeices=GameObject.FindGameObjectsWithTag("Peice");
            foreach(GameObject peice in localpeices){
                pData=peice.GetComponent<Peice>();
                if(pData!=null&&pData.side==turnTrack){
                    pData.Reset();
                }
            }
        }
    }
    public float[] posToHexPos(float x, float y){
        //15:12 board
        float[] ret={0,0};
        if(Mathf.Round(y/(Mathf.Sqrt(3)/2.0f))*(Mathf.Sqrt(3)/2.0f)%Mathf.Sqrt(3)==0){
            ret[0]=Mathf.Round(x);
        }else{
            ret[0]=Mathf.Round(x+0.5f)-0.5f;
        }
        ret[1]=Mathf.Round(y/(Mathf.Sqrt(3)/2.0f))*(Mathf.Sqrt(3)/2.0f);
        return ret;
    }
    public int[] coordToHexCode(float x, float y){
        int[] ret={0,0};
        ret[0]=Mathf.RoundToInt(x);
        ret[1]=Mathf.RoundToInt(y/(Mathf.Sqrt(3)/2.0f));
        return ret;
    }
    public float[] hexCodeToCoord(float x, float y){
        float[] ret={0,0};
        if(y%2==0){
            ret[0]=x;
        }else{
            ret[0]=x+0.5f;
        }
        ret[1]=y*(Mathf.Sqrt(3)/2.0f);
        return ret;
    }
    public Peice getPeiceData(){
        return peiceData;
    }
    public void Move(Peice peicedata){
        if(!peicedata.getMoved()){
            for(int x=0; x<maxX; x++){//for (every hexcode in the board boundaries){
                for(int y=0; y<maxY; y++){
                    //if !(blocked tiles||tile outside range 1 of obj||other dots||onpeice)
				    //    Instantiate new MoveDot(x,y,1);
            }}
            for (int n=0; n<peicedata.speed;n++){
                GameObject[] dots=GameObject.FindGameObjectsWithTag("Dot");
                foreach(GameObject dot in dots){
                    //for int [][] ajecenttiles(): tile
                    //    if ! (blocked tiles||tile outside range 1 of tile||other dots||onpeice)
					//	    Instantiate new MoveDot(x,y,n)
                }
        }}
        else{
            for (int n=0; n<peicedata.speed;n++){
                GameObject[] dots=GameObject.FindGameObjectsWithTag("Dot");
                foreach(GameObject dot in dots){
                    Destroy(dot);
                }
            }
        }
    }
}
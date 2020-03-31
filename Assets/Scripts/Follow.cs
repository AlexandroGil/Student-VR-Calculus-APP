using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Follow : Photon.Pun.MonoBehaviourPun 
{

    public GameObject wayPoint;
    private Vector3 wayPointPos;
 //This will be the zombie's speed. Adjust as necessary.
    private float speed = 6.0f;

    private PhotonView PV;
    void Start ()
    {
        PV = GetComponent<PhotonView>();
        //At the start of the game, the zombies will find the gameobject called wayPoint.
        //wayPoint = GameObject.Find("wayPoint");
    }
    
    void Update ()
    {
        if(PV.IsMine) {
            wayPointPos = new Vector3(wayPoint.transform.position.x, wayPoint.transform.position.y, wayPoint.transform.position.z);
            //Here, the zombie's will follow the waypoint.
            transform.position = Vector3.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
        }
    }
}

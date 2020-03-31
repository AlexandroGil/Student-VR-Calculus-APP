using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Teleportation : Photon.Pun.MonoBehaviourPun
{
    private PhotonView PV;
    
    public GameObject principal;
    public GameObject temporary;
    public GameObject[] bringers;
    public List<GameObject> students;
    public GameObject secundary;
    public bool habilitado;

    private bool firstime;
    // Start is called before the first frame update
    void Start()
    {
        habilitado = true;
        students = new List<GameObject>();
        PV = GetComponent<PhotonView>();
        principal = this.gameObject;
        secundary = GameObject.Find("TeacherCharacter(Clone)");
        //students.Add(principal);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(students[1]);
        if(PV.IsMine && principal == secundary) {
            if (Input.GetKeyDown((KeyCode.G)))
            {
                for(int i = 0; i <= students.Count - 1; i++) {
                    Debug.Log(students[i]);
                    Debug.Log(bringers[i].transform.position);
                    Vector3 positionb = students[i].transform.position;
                    bringers[i].transform.position = positionb;
                    Debug.Log(positionb);
                    //students[i].transform.position = positionb;
                }  
            }
        }
    }
}

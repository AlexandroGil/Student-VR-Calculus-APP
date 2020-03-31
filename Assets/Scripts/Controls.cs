using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Controls : Photon.Pun.MonoBehaviourPun
{

    public GameObject planoX;
    public GameObject planoY;
    public GameObject planoZ;
    public GameObject teacher;
    private GameObject planoS;
    private bool activo;
    public GameObject suelo;
    public MeshRenderer m;
    public static int planoE;
    private bool planoC;
    public GameObject spawner;

    private PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        planoC = false;
        spawner = GameObject.Find("SpawnerA");
        teacher = GameObject.Find("ThirdPersonControllerT");
        suelo = GameObject.Find("Plane");
        m = suelo.GetComponent<MeshRenderer>();
        if(PV.IsMine) {
            planoX = Photon.Pun.PhotonNetwork.Instantiate(planoX.name, spawner.transform.position, Quaternion.Euler(0, 0, 90));
            planoY = Photon.Pun.PhotonNetwork.Instantiate(planoY.name, spawner.transform.position, Quaternion.Euler(0, 0, 0));
            planoZ = Photon.Pun.PhotonNetwork.Instantiate(planoZ.name, spawner.transform.position, Quaternion.Euler(0, 90, 90));
        } else {
            planoX = GameObject.Find("PlaneX(Clone)");
            planoY = GameObject.Find("PlaneY(Clone)");
            planoZ = GameObject.Find("PlaneZ(Clone)");
            //Debug.Log("Buenas soy el teacher");
        }
        planoX.SetActive(false);
        planoY.SetActive(false);
        planoZ.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(PV.IsMine) {
            if (Input.GetKeyDown((KeyCode.F)))
            {
                PV.RPC("keyF", RpcTarget.AllBuffered);
            }
            if (Input.GetKeyDown((KeyCode.R)))
            {
                teacher.transform.position = spawner.transform.position;
            }
            if (Input.GetKeyDown((KeyCode.X)))
            {
                PV.RPC("keyX", RpcTarget.AllBuffered);
            }
            if (Input.GetKeyDown((KeyCode.Y)))
            {
                PV.RPC("keyY", RpcTarget.AllBuffered);
            }
            if (Input.GetKeyDown((KeyCode.Z)))
            {
                PV.RPC("keyZ", RpcTarget.AllBuffered);
            }
        }
    }

    [PunRPC]
    void keyF() {
        if(activo == true) {
                m.enabled = false;
                activo = false;
            } else {
                m.enabled = true;
            }
    }

    [PunRPC]
    void keyX() {
        if(planoC == true){
                //planoX.transform.position = spawner.transform.position;
                planoX.transform.rotation = Quaternion.Euler(0, 0, 90);
                planoX.SetActive(false);
                planoC = false;
            } else {
                planoE = 1;
                planoX.SetActive(true);
                planoY.SetActive(false);
                planoZ.SetActive(false);
                planoC = true;
            }
    }

    [PunRPC]
    void keyY() {
        if(planoC == true){
                //planoY.transform.position = spawner.transform.position;
                planoY.SetActive(false);
                planoC = false;
            } else {
                planoE = 2;
                planoX.SetActive(false);
                planoY.SetActive(true);
                planoZ.SetActive(false);
                planoC = true;
            }
    }

    [PunRPC]
    void keyZ() {
        if(planoC == true){
                //planoZ.transform.position = spawner.transform.position;
                planoZ.SetActive(false);
                planoC = false;
            } else {
                planoE = 3;
                planoX.SetActive(false);
                planoY.SetActive(false);
                planoZ.SetActive(true);
                planoC = true;
            }
    }
}

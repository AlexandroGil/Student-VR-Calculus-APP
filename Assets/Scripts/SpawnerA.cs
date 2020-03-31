using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnerA : Photon.Pun.MonoBehaviourPun
{
    public GameObject[] charPrefab;
    public Transform Spawners;
    private bool Creado = false;
    // Update is called once per frame
    public int characterValue;

    private PhotonView PV;

    void Start() {
        PV = GetComponent<PhotonView>();
        //Debug.Log("Empece");
        if(!PV.IsMine) {
            PV.RPC("RPC_AddCharacter", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedCharacter);
            //Debug.Log("No estoy haciendo esta monda");
        }
    }

    void Update()
    {
        if(!Creado) {
            //Debug.Log("Soy de pronto: " + characterValue);
            Photon.Pun.PhotonNetwork.Instantiate(charPrefab[characterValue].name, Spawners.position, Quaternion.identity);
            Creado = true;  
        }
    }

    [PunRPC]
    void RPC_AddCharacter(int whichCharacter) {
        characterValue = whichCharacter;
        //Debug.Log("Soy: " + characterValue);
    }
}

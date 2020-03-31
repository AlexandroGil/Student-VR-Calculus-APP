using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NameTag : Photon.Pun.MonoBehaviourPun
{
    private PhotonView PV;
    public TextMesh nickName;
    public TextMesh nickNameB;
    public bool setname;
    public string nametag;
    // Start is called before the first frame update
    void Start()
    {
        nickName = GetComponentInChildren<TextMesh>();
        PV = GetComponent<PhotonView>();
        if(PV.IsMine) {
            PV.RPC("Nametag", RpcTarget.AllBuffered, PlayerInfo.PI.myNameTag);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!setname) {
            //Debug.Log("Mi nombre es: " + nametag);
            nickName.text = nametag;
            nickNameB.text = nametag;
            setname = true;  
        }
    }

    [PunRPC]
    void Nametag(string myNameTag) {
        nametag = myNameTag;
    }
}

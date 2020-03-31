using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Prueba : Photon.Pun.MonoBehaviourPun
{
    // Start is called before the first frame update
    private PhotonView PV;
    
    void Start()
    {
        PV = GetComponent<PhotonView>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

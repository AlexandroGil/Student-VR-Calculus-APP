using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerClicking : Photon.Pun.MonoBehaviourPun {

    [SerializeField]
    private Camera camera;
    [SerializeField]
    private LayerMask studentMask;
    public int character;
    private int temporal;
    private PhotonView PV;
    public bool teleport = false;

    // Start is called before the first frame update
    void OnEnable() {
        camera = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Camera>();
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, studentMask)) {
                if (hit.transform.tag == "Student") {
                    Debug.Log("Raycast Hit a Student, yay!");
                    if(PV.IsMine) {
                        temporal = hit.transform.root.GetComponent<PhotonView>().OwnerActorNr;  
                        PV.RPC("Teleport", RpcTarget.AllBuffered, temporal);
                    }
                }
                
            }
            teleport = false;
        }
    }

    [PunRPC]
    void Teleport(int temporal) {
        character = temporal;
        teleport = true;
        Debug.Log("Hola");
    }
}

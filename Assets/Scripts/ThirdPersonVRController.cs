using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.ThirdPerson;
using Photon.Pun;

public class ThirdPersonVRController : Photon.Pun.MonoBehaviourPun {
    
    public ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        
    private Vector3 VectorZ;

    private PhotonView PV;

    public GameObject m_Camera;
    public GameObject spawner;

    void Start()
    {
        spawner = GameObject.Find("SpawnerA");
        PV = GetComponent<PhotonView>();
        if(PV.IsMine) {
            VectorZ = new Vector3(0.0f, 0.0f, 0.0001f);
            //Debug.Log("Started");
            // get the transform of the main camera
            if (Camera.main != null) {
                m_Cam = Camera.main.transform;
            }
            else {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }
        } else {
            //m_Camera.enabled = false;
            Destroy(m_Camera);
        }
    }

    void Update()
    {
        if(!PV.IsMine) {
            return;
        }

        if((Input.GetKeyUp(KeyCode.M)) || (Input.GetKeyUp(KeyCode.UpArrow)) || (Input.GetAxis("Vertical") <= 0)) {
            VectorZ = new Vector3(0.0f, 0.0f, 0.000001f);
        } 
        if((Input.GetKeyDown(KeyCode.M)) || (Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetAxis("Vertical") > 0)) {
            VectorZ = new Vector3(0.0f, 0.0f, 1.0f);
        } 
        Vector3 forward = m_Cam.TransformDirection(VectorZ);
        m_Character.Move( forward, false, false);

        if (Input.GetAxis("Cancel") == 1){
            m_Character.transform.position = spawner.transform.position;
        }
        //if (Input.GetButtonDown("C")){
//
  //      }
        //if(Input.anyKeyDown){
        //    Debug.Log(Input.inputString);
        //}
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class TeleportingManager : MonoBehaviourPun {

    private PhotonView pView;

    //With a TP event, check the player Number
    [SerializeField]
    private int playerNumber;

    [SerializeField]
    private LayerMask functionMask;

    byte tpAllEventCode = 1;
    byte tpOneEventCode = 2;
    RaiseEventOptions eventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
    SendOptions sendOptions = new SendOptions { Reliability = true };

    #region UnityCallbacks
    // Start is called before the first frame update
    void OnEnable() {
        pView = GetComponent<PhotonView>();

        playerNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        PhotonNetwork.NetworkingClient.EventReceived += TPToPosEvent;
        PhotonNetwork.NetworkingClient.EventReceived += TPOneToPosEvent;
    }

    void OnDisable() {
        PhotonNetwork.NetworkingClient.EventReceived -= TPToPosEvent;
        PhotonNetwork.NetworkingClient.EventReceived -= TPOneToPosEvent;
    }

    #endregion

    #region TPFunctions

    private void TPToPosEvent(EventData obj) {
        if (obj.Code == tpAllEventCode) {
            object[] data = (object[])obj.CustomData;

            Vector3 positionToTP = (Vector3)data[0];

            transform.GetChild(0).position = positionToTP;
        }
    }

    private void TPOneToPosEvent(EventData obj) {
        if(obj.Code == tpOneEventCode) {
            object[] data = (object[])obj.CustomData;
            if((int)data[0] == playerNumber) {
                Vector3 posToTP = (Vector3)data[1];

                transform.GetChild(0).position = posToTP;
            }
        }
    }
    #endregion


    #region SpawnCalculations
    //Function used to avoid Code Duplication, Gets the upper position of the Player
    public Vector3 GetUpperFunctionPos() {
        return RayCasting(GetLocationRay());
    }

    //Method that recieves wether the calculation is done on spawn or not, creates a ray starting from either the spawn position if true
    //or the teacher's position if false. The ray points downwards every time.
    Ray GetLocationRay() {
        Vector3 raycastStart = GetRayStartingPosition();
        Vector3 dir = Vector2.down;

        return new Ray(raycastStart,dir);
    }

    //Method that calculates the starting position of the ray, and positions it on the top of the room. If false, the position is
    //the teachers current position, otherwise its the center of the scene.
    Vector3 GetRayStartingPosition() {
        Vector3 raycastStart = Vector3.zero;
        raycastStart.y += 183f;
        return raycastStart;
    }

    //Method that handles the raycasting, the method alculates every point of the function, the upper part, middle part and lowest part
    //and returns the respective requested position. If by any chance the requested position does not exist, the returned position is
    //the current position.
    Vector3 RayCasting(Ray functionRay) {
        Vector3 pos = transform.GetChild(1).position;

        //Getting all function hitpoints in the scene
        RaycastHit hit;
        if (Physics.Raycast(functionRay,out hit,Mathf.Infinity,functionMask)) {
            Vector3 temp = hit.point;
            temp.y += 0.15f;
            pos = temp;
        }

        return pos;
    }
    #endregion
}

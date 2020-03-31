using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AutoLobbyA : MonoBehaviourPunCallbacks
{

    public GameObject ConnectButton;
    public GameObject JoinRandomButton;
    public GameObject WomanButton;
    public GameObject ManButton;
    public GameObject CancelButton;

    public GameObject teacher;
    public Text Log;
    public Text PlayerCount;
    public int playersCount;

    public int charSelect; 

    public bool playerSee = false;
    private SpawnerA spawn;
    public byte maxPlayersPerRoom = 4;
    public byte minPlayersPerRoom = 2;
    private bool IsLoading = false;

    void Awake() {
        spawn = GameObject.FindObjectOfType<SpawnerA>();
        //Debug.Log(spawn);
    }
    public void Connect() {
        if(!PhotonNetwork.IsConnected) {
            if(PhotonNetwork.ConnectUsingSettings()) {
                Log.text += "\nConnected to Server";
            } else {
                Log.text += "\nFailing Connecting to Server";
            }
        }
        //Debug.Log("hola aqui estoy");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        ConnectButton.SetActive(false);
        JoinRandomButton.SetActive(true);
        //Debug.Log("Hola llegue aqui");
    }


    public void OnClickCharacterPick(int whichCharacter) {
        if(PlayerInfo.PI != null) {
            PlayerInfo.PI.mySelectedCharacter = whichCharacter;
            PlayerPrefs.SetInt("MyCharacter", whichCharacter);
        }
        //Debug.Log(whichCharacter);
        ManButton.SetActive(false);
        WomanButton.SetActive(false);
    }

    public void Cancelgender() {
        ManButton.SetActive(true);
        WomanButton.SetActive(true);
    }

    public void JoinRandom() {
        if (!PhotonNetwork.JoinRandomRoom()) {
            Log.text += "\nFail Joining Room";
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message) {
        Log.text += "\nNo Rooms to Join, creating one...";

        if(PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions() { MaxPlayers = maxPlayersPerRoom })){
            Log.text += "\nRoom created";
        } else {
            Log.text += "\nFail Creating Room";
        }
    }


    public override void OnJoinedRoom() {
        Log.text += "\nJoinned";
        //StartButton.SetActive(true);
        JoinRandomButton.SetActive(false);
    }

    void Update() {
        teacher = GameObject.Find("TeacherCharacter(Clone)");
        //Debug.Log(teacher);
        if(teacher != null) {
            playerSee = true;
        }
    }

    public void FixedUpdate(){
        if(PhotonNetwork.CurrentRoom != null) 
            playersCount = PhotonNetwork.CurrentRoom.PlayerCount;
        
        PlayerCount.text = playersCount + "/" + maxPlayersPerRoom;

        if(!IsLoading && playerSee == true){
            Debug.Log("Entrare al Server");
            LoadMap();
        }
    }

    public void StartThis() {
        playerSee = true;
    }

    private void LoadMap() {
        IsLoading = true;
        //if(!PhotonNetwork.IsMasterClient){
        //    return;
        //}
        PhotonNetwork.LoadLevel("VRapp");
    }
}

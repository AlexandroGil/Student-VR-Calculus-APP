using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NameLobby : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField NameInput;
    public GameObject Namebutton;
    public bool IsLoading = false;
    public bool start = false;
    public void OnClickName() {
        string namep = NameInput.text;
        if(PlayerInfo.PI != null) {
            PlayerInfo.PI.myNameTag = namep;
            PlayerPrefs.SetString("NameTag", namep);
        }
        start =  true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsLoading && start == true){
            SceneManager.LoadScene("Multiplayer");
        }
    }
}

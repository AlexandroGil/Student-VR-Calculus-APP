using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTypeManager : MonoBehaviour {

    public static PlayerTypeManager typeManager;

    public enum Types { Teacher, Player};

    public Types thisType;

    void OnEnable() {
        if(PlayerTypeManager.typeManager == null) {
            PlayerTypeManager.typeManager = this;
        } else {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }
}

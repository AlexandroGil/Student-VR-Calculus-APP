using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UIController : MonoBehaviourPun
{
    public GameObject InputPanel;
    public GameObject SettingsButton;
    public GameObject PlusButton;
    public GameObject MinusButton;

    public InputField FunctionInput, XminInput, XmaxInput, YminInput, YmaxInput;
    public Slider SmoothnessSlider;
    public FunctionMeshManager FunctionMeshManager;

    public void Awake() {
        if(PlayerTypeManager.typeManager.thisType == PlayerTypeManager.Types.Teacher) {
            SettingsButton.SetActive(true);
        } else {
            SettingsButton.SetActive(false);
        }
        //SettingsButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public void ToggleInputPanel()
    {
        InputPanel.SetActive(!InputPanel.activeSelf);
    }

    public void ApplyParameters()
    {
        string expression = FunctionInput.text;

        float Xmin = float.Parse(XminInput.text), Xmax = float.Parse(XmaxInput.text);
        float Ymin = float.Parse(YminInput.text), Ymax = float.Parse(YmaxInput.text);

        int n = (int)SmoothnessSlider.value;

        if (expression != "" && Xmin < Xmax && Ymin < Ymax && n > 0)
        {
            this.photonView.RPC("ApplyParametersRPC", RpcTarget.AllBuffered, expression, Xmin, Xmax, Ymin, Ymax, n);
        }
    }

    public void OnPlusButton()
    {
        bool more = true;
        this.photonView.RPC("LessFunction", RpcTarget.AllBuffered, more);
    }
    public void OnMinusButton()
    {
        bool more = false;
        this.photonView.RPC("LessFunction", RpcTarget.AllBuffered, more);
    }

    [PunRPC]
    public void ApplyParametersRPC(string expression, float Xmin, float Xmax, float Ymin, float Ymax, int n, PhotonMessageInfo info)
    {
        FunctionMeshManager.expr = expression;
        FunctionMeshManager.xMin = Xmin;
        FunctionMeshManager.xMax = Xmax;
        FunctionMeshManager.zMin = Ymin;
        FunctionMeshManager.zMax = Ymax;
        FunctionMeshManager.n = n;
        FunctionMeshManager.RecalculateMesh();
    }

    [PunRPC]
    public void LessFunction(bool boolean)
    {
        FunctionMeshManager.ScaleMesh(boolean);
    }
}

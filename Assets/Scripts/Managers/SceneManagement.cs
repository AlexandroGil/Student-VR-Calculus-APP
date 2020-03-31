using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class SceneManagement : MonoBehaviour {

    #region Variables

    public static SceneManagement scenes;

    private int currentScene;

    #endregion

    #region Unity Callbacks
    void OnEnable() {
        if (SceneManagement.scenes == null) {
            SceneManagement.scenes = this;
        } else {
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnLevelWasLoaded(int level) {
        if (level > 1) {
            StartCoroutine(SwitchToVR());
        } else {
            StartCoroutine(SwitchTo2D());
        }
    }

    #endregion

    #region Scene Operations
    public void SwitchScene(int newScene) {
        StartCoroutine(LoadAsyncLevel(newScene));
    }

    IEnumerator LoadAsyncLevel(int scene) {
        AsyncOperation op = SceneManager.LoadSceneAsync(scene);

        while (!op.isDone) {
            yield return null;
        }
        currentScene = scene;
    }

    #endregion

    #region Hybrid Modes Switching
    IEnumerator SwitchToVR() {
        string desiredDevice = "cardboard";

        if (string.Compare(XRSettings.loadedDeviceName,desiredDevice,true) != 0) {
            XRSettings.LoadDeviceByName(desiredDevice);

            yield return null;
        }

        XRSettings.enabled = true;
    }

    IEnumerator SwitchTo2D() {
        XRSettings.LoadDeviceByName("");

        yield return null;

        ResetCameras();
    }

    void ResetCameras() {
        for (int i = 0; i < Camera.allCameras.Length; i++) {
            Camera cam = Camera.allCameras[i];
            if (cam.enabled && cam.stereoTargetEye != StereoTargetEyeMask.None) {
                cam.transform.localPosition = Vector3.zero;

                cam.transform.localRotation = Quaternion.identity;
            }
        }
    }
    #endregion
}

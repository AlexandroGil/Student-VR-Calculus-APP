 using System.Collections;
 using UnityEngine;
 using UnityEngine.VR;
using UnityEngine.XR;

public class RuntimeCardboardLoader : MonoBehaviour
 {
     void Start()
     {
         XRSettings.LoadDeviceByName("cardboard");
     }
 
     IEnumerator LoadDevice(string newDevice)
     {
         XRSettings.LoadDeviceByName(newDevice);
         yield return null;
         XRSettings.enabled = true;
     }
 }
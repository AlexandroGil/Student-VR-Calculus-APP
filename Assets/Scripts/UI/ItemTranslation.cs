using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ItemTranslation : MonoBehaviour {
    public static ItemTranslation itemsTr;

    [Header("Objects")]
    [SerializeField]
    private Text translationSpace;
    [SerializeField]
    private Canvas thisCanvas;
    [SerializeField]
    private Image speechBubbles;
    [SerializeField]
    private GameObject videoPlane;

    public Sprite[] bubbles;


    void OnEnable() {
        if(ItemTranslation.itemsTr == null) {
            ItemTranslation.itemsTr = this;
        } else {
            Destroy(this.gameObject);
        }

        thisCanvas = gameObject.GetComponent<Canvas>();
        videoPlane = thisCanvas.transform.GetChild(1).gameObject;
        speechBubbles = thisCanvas.transform.GetChild(0).GetComponent<Image>();
        translationSpace = speechBubbles.transform.GetChild(0).GetComponent<Text>();

        thisCanvas.enabled = false;
        videoPlane.SetActive(false);
    }

    public void SetObjectText(string translation) {
        int speechBubbleSelected = (int)Random.Range(0,2);
        if (speechBubbleSelected > 1) {
            speechBubbleSelected = 1;
        }
        speechBubbles.sprite = bubbles[speechBubbleSelected];

        translationSpace.text = translation;
        thisCanvas.transform.position = thisCanvas.transform.parent.GetChild(0).position + 
            thisCanvas.transform.parent.GetChild(0).forward;
        this.thisCanvas.transform.rotation = thisCanvas.transform.parent.GetChild(0).rotation;
        thisCanvas.enabled = true;
        
    }

    public void SetObjectVideo(VideoClip video) {
        if(video != null) {
            VideoPlayer vp = videoPlane.GetComponent<VideoPlayer>();
            vp.clip = video;
        }

        videoPlane.SetActive(true);
    }

    public void ResetCanvas() {
        translationSpace.text = "";
        thisCanvas.enabled = false;
        videoPlane.SetActive(false);
    }
}

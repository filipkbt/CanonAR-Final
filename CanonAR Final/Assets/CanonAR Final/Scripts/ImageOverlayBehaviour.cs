using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ImageOverlayBehaviour : MonoBehaviour {

    // Use this for initialization
    private GameObject overlay;
    private Image image;

    private UnityAction<CustomEventData> onPhotoTapped;

    void Awake()
    {
        overlay = transform.Find("ImageOverlay").gameObject;
        image = overlay.transform.Find("ImageContainer").gameObject.GetComponent<Image>();
        overlay.SetActive(false);
        onPhotoTapped = new UnityAction<CustomEventData>(DisplayImageOverlay);
    }

    void OnEnable()
    {
        EventManager.StartListening("onPhotoTapped", onPhotoTapped);
    }

    void OnDisable()
    {
        EventManager.StopListening("onPhotoTapped", onPhotoTapped);
    }

    public void CloseOverlay()
    {
        overlay.SetActive(false);
    }

    public void OpenOverlay()
    {
        overlay.SetActive(true);
    }

    void SetImage(Sprite imageSprite)
    {
        image.sprite = imageSprite;
    }

    void DisplayImageOverlay(CustomEventData eventData)
    {
        GameObject tappedObject = eventData.obj;
        Image tappedImageComponent = tappedObject.GetComponent<Image>(); ;
        Sprite imageSprite = tappedImageComponent.sprite;
        SetImage(imageSprite);
        OpenOverlay();
    }
}

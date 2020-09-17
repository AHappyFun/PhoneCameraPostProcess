using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCamPostProcess : MonoBehaviour
{
    RawImage cameraImage;
    WebCamTexture webCamTex;
    public Slider slider;
    public EdgeDetectionEffect detectionEffect;

    private IEnumerator Start()
    {
        cameraImage = GetComponent<RawImage>();

        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);

        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            if(devices != null)
            {
                webCamTex = new WebCamTexture(devices[0].name, 1080, 1920, 30);
                webCamTex.Play();

                cameraImage.texture = webCamTex;
            }
        }
    }

    public void SetEdge()
    {
        print(slider.value);
        detectionEffect.edgesOnly = slider.value;
    }
}

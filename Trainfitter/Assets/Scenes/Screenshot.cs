using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;
using AUP;
using System;

public class Screenshot : MonoBehaviour {
    private CameraPlugin cameraPlugin;
    private SharePlugin sharePlugin;
    Camera mainCamera;
    Texture2D screenshot;
    Texture2D LoadScreenshot;
    int width = Screen.width;
    int height = Screen.height;
    private string folderName = "TrainfitterImages";
    //private string imagePath = "";
    string fileName;

    double latitude;
    double longitude;
    
    int screenShotId;

    //private ImagePickerPlugin imagePickerPlugin;
    private Dispatcher dispatcher;

    private GPSPlugin gpsPlugin;

	// Use this for initialization
	void Start () {
        sharePlugin = SharePlugin.GetInstance();
        sharePlugin.SetDebug(0);

        cameraPlugin = CameraPlugin.GetInstance();
        cameraPlugin.SetDebug(0);
        cameraPlugin.Init(folderName);

        //imagePickerPlugin = ImagePickerPlugin.GetInstance();
        dispatcher = Dispatcher.GetInstance();

        gpsPlugin = GPSPlugin.GetInstance();
        gpsPlugin.SetDebug(0);

        long updateInterval = 200;
        long minimumMeterChangeForUpdate = 0;

        gpsPlugin.Init(updateInterval, minimumMeterChangeForUpdate);
        AddGPSEventListeners();
        gpsPlugin.StartGPS();
        GetLatitude();
        GetLongitude();
        /*
        imagePickerPlugin.SetDebug(0);
        imagePickerPlugin.Init();

        imagePickerPlugin.OnGetImagesComplete += onGetImagesComplete;
        imagePickerPlugin.OnGetImageCancel += onGetImagesCancel;
        imagePickerPlugin.OnGetImageFail += onGetImageFail;

        imagePickerPlugin.GetImages();*/
        screenShotId = PlayerPrefs.GetInt("SitePicId");
    }
    private void AddGPSEventListeners()
    {
        if (gpsPlugin != null)
        {
 
        }
    }
    private void RemoveGPSEventListeners()
    {
        if (gpsPlugin != null)
        {
  
        }
    }

    public void GetLatitude()
    {
        latitude = gpsPlugin.GetLatitude();
        Debug.Log("Latitude  " + latitude);
    }

    public void GetLongitude()
    {
        longitude = gpsPlugin.GetLongitude();
        Debug.Log("Longitude  " + longitude);
    }

    private void onGetImageFail()
    {
        throw new NotImplementedException();
    }

    private void onGetImagesCancel()
    {
        throw new NotImplementedException();
    }

    private void onGetImagesComplete(string rawImagePath)
    {
        /*dispatcher.InvokeAction(
            () =>
                {
                    imagePaths = rawImagePath.Split(',');

                    rawImage1.texture = AUP.Utils.LoadTexture(imagePaths[0]);
                    rawImage2.texture = AUP.Utils.LoadTexture(imagePaths[1]);
                }
            );
        */
    }

    public void Snapshot ()
    {
        StartCoroutine(CaptureScreen());
    }

    public IEnumerator CaptureScreen ()
    {
        yield return null;
        //hide UI for taking pictures
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
        yield return new WaitForEndOfFrame();

        if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            mainCamera = Camera.main.GetComponent<Camera>(); // for Taking Picture
            RenderTexture renderTex = new RenderTexture(width, height, 24);
            mainCamera.targetTexture = renderTex;
            RenderTexture.active = renderTex;
            mainCamera.Render();
            screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
            screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            screenshot.Apply(); //false
            RenderTexture.active = null;
            mainCamera.targetTexture = null;
        }
        if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            mainCamera = Camera.main.GetComponent<Camera>(); // for Taking Picture
            RenderTexture renderTex = new RenderTexture(height, width, 24);
            mainCamera.targetTexture = renderTex;
            RenderTexture.active = renderTex;
            mainCamera.Render();
            screenshot = new Texture2D(height, width, TextureFormat.RGB24, false);
            screenshot.ReadPixels(new Rect(0, 0, height, width), 0, 0);
            screenshot.Apply(); //false
            RenderTexture.active = null;
            mainCamera.targetTexture = null;
        }
        // on Android - /Data/Data/com.companyname.gamename/Files
        File.WriteAllBytes(Application.persistentDataPath + "/" + PlayerPrefs.GetString("SiteName") + "_" + screenShotId + ".png", screenshot.EncodeToPNG());
        screenShotId++;
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true; // Show UI after we're done
    }

    public void ShareImage()
    {
        int ssid = screenShotId-1;
        string path = Application.persistentDataPath + "/" + PlayerPrefs.GetString("SiteName") + "_" + ssid +".png";
        sharePlugin.ShareImage(PlayerPrefs.GetString("SiteName"),"Latitude: "+latitude+"\n"+"Longitude: "+longitude, path);
    }


    // Update is called once per frame
    void Update () {
		
	}

    public void close()
    {
        PlayerPrefs.SetInt("SitePicId", screenShotId);
        RemoveGPSEventListeners();
        Application.Quit();
    }
}

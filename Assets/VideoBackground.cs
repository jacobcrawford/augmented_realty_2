using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCVForUnity.CoreModule;
using Vuforia;
using OpenCVForUnity.ImgprocModule;

public class VideoBackground : MonoBehaviour
{
	Mat cameraImageMat;
	Mat grayMat;
	private bool mFormatRegistered = false;
	private PIXEL_FORMAT mPixelFormat;
	private Camera cam;

    void Start()
    {
		mPixelFormat = PIXEL_FORMAT.RGBA8888;
		cam = GetComponent<Camera>();

		VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
		VuforiaARController.Instance.RegisterTrackablesUpdatedCallback(OnTrackablesUpdated);
	}

	private void OnVuforiaStarted()
	{
		// Try register camera image format
		if (CameraDevice.Instance.SetFrameFormat(mPixelFormat, true))
		{
			mFormatRegistered = true;
		}
		else
		{
			mFormatRegistered = false;
		}
	}

	private void OnTrackablesUpdated()
	{
		cam.fieldOfView = 41.5f;
		Image cameraImage = CameraDevice.Instance.GetCameraImage(mPixelFormat);
		if (cameraImage != null && mFormatRegistered)
		{
			if (cameraImageMat == null)
			{
				cameraImageMat = new Mat(cameraImage.Height, cameraImage.Width,  CvType.CV_8UC4);
				grayMat = new Mat(cameraImage.Height, cameraImage.Width, CvType.CV_8UC1);
			}
			cameraImageMat.put(0, 0, cameraImage.Pixels);
			Imgproc.cvtColor(cameraImageMat, grayMat, Imgproc.COLOR_BGRA2GRAY);
			MatDisplay.DisplayMat(grayMat, MatDisplaySettings.FULL_BACKGROUND);
		}
	}


	// Update is called once per frame
	void Update()
    {
		//MatDisplay.SetCameraFoV(41.5f);

		
    }
}

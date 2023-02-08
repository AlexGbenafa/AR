using UnityEngine;
using OpenCVForUnity;
using System.IO;
using UnityEngine.UI;

public class QRCodeGenerator : MonoBehaviour
{
    public RawImage qrCodeImage;
    public string mobileUrl;

    private void Start()
    {
        // Check if the application is running on a mobile device
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
        {
            // Generate the QR code
            Mat img = new Mat(256, 256, CvType.CV_8UC3);
            QRCodeDetector qrCode = new QRCodeDetector();
            qrCode.detectAndDecode(img);

            Texture2D texture = new Texture2D(img.cols(), img.rows(), TextureFormat.RGBA32, false);
            Utils.matToTexture2D(img, texture);

            // Display the QR code on the screen
            qrCodeImage.texture = texture;
            qrCodeImage.gameObject.SetActive(true);
        }
    }
}

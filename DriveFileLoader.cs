using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google.Apis.Drive.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using System.IO;
using System.Threading;
using System.Linq;

public class DriveFileLoader : MonoBehaviour
{
    [SerializeField] private GameObject modelParent;
    [SerializeField] private Button importButton;
    private DriveService driveService;
    private string[] scopes = { DriveService.Scope.Drive };
    private string fileId;

    private void Start()
    {
        importButton.onClick.AddListener(ImportModel);
        // Authenticate with Google Drive
        UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            new ClientSecrets
            {
                ClientId = "YOUR_CLIENT_ID",
                ClientSecret = "YOUR_CLIENT_SECRET"
            },
            scopes,
            "user",
            CancellationToken.None,
            new FileDataStore("Drive.Auth.Store")
        ).Result;

        driveService = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "Drive API"
        });

    }

    private void ImportModel()
    {
        // Open file picker dialog to select file from Google Drive
        fileId = FilePicker.OpenFilePicker("Select model file", "application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet,application/pdf", driveService);
        if (string.IsNullOrEmpty(fileId))
        {
            Debug.LogError("Please select a valid file.");
            return;
        }

        // Download file from Google Drive
        MemoryStream stream = new MemoryStream();
        // create the service request to download the file
var request = driveService.Files.Get(fileId);
request.MediaDownloader.ChunkSize = 256 * 1024; // 256KB per chunk
request.Download(stream);

// create a new GameObject and load the model asset from the stream
GameObject model = (GameObject)AssetDatabase.LoadAssetAtPath(stream, typeof(GameObject));
if (model == null)
{
Debug.LogError("Failed to load model asset from Google Drive file: " + fileId);
return;
}

// Instantiate the model and attach it to the parent game object
GameObject instance = Instantiate(model, modelParent.transform);
}
}

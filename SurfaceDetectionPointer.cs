using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SurfaceDetectionPointer : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private GameObject pointer;

    private void Update()
    {
        // Raycast from the center of the screen
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        // If we hit a surface, position the pointer at the hit point
        if (hits.Count > 0)
        {
            pointer.SetActive(true);
            pointer.transform.position = hits[0].pose.position;
            pointer.transform.rotation = hits[0].pose.rotation;
        }
        else
        {
            pointer.SetActive(false);
        }
    }
}

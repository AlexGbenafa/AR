using UnityEngine;
using UnityEngine.Windows.Speech;

public class ModelDimensions : MonoBehaviour
{
    public GameObject model;
    public Button dimensionsButton;

    private void Start()
    {
        dimensionsButton.onClick.AddListener(ReadModelDimensions);
    }

    private void ReadModelDimensions()
    {
        // Get the renderer component of the model
        Renderer modelRenderer = model.GetComponent<Renderer>();
        if (modelRenderer == null)
        {
            Debug.LogError("Model does not have a renderer component.");
            return;
        }

        // Get the dimensions of the model
        Vector3 modelSize = modelRenderer.bounds.size;
        string dimensions = string.Format("Width: {0}m, Height: {1}m, Depth: {2}m", modelSize.x, modelSize.y, modelSize.z);

        // Use the text-to-speech API to speak the model dimensions
        SpeechSynthesizer synth = new SpeechSynthesizer();
        synth.Speak(dimensions);
    }
}

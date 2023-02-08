using UnityEngine;
using UnityEditor;

public class ModelImporter : MonoBehaviour
{
    [SerializeField] private GameObject modelParent;
    [SerializeField] private Button importButton;

    private void Start()
    {
        importButton.onClick.AddListener(ImportModel);
    }

    private void ImportModel()
    {
        // Ouvrir la boîte de dialogue de sélection de fichiers
        string filePath = EditorUtility.OpenFilePanel("Sélectionnez un fichier de modèle", "", "fbx,obj,bim");

        // Vérifier si le chemin du fichier n'est pas vide
        if (string.IsNullOrEmpty(filePath))
        {
            return;
        }

        // Extraire le chemin d'accès relatif à partir du chemin du fichier
        int assetPathIndex = filePath.IndexOf("Assets");
        string assetPath = filePath.Substring(assetPathIndex);

        // Charger le modèle à partir du chemin d'accès spécifié
        GameObject model = (GameObject)AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject));
        if (model == null)
        {
            Debug.LogError("Impossible de charger le modèle à partir du chemin : " + assetPath);
            return;
        }

        // Instancier le modèle et l'attacher au GameObject parent
        GameObject instance = Instantiate(model, modelParent.transform);
    }
}

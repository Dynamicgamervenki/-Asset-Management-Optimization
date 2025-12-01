using System.IO;
using UnityEngine;

public class AssetBundleDemo : MonoBehaviour
{
    private string path = Application.dataPath + "/AssetBundles/areanassetbundle";
    private AssetBundle assetBundle;
    private GameObject arenaInstance;

    public Transform ArenaParent;

    public void LoadAssetBundle()
    {
        if (assetBundle != null) return;

        if (!File.Exists(path))
        {
            Debug.LogError("AssetBundle not found at: " + path);
            return;
        }

        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(path);
        request.completed += handle =>
        {
            assetBundle = request?.assetBundle;

            if (assetBundle == null){ Debug.LogError("Failed to load AssetBundle!");}
        
            AssetBundleRequest assetRequest = assetBundle.LoadAssetAsync<GameObject>("Terrain_1_1_4bf22c8f-eb55-436d-84e3-dedfa85665b1");
            //assetBundle.LoadAllAssets();
          
            assetRequest.completed += handle =>
            {
                var prefab = assetRequest?.asset as GameObject;
                 arenaInstance = Instantiate(prefab,ArenaParent);
            };
          
        };
        //arenaInstance = assetBundle.LoadAsset<GameObject>("Terrain_1_1_4bf22c8f-eb55-436d-84e3-dedfa85665b1");
        //arenaInstance = assetBundle.LoadAssetAsync<GameObject>("Terrain_1_1_4bf22c8f-eb55-436d-84e3-dedfa85665b1").asset as GameObject;
        //Instantiate(arenaInstance,ArenaParent);
    }

    public void Unload()
    {
        if (assetBundle != null)
        {
            Destroy(arenaInstance);
            assetBundle.Unload(true);
            assetBundle = null;
            Debug.Log("AssetBundle unloaded.");
        }
    }
}

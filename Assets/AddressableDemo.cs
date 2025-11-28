using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableDemo : MonoBehaviour
{
    public Transform ArenaParent;
    public AssetLabelReference ArenaLabel;
    private AsyncOperationHandle<IList<GameObject>> assetsHandle;
    List<GameObject> Spawned;
    bool isSpawned = false;

    public void InstantiateAsyncArena()
    {
        if(isSpawned) return;
        Spawned = new List<GameObject>();

        //Addressables.InstantiateAsync(ArenaLabel, ArenaParent).Completed += handle =>
        //{
        //    if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        //    {
        //        isSpawned = true;
        //        Debug.Log("Arena instantiated successfully.");
        //        arenaInstance = handle.Result;
        //      //  Invoke(nameof(ReleaseFromMemrory), 20.0f);

        //    }
        //    else
        //    {
        //        Debug.LogError("Failed to instantiate arena.");
        //    }
        //};

        assetsHandle = Addressables.LoadAssetsAsync<GameObject>(ArenaLabel, null);


        assetsHandle.Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                isSpawned = true;
                Debug.Log("Arena instantiated successfully.");
                foreach(var asset in handle.Result)
                {
                    var instance = Instantiate(asset, ArenaParent);
                    Spawned.Add(instance);
                }
            }
            else
            {
                Debug.LogError("Failed to instantiate arena.");
            }
        };
    }

    public void ReleaseFromMemrory()
    {
        if(!isSpawned) return;

        foreach(var a in Spawned)
        {
            Destroy(a);
        }
        isSpawned = false;
        //Addressables.ReleaseInstance(arenaInstance);
        if(assetsHandle.IsValid())
        {
            Addressables.Release(assetsHandle);
        }
    }



}

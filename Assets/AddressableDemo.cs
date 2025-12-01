using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class AddressableDemo : MonoBehaviour
{
    public Transform ArenaParent;
    public AssetLabelReference ArenaLabel;
    private AsyncOperationHandle<IList<GameObject>> assetsHandle;
    List<GameObject> Spawned;
    bool isSpawned = false;
    public TextMeshProUGUI status;

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

    AsyncOperationHandle downlaodHandle;
    public void DownloadData()
    {
        Spawned = new List<GameObject>();
        downlaodHandle = default;

        try
        {

            downlaodHandle = Addressables.DownloadDependenciesAsync(ArenaLabel);
            status.text = "Donwlaoding ...........";
            downlaodHandle.Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    status.text = "Donwloading Finished !";
                    assetsHandle = Addressables.LoadAssetsAsync<GameObject>(ArenaLabel, null);
                    status.text = "Loading Arena Asyncrousouly";
                    assetsHandle.Completed += handle =>
                    {
                        if(handle.Status == AsyncOperationStatus.Succeeded)
                            StartCoroutine(Delay(handle));
                    };
                }
                else
                {
                    status.text = "Failed to Load Arena";
                    Debug.LogError("Failed to Load");
                }
            };
        }
        catch (Exception e)
        {
            status.text = "Failed to download data : " + e;
            Debug.LogError("Failed to Download");
        }
    }

    public void Release()
    {
        if (!isSpawned) return;

        // Destroy scene instances
        foreach (var a in Spawned)
        {
            if (a != null) Destroy(a);
        }
        Spawned.Clear();
        isSpawned = false;

        // Release loaded asset references (one call frees the loaded assets list)
        if (assetsHandle.IsValid())
        {
            Addressables.Release(assetsHandle);
            assetsHandle = default;
        }

        // If you kept a downloadHandle for some reason, release it as well
        if (downlaodHandle.IsValid())
        {
            Addressables.Release(downlaodHandle);
            downlaodHandle = default;
        }
    }

    IEnumerator  Delay(AsyncOperationHandle<IList<GameObject>> handle)
    {
        status.text = "INside Courotuine";
        yield return new WaitForSeconds(5.0f);
        foreach (var asset in handle.Result)
        {
            status.text = "Inside for loop";
            var instance = Instantiate(asset, ArenaParent);
            Spawned.Add(instance);
        }
        isSpawned = true;
        status.text = "Instantiated Arena !";
    }
}

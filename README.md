# 🌐 Unity Addressables & AssetBundles Demo
A Unity project demonstrating **cloud-based Addressables**, **asynchronous loading**, **dependency downloading**, and **local AssetBundle loading/unloading**. This repository showcases how to efficiently manage and load game assets both from local storage and the cloud.
## 🚀 Features
### ✅ Addressables (Cloud-Based)
- 🔄 *Asynchronous asset loading* using labels
- ☁️ *Downloading dependencies* from the cloud
- 🧹 *Automatic memory releasing* using `Addressables.Release()`
- 🏗️ Instantiating multiple assets dynamically
- 📊 Text-based UI feedback for download status
### 🌐 AssetBundles (Local)
- 📦 Loading AssetBundles directly from file
- 🏗️ Instantiating prefabs from the bundle
- 🧽 Memory-safe unloading with `.Unload(true)`
- 🔍 Error-safe loading with file validation


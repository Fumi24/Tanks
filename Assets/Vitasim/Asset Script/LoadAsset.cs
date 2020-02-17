using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAsset : MonoBehaviour
{
    public void LoadScene()
    {
        AssetBundle bundle = AssetBundle.LoadFromFile(@"C:\Users\Fumse\Documents\SceneTest\Assets\StreamingAssets\AssetBundles\scenetestbundle");
        string[] scenes = bundle.GetAllScenePaths();
        string scene = Path.GetFileNameWithoutExtension(scenes[0]);
        SceneManager.LoadScene(scene);
    }
}
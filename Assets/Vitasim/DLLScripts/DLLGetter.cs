using UnityEngine;

public class DLLGetter : MonoBehaviour
{
    public void Get()
    {
        var assembly = System.Reflection.Assembly.LoadFile(@"C:\Users\Fumse\Documents\SceneTest\Assets\StreamingAssets\AssetBundles\DLLTest1.dll");
        var type = assembly.GetTypes();

        if (type != null)
        {
            var methods = type[0].GetMethods();
            var btn = GameObject.FindGameObjectWithTag("Button");
            btn.AddComponent(type[0]);
        }
    }
}
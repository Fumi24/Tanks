using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BundleScript : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(GetAllMonoBehaviors());
        StartCoroutine(MakeDLL());

        IEnumerator GetAllMonoBehaviors()
        {
            yield return new WaitForSeconds(2f);
            using (StreamWriter sw = File.CreateText(@"C:\Users\Fumse\source\repos\Tanks\Assets\Vitasim\VitasimBundle.txt"))
            {
                foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
                {
                    var scripts = obj.GetComponents<MonoBehaviour>();
                    foreach (var script in scripts)
                    {
                        UnityEngine.Debug.Log(script);
                        if (!script.ToString().Contains("<") && !script.ToString().Contains("/"))
                            if (script.ToString().Contains("("))
                                sw.WriteLine(script.ToString());
                    }
                }
            }
        }
        IEnumerator MakeDLL()
        {
            yield return new WaitForSeconds(2f);
            Process cmd = new Process();
            cmd.StartInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Normal,
                FileName = "cmd.exe",
                Arguments = @"/c C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe /target:library /out:C:\Users\Fumse\Desktop\test.dll /r:F:\Unity\2019.1.14f1\Editor\Data\Managed\UnityEngine.dll C:\Users\Fumse\source\repos\Tanks\Assets\Vitasim\DLLScripts\DLLGetter.cs",
                Verb = "runas"
            };
            cmd.Start();
            UnityEngine.Debug.Log("dll made");
        }
    }
}

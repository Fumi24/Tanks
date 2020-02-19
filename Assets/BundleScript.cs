using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
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
            using (StreamWriter sw = File.CreateText(@"C:\Users\Fumse\Documents\Tanks\Assets\Vitasim\VitasimBundle.txt"))
            {
                foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
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
            string[] scriptpaths = Directory.GetFiles(@"C:\Users\Fumse\Documents\Tanks\Assets\_Completed-Assets\", "*.cs", SearchOption.AllDirectories);
            string[] DLLpaths = Directory.GetFiles(@"C:\Users\Fumse\OneDrive\Skrivebord\DLL", "*.dll", SearchOption.AllDirectories);
            for (int i = 0; i < DLLpaths.Length; i++)
            {
                DLLpaths[i] = "/r:" + DLLpaths[i] + " ";
            }
            Process cmd = new Process();
            cmd.StartInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Normal,
                FileName = "cmd.exe",
                Arguments = @"/k C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe " +
                @"/target:library " +
                @"/out:C:\Users\Fumse\OneDrive\Skrivebord\testbig.dll " +
                string.Join(" ",DLLpaths) +
                string.Join(" ", scriptpaths),
                Verb = "runas"  
            };
            cmd.Start();
        }
    }
}
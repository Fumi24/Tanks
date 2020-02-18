using System;
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
            string[] scriptpaths = Directory.GetFiles(@"C:\Users\Fumse\Documents\Tanks\Assets\Scripts", "*.cs", SearchOption.AllDirectories);
            int dllid = 1;
            foreach (string file in scriptpaths)
            {
                Process cmd = new Process();
                cmd.StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Normal,
                    FileName = "cmd.exe",
                    Arguments = @"/k C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe " +
                    @"/target:library   test"+dllid+".dll " +
                    @"/r:C:\Users\Fumse\OneDrive\Skrivebord\UnityEngine.dll " +
                    @"/r:C:\Users\Fumse\OneDrive\Skrivebord\UnityEditor.dll " +
                    @"/r:C:\Users\Fumse\OneDrive\Skrivebord\UnityEngine.UI.dll "
                    + file,
                    Verb = "runas"
                };
                cmd.Start();
                dllid++;
            }
        //    string[] DLLpaths = Directory.GetFiles(@"C:\Users\Fumse\Documents\Tanks\Assets\Scripts", "*.dll", SearchOption.AllDirectories);
        //    foreach (string file in DLLpaths)
        //    {
        //        Process cmd = new Process();
        //        cmd.StartInfo = new ProcessStartInfo
        //        {
        //            WindowStyle = ProcessWindowStyle.Normal,
        //            FileName = "cmd.exe",
        //            Arguments = @"/k C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe " +
        //            @"/target:library /out:C:\Users\Fumse\OneDrive\Skrivebord\test" + dllid + ".dll " +
        //            @"/r:C:\Users\Fumse\OneDrive\Skrivebord\UnityEngine.dll " +
        //            @"/r:C:\Users\Fumse\OneDrive\Skrivebord\UnityEditor.dll " +
        //            @"/r:C:\Users\Fumse\OneDrive\Skrivebord\UnityEngine.UI.dll "
        //            + file,

        //            Verb = "runas"
        //        };
        //        cmd.Start();
        //    }
        //}
    }
}
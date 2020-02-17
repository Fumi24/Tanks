using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BundleScript : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(GetAllMonoBehaviors());

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
                        Debug.Log(script);
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

        }
    }
}

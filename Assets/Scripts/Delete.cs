using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class Delete : MonoBehaviour
{
    public String javaVer;
    public GameObject alreadyInstall;
    public void delete()
    {
        Directory.Delete(Application.persistentDataPath + "/java" + javaVer);
        Debug.Log("Java unnistall");
        Debug.Log(Application.persistentDataPath + "/java" + javaVer);
        alreadyInstall.SetActive(false);
        GameObject javaChild = Install.FindChildRecursive(GameObject.Find("Canvas"), "Java"+javaVer);
        GameObject textChild = Install.FindChildRecursive(Install.FindChildRecursive(javaChild, "Instalar"), "Text");
        Text text = textChild.GetComponent<Text>();
        if (Directory.Exists(Application.persistentDataPath + "/java" + javaVer))
        {
            text.text = "Uninstall";
        }
        else
        {
            text.text = "Install";
        }
    }
    
}

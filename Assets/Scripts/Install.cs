using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.UI;
using static System.EnvironmentVariableTarget;

public class Install : MonoBehaviour
{
    public string downloadURL;
    public string javaPathCmd;
    public string javaVer;
    private GameObject _installing;
    private GameObject _installComplete;

    void Start()
    {
        GameObject textChild = FindChildRecursive(this.gameObject, "Text");
        Text text = textChild.GetComponent<Text>();
        if (Directory.Exists(Application.persistentDataPath + "/java" + javaVer))
        {
            text.text = "Uninstall";
        }
        else
        {
            text.text = "Install";
        }

        _installing = FindChildRecursive(GameObject.Find("Canvas"), "Installing");
        _installComplete = FindChildRecursive(GameObject.Find("Canvas"), "InstallComplete");


    }


    public void DownloadJava()
    {
        if (!(downloadURL == null && javaPathCmd == null))
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler( DownloadFileCompleted );
            if (!Directory.Exists(Application.persistentDataPath + "/java" + javaVer))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/java" + javaVer);
                Debug.Log("Creating folder");
                _installing.SetActive(true);
                Main.DisableButtons();
                webClient.DownloadFileAsync(new Uri(downloadURL), Application.persistentDataPath +"/java" +javaVer+ "/java"+javaVer+".zip");
                Debug.Log(Application.persistentDataPath +"/java" +javaVer+ "/java/"+javaVer+".zip");
                Debug.Log("Starting download");
            }
            else
            {
                GameObject parent = GameObject.Find("Canvas");
                GameObject alreadyInstall = FindChildRecursive(parent,"AlreadyInstall");
                GameObject yes = FindChildRecursive(alreadyInstall, "Yes");
                Delete delete = yes.GetComponent<Delete>();
                delete.javaVer = javaVer;
                delete.alreadyInstall = alreadyInstall;
                Text text = FindChildRecursive(_installComplete, "Text").GetComponent<Text>();
                text.text = text.text.Replace("{ver}", javaVer);
                alreadyInstall.SetActive(true);
            }

        }
        else
        {
            Debug.Log("Failed");
        }
    }


    private void CorrectDownload()
    {
        Debug.Log("Download complete");
        ZipFile.ExtractToDirectory(Application.persistentDataPath +"/java" +javaVer+ "/java"+javaVer+".zip", Application.persistentDataPath + "/java" + javaVer);
        string javaBinPath = Application.persistentDataPath + "/java" + javaVer + "/bin";
        if (javaBinPath == null) throw new ArgumentNullException(nameof(javaBinPath));
        if (Environment.GetEnvironmentVariable("PATH", User).Contains(javaBinPath))
        {
            Debug.Log("The path is installed yet");
        }
        else
        {
            Debug.Log("No existe");
            
            string pathvar = System.Environment.GetEnvironmentVariable("PATH", User);
            System.Environment.SetEnvironmentVariable("PATH", pathvar + @";"+ javaBinPath, User);        
            Debug.Log("Agregada");
        }
        
        
        Debug.Log(System.Environment.GetEnvironmentVariable("PATH", User));
        _installing.SetActive(false);
        Main.EnableButtons();
        _installComplete.SetActive(true);
        Start();

    }
    
    public void DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        if (e.Error == null)
        {
            CorrectDownload();
        }
    }
    public static GameObject FindChildRecursive(GameObject parent, string childName) {
        foreach(Transform child in parent.transform) {
            if(child.name == childName) {
                return child.gameObject;
            } else {
                if(child.childCount > 0) {
                    GameObject result = FindChildRecursive(child.gameObject, childName);
                    if(result != null) return result;
                }
            }
        }
        return null;
    }
}

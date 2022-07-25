using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public string version;
    

    
    public Text versionTextObject;
    // Start is called before the first frame update
    public void Start()
    {
        Debug.Log("Main script loaded");
        Debug.Log("Aplication data path: "+ Application.persistentDataPath);
        string final = versionTextObject.text.Replace("{ver}", version);
        versionTextObject.text = final;
    }

    public static void DisableButtons()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("InstallButton");

        foreach (GameObject button in objs)
        {
            button.GetComponent<Button>().interactable = false;
        }
    }
    
    public static void EnableButtons()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("InstallButton");
 
        foreach (GameObject button in objs)
        {
            button.GetComponent<Button>().interactable = true;
        }
    }
}

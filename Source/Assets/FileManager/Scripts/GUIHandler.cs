using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIHandler : MonoBehaviour
{
    public GameObject errorPanel;
    public GameObject cautionPanel;
    public GameObject fileInfoPanel;
    public GameObject folderPanel;
    public GameObject pocketPanel;
    bool guiState = false;

    public UnityEngine.UI.Text errorText;
    public UnityEngine.UI.Text cautionText;
    public UnityEngine.UI.Text fileInfoText;
    public UnityEngine.UI.InputField folderText;
    public UnityEngine.UI.Text pocket1Text;
    public UnityEngine.UI.Text pocket2Text;
    public UnityEngine.UI.Text pocket3Text;
    public UnityEngine.UI.Text pocket4Text;
    public UnityEngine.UI.Text pocket5Text;
    public string currentFolderPath = "";

    private List<UnityEngine.UI.Text> pockets = new List<UnityEngine.UI.Text>();





	// Use this for initialization
	void Start () {
		/*
         */ //CommentSwitch, add a space between * and / to switch 
        pocket1Text.text = "-Empty-";
        pocket2Text.text = "-Empty-";
        pocket3Text.text = "-Empty-";
        pocket4Text.text = "-Empty-";
        pocket5Text.text = "-Empty-";
        pocketPanel.SetActive(false);
        folderPanel.SetActive(false);
        /**/
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Unlock"))
        {
            flipInterface(!guiState);
            guiState = !guiState;
        }
	}

    public void setFileText(FileAbstraction fa)
    {
        System.IO.FileInfo fI = new System.IO.FileInfo(fa.filePath);
        long fileLength = fI.Length;
        float fileMB = fileLength / (1024f * 1024f);
        string fileinfo = "File Details\r\n--------------\r\n\r\nFile Name:" + fI.Name + "\r\nFile Size:" + fileMB.ToString("#0.00 MB") + "\r\n\r\n" + "Preview:";
        fileInfoText.text = fileinfo;
        fileInfoPanel.SetActive(true);
    }

    public void setFolderText(string s)
    {
        Debug.LogWarning("Triggered foldertext:"+s);
        currentFolderPath = s;
    }

    public void flipInterface(bool b)
    {
        folderPanel.SetActive(b);
        pocketPanel.SetActive(b);
        
        //this.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().flipMouseLook(b);

        Debug.LogWarning("currentPath:" + currentFolderPath);
        folderText.text = currentFolderPath;
    }
    public void hideFileText()
    {
        fileInfoPanel.SetActive(false);
    }
    
    public void jumpClick()
    {
        string path = folderText.text;
        string[] split = path.Split('\\');
        FolderAbstraction fa = new FolderAbstraction(path, split[split.Length - 1]);
        this.GetComponent<EnvironmentHandler>().changeFolder(fa);
    }

    public void setPocketText(int index, string s)
    {
        switch (index)
        {
            case 0:
                pocket1Text.text = s;
                break;
            case 1:
                pocket2Text.text = s;
                break;
            case 2:
                pocket3Text.text = s;
                break;
            case 3:
                pocket4Text.text = s;
                break;
            case 4:
                pocket5Text.text = s;
                break;
            default:
                break;
        }
    }


}

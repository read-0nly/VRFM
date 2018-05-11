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

    public UnityEngine.UI.Text errorText;
    public UnityEngine.UI.Text cautionText;
    public UnityEngine.UI.Text fileInfoText;
    public UnityEngine.UI.Text folderText;
    public UnityEngine.UI.Text pocket1Text;
    public UnityEngine.UI.Text pocket2Text;
    public UnityEngine.UI.Text pocket3Text;
    public UnityEngine.UI.Text pocket4Text;
    public UnityEngine.UI.Text pocket5Text;

    private List<UnityEngine.UI.Text> pockets = new List<UnityEngine.UI.Text>();





	// Use this for initialization
	void Start () {
		/*
         */ //CommentSwitch, add a space between * and / to switch 
        pocket1Text.text = "Pocket 1";
        pocket2Text.text = "Pocket 2";
        pocket3Text.text = "Pocket 3";
        pocket4Text.text = "Pocket 4";
        pocket5Text.text = "Pocket 5";
        pocketPanel.SetActive(false);
        folderPanel.SetActive(false);
        /**/
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Unlock"))
        {
            flipInterface();
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

    public void flipInterface()
    {
        this.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().flipMouseLook();
        pocketPanel.SetActive(!folderPanel.activeSelf);
        folderPanel.SetActive(!folderPanel.activeSelf);
    }
    public void hideFileText()
    {
        fileInfoPanel.SetActive(false);
    }

    public void flipInventory()
    {

    }

    public void jumpClick()
    {
        string path = folderText.text;
        string[] split = path.Split('\\');
        FolderAbstraction fa = new FolderAbstraction(path, split[split.Length - 1]);
        this.GetComponent<EnvironmentHandler>().changeFolder(fa);
    }


}

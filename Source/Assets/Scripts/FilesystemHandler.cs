using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilesystemHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void moveToFolder(FileAbstraction fiA, FolderAbstraction foA)
    {
        if (!System.IO.File.Exists(foA.folderPath+"\\"+fiA.fileName))
        {
            System.IO.File.Move(fiA.filePath, foA.folderPath + "\\" + fiA.fileName);
        }

    }
}

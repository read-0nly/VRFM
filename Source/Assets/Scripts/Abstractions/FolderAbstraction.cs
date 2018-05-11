using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderAbstraction : MonoBehaviour {

    public string folderPath;
    public string folderName;
    public GameObject selfDoor;

    public FolderAbstraction(string path, string name, GameObject door)
    {
        folderPath = path;
        folderName = name;
        selfDoor = door;
    }
    public FolderAbstraction(string path, string name)
    {

        folderPath = path;
        folderName = name;
    }

    public void enumerate(List<FileAbstraction> files, List<FolderAbstraction> folders)
    {
        string[] sFolders = System.IO.Directory.GetDirectories(folderPath);
        string[] sFiles = System.IO.Directory.GetFiles(folderPath);

        foreach (string sFo in sFolders)
        {
            string[] pathParts = sFo.Split('\\');
            string name = pathParts[pathParts.Length-1];
            FolderAbstraction newFoA = new FolderAbstraction(sFo, name);
            folders.Add(newFoA);
        }
        foreach (string sFi in sFiles)
        {
            string[] pathParts = sFi.Split('\\');
            string name = pathParts[pathParts.Length - 1];
            FileAbstraction newFiA = new FileAbstraction(sFi, name, Color.red, 3);
            files.Add(newFiA);
        }
    }

    public void clone(FolderAbstraction fa)
    {
        folderPath = fa.folderPath;
        folderName = fa.folderName;
        selfDoor = fa.selfDoor;
    }

}

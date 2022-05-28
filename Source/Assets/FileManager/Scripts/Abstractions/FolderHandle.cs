using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderHandle:MonoBehaviour{
    public FolderAbstraction folder;

}
public class FolderAbstraction {

    public string folderPath;
    public string folderName;
    public FolderAbstraction(string path, string name)
    {

        folderPath = path.Replace("\\.\\", "\\").Replace("\\..\\", "\\").Replace("\\\\", "\\");
        folderName = name;
    }

    public bool enumerate(List<FileAbstraction> files, List<FolderAbstraction> folders)
    {
        try
        {
            string[] sFolders = System.IO.Directory.GetDirectories(folderPath);
            string[] sFiles = System.IO.Directory.GetFiles(folderPath);

            foreach (string sFo in sFolders)
            {
                string[] pathParts = sFo.Split('\\');
                string name = pathParts[pathParts.Length - 1];
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
            return true;
        }
        catch (System.Exception e)
        {
            return false;
        }
    }

    public FolderAbstraction(FolderAbstraction fa)
    {
        folderPath = fa.folderPath;
        folderName = fa.folderName;
    }

}

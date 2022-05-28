using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileHandle:MonoBehaviour{
    public FileAbstraction file;
}
public class FileAbstraction{
    public string filePath;
    public string fileName;
    public Color cubeColor;
    public int size;

    public FileAbstraction(FileAbstraction fSource)
    {
        filePath=fSource.filePath;
        fileName=fSource.fileName;
        cubeColor=fSource.cubeColor;
        size=fSource.size;

    }
    public FileAbstraction(string path, string name, Color color, int cubeSize)
    {
        filePath = path;
        fileName = name;
        cubeColor = color;
        size = cubeSize;
    }

    public void execute()
    {
        System.Diagnostics.Process.Start(filePath);
    }
    

}

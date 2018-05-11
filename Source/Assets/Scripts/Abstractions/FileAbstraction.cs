using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileAbstraction : MonoBehaviour{
    public string filePath;
    public string fileName;
    public Color cubeColor;
    public int size;
    public GameObject selfCube;

    public FileAbstraction(FileAbstraction fSource)
    {
        filePath=fSource.filePath;
        fileName=fSource.fileName;
        cubeColor=fSource.cubeColor;
        size=fSource.size;

    }

    public FileAbstraction(string path, string name, Color color, int cubeSize, GameObject cube)
    {
        filePath = path;
        fileName = name;
        cubeColor = color;
        size = cubeSize;
        selfCube = cube;
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

    public void clone(FileAbstraction fa)
    {
        this.fileName = fa.fileName;
        this.filePath = fa.filePath;
        this.cubeColor = fa.cubeColor;
        this.size = fa.size;
        selfCube = fa.selfCube;
    }
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketAbstraction : MonoBehaviour {

    public FileAbstraction fileAbs;

    public PocketAbstraction(FileAbstraction fa)
    {
        fileAbs = fa;
    }
    public PocketAbstraction()
    {
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public PocketAbstraction addToPocket(GameObject obj, GameObject player)
    {
        Debug.LogError(obj.name);
        Debug.LogError(obj.GetComponent<FileHandle>().file.fileName);
        fileAbs = new FileAbstraction(obj.GetComponent<FileHandle>().file);
        Debug.LogError(fileAbs.fileName);
        player.GetComponent<EnvironmentHandler>().Cubes.Remove(obj);
        Destroy(obj);
        return this;
    }
    public GameObject removeFromPocket(GameObject player)
    {
        GameObject newCube = player.GetComponent<EnvironmentHandler>().createCube(fileAbs);
        newCube.GetComponent<FileHandle>().file = new FileAbstraction(fileAbs);
        fileAbs = null;
        return newCube;

    }
}

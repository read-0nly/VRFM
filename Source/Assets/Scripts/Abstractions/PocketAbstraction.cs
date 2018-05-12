using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketAbstraction : MonoBehaviour {

    public GameObject fileCube;
    public FileAbstraction fileAbs;

    public PocketAbstraction(FileAbstraction fa)
    {
        fileAbs = fa;
        fileCube = fa.selfCube;
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
        Debug.LogError(obj.GetComponent<FileAbstraction>().fileName);
        fileAbs = new FileAbstraction(obj.GetComponent<FileAbstraction>());
        Debug.LogError(fileAbs.fileName);
        fileCube = obj;
        player.GetComponent<EnvironmentHandler>().Cubes.Remove(obj);
        Destroy(obj);
        return this;
    }
    public GameObject removeFromPocket(GameObject player)
    {
        GameObject newCube = player.GetComponent<EnvironmentHandler>().createCube(fileAbs);
        newCube.GetComponent<FileAbstraction>().clone(fileAbs);
        fileAbs = null;
        fileCube = null;
        return newCube;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour {

    public List<PocketAbstraction> pockets = new List<PocketAbstraction>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void pocketSelected(GameObject obj)
    {
        int i = 0;
        bool pocketed = false;
        while (i < pockets.Count && !pocketed)
        {
            if (Object.Equals(pockets[i].fileAbs, null))
            {
                pockets[i].addToPocket(obj, this.gameObject);
                this.GetComponent<GUIHandler>().setPocketText(i, pockets[i].fileAbs.fileName);
                pocketed = true;
            }
            i++;
        }
        if (!pocketed)
        {
            Debug.LogError("FullPockets");
        }

    }
    public void unPocket()
    {
        int i = 0;
        bool pocketed = false;
        while (i < pockets.Count && !pocketed)
        {
            if (!Object.Equals(pockets[i].fileAbs, null))
            {
                GameObject newCube = pockets[i].removeFromPocket(this.gameObject);
                bool b = GetComponent<FilesystemHandler>().moveToFolder(newCube.GetComponent<FileAbstraction>(), GetComponent<EnvironmentHandler>().currentFolder);
                if (!this.gameObject.GetComponent<EnvironmentHandler>().isCubeLoaded(newCube.GetComponent<FileAbstraction>()))
                {
                    if (!this.gameObject.GetComponent<ObjectHandler>().flipHold(newCube)) { this.gameObject.GetComponent<ObjectHandler>().flipHold(newCube); }
                    this.gameObject.GetComponent<EnvironmentHandler>().Cubes.Add(newCube);
                }
                else
                {
                    Destroy(newCube);
                }
                this.GetComponent<GUIHandler>().setPocketText(i, "-Empty-");
                pocketed = true;
            }
            i++;
        }
        if (!pocketed)
        {
            Debug.LogError("EmptyPockets");
        }

    }
    public void pocketSelected(GameObject obj, int i)
    {
       
            if (pockets[i].fileAbs == null)
            {
                pockets[i].addToPocket(obj, this.gameObject);
            }
            else
            {
                pockets[i].removeFromPocket(this.gameObject);
            }
    }
}

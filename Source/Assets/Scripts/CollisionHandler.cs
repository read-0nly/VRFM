using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {
    public string path;
    public string texture;
    public Color primaryColor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision other)
    {
        Debug.LogError("Collided:" + this.gameObject.GetComponent<Rigidbody>().name + " : " + other.gameObject.name + " + " + other.gameObject.transform.parent.name);
        if (other.gameObject.name == "Face" && other.gameObject.transform.parent.name == "Door(Clone)")
        {
            this.gameObject.transform.position = new Vector3(0, 1, 0);
            FolderAbstraction fa = other.gameObject.transform.parent.gameObject.GetComponent<FolderAbstraction>();
            Debug.LogError(fa.folderName);

            this.gameObject.GetComponent<EnvironmentHandler>().changeFolder(fa);
        }
          
    }
}

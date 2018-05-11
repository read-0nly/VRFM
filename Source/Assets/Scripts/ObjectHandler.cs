using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour {
    public GameObject selectedObject;
    public GameObject heldObject;
    public GameObject emptyObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")){

            RaycastHit hit;
            Ray ray = this.transform.Find("Camera").GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f,0));
        
            if (Physics.Raycast(ray, out hit)) {
                Transform objectHit = hit.transform;
                if (objectHit.name.Contains("Cube")) { 
                    selectedObject = objectHit.gameObject;
                    this.GetComponent<GUIHandler>().setFileText(selectedObject.GetComponent<FileAbstraction>());
                }
                else
                {
                    this.GetComponent<GUIHandler>().hideFileText();
                }
                
                // Do something with the object that was hit by the raycast.
            }
        }
        if (Input.GetButtonDown("Grab"))
        {
            flipHold();
        }
        if (Input.GetButtonDown("Pocket"))
        {
            if (!Object.Equals(selectedObject, null) && selectedObject != emptyObject) { this.GetComponent<InventoryHandler>().pocketSelected(selectedObject); selectedObject = emptyObject; }
            else { this.GetComponent<InventoryHandler>().unPocket(); }
            
        }



        if (heldObject != null)
        {
            heldObject.transform.position = this.transform.Find("Camera").GetComponent<Camera>().transform.position +
                Vector3.Scale(this.transform.Find("Camera").GetComponent<Camera>().transform.forward, new Vector3(4, 4, 4)) + new Vector3(0, 0, 0);
        }
	}
    public void flipHold()
    {
        if (heldObject == null || heldObject == emptyObject)
        {
            heldObject = selectedObject.gameObject;
            heldObject.GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            heldObject.GetComponent<Rigidbody>().useGravity = true;
            heldObject = emptyObject;
        }
    }
    public bool flipHold(GameObject passedObject)
    {
        if (heldObject == null || Object.Equals(heldObject, emptyObject))
        {
            heldObject = passedObject.gameObject;
            heldObject.GetComponent<Rigidbody>().useGravity = false;
            return true;
        }
        else
        {
            heldObject.GetComponent<Rigidbody>().useGravity = true;
            heldObject = emptyObject;
            return false;
        }
    }
}

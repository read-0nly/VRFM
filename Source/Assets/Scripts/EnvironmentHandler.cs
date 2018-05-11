using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHandler : MonoBehaviour {

    public FolderAbstraction currentFolder;
    List<FolderAbstraction> folderList = new List<FolderAbstraction>();
    List<FileAbstraction> fileList = new List<FileAbstraction>();
    private float colwidth = 1;
    private int shelfWidth = 6;
    public int ShelfCount = 0;
    public float ShelfBuffer = 2f;
    public float doorBuffer = 1f;

    public GameObject d_Cubes;
    public GameObject d_Lamp;
    public GameObject d_Shelves;
    public GameObject d_RoomParts;
    public GameObject d_Door;
    public GameObject d_Player;
    
    public GameObject Room;
    public List<GameObject> Doors;
    public List<GameObject> Shelves;
    public List<GameObject> Cubes;


	// Use this for initialization
    void Start()
    {
        createEnv();
	}

    public void changeFolder(FolderAbstraction fa)
    {

        d_Player.transform.position = new Vector3(0, 1, 0);
        currentFolder=new FolderAbstraction(fa.folderPath,fa.folderName);
        folderList = new List<FolderAbstraction>();
        fileList = new List<FileAbstraction>();
        destroyEnv();
        createEnv();
    }

    public void destroyEnv()
    {
        GameObject.Destroy(Room);
        foreach (GameObject go in Doors)
        {
            GameObject.Destroy(go);
        }
        foreach (GameObject go in Shelves)
        {
            GameObject.Destroy(go);
        }
        foreach (GameObject go in Cubes)
        {
            GameObject.Destroy(go);
        }
        Doors = new List<GameObject>();
        Shelves = new List<GameObject>();
        Cubes = new List<GameObject>();
        Room = null;
    }

    public void createEnv()
    {
       // currentFolder = new FolderAbstraction("C:\\", "C:");
        if ( Object.Equals(currentFolder,null))
        {
            Debug.LogError("istrue");
            string[] Drives = System.IO.Directory.GetLogicalDrives();
            foreach (string d in Drives)
            {

                FolderAbstraction fa = new FolderAbstraction(d, d);
                folderList.Add(fa);
            }
            createDoors(folderList, 2f);
        }
        else
        {
            Debug.LogError("iselse");
            FolderAbstraction fa = new FolderAbstraction(currentFolder.folderPath + "\\..", "..");
            folderList.Add(fa);
            fa = new FolderAbstraction(currentFolder.folderPath + "\\.", ".");
            folderList.Add(fa);
                
            currentFolder.enumerate(fileList,folderList);
            
            createDoors(folderList, 2f);
        }
            
        

        //demo doors
        /*GameObject newDoor = Instantiate(d_Door);
        newDoor.transform.position = newDoor.transform.position + new Vector3(0f, 0f, 2.5f);
        Doors.Add(newDoor);
        
        newDoor = Instantiate(d_Door);
        newDoor.transform.position = newDoor.transform.position - new Vector3(0f, 0f, 2.5f);
        Doors.Add(newDoor);
        */
        createCubes(fileList);
        
        //Calc how many shelves we'll need
        ShelfCount = (int)((Cubes.Count / (shelfWidth * 3)) + (Cubes.Count % (shelfWidth * 3) != 0 ? 1 : 0));

        //fix player position so we don't clip a shelf
        if (ShelfCount % 2 != 0) { d_Player.transform.position = d_Player.transform.position + new Vector3(0, 0, ShelfBuffer / 2f + 0.5f); }

        //create shelves
        createShelves(ShelfCount, ShelfBuffer);

        //Put the cubes on the shelves
        AlignCubes();

        //Create and align the room as necessary to fit contents
        Room = createRoom();

        
    }
    void createShelves(int number, float buffer)
    {
        if (number > 0) {
            GameObject shelf = Instantiate(d_Shelves);
            shelf.transform.position = shelf.transform.position + new Vector3(0, 0, number / 2f + (number / 2f * buffer) - (buffer / 2f) - 0.5f);
            Shelves.Add(shelf);
            int i = 1;
            while (i < number)
            {
                GameObject newShelf = Instantiate(d_Shelves);
                newShelf.transform.position = shelf.transform.position - new Vector3(0, 0, 1 + buffer);
                Shelves.Add(newShelf);

                shelf = null;
                shelf = newShelf.gameObject;
                i++;
            }
        }
    }
    void createDoors(List<FolderAbstraction> folders, float buffer)
    {

        if (folders.Count > 0)
        {
            GameObject door = Instantiate(d_Door);
            int number = folders.Count;
            door.transform.position = door.transform.position + new Vector3(0, 0, number / 2f + (number / 2f * buffer) - (buffer / 2f) - 0.5f);
            folders[0].selfDoor = door;
            door.GetComponent<FolderAbstraction>().clone(folders[0]);
            door.transform.Find("Text").gameObject.GetComponent<TextMesh>().text = folders[0].folderName;

            Doors.Add(door);
            int i = 1;
            while (i < number)
            {
                GameObject newDoor = Instantiate(d_Door);
                newDoor.transform.position = door.transform.position - new Vector3(0, 0, 1 + buffer);

                folders[i].selfDoor = door;
                newDoor.GetComponent<FolderAbstraction>().clone(folders[i]);
                newDoor.transform.Find("Text").GetComponent<TextMesh>().text = folders[i].folderName;

                Doors.Add(newDoor);

                door = null;
                door = newDoor.gameObject;
                i++;
            }
        }

    }

    void createCubes(List<FileAbstraction> files)
    {

        if (files.Count > 0)
        {
            int number = files.Count;
            GameObject c = createCube(files[0]);
            Cubes.Add(c);
            int i = 1;
            while (i < number)
            {
                c = createCube(files[i]);
                Cubes.Add(c);
                i++;
            }
        }

    }

    public GameObject createCube(FileAbstraction fa)
    {
        GameObject cube;

        switch (fa.size)
        {
            case 1:
                cube = Instantiate(d_Cubes.transform.Find("SmallCube").gameObject);
                break;
            case 2:
                cube = Instantiate(d_Cubes.transform.Find("MediumCube").gameObject);
                break;
            case 3:
                cube = Instantiate(d_Cubes.transform.Find("BigCube").gameObject);
                break;
            default:
                cube = Instantiate(d_Cubes.transform.Find("TinyCube").gameObject);
                break;
        }

        fa.selfCube = cube;
        cube.GetComponent<FileAbstraction>().clone(fa);

        return cube;

    }

    GameObject createDoor(FolderAbstraction fa)
    {
        GameObject door = Instantiate(d_Door);
        door.transform.Find("Text").GetComponent<TextMesh>().text = fa.folderName;
        fa.selfDoor = door;
        door.GetComponent<FolderAbstraction>().clone(fa);
        return door;
    }

    GameObject createRoom()
    {
        float farZ = 0;
        if (Shelves.Count > 0)
        {
            if (Doors.Count > 0)
            {
                farZ = (Shelves[0].transform.position.z > Doors[0].transform.position.z ? Shelves[0].transform.position.z + 0.5f : Doors[0].transform.position.z + 2f);
            }
            else{
                farZ = Shelves[0].transform.position.z + 0.5f;
            }
        }
        else{
            if (Doors.Count > 0)
            {
                farZ = Doors[0].transform.position.z + 2f;
            }
            else{
                farZ = 2f;
            }
        }float roomZ = (farZ * 2f) / 10f;

        GameObject newRoom = Instantiate(d_RoomParts);

        newRoom.transform.Find("Floor").localScale = new Vector3(1.5f, 1, roomZ);
        newRoom.transform.Find("Ceiling").localScale = new Vector3(1.5f, 1, roomZ);


        newRoom.transform.Find("RightWall").localScale = new Vector3(newRoom.transform.Find("RightWall").localScale.x, newRoom.transform.Find("RightWall").localScale.y, roomZ);
        newRoom.transform.Find("LeftWall").localScale = new Vector3(newRoom.transform.Find("LeftWall").localScale.x, newRoom.transform.Find("LeftWall").localScale.y, roomZ);
        newRoom.transform.Find("BackWall").position = new Vector3(newRoom.transform.Find("BackWall").position.x, newRoom.transform.Find("BackWall").position.y, -farZ);
        newRoom.transform.Find("ForwardWall").position = new Vector3(newRoom.transform.Find("ForwardWall").position.x, newRoom.transform.Find("ForwardWall").position.y, farZ);
        return newRoom;
    }
    
    void AlignCubes()
    {
        int currentShelves = 0;
        int currentLevel = 0;
        int column = 0;
        int direction;
        GameObject prevCube;
        GameObject textR;
        GameObject textL;

        foreach(GameObject c in Cubes){

            Transform curShelf;
            switch (currentLevel)
            {
                case 1:
                    curShelf = Shelves[currentShelves].transform.Find("BottomShelf").transform;
                    textL = Shelves[currentShelves].transform.Find("LeftBookend").Find("MiddleText").gameObject;
                    textR = Shelves[currentShelves].transform.Find("RightBookend").Find("MiddleText").gameObject;
                    direction = 1;
                    break;
                case 2:
                    curShelf = Shelves[currentShelves].transform.Find("BottomShelf").transform;
                    textL = Shelves[currentShelves].transform.Find("LeftBookend").Find("BottomText").gameObject;
                    textR = Shelves[currentShelves].transform.Find("RightBookend").Find("BottomText").gameObject;
                    direction = -1;
                    break;
                default:
                    curShelf = Shelves[currentShelves].transform.Find("TopShelf").transform;
                    textL = Shelves[currentShelves].transform.Find("LeftBookend").Find("TopText").gameObject;
                    textR = Shelves[currentShelves].transform.Find("RightBookend").Find("TopText").gameObject;
                    direction = 1;
                    break;  
            }
            if (column == 0)
            {
                textL.GetComponent<TextMesh>().text = c.GetComponent<FileAbstraction>().fileName.Substring(0, 3).ToUpper();
                textR.GetComponent<TextMesh>().text = c.GetComponent<FileAbstraction>().fileName.Substring(0, 3).ToUpper();
            }
            else if (column == shelfWidth - 1)
            {
                textL.GetComponent<TextMesh>().text += "\r\n" + c.GetComponent<FileAbstraction>().fileName.Substring(0, 3).ToUpper();
                textR.GetComponent<TextMesh>().text += "\r\n" + c.GetComponent<FileAbstraction>().fileName.Substring(0, 3).ToUpper();

            }
            int colFromMid = (int)(column - (shelfWidth/2));
            float newX = colFromMid + 0.5f;
            c.transform.position = curShelf.position + new Vector3(newX, direction, 0);
            if (column < shelfWidth - 1)
            {
                column++;
            }
            else
            {
                column = 0;
                currentLevel++; 
                if (currentLevel < 3)
                {
                }
                else
                {
                    currentLevel = 0;
                    currentShelves++;
                }
            }


        }

    }
	// Update is called once per frame
	
    
    void Update (){
		
	}
}

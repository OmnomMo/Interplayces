using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadedObjectManager : MonoBehaviour {

    public LinkedList<GameObject> persistentObjects;

    private static LoadedObjectManager instance;
    public static LoadedObjectManager Instance
    {
        get { return instance; }
    }




    private void Awake()
    {


        if (LoadedObjectManager.Instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        persistentObjects = new LinkedList<GameObject>();
    }

    // Use this for initialization
    void Start () {
        
	}
	
    public void AddPersistenObject(GameObject newObject) {
        persistentObjects.AddLast(newObject);    

        }

    public void ResetPersistentObjects()
    {
        Debug.Log("ResetPersistentObjects");

        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
            {
                GameObject.Destroy(obj);
            }
        }

        persistentObjects = new LinkedList<GameObject>();
    }


	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour {

    public MeshFilter cloudMesh;
    public Transform centerPoint;

    public GameObject asteroidPrefab;

    public int nAsteroids;

	// Use this for initialization
	void Start () {

        if (GameState.Instance.isPlayerCaptain()) { 
         Spawn();
            cloudMesh.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
	}
	

    public void Spawn()
    {

        Vector3 randomPoint = new Vector3();

        for (int i = 0; i < nAsteroids; i++)
        {
            Vector3 randomVertex = cloudMesh.mesh.vertices[(int)(Random.Range(0f, (float)cloudMesh.mesh.vertices.Length))];

            Debug.Log(randomVertex);

            randomPoint = (randomVertex + ( centerPoint.localPosition - randomVertex) * Random.Range(0f, 1f) );

            GameObject newAsteroid = GameObject.Instantiate(asteroidPrefab);

            //newAsteroid.GetComponent<Rigidbody>().isKinematic = true;

            newAsteroid.transform.parent = transform;
            newAsteroid.transform.localPosition = randomPoint;
            

        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}

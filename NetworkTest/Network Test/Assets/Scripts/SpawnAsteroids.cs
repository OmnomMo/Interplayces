using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour {

    public MeshFilter cloudMesh;
    public Transform centerPoint;

    public GameObject asteroidPrefab;

    public int nAsteroids;
    public bool testScene;

	// Use this for initialization
	void Start () {

        if ((GameState.Instance != null && GameState.Instance.isPlayerCaptain()) || testScene) { 
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

//            Debug.Log(randomVertex);

            randomPoint = (randomVertex + ( centerPoint.localPosition - randomVertex) * Mathf.Pow(Random.Range(0f, 1f),2) );

            GameObject newAsteroid = GameObject.Instantiate(asteroidPrefab);

            //newAsteroid.GetComponent<Rigidbody>().isKinematic = true;

            float a_scale = Random.Range(0.5f, 2f);



            newAsteroid.transform.localScale = new Vector3(a_scale, a_scale, a_scale);
            newAsteroid.transform.Translate(new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));

            newAsteroid.transform.localEulerAngles = new Vector3(Random.Range(0,360), Random.Range(0, 360), Random.Range(0, 360));
            newAsteroid.transform.parent = transform;
            newAsteroid.transform.localPosition = randomPoint;
            

        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}

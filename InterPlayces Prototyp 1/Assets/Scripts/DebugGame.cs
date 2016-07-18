using UnityEngine;
using System;
using System.Collections;

public class DebugGame : MonoBehaviour {
    

    GameObject testPartx = null;
    

	// Use this for initialization
	void Start () {

        //Cylinder.GetComponent<Connection>().SetupConnection(0, 0, 1, 5);

        //StartCoroutine(DelayedSetup());

        //foreach (Robopart rp in Parts.Instance.GetComponentsInChildren<Robopart>()) { 
        //   Parts.Instance.AddRobopart(rp.gameObject);
        //}


        //TODO: Setup Fixed Center Part (Cockpit)


        //testPartx = ImportCubes.Instance.ImportCube(0, 0, 45, 0);
        //GameObject testPart2 = ImportCubes.Instance.ImportCube(2, -2, 0, 2);
        //GameObject testPart0 = ImportCubes.Instance.ImportCube(-2, -2, 0, 2);
        //ImportCubes.Instance.ImportCube(3, -2, 0, 1);
        //ImportCubes.Instance.ImportCube(-3, -2, 0, 1);
        //ImportCubes.Instance.ImportCube(-1, 3, 0, 2);
        //ImportCubes.Instance.ImportCube(-3, 0, 0, 2);
        //ImportCubes.Instance.ImportCube(0, -3, 0, 0);
        //ImportCubes.Instance.ImportCube(0, -5, 0, 2);
        //ImportCubes.Instance.ImportCube(1, -5, 0, 2);
        //ImportCubes.Instance.ImportCube(-1, -5, 0, 2);
        //ImportCubes.Instance.ImportCube(2, -5, 0, 1);
        //GameObject testPart1 = ImportCubes.Instance.ImportCube(-1, -2, 0, 0);





        
       //Parts.Instance.TranslateRobot();

    }

    //public IEnumerator DelayedSetup()
    //{
    //    yield return new WaitForSeconds(0.1f);

    //    Parts.Instance.FreezeParts();
    //}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Unhinge

            Time.timeScale = 1;

            

        }


        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Unhinge
            Debug.Log("2");
            Time.timeScale = 0;



        }

        //Debug.Log("Parts Frozen: " + Parts.Instance.GetComponent<Rigidbody>().isKinematic);



        //Connections change when Physics are at play
        //Parts.Instance.ResetConnections();

        //partXPos += 1 * Time.deltaTime;

        //testPartx.GetComponent<Robopart>().UpdatePosition(0, partXPos, 0);

        //print(GameState.Instance.IsInPlay());

    }
}

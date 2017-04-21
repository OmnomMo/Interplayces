using UnityEngine;
using System.Collections;


//Rotates Object to face camera at every frame
public class Billboard : MonoBehaviour
{

    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
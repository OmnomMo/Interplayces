using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScanner_Slider : MonoBehaviour
{

    /**
float z = 6.818f;
float x = -0.8404f;
void OnMouseDrag()
{
    //z = transform.position.z;
    //x = transform.position.x;
    Vector3 mousePosition = new Vector3(x, Input.mousePosition.y, z);
    Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);


    transform.position = objPosition;//new Vector3(transform.position.x, transform.position.y, Input.mousePosition.y);


    Vector3 mousePosition = new Vector3(transform.position.x, transform.position.y, Input.mousePosition.y);
    Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);


    transform.position = objPosition;//new Vector3(transform.position.x, transform.position.y, Input.mousePosition.y);

}
    **/
    private Vector3 screenPoint;
    private Vector3 offset;
    public float min;
    public float max;
    float currenty;
    void OnMouseDown()
    {
        currenty = transform.position.z;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(screenPoint.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 cursorPoint = new Vector3(screenPoint.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        //if (transform.position.z <= max && transform.position.z >= min)
        //{
            transform.position = cursorPosition;
            print(transform.position);
        //}

     }

    void OnMouseUp()
    {
        if (gameObject.tag == "Scanner")
        {
            GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
            cam.transform.Translate(0, 0, (currenty - transform.position.z) * 100);

        }
        if (gameObject.tag == "Boost")
        {
            GameObject boost = GameObject.FindGameObjectWithTag("BoostPointer");
            boost.transform.Rotate(0, -(currenty - transform.position.z) * 200, 0);
            GameObject ship = GameObject.FindGameObjectWithTag("Player");
            testMovement_Delete shipScript = ship.GetComponent<testMovement_Delete>();
            shipScript.speed = -(currenty - transform.position.z)*20;

        }
    }
}

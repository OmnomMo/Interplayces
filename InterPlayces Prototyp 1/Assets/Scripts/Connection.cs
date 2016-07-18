using UnityEngine;
using System.Collections;

public class Connection : MonoBehaviour
{

    public float length;

    public GameObject correspondingConnection;

    // Use this for initialization
    void Start()
    {

    }

    public void SetupConnection(float x1, float y1, float x2, float y2)
    {

        //print("1: (" + x1 + "/" + y1 + ")" + "  2: (" + x2 + "/" + y2 + ")");

        //if (x1 > x2)
        //{
        //    print("Swap!");

        //    float tempX;
        //    tempX = x2;
        //    x2 = x1;
        //    x1 = tempX;

        //    float tempY;
        //    tempY = y2;
        //    y2 = y1;
        //    y1 = tempY;
        //}

        Vector3 path = new Vector3();
        Vector3 origin = new Vector3(x1, y1, 0);
        Vector3 target = new Vector3(x2, y2, 0);
        path = target - origin;

     //   print(path);

        

         length = path.magnitude;


        float scale = (length +1f) / 2 + 0.3f;


        transform.localScale = new Vector3(1f, scale, 1f);

        transform.parent = Parts.Instance.gameObject.transform;


        Vector3 newPosition = origin + new Vector3(path.x / 2, path.y / 2, 0);

        

        //print(new Vector3(path.x / 2, path.y / 2, 0));


        float angle = Vector3.Angle(path, Vector3.right);

  



        angle = (angle - 90);
   
       

        if (path.y >= 0)
        {

        } else
        {
            angle = angle * -1;
        }

      // print(angle);

        transform.position = newPosition;
        transform.Rotate(Vector3.forward * angle);


        //} else
        //{
        //    transform.Rotate(Vector3.forward * (Vector3.Angle(path, Vector3.right)-90) * -1);
        //}
    

        //transform.rotation = Vector3.Angle(origin, target);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using UnityEngine;
using System.Collections;

public class TrackingDummy : MonoBehaviour {

    public bool followMouse;
    public Camera overview;
    public bool isVisible;
    public bool movable;

    public GameObject playingField;

    // Use this for initialization
    void Start () {
        if (this.GetComponent<SpriteRenderer>() != null)
        {
            isVisible = false;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        }

            
       
        followMouse = false;
	}
	
    void OnMouseDown()
    {
        if (movable && isVisible)
        {
            followMouse = true;
        }
    }

    void OnMouseUp()
    {
        followMouse = false;
    }

    void OnMouseOver()
    {

        if (this.GetComponent<SpriteRenderer>() != null)
        {

            if (Input.GetMouseButtonUp(1))
            {
                if (isVisible)
                {
                    isVisible = false;
                    GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

                }
                else
                {
                    isVisible = true;
                    GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
                }
            }
        }
    }

	// Update is called once per frame
	void Update () {
	



        if (followMouse)
        {



            Vector3 mousePos = Input.mousePosition;

            Vector3 newPos = overview.ScreenToWorldPoint(mousePos);

            //clamp values

            if (newPos.x > playingField.transform.position.x + playingField.transform.localScale.x / 2 - 0.5f)
            {
                newPos.x = playingField.transform.position.x + playingField.transform.localScale.x / 2 - 0.5f;
            }

            if (newPos.x < playingField.transform.position.x - playingField.transform.localScale.x / 2 + 0.5f)
            {
                newPos.x = playingField.transform.position.x - playingField.transform.localScale.x / 2 + 0.5f;
            }

            if (newPos.y > playingField.transform.position.y + playingField.transform.localScale.y / 2 - 0.5f)
            {
                newPos.y = playingField.transform.position.y + playingField.transform.localScale.y / 2 - 0.5f;
            }

            if (newPos.y < playingField.transform.position.y - playingField.transform.localScale.y / 2 + 0.5f)
            {
                newPos.y = playingField.transform.position.y - playingField.transform.localScale.y / 2 + 0.5f;
            }


            newPos.z = transform.position.z;

            transform.position = newPos;

           
        }
	}
}

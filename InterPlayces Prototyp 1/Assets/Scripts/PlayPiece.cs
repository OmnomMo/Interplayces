using UnityEngine;
using System.Collections;

public class PlayPiece : MonoBehaviour {

    public GameObject trackedCube;
    //public GameObject piece;

    public int posX;
    public int posY;

	// Use this for initialization
	void Start () {
	
	}
	

    // Handles connection with playingGrid
	// Update is called once per frame
	void LateUpdate () {
        transform.position = trackedCube.transform.position;


        GameObject piece = null;

        if (GetComponentInChildren<Robopart>(true) != null)
        {
            piece = GetComponentInChildren<Robopart>(true).gameObject;
        

       
            //Debug.Log("EXISTINGGGGG!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            if (trackedCube.GetComponent<TrackCubePosition>().isVisible)
            {
                //Debug.Log("VISIBLLEEEEEE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                if (!piece.activeInHierarchy) { piece.SetActive(true); }
                PlayingGrid.Instance.IncludePiece(this.gameObject);
                // transform.localPosition = PlayingGrid.Instance.SnapTranslation(transform.localPosition);

            } else
            {
                if (piece.activeInHierarchy) { piece.SetActive(false); }
                PlayingGrid.Instance.RemovePiece(this.gameObject);
            }
        }

        

        //Todo: SnapToGrid
    }
}

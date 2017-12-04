using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayingGrid : MonoBehaviour
{

    //Twodimensional Array in which the GameObject on that position is stored;

    public GameObject[,] containerGrid;

    public int gridColumns;
    public int gridRows;

    public GameObject gridPart;



    private static PlayingGrid instance;
    public static PlayingGrid Instance
    {
        get { return instance; }
    }



    void Awake()
    {
        instance = this;
    }

    public LinkedList<GameObject> GetAllParts()
    {

        LinkedList<GameObject> partList = new LinkedList<GameObject>();

        for (int x = 0; x < gridColumns; x++)
        {
            for (int y = 0; y < gridRows; y++)
            {
                if (containerGrid[x, y].GetComponentInChildren<ShipPart>().getID() != 4)
                {
                    partList.AddLast(containerGrid[x, y].transform.GetChild(0).gameObject);
                }
            }
        }

        return partList;
    }

    // Use this for initialization
    void Start()
    {
        containerGrid = new GameObject[gridColumns, gridRows];
        //CreateDebugGrid();
    }

    public void CreateDebugGrid()
    {
        for (int x = 0; x < gridColumns; x++)
        {
            for (int y = 0; y < gridRows; y++)
            {
                GameObject newPart = GameObject.Instantiate(gridPart);
                newPart.transform.parent = this.transform;
                newPart.transform.position = new Vector3(x, y, 0);
                newPart.SetActive(true);
            }
        }
    }



    public void ClearChildren(int posX, int posY)
    {
        int n = 0;

        foreach (Transform child in containerGrid[posX, posY].transform)
        {
           // Debug.Log("Clear Child " + n++ + " at position " + posX + "/" + posY + "!");
            child.parent = null;
            Destroy(child.gameObject);
        }

    }


    public void ClearPiece(int x, int y)
    {

        NetworkActions.Instance.CmdSetPartTypes(x, y, 4);
    }

   

    //Remove Piece. Return piece when found, return null when not
    public GameObject RemovePiece(GameObject oldPiece)
    {

        //NetworkActions.Instance.CmdSetPartTypes(oldPiece.GetComponentInChildren<ShipPart>().GetPosX(), oldPiece.GetComponentInChildren<ShipPart>().GetPosY(), 4);
        NetworkActions.Instance.CmdSetPartTypes(oldPiece.GetComponentInChildren<ShipPart>().GetPosX(), oldPiece.GetComponentInChildren<ShipPart>().GetPosY(), 4);


        return oldPiece;
    }



    //Check if piece is in game;
    public bool IsPieceInPlay(GameObject piece)
    {



        for (int x = 0; x < gridColumns; x++)
        {
            for (int y = 0; y < gridRows; y++)
            {
                if (containerGrid[x, y] != null && containerGrid[x, y].Equals(piece))
                {
                    return true;
                }
            }
        }

        return false;
    }



    public bool IsEmpty(int posX, int posY)
    {
        //Debug.Log("posX: " + posX + "    posY: " + posY);

        if (containerGrid[posX, posY].GetComponentInChildren<ShipPart>().getID() == 4)  // || grid[posX,posY].GetComponent<ShipPart>().getID() == 4)
        {
           // Debug.Log("GridPos Empty!");
            return true;
        }
        else
        {
           // Debug.Log("Careful, field not empty!");
            return false;
        }
    }

    public bool IsEmptyOrSame(int posX, int posY, GameObject comparator)
    {
        if (containerGrid[posX, posY].GetComponentInChildren<ShipPart>().getID() == 4)
        {
            return true;
        }
        else
        {

            //Debug.Log(containerGrid[posX, posY].GetComponentInChildren<ShipPart>().GetPosX() + " " + comparator.GetComponent<ShipPart>().GetPosX() + 
            //    " " + containerGrid[posX, posY].GetComponentInChildren<ShipPart>().GetPosY() + " " + comparator.GetComponent<ShipPart>().GetPosY());
            if (containerGrid[posX, posY].GetComponentInChildren<ShipPart>().GetPosX() == comparator.GetComponent<ShipPart>().GetPosX()
                && containerGrid[posX, posY].GetComponentInChildren<ShipPart>().GetPosY() == comparator.GetComponent<ShipPart>().GetPosY())
            {
                return true;
            }
            
            return false;
        }
    }


    public bool IsEmpty(Vector2 pos)
    {
        return IsEmpty((int)pos.x, (int)pos.y);
    }

    public bool HasPieces()
    {
        for (int x = 0; x < gridColumns; x++)
        {
            for (int y = 0; y < gridRows; y++)
            {
                if (containerGrid[x, y] != null)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public GameObject GetPiece(int posX, int posY)
    {
        return containerGrid[posX, posY].transform.GetChild(0).gameObject;
    }

    

    public GameObject SnapTranslationToNearest(GameObject piece)
    {


        float nearest = 100000f;
        Vector3 pos = Vector3.zero;

        GameObject nearestGridPeace = null;


        foreach (GameObject gridPiece in containerGrid)
        {
            
            float distanceSq = Mathf.Pow(gridPiece.transform.position.x - piece.transform.position.x, 2) + Mathf.Pow(gridPiece.transform.position.y - piece.transform.position.y, 2) + Mathf.Pow(gridPiece.transform.position.z - piece.transform.position.z, 2);
         



            if (distanceSq < nearest)
            {
               
                    nearest = distanceSq;
                

                    pos = new Vector3(gridPiece.transform.position.x, gridPiece.transform.position.y, -2);//gridPiece.transform.position.z);


                nearestGridPeace = gridPiece;


            }
        }


        if (nearestGridPeace != null)
        {
            if (IsEmpty(nearestGridPeace.GetComponentInChildren<ShipPart>().GetPosX(), nearestGridPeace.GetComponentInChildren<ShipPart>().GetPosY()))//, piece.transform.GetChild(0).gameObject))
            {

                piece.GetComponentInChildren<ShipPart>().SetPosX(nearestGridPeace.GetComponentInChildren<ShipPart>().GetPosX());
                piece.GetComponentInChildren<ShipPart>().SetPosY(nearestGridPeace.GetComponentInChildren<ShipPart>().GetPosY());

                piece.transform.position = pos;
                piece.GetComponent<FollowSphere>().partIncluded = true;
            }
            else
            {

                if (!piece.GetComponent<FollowSphere>().partIncluded)
                {
                    return null;
                }

                if (containerGrid[piece.GetComponentInChildren<ShipPart>().GetPosX(), piece.GetComponentInChildren<ShipPart>().GetPosY()] != null)
                {

                    GameObject oldGridPiece = containerGrid[piece.GetComponentInChildren<ShipPart>().GetPosX(), piece.GetComponentInChildren<ShipPart>().GetPosY()];
                    piece.transform.position = new Vector3 (oldGridPiece.transform.position.x, oldGridPiece.transform.position.y, -2);

                    piece.GetComponent<FollowSphere>().partIncluded = true;

                } else
                {
                    Debug.Log("ContainerGrid on " + piece.GetComponentInChildren<ShipPart>().GetPosX() + "/" + piece.GetComponentInChildren<ShipPart>().GetPosY() +  " is null!"); 
                }
            }
 


        }

        

        return piece;
    }

    public Vector3 SnapTranslation(Vector3 transl)
    {
        return SnapTranslation(transl.x, transl.y, transl.z);
    }


    public Vector2 ConvertToLocalCoordinates(int posX, int posY)
    {
        Vector2 coord = new Vector3(posX, posY);
        return coord;
    }






    public void UpdatePartTypes()
    {

        for (int x = 0; x < gridColumns; x++)
        {
            for (int y = 0; y < gridRows; y++)
            {


                if (containerGrid[x, y] != null)
                {
                    NetworkActions.Instance.CmdSetPartTypes(x, y, containerGrid[x, y].GetComponentInChildren<ShipPart>().getID());
                    Debug.Log("Ahoi - " + x + "/" + y + " - " + containerGrid[x, y].GetComponentInChildren<ShipPart>().getID());
                }
                else
                {

                    Debug.Log("Grid " + "[" + x + "/" + y + "] null");
                    NetworkActions.Instance.CmdSetPartTypes(x, y, 4);
                }
            }
        }
    }

    public void IncludePieceV02(GameObject piece)
    {


        //Get previous part position
        int oldPosX = piece.GetComponentInChildren<ShipPart>().GetPosX();
        int oldPosY = piece.GetComponentInChildren<ShipPart>().GetPosY();



        //returns null if part has no place on field
        if (SnapTranslationToNearest(piece) != null)
        {

            //If piece moved
            if (piece.GetComponentInChildren<ShipPart>().GetPosX() != oldPosX || piece.GetComponentInChildren<ShipPart>().GetPosY() != oldPosY)
            {

                NetworkActions.Instance.CmdSetPartTypes(oldPosX, oldPosY, 4);

                NetworkActions.Instance.CmdSetPartTypes(piece.GetComponentInChildren<ShipPart>().GetPosX(), piece.GetComponentInChildren<ShipPart>().GetPosY(), piece.GetComponentInChildren<ShipPart>().getID());
            }
            // Debug.Log(piece.GetComponentInChildren<ShipPart>().getID());
        }

    }

    public IEnumerator delayedSwitch(int x, int y, int id)
    {
        yield return null;


        NetworkActions.Instance.CmdSetPartTypes(x, y, id);

    }

    // Update is called once per frame
    void Update()
    {
        //UpdatePartTypes();
    }

    public bool CanFly()
    {
        bool hasThruster = false;
        bool hasBatteries = false;

        for (int x = 0; x < gridColumns; x++)
        {
            for (int y = 0; y < gridRows; y++)
            {

                if (hasThruster && hasBatteries)
                {
                    break;
                }

                if (containerGrid[x, y] != null)
                {
                    if (containerGrid[x, y].GetComponentInChildren<ShipPart>().getID() == 0)
                    {
                        hasBatteries = true;
                    }

                    if (containerGrid[x, y].GetComponentInChildren<ShipPart>().getID() == 1)
                    {
                        hasThruster = true;
                    }
                }
            }
        }


        return (hasThruster && hasBatteries);
    }



    public Vector3 SnapTranslation(float transX, float transY, float transZ)
    {
        //get nearest Field: Snap to whole numbers, translate afterwards;

        Vector3 newPosition = new Vector3();



        newPosition.x = Mathf.Round(transX + 1f) - 0.5f;
        newPosition.y = Mathf.Round(transY + 1f) - 0.5f;
        newPosition.z = transZ;

        //clamp values;
        //newPosition.x = (newPosition.x > (gridColumns - 1) / 2f) ? ((gridColumns - 1) / 2f) : newPosition.x;
        //newPosition.x = (newPosition.x < (gridColumns - 1) / -2f) ? ((gridColumns - 1) / -2f) : newPosition.x;
        //newPosition.y = (newPosition.y > (gridRows - 1) / 2f) ? ((gridRows - 1) / 2f) : newPosition.y;
        //newPosition.y = (newPosition.y < (gridRows - 1) / -2f) ? ((gridRows - 1) / -2f) : newPosition.y;
        newPosition.x = (newPosition.x > (gridColumns - 1)) ? ((gridColumns - 1)) : newPosition.x;
        newPosition.x = (newPosition.x < (0)) ? 0 : newPosition.x;
        newPosition.y = (newPosition.y > (gridRows - 1)) ? ((gridRows - 1)) : newPosition.y;
        newPosition.y = (newPosition.y < 0) ? 0 : newPosition.y;

        // Debug.Log("Snap to: " + newPosition);

        return newPosition;
    }

    //public void IncludePiece(GameObject piece)
    //{


    //    Debug.Log("Try to include " + piece.ToString());
    //    //Check if piece is already in Play


    //    RemovePiece(piece);


    //    //Update Position if necessary
    //    Vector3 tempPosition = SnapTranslation(piece.transform.localPosition);

    //    //Set GridCoordinates of Piece
    //    Vector2 gridPos = ConvertToGridCoordinates(tempPosition);

    //    //Debug.Log(gridPos);

    //    //If position is already occupied, stay in current position

    //    if (!IsEmpty(gridPos))
    //    {
    //        gridPos = new Vector2(piece.GetComponentInChildren<ShipPart>().GetPosX(), piece.GetComponentInChildren<ShipPart>().GetPosY());
    //    }



    //    piece.GetComponentInChildren<ShipPart>().SetPosX((int)gridPos.x);
    //    piece.GetComponentInChildren<ShipPart>().SetPosY((int)gridPos.y);

    //    piece.transform.localPosition = ConvertToLocalCoordinates(piece.GetComponentInChildren<ShipPart>().GetPosX(), piece.GetComponentInChildren<ShipPart>().GetPosY());
    //    piece.transform.Translate(0, 0, -1);



    //    AddPiece(piece, piece.GetComponentInChildren<ShipPart>().GetPosX(), piece.GetComponentInChildren<ShipPart>().GetPosY());
    //    // Debug.Log(GetPiece(5, 2));



    //    //Include if not already here
    //}

    public Vector2 ConvertToGridCoordinates(Vector3 pos)
    {
        //Vector2 coord = new Vector2(pos.x + ((gridColumns-1)/2f), pos.y + ((gridRows-1)/2f));
        // Debug.Log("Position: " + pos);


        Vector2 coord = new Vector2(pos.x, pos.y);

        //Debug.Log("New Coordinates: " + coord);

        //clamp values;
        coord.x = (coord.x > gridColumns - 1) ? (gridColumns - 1) : coord.x;
        coord.x = (coord.x < 0) ? 0 : coord.x;
        coord.y = (coord.y > gridRows - 1) ? (gridRows - 1) : coord.y;
        coord.y = (coord.y < 0) ? 0 : coord.y;

        //Debug.Log("Clamped Coordinates: " + coord);

        return coord;
    }


}

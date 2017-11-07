using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayingGrid : MonoBehaviour
{

    //Twodimensional Array in which the GameObject on that position is stored;

    public GameObject[,] grid;

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
                if (grid[x, y] != null)
                {
                    partList.AddLast(grid[x, y]);
                }
            }
        }

        return partList;
    }

    // Use this for initialization
    void Start()
    {
        grid = new GameObject[gridColumns, gridRows];
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

    //Add piece to grid. If There is already a piece there do nothing and return existing piece
    public GameObject AddPiece(GameObject newPiece, int posX, int posY)
    {

        if (grid[posX, posY] == null || grid[posX, posY].GetComponentInChildren<ShipPart>().getID() == 4)
        {


            grid[posX, posY] = newPiece;
            // newPiece.SetActive(true);
            //Parts.Instance.ResetConnections();
            return newPiece;
        }
        else
        {
            //Parts.Instance.ResetConnections();
            return grid[posX, posY];
        }


    }


    public void ClearPiece(int x, int y)
    {

        NetworkActions.Instance.CmdSetPartTypes(x, y, 4);
    }

    public void RemovePiece(int x, int y)
    {

        if (grid[x, y] != null)
        {

            GameObject oldPiece = grid[x, y];
            oldPiece.SetActive(false);
            GameObject.Destroy(oldPiece);
            grid[x, y] = null;
        }
    }

    public void RemovePieceNonDestructive(int x, int y)
    {
        if (grid[x, y] != null)
        {

            GameObject oldPiece = grid[x, y];
            // oldPiece.SetActive(false);
            // GameObject.Destroy(oldPiece);
            grid[x, y] = null;
        }
    }

    public bool RemovePieceNonDestructive(GameObject oldPiece)
    {
        grid[oldPiece.GetComponentInChildren<ShipPart>().GetPosX(), oldPiece.GetComponentInChildren<ShipPart>().GetPosY()] = null;
        //oldPiece.SetActive(false);
        //GameObject.Destroy(oldPiece);

        return true;

    }

    //Remove Piece. Return piece when found, return null when not
    public GameObject RemovePiece(GameObject oldPiece)
    {

        //NetworkActions.Instance.CmdSetPartTypes(oldPiece.GetComponentInChildren<ShipPart>().GetPosX(), oldPiece.GetComponentInChildren<ShipPart>().GetPosY(), 4);
        grid[oldPiece.GetComponentInChildren<ShipPart>().GetPosX(), oldPiece.GetComponentInChildren<ShipPart>().GetPosY()] = null;
        oldPiece.SetActive(false);
        GameObject.Destroy(oldPiece);


        //return oldPiece;



        //for (int x = 0; x < gridColumns; x++)
        //{
        //    for (int y = 0; y < gridRows; y++)
        //    {
        //        if (grid[x, y] != null && grid[x, y].Equals(oldPiece))
        //        {

        //            //NetworkActions.Instance.CmdSetPartTypes(x, y, 4);
        //            Debug.Log("Remove Piece" + x + y);
        //            grid[x, y].SetActive(false);
        //            grid[x, y] = null;

        //            //Parts.Instance.ResetConnections();
        //            // Debug.Log("Removepiece" + oldPiece.ToString());
        //            return oldPiece;
        //        }
        //    }
        //}
        //Parts.Instance.ResetConnections();
        return oldPiece;
    }



    //Check if piece is in game;
    public bool IsPieceInPlay(GameObject piece)
    {



        for (int x = 0; x < gridColumns; x++)
        {
            for (int y = 0; y < gridRows; y++)
            {
                if (grid[x, y] != null && grid[x, y].Equals(piece))
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

        if (grid[posX, posY] == null)  // || grid[posX,posY].GetComponent<ShipPart>().getID() == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsEmptyOrSame(int posX, int posY, GameObject comparator)
    {
        if (grid[posX, posY] == null || grid[posX, posY].GetComponent<ShipPart>().getID() == 4)
        {
            return true;
        }
        else
        {

            Debug.Log(grid[posX, posY].GetComponent<ShipPart>().GetPosX() + " " + comparator.GetComponent<ShipPart>().GetPosX() + " " + grid[posX, posY].GetComponent<ShipPart>().GetPosY() + " " + comparator.GetComponent<ShipPart>().GetPosY());
            if (grid[posX, posY].GetComponent<ShipPart>().GetPosX() == comparator.GetComponent<ShipPart>().GetPosX() && grid[posX, posY].GetComponent<ShipPart>().GetPosY() == comparator.GetComponent<ShipPart>().GetPosY())
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
                if (grid[x, y] != null)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public GameObject GetPiece(int posX, int posY)
    {
        return grid[posX, posY];
    }


    //public Vector3 SnapTranslationToNearest(Vector3 pos)
    //{
    //    return SnapTranslationToNearest(pos.x, pos.y, pos.z);
    //}

    public GameObject SnapTranslationToNearest(GameObject piece)
    {


        float nearest = 100000f;
        Vector3 pos = Vector3.zero;
        //int OldPosX = piece.GetComponentInChildren<ShipPart>().GetPosX();
        //int OldPosY = piece.GetComponentInChildren<ShipPart>().GetPosY();

        foreach (GameObject gridPiece in SwitchShipParts.Instance.shipContainers)
        {

            //Debug.Log((gridPiece.transform.position.x - piece.transform.position.x) + "/" + (gridPiece.transform.position.y - piece.transform.position.y) + "/" + (gridPiece.transform.position.z - piece.transform.position.z));
            float distanceSq = Mathf.Pow(gridPiece.transform.position.x - piece.transform.position.x, 2) + Mathf.Pow(gridPiece.transform.position.y - piece.transform.position.y, 2) + Mathf.Pow(gridPiece.transform.position.z - piece.transform.position.z, 2);
            //Debug.Log(gridPiece.GetComponentInChildren<ShipPart>().GetPosX() + "/" + gridPiece.GetComponentInChildren<ShipPart>().GetPosY() + "     " + distanceSq);



            if (distanceSq < nearest)
            {
                //Debug.Log(piece);
                //  Debug.Log(nearest + "is empty?");
                if (IsEmptyOrSame(gridPiece.GetComponentInChildren<ShipPart>().GetPosX(), gridPiece.GetComponentInChildren<ShipPart>().GetPosY(), piece.transform.GetChild(0).gameObject))


                {
                    nearest = distanceSq;

                    //  Debug.Log(nearest);

                    pos = new Vector3(gridPiece.transform.position.x, gridPiece.transform.position.y, -2);//gridPiece.transform.position.z);

                    piece.GetComponentInChildren<ShipPart>().SetPosX(gridPiece.GetComponentInChildren<ShipPart>().GetPosX());
                    piece.GetComponentInChildren<ShipPart>().SetPosY(gridPiece.GetComponentInChildren<ShipPart>().GetPosY());


                }
                else
                {
                    if (GetPiece(gridPiece.GetComponentInChildren<ShipPart>().GetPosX(), gridPiece.GetComponentInChildren<ShipPart>().GetPosY()) != piece)
                    {

                        nearest = distanceSq;

                        Debug.Log("FIELD NOT EMPTY LOOSER");

                        pos = new Vector3(gridPiece.transform.position.x, gridPiece.transform.position.y, gridPiece.transform.position.z);

                        piece.GetComponentInChildren<ShipPart>().SetPosX(gridPiece.GetComponentInChildren<ShipPart>().GetPosX());
                        piece.GetComponentInChildren<ShipPart>().SetPosY(gridPiece.GetComponentInChildren<ShipPart>().GetPosY());
                    }

                }
            }
        }

        //if (GetPiece(gridPiece.GetComponentInChildren<ShipPart>().GetPosX(), gridPiece.GetComponentInChildren<ShipPart>().GetPosY()) != piece)

        // NetworkActions.Instance.CmdSetPartTypes(OldPosX, OldPosY, 4);



        //  AddPiece(piece, piece.GetComponentInChildren<ShipPart>().GetPosX(), piece.GetComponentInChildren<ShipPart>().GetPosY());

        piece.transform.position = pos;

        //Debug.Log("Snap to " + piece.GetComponentInChildren<ShipPart>().GetPosX() + "/" + piece.GetComponentInChildren<ShipPart>().GetPosY());

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

                // Debug.Log(grid[x, y].GetComponentInChildren<ShipPart>().GetPosX() + "   " + grid[x, y].GetComponentInChildren<ShipPart>().GetPosY() + "   " + grid[x, y].GetComponentInChildren<ShipPart>().getID());


                if (grid[x, y] != null)
                {
                    NetworkActions.Instance.CmdSetPartTypes(x, y, grid[x, y].GetComponentInChildren<ShipPart>().getID());
                    Debug.Log("Ahoi - " + x + "/" + y + " - " + grid[x, y].GetComponentInChildren<ShipPart>().getID());
                }
                else
                {

                    Debug.Log("Grid " + "[" + x + "/" + y + "] null");
                    NetworkActions.Instance.CmdSetPartTypes(x, y, 4);
                }
            }
        }

        //foreach (GameObject gridPiece in SwitchShipParts.Instance.shipContainers)
        //{
        //    NetworkActions.Instance.CmdSetPartTypes(gridPiece.GetComponentInChildren<ShipPart>().GetPosX(), gridPiece.GetComponentInChildren<ShipPart>().GetPosY(), gridPiece.GetComponentInChildren<ShipPart>().getID());
        //}
    }

    public void IncludePieceV02(GameObject piece)
    {

        //ClearPiece(piece.GetComponentInChildren<ShipPart>().GetPosX(), piece.GetComponentInChildren<ShipPart>().GetPosY());



        //RemovePiece(piece);

        // Debug.Log(grid[GetComponentInChildren<ShipPart>().GetPosX(), GetComponentInChildren<ShipPart>().GetPosY()].GetComponentInChildren<ShipPart>().GetPosX() + "   " + grid[GetComponentInChildren<ShipPart>().GetPosX(), GetComponentInChildren<ShipPart>().GetPosY()].GetComponentInChildren<ShipPart>().GetPosY() + "   " + grid[GetComponentInChildren<ShipPart>().GetPosX(), GetComponentInChildren<ShipPart>().GetPosY()].GetComponentInChildren<ShipPart>().getID());


        NetworkActions.Instance.CmdSetPartTypes(piece.GetComponentInChildren<ShipPart>().GetPosX(), piece.GetComponentInChildren<ShipPart>().GetPosY(), 4);

        // StartCoroutine(delayedSwitch(piece.GetComponentInChildren<ShipPart>().GetPosX(), piece.GetComponentInChildren<ShipPart>().GetPosY(), 4));

        SnapTranslationToNearest(piece);

        NetworkActions.Instance.CmdSetPartTypes(piece.GetComponentInChildren<ShipPart>().GetPosX(), piece.GetComponentInChildren<ShipPart>().GetPosY(), piece.GetComponentInChildren<ShipPart>().getID());
        // Debug.Log(piece.GetComponentInChildren<ShipPart>().getID());

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

                if (grid[x, y] != null)
                {
                    if (grid[x, y].GetComponentInChildren<ShipPart>().getID() == 0)
                    {
                        hasBatteries = true;
                    }

                    if (grid[x, y].GetComponentInChildren<ShipPart>().getID() == 1)
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

    public void IncludePiece(GameObject piece)
    {


        Debug.Log("Try to include " + piece.ToString());
        //Check if piece is already in Play


        RemovePiece(piece);


        //Update Position if necessary
        Vector3 tempPosition = SnapTranslation(piece.transform.localPosition);

        //Set GridCoordinates of Piece
        Vector2 gridPos = ConvertToGridCoordinates(tempPosition);

        //Debug.Log(gridPos);

        //If position is already occupied, stay in current position

        if (!IsEmpty(gridPos))
        {
            gridPos = new Vector2(piece.GetComponentInChildren<ShipPart>().GetPosX(), piece.GetComponentInChildren<ShipPart>().GetPosY());
        }



        piece.GetComponentInChildren<ShipPart>().SetPosX((int)gridPos.x);
        piece.GetComponentInChildren<ShipPart>().SetPosY((int)gridPos.y);

        piece.transform.localPosition = ConvertToLocalCoordinates(piece.GetComponentInChildren<ShipPart>().GetPosX(), piece.GetComponentInChildren<ShipPart>().GetPosY());
        piece.transform.Translate(0, 0, -1);



        AddPiece(piece, piece.GetComponentInChildren<ShipPart>().GetPosX(), piece.GetComponentInChildren<ShipPart>().GetPosY());
        // Debug.Log(GetPiece(5, 2));



        //Include if not already here
    }

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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayingGrid : MonoBehaviour {

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
                if (grid[x,y] != null)
                {
                    partList.AddLast(grid[x, y]);
                }
            }
        }

        return partList;
    }

    // Use this for initialization
    void Start () {
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

        if (grid[posX, posY] == null)
        {
            grid[posX, posY] = newPiece;
            // newPiece.SetActive(true);
            //Parts.Instance.ResetConnections();
            return newPiece;
        } else
        {
            //Parts.Instance.ResetConnections();
            return grid[posX, posY];
        }

        
    }


    //Remove Piece. Return piece when found, return null when not
    public GameObject RemovePiece(GameObject oldPiece)
    {
       
        

        for (int x = 0; x < gridColumns; x++)
        {
            for (int y = 0; y<gridRows; y++)
            {
                if (grid[x, y] != null && grid[x,y].Equals(oldPiece))
                {
                    grid[x, y] = null;
                    // grid[x, y].SetActive(false);
                    //Parts.Instance.ResetConnections();
                    Debug.Log("Removepiece" + oldPiece.ToString());
                    return oldPiece;
                }
            }
        }
        //Parts.Instance.ResetConnections();
        return null;
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

        if (grid[posX,posY] == null) // || grid[posX,posY].GetComponent<ShipPart>().getID() == 4)
        {
            return true;
        } else
        {
            return false;
        }
    }


    public bool IsEmpty(Vector2 pos)
    {
        return IsEmpty((int) pos.x, (int) pos.y);
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

    public Vector3 SnapTranslation (float transX, float transY, float transZ)
    {
        //get nearest Field: Snap to whole numbers, translate afterwards;

        Vector3 newPosition = new Vector3();



        newPosition.x = Mathf.Round(transX +1f) - 0.5f;
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


    //public Vector3 SnapTranslationToNearest(Vector3 pos)
    //{
    //    return SnapTranslationToNearest(pos.x, pos.y, pos.z);
    //}

    public GameObject SnapTranslationToNearest (GameObject piece)
    {


        float nearest = float.MaxValue;
        Vector3 pos = Vector3.zero;


        foreach (GameObject gridPiece in SwitchShipParts.Instance.shipContainers)
        {
            float distanceSq = Mathf.Pow(gridPiece.transform.position.x - piece.transform.position.x, 2) + Mathf.Pow(gridPiece.transform.position.y - piece.transform.position.y, 2) + Mathf.Pow(gridPiece.transform.position.z - piece.transform.position.z, 2);


            if (distanceSq < nearest)
            {

                if (IsEmpty(new Vector2(gridPiece.GetComponentInChildren<ShipPart>().GetPosX(), gridPiece.GetComponentInChildren<ShipPart>().GetPosY()))) {
                    nearest = distanceSq;
                    pos = new Vector3(gridPiece.transform.position.x, gridPiece.transform.position.y, gridPiece.transform.position.z);

                    piece.GetComponentInChildren<ShipPart>().SetPosX(gridPiece.GetComponentInChildren<ShipPart>().GetPosX());
                    piece.GetComponentInChildren<ShipPart>().SetPosY(gridPiece.GetComponentInChildren<ShipPart>().GetPosY());

                    
                }
            }
        }

        AddPiece(piece, piece.GetComponentInChildren<ShipPart>().GetPosX(), piece.GetComponentInChildren<ShipPart>().GetPosY());

        piece.transform.position = pos;

        return piece;
    }

    public Vector3 SnapTranslation (Vector3 transl)
    {
        return SnapTranslation(transl.x, transl.y, transl.z);
    }

    public Vector2 ConvertToGridCoordinates(Vector3 pos)
    {
        //Vector2 coord = new Vector2(pos.x + ((gridColumns-1)/2f), pos.y + ((gridRows-1)/2f));
       // Debug.Log("Position: " + pos);


        Vector2 coord = new Vector2(pos.x, pos.y );

        //Debug.Log("New Coordinates: " + coord);

        //clamp values;
        coord.x = (coord.x > gridColumns - 1) ? (gridColumns - 1) : coord.x;
        coord.x = (coord.x < 0) ? 0 : coord.x;
        coord.y = (coord.y > gridRows - 1) ? (gridRows - 1) : coord.y;
        coord.y = (coord.y < 0) ? 0 : coord.y;

        //Debug.Log("Clamped Coordinates: " + coord);

        return coord;
    }

    public Vector2 ConvertToLocalCoordinates(int posX, int posY)
    {
        Vector2 coord = new Vector3(posX, posY);
        return coord;
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
                    if (grid[x,y].GetComponentInChildren<ShipPart>().getID() == 0)
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


    public void IncludePieceV02(GameObject piece)
    {


        Debug.Log("Try to include " + piece.ToString());
        //Check if piece is already in Play


        RemovePiece(piece);


        //Update Position if necessary
         SnapTranslationToNearest(piece);

        //Set GridCoordinates of Piece
       // Vector2 gridPos = ConvertToGridCoordinates(tempPosition);

        //Debug.Log(gridPos);

        //If position is already occupied, stay in current position

        //if (!IsEmpty(gridPos))
        //{
        //    gridPos = new Vector2(piece.GetComponentInChildren<ShipPart>().GetPosX(), piece.GetComponentInChildren<ShipPart>().GetPosY());
        //}



        //piece.GetComponentInChildren<ShipPart>().SetPosX((int)gridPos.x);
        //piece.GetComponentInChildren<ShipPart>().SetPosY((int)gridPos.y);

        //piece.transform.localPosition = ConvertToLocalCoordinates(piece.GetComponentInChildren<ShipPart>().GetPosX(), piece.GetComponentInChildren<ShipPart>().GetPosY());
        //piece.transform.Translate(0, 0, -1);



        //AddPiece(piece, piece.GetComponentInChildren<ShipPart>().GetPosX(), piece.GetComponentInChildren<ShipPart>().GetPosY());
        // Debug.Log(GetPiece(5, 2));



        //Include if not already here
    }

    // Update is called once per frame
    void Update () {
        
	}
}

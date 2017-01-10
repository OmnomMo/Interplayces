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
        CreateDebugGrid();
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

        return false; ;
    }



    public bool IsEmpty(int posX, int posY)
    {
        //Debug.Log("posX: " + posX + "    posY: " + posY);

        if (grid[posX,posY] == null)
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

    // Update is called once per frame
    void Update () {
        
	}
}

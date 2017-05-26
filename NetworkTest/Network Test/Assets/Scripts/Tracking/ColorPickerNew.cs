using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickerNew : MonoBehaviour {

    public GameObject overlayImage;
    Texture2D overlayTexture;
    WebCamTexture tex;

    Rect[,] fields;

    Color[,][] pickedColors;
    public Color[,] averageColors;

    public int nRows;
	public int nCols;


    // Use this for initialization
    void Start () {

        if (WebcamManager.Instance.hasWebcam)
        {

            tex = WebcamManager.Instance.tex;

            Debug.Log("Texture: " + tex.width + "/" + tex.height);
            overlayTexture = new Texture2D(tex.width, tex.height);
            overlayImage.GetComponent<RawImage>().texture = overlayTexture;

            fields = new Rect[nCols, nRows];

            pickedColors = new Color[nCols, nRows][];
            averageColors = new Color[nCols, nRows];
            // cubes = new GameObject[nFields];

            float imageProportion = (float)tex.height / (float)tex.width;

            for (int i = 0; i < nCols; i++)
            {
                for (int j = 0; j < nRows; j++)
                {
                    fields[i, j] = new Rect(i * tex.width / nCols + 10, j * tex.height / nRows + 20, tex.width / 12, tex.height / 8);

                    //Debug.Log("Rect " + i + "/" + j + ": " + fields[i, j].x + "/" + fields[i, j].y + " -- " + fields[i, j].width + "/" + fields[i, j].height);
                }
            }

            


            //Scale Image plane
            GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x, GetComponent<RectTransform>().localScale.y * imageProportion, GetComponent<RectTransform>().localScale.z);
            overlayImage.GetComponent<RectTransform>().localScale = new Vector3(overlayImage.GetComponent<RectTransform>().localScale.x, overlayImage.GetComponent<RectTransform>().localScale.y * imageProportion, overlayImage.GetComponent<RectTransform>().localScale.z);

            GetComponent<RawImage>().texture = tex;

        }

    }


	void Update () {
        if (WebcamManager.Instance.hasWebcam)
        {

       

            if (!tex.isPlaying)
            {
                tex.Play();
            }

            for (int i = 0; i < nCols; i++)
            {
                for (int j = 0; j < nRows; j++)
                {



                    //Debug.Log ("AVG Color for field " + i + "/" + j  + ": " +  getCubeColor (averageColors [i, j]));
                    pickedColors[i, j] = tex.GetPixels((int)fields[i, j].x, (int)fields[i, j].y, (int)fields[i, j].width, (int)fields[i, j].height);
                    averageColors[i, j] = getAverage(pickedColors[i, j]);
                }
            }
            
            //printDetectedColorConfiguration ();

            overlayTexture.Apply();
        }
      
    }


	private void printDetectedColorConfiguration() {
        //Check if Pixel is on field, if yes, fill

        for (int i = 0; i < nCols; i++)
        {
            for (int j = 0; j < nRows; j++)
            {

                Color[] cols = new Color[(int)fields[i, j].width * (int)fields[i, j].height];
                Color col = (averageColors[i, j]);



                for (int a = 0; a < cols.Length; a++)
                {
                    cols[a] = col;
                }

                //for (int x = (int)fields[i, j].x; x < (int)fields[i, j].x + (int)fields[i, j].width; x++)
                //{
                //    for (int y = (int)fields[i, j].y; x < (int)fields[i, j].y + (int)fields[i, j].height; y++)
                //    {
                //        overlayTexture.SetPixel(x, y, col);
                //    }
                //}



                overlayTexture.SetPixels((int)fields[i, j].x, (int)fields[i, j].y, (int)fields[i, j].width, (int)fields[i, j].height, cols);

                Debug.Log(col);

            }
        }
        



	}


    private Color getAverage(Color[] colors)
    {
        int n = 0;
        Vector3 sum = new Vector3();

        foreach (Color c in colors)
        {
            n++;
            sum.x += c.r;
            sum.y += c.g;
            sum.z += c.b;
        }

        return new Color(sum.x / (float)n, sum.y / (float)n, sum.z / (float)n);
    }


    //private Color getCubeColor (Color c)
    //{

    //    Debug.Log(c.ToString());

    //    //red
    //    if (c.r > c.g * 1.2 && c.r > c.b * 1.2)
    //    {
    //        return Color.red;
    //    }

    //    //green
    //    if (c.g > c.r /** 1.2*/ && c.g > c.b /** 1.2*/ && c.r < 0.25 && c.g < 0.3)
    //    {
    //        return Color.green;
    //    }

    //    //blue
    //    if (c.b > c.g * 1.2 && c.b > c.r * 1.2)
    //    {
    //        return Color.blue;
    //    }

    //    //yellow
    //    if (c.r > c.b * 1.5f && c.g > c.b * 1.5f && c.b < 0.25)
    //    {
    //        return Color.yellow;
    //    }

    //    return Color.white;
    //}
}


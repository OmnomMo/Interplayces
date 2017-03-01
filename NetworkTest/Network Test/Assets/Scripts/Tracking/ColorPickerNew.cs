using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickerNew : MonoBehaviour {

    public GameObject overlayImage;
    Texture2D overlayTexture;
    WebCamTexture tex;

    Rect[,] fields;

    Color[][] pickedColors;
    public Color[,] averageColors;

    public int nRows;
	public int nCols;


    // Use this for initialization
    void Start () {


        tex = WebcamManager.Instance.tex;
    
       
        overlayTexture = new Texture2D(tex.width, tex.height);
        overlayImage.GetComponent<RawImage>().texture = overlayTexture;

		fields = new Rect[nCols, nRows];

		pickedColors = new Color[nRows + nCols][];
		averageColors = new Color[nCols, nRows];
       // cubes = new GameObject[nFields];


		for (int i = 0; i < nCols; i++) {
			for (int j = 0; j < nRows; j++) {
				fields[i,j] = new Rect(j*tex.width/7+10, i*tex.height / 5+20, tex.width / 12, tex.height / 8);
			}
		}

        float imageProportion = (float)tex.height / (float)tex.width;


        //Scale Image plane
        GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x, GetComponent<RectTransform>().localScale.y * imageProportion, GetComponent<RectTransform>().localScale.z);
        overlayImage.GetComponent<RectTransform>().localScale = new Vector3(overlayImage.GetComponent<RectTransform>().localScale.x, overlayImage.GetComponent<RectTransform>().localScale.y * imageProportion, overlayImage.GetComponent<RectTransform>().localScale.z);

        GetComponent<RawImage>().texture = tex;

    }


	void Update () {
        
        if (!tex.isPlaying)
        {
            tex.Play();
        }

		for (int i = 0; i < nCols; i++) {
			for (int j = 0; j < nRows; j++) {

                

				//Debug.Log ("AVG Color for field " + i + "/" + j  + ": " +  getCubeColor (averageColors [i, j]));
				pickedColors[i+j] = tex.GetPixels((int)fields[i,j].x, (int)fields[i,j].y, (int)fields[i,j].width, (int)fields[i,j].height);
				averageColors[i,j] = getAverage(pickedColors[i+j]);
			}
        }  

		//printDetectedColorConfiguration ();
      
    }


	private void printDetectedColorConfiguration() {
		//Check if Pixel is on field, if yes, fill
		for (int x = 0; x < tex.width; x++) {
			for (int y = 0; y < tex.height; y++) {

				for (int i = 0; i < nCols; i++) {
					for (int j = 0; j < nRows; j++) {

						if (fields[i,j].Contains(new Vector2(x,y))) {
							overlayTexture.SetPixel(x, y, getCubeColor (averageColors [i,j]));
						}
					}
				}
			}
		}

		overlayTexture.Apply();

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


    private Color getCubeColor (Color c)
    {
        //red
        if (c.r > c.g * 1.2 && c.r > c.b * 1.2) {
            return Color.red;
        }

        //green
        if (c.g > c.r /** 1.2*/ && c.g > c.b /** 1.2*/) {
            return Color.green;
        }

        //blue
        if (c.b > c.g * 1.2 && c.b > c.r * 1.2) {
            return Color.blue;
        }

		//yellow
		if (c.r > c.b && c.g > c.b) {
			return Color.yellow;
		}

        return Color.white;
    }
}


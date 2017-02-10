using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour {


    enum cubeColors { Red, Blue, Green, none}

    public GameObject overlayImage;
    Texture2D overlayTexture;
    WebCamTexture tex;

    Rect[] fields;

    Color[][] pickedColors;
    Color[] averageColors;
    public GameObject[] cubes;

    int nFields;

    public Material matDefault;
    public Material matRed;
    public Material matGreen;
    public Material matBlue;

    //Color[] colors01;
    //Color colors01Average;
    //Color[] colors02;
    //Color colors02Average;
    //Color[] colors03;
    //Color colors03Average;


    // Use this for initialization
    void Start () {

        

        tex = new WebCamTexture(WebCamTexture.devices[0].name);
        tex.Play();
        overlayTexture = new Texture2D(tex.width, tex.height);
        overlayImage.GetComponent<RawImage>().texture = overlayTexture;

        //define number of fields and create arrays
        nFields = 4;
        fields = new Rect[nFields];
        pickedColors = new Color[nFields][];
        averageColors = new Color[nFields];
       // cubes = new GameObject[nFields];


        //Define Fields where averages are  to be picked
        fields[0] = new Rect(tex.width/5, tex.height / 5, tex.width / 5, tex.height / 5);

        fields[1] = new Rect(3 * tex.width / 5, tex.height / 5, tex.width / 5, tex.height / 5);

        fields[2] = new Rect(tex.width / 5, 3 * tex.height / 5, tex.width / 5, tex.height / 5);

        fields[3] = new Rect(3*tex.width / 5, 3*tex.height / 5, tex.width / 5, tex.height / 5);
        


        float imageProportion = (float)tex.height / (float)tex.width;


        //Scale Image plane
        GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x, GetComponent<RectTransform>().localScale.y * imageProportion, GetComponent<RectTransform>().localScale.z);
        overlayImage.GetComponent<RectTransform>().localScale = new Vector3(overlayImage.GetComponent<RectTransform>().localScale.x, overlayImage.GetComponent<RectTransform>().localScale.y * imageProportion, overlayImage.GetComponent<RectTransform>().localScale.z);

        GetComponent<RawImage>().texture = tex;

        

    }

    private void SetCubeColor(GameObject cube, cubeColors color)
    {
        if (color == cubeColors.Red)
        {
            cube.GetComponent<Renderer>().material = matRed;
        }

        if (color == cubeColors.Blue)
        {
            cube.GetComponent<Renderer>().material = matBlue;
        }

        if (color == cubeColors.Green)
        {
            cube.GetComponent<Renderer>().material = matGreen;
        }

        if (color == cubeColors.none)
        {
            cube.GetComponent<Renderer>().material = matDefault;
        }
    }

	// Update is called once per frame
	void Update () {
        Debug.Log(tex.height + "/" + tex.width);



        for (int i = 0; i < nFields; i++)
        {
           pickedColors[i] = tex.GetPixels((int)fields[i].x, (int)fields[i].y, (int)fields[i].width, (int)fields[i].height);
           averageColors[i] = getAverage(pickedColors[i]);
        }
        
       

        //Check if Pixel is on field, if yes, fill
        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {


                for (int i = 0; i < nFields; i++)
                {
                    if (fields[i].Contains(new Vector2(x,y)))
                    {
                        overlayTexture.SetPixel(x, y, averageColors[i]);
                    }
                }

  
            }
        }

        overlayTexture.Apply();

        for (int i = 0; i < nFields; i++)
        {
            SetCubeColor(cubes[i], getCubeColor(averageColors[i]));
        }

        Debug.Log(averageColors[2]);

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


    private cubeColors getCubeColor (Color c)
    {
        //red
        if (c.r > c.g * 1.2 && c.r > c.b * 1.2)
        {
            return cubeColors.Red;
        }

        //blue
        if (c.g > c.r * 1.2 && c.g > c.b * 1.2)
        {
            return cubeColors.Green;
        }

        //green
        if (c.b > c.g * 1.2 && c.b > c.r * 1.2)
        {
            return cubeColors.Blue;
        }




        return cubeColors.none;
    }
    
}


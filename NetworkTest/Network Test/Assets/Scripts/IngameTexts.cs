using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using System.IO;

[System.Serializable]
public class IngameTexts : MonoBehaviour {


    public TextContainer allTexts;
    public string sourceTextPath;
    public Hashtable textHashtable;

    private static IngameTexts instance;
    public static IngameTexts Instance
    {
        get { return instance; }
    }


    void Awake()
    {
        instance = this;
    }


    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);

        LoadedObjectManager.Instance.AddPersistenObject(gameObject);

        textHashtable = new Hashtable();
        ReadFromFile();
        AddTextsToHashTable();
	}
	
    public void ReadFromFile()
    {

        StreamReader reader = new StreamReader(Application.streamingAssetsPath + sourceTextPath);

        string json = UnEscapeChars(reader.ReadToEnd());

        if (json != "") {

            JsonUtility.FromJsonOverwrite(json, allTexts);
            reader.Close();

           // Debug.Log(json);
        }
        //this = JsonUtility.FromJsonOverwrite()
        //Debug.Log(sourceText.ToString());
    }

    public void AddTextsToHashTable()
    {
        textHashtable.Add("lowEnergyTooltipText", allTexts.lowEnergyTooltipText);
        textHashtable.Add("lowHealthTooltipText", allTexts.lowHealthTooltipText);
        textHashtable.Add("allPlanetsFountTooltipText", allTexts.allPlanetsFountTooltipText);
        textHashtable.Add("energyPickupInfoText", allTexts.energyPickupInfoText);
        textHashtable.Add("alienShipCaughtText", allTexts.alienShipCaughtText);
        textHashtable.Add("allAlienShipsCaughtText", allTexts.allAlienShipsCaughtText);
        textHashtable.Add("buildingHelp1", allTexts.buildingHelp1);
        textHashtable.Add("tutorial1_CheckBoxExplanation", allTexts.tutorial1_CheckBoxExplanation);
        textHashtable.Add("tutorial2_Welcome", allTexts.tutorial2_Welcome);
        textHashtable.Add("tutorial3_TestRegionExplanation", allTexts.tutorial3_TestRegionExplanation);
        textHashtable.Add("tutorial4_ThrustSliderExplanation", allTexts.tutorial4_ThrustSliderExplanation);
        textHashtable.Add("tutorial5_MovementExplanation", allTexts.tutorial5_MovementExplanation);
        textHashtable.Add("tutorial6_MovementExplanationHelp", allTexts.tutorial6_MovementExplanationHelp);
        textHashtable.Add("tutorial7_WellDone", allTexts.tutorial7_WellDone);
        textHashtable.Add("tutorial8_NavigatorRoleExplanation", allTexts.tutorial8_NavigatorRoleExplanation);
        textHashtable.Add("tutorial9_ScanSliderExplanation", allTexts.tutorial9_ScanSliderExplanation);
        textHashtable.Add("tutorial10_MoonTargetExplanation", allTexts.tutorial10_MoonTargetExplanation);
        textHashtable.Add("tutorial11_MoonTargetHelp", allTexts.tutorial11_MoonTargetHelp);
        textHashtable.Add("tutorial12_ShieldSliderExplnanation", allTexts.tutorial12_ShieldSliderExplnanation);
        textHashtable.Add("tutorial13_EndExplanation1", allTexts.tutorial13_EndExplanation1);
        textHashtable.Add("tutorial14_WayOutExplanation", allTexts.tutorial14_WayOutExplanation);
        textHashtable.Add("level2_objectives_allPlanetsStartText", allTexts.level2_objectives_allPlanetsStartText);
        textHashtable.Add("level2_objectives_VenusHelpText", allTexts.level2_objectives_VenusHelpText);
        textHashtable.Add("level2_objectives_MercuryHelpText", allTexts.level2_objectives_MercuryHelpText);
        textHashtable.Add("level2_objectives_EarthHelpText", allTexts.level2_objectives_EarthHelpText);
        textHashtable.Add("level2_objectives_MarsHelpText", allTexts.level2_objectives_MarsHelpText);
        textHashtable.Add("level3_objectives_pickupObjectiveStart", allTexts.level3_objectives_pickupObjectiveStart);
        textHashtable.Add("level3_objectives_pickupObjectiveHelp", allTexts.level3_objectives_pickupObjectiveHelp);
        textHashtable.Add("level3_objectives_ReturnObjectiveStart", allTexts.level3_objectives_ReturnObjectiveStart);
        textHashtable.Add("level4_objectives_allAreasStartText", allTexts.level4_objectives_allAreasStartText);
        textHashtable.Add("level4_objectives_UranusHelpText", allTexts.level4_objectives_UranusHelpText);
        textHashtable.Add("level4_objectives_NeptunHelpText", allTexts.level4_objectives_NeptunHelpText);
        textHashtable.Add("level4_objectives_PlutoHelpText", allTexts.level4_objectives_PlutoHelpText);
        textHashtable.Add("level2_descriptions1_mercury", allTexts.level2_descriptions1_mercury);
        textHashtable.Add("level2_descriptions2_venus", allTexts.level2_descriptions2_venus);
        textHashtable.Add("level2_descriptions3_earth", allTexts.level2_descriptions3_earth);

        textHashtable.Add("level2_descriptions4_moon", allTexts.level2_descriptions4_moon);
        textHashtable.Add("level2_descriptions5_mars", allTexts.level2_descriptions5_mars);
        textHashtable.Add("level2_descriptions6_deimos", allTexts.level2_descriptions6_deimos);
        textHashtable.Add("level2_descriptions7_phobos", allTexts.level2_descriptions7_phobos);
        textHashtable.Add("level3_descriptions1_jupiter", allTexts.level3_descriptions1_jupiter);
        textHashtable.Add("level3_descriptions2_io", allTexts.level3_descriptions2_io);
        textHashtable.Add("level3_descriptions3_callisto", allTexts.level3_descriptions3_callisto);
        textHashtable.Add("level3_descriptions4_ganymede", allTexts.level3_descriptions4_ganymede);
        textHashtable.Add("level3_descriptions5_europa", allTexts.level3_descriptions5_europa);
        textHashtable.Add("level3_descriptions6_saturn", allTexts.level3_descriptions6_saturn);
        textHashtable.Add("level3_descriptions7_dione", allTexts.level3_descriptions7_dione);
        textHashtable.Add("level3_descriptions8_rhea", allTexts.level3_descriptions8_rhea);
        textHashtable.Add("level3_descriptions9_titan", allTexts.level3_descriptions9_titan);
        textHashtable.Add("level4_descriptions1_uranus", allTexts.level4_descriptions1_uranus);
        textHashtable.Add("level4_descriptions2_oberon", allTexts.level4_descriptions2_oberon);
        textHashtable.Add("level4_descriptions3_titania", allTexts.level4_descriptions3_titania);
        textHashtable.Add("level4_descriptions4_umbriel", allTexts.level4_descriptions4_umbriel);
        textHashtable.Add("level4_descriptions5_neptun", allTexts.level4_descriptions5_neptun);
        textHashtable.Add("level4_descriptions6_triton", allTexts.level4_descriptions6_triton);
        textHashtable.Add("level4_descriptions7_pluto", allTexts.level4_descriptions7_pluto);
        textHashtable.Add("level4_descriptions8_charon", allTexts.level4_descriptions8_charon);

    }

    public string UnEscapeChars(string json)
    {
        json = json.Replace("&Auml;", "Ä");
        json = json.Replace("&auml;", "ä");
        json = json.Replace("&Uuml;", "Ü");
        json = json.Replace("&uuml;", "ü");
        json = json.Replace("&Ouml;", "Ö");
        json = json.Replace("&ouml;", "ö");
        json = json.Replace("&szlig;", "ß");
        json = json.Replace("&deg;", "°");




        return json;
    }

    public void WriteToFile()
    {
        string json = JsonUtility.ToJson(allTexts);
        Debug.Log(json);
        StreamWriter writer = new StreamWriter(Application.streamingAssetsPath + sourceTextPath, true);
        writer.Write(json);
        writer.Close();
        //AssetDatabase.ImportAsset(sourceTextPath);


        
    }


	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("u"))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {

                ReadFromFile();

                
                // do something
            }
        }

        if (Input.GetKeyDown("i"))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {

                WriteToFile();


                // do something
            }
        }

    }
}

[System.Serializable]
public class TextContainer
{

   


    [Header("GeneralTooltips")]

    [Multiline]
    public string lowEnergyTooltipText;

    [Multiline]
    public string lowHealthTooltipText;

    [Multiline]
    public string allPlanetsFountTooltipText;

    [Multiline]
    public string energyPickupInfoText;

    [Multiline]
    public string alienShipCaughtText;

    [Multiline]
    public string allAlienShipsCaughtText;


    [Header("Tooltips Building Phase")]

    [Multiline]
    public string buildingHelp1;


    [Header("Objectives Tutorial")]

    [Multiline]
    public string tutorial1_CheckBoxExplanation;

    [Multiline]
    public string tutorial2_Welcome;

    [Multiline]
    public string tutorial3_TestRegionExplanation;

    [Multiline]
    public string tutorial4_ThrustSliderExplanation;

    [Multiline]
    public string tutorial5_MovementExplanation;


    [Multiline]
    public string tutorial6_MovementExplanationHelp;


    [Multiline]
    public string tutorial7_WellDone;


    [Multiline]
    public string tutorial8_NavigatorRoleExplanation;


    [Multiline]
    public string tutorial9_ScanSliderExplanation;

    [Multiline]
    public string tutorial10_MoonTargetExplanation;

    [Multiline]
    public string tutorial11_MoonTargetHelp;

    [Multiline]
    public string tutorial12_ShieldSliderExplnanation;

    [Multiline]
    public string tutorial13_EndExplanation1;

    [Multiline]
    public string tutorial14_WayOutExplanation;



    [Header("Objectives Level2")]

    [Multiline]
    public string level2_objectives_allPlanetsStartText;

    [Multiline]
    public string level2_objectives_VenusHelpText;

    [Multiline]
    public string level2_objectives_MercuryHelpText;

    [Multiline]
    public string level2_objectives_EarthHelpText;

    [Multiline]
    public string level2_objectives_MarsHelpText;

    [Header("Objectives level3")]

    [Multiline]
    public string level3_objectives_pickupObjectiveStart;

    [Multiline]
    public string level3_objectives_pickupObjectiveHelp;

    [Multiline]
    public string level3_objectives_ReturnObjectiveStart;


    [Header("Objectives Level4")]

    [Multiline]
    public string level4_objectives_allAreasStartText;

    [Multiline]
    public string level4_objectives_UranusHelpText;

    [Multiline]
    public string level4_objectives_NeptunHelpText;

    [Multiline]
    public string level4_objectives_PlutoHelpText;


    [Header("Descriptions Level2")]

    [Multiline]
    public string level2_descriptions1_mercury;

    [Multiline]
    public string level2_descriptions2_venus;

    [Multiline]
    public string level2_descriptions3_earth;

    [Multiline]
    public string level2_descriptions4_moon;

    [Multiline]
    public string level2_descriptions5_mars;

    [Multiline]
    public string level2_descriptions6_deimos;

    [Multiline]
    public string level2_descriptions7_phobos;



    [Header("Descriptions Level3")]


    [Multiline]
    public string level3_descriptions1_jupiter;
    [Multiline]
    public string level3_descriptions2_io;
    [Multiline]
    public string level3_descriptions3_callisto;
    [Multiline]
    public string level3_descriptions4_ganymede;
    [Multiline]
    public string level3_descriptions5_europa;
    [Multiline]
    public string level3_descriptions6_saturn;
    [Multiline]
    public string level3_descriptions7_dione;
    [Multiline]
    public string level3_descriptions8_rhea;
    [Multiline]
    public string level3_descriptions9_titan;



    [Header("Descriptions Level4")]

    [Multiline]
    public string level4_descriptions1_uranus;

    [Multiline]
    public string level4_descriptions2_oberon;
    [Multiline]
    public string level4_descriptions3_titania;

    [Multiline]
    public string level4_descriptions4_umbriel;

    [Multiline]
    public string level4_descriptions5_neptun;

    [Multiline]
    public string level4_descriptions6_triton;

    [Multiline]
    public string level4_descriptions7_pluto;

    [Multiline]
    public string level4_descriptions8_charon;

}


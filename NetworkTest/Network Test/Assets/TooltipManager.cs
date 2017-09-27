using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : MonoBehaviour {
    
    LinkedList<Tooltip> tooltipQueue;

    public Tooltip tooltipPrefab;


    public int maxTooltipSlots;

    public int GetFreeTooltipSpaces;


    private static TooltipManager instance;
    public static TooltipManager Instance
    {
        get { return instance; }
    }


    

    public void AddTooltipToQueue(Tooltip newTt)
    {

        tooltipQueue.AddLast(newTt);
        UpdateTooltips();

        Debug.Log("Addd Tooltip: " + newTt.ttText + " Queue length: " + tooltipQueue.Count);




    }



    

    public Tooltip NewTooltip(string text, Sprite image = null, float ttDuration = 0, Transform target = null, bool visibleCaptain = true, bool visibleNavigator = true)
    {

        Tooltip newTooltip = GameObject.Instantiate(tooltipPrefab);
        newTooltip.GetComponent<RectTransform>().SetParent(this.GetComponent<RectTransform>(), false);
        newTooltip.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        newTooltip.GetComponent<RectTransform>().offsetMin = Vector2.zero;

        if (ttDuration == 0)
        {
            newTooltip.timed = false;
        } else
        {
            newTooltip.timed = true;
            newTooltip.duration = ttDuration;
        
        }
        

        newTooltip.gameObject.SetActive(false);
        newTooltip.SetTTVisibility(visibleCaptain, visibleNavigator);
        newTooltip.SetTTArrowTarget(target);

        if (IngameTexts.Instance.textHashtable[text] != null)
        {
            newTooltip.SetContent(IngameTexts.Instance.textHashtable[text].ToString(), image);
        } else
        {
            newTooltip.SetContent(text, image);
        }

        AddTooltipToQueue(newTooltip);

        return newTooltip;
    }


    public void RemoveToolTipFromQueue(Tooltip removedTt)
    {
        tooltipQueue.Remove(removedTt);
        UpdateTooltips();
       // removedTt.GetComponent<RectTransform>().localPosition = Vector3.zero;
    }

    public void UpdateTooltips()
    {
        int n = 0;
        
        foreach (Tooltip tt in tooltipQueue)
        {

            if (Application.isEditor)
            {
                tt.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, n * -250 * (GetMainGameViewSize().x / 1920f));
            } else
            {
                tt.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, n * -250 );
            }
            if (n< maxTooltipSlots)
            {
                if (!tt.gameObject.activeInHierarchy)
                {
                    //initialize Tooltip
                    tt.gameObject.SetActive(true);
                    tt.Show();

                    if (tt.timed)
                    {
                        tt.startTime = Time.time;
                    }

                }



                

            }

            n++;


            
        }
    }

    public static Vector2 GetMainGameViewSize()
    {
        System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
        System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        System.Object Res = GetSizeOfMainGameView.Invoke(null, null);
        return (Vector2)Res;
    }

    void Awake()
    {
        tooltipQueue = new LinkedList<Tooltip>();
        instance = this;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Check all tooltips for expiration
        foreach (Tooltip tt in tooltipQueue)
        {
            if (tt.timed && tt.startTime != 0)
            {
                if (Time.time - tt.duration > tt.startTime)
                {
                    tt.Hide();

                    //breaks out of foreach, because modifying list while iterating over it causes problems
                    //Will check list the next frame anyways
                    break;
                }
            }
        }
	}
}

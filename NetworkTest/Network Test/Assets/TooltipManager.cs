using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : MonoBehaviour {

    LinkedList<Tooltip> allTooltips;
    LinkedList<Tooltip> activeTooltips;

    public int maxTooltipSlots;

    public int GetFreeTooltipSpaces;


    private static TooltipManager instance;
    public static TooltipManager Instance
    {
        get { return instance; }
    }

    public void AddTooltip(Tooltip newTt)
    {
        allTooltips.AddLast(newTt);
    }
    

    public void AddTooltipToQueue(Tooltip newTt)
    {
        if (activeTooltips.Count < maxTooltipSlots)
        {
            activeTooltips.AddLast(newTt);

            Debug.Log("Addd Tooltip: " + newTt.ttText);
            ReArramgeTooltips();
        } else
        {
            //TODO: Implement Queue
        }
    }

    public void RemoveToolTipFromQueue(Tooltip removedTt)
    {
        activeTooltips.Remove(removedTt);
       // removedTt.GetComponent<RectTransform>().localPosition = Vector3.zero;
    }

    public void ReArramgeTooltips()
    {
        int n = 0;
        
        foreach (Tooltip tt in activeTooltips)
        {
            //    tt.GetComponent<RectTransform>().anchorMin.y = 0.7f;
            //    tt.GetComponent<RectTransform>().anchorMax.y = 0.92f;
            //Debug.Log("nActiveTooltips: " + activeTooltips.Count);
            //tt.GetComponent<RectTransform>().Translate(new Vector3(0, n * -75, 0));
            tt.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, n * -250 * (GetMainGameViewSize().x/1920f));
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
        allTooltips = new LinkedList<Tooltip>();
        activeTooltips = new LinkedList<Tooltip>();
        instance = this;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

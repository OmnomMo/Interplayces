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
            ReArramgeTooltips();
        } else
        {
            //TODO: Implement Queue
        }
    }

    public void RemoveToolTipFromQueue(Tooltip removedTt)
    {
        activeTooltips.Remove(removedTt);
    }

    public void ReArramgeTooltips()
    {
        int n = 0;

        foreach (Tooltip tt in activeTooltips)
        {
            //    tt.GetComponent<RectTransform>().anchorMin.y = 0.7f;
            //    tt.GetComponent<RectTransform>().anchorMax.y = 0.92f;

            tt.GetComponent<RectTransform>().Translate(new Vector3(0, n * -55, 0));
            n++;
        }
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

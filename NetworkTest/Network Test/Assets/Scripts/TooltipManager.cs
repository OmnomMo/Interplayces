using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


public class TooltipManager : MonoBehaviour {
    
    public LinkedList<Tooltip> tooltipQueue;

    public Tooltip tooltipPrefab;
    public Tooltip objectiveTooltipPrefab;


    public AudioClip infoClip;
    public AudioClip scanClip;
    public AudioClip warningClip;
    public AudioClip successClip;

    public AudioSource infoSource;
    public AudioSource scanSource;
    public AudioSource warningSource;
    public AudioSource successSource;

    public int maxTooltipSlots;

    public int GetFreeTooltipSpaces;


    private static TooltipManager instance;
    public static TooltipManager Instance
    {
        get { return instance; }
    }


    // Use this for initialization
    void Start()
    {
        InvokeRepeating("UpdateTooltips", 0.5f, 0.1f);

        infoSource = AddAudioAudioSource(infoClip, false, false, 0.3f);
        scanSource = AddAudioAudioSource(scanClip, false, false, 0.3f);
        warningSource = AddAudioAudioSource(warningClip, false, false, 0.3f);
        successSource = AddAudioAudioSource(successClip, false, false, 0.3f);

    }

    public AudioSource AddAudioAudioSource(AudioClip clip, bool loop, bool playAwake, float vol, float pitch = 1)
    {
        AudioSource newAudio = this.gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.pitch = pitch;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        return newAudio;
    }

    public void AddTooltipToQueue(Tooltip newTt)
    {

        //if (newTt == null)
        //{
        //   // Debug.Log("NullTooltip Added");
        //} else
        //{
        //   // Debug.Log("Added tt to queue: " + newTt.ttText);
        //}

        tooltipQueue.AddLast(newTt);
     //   UpdateTooltips();

//Debug.Log("Addd Tooltip: " + newTt.ttText + " Queue length: " + tooltipQueue.Count);




    }


    public Tooltip NewConfirmTooltip(Objective objective, string text, Sprite image = null, bool visibleCaptain = true, bool visibleNavigator = true)
    {
        Tooltip newTooltip = GameObject.Instantiate(objectiveTooltipPrefab);
        newTooltip.GetComponent<RectTransform>().SetParent(this.GetComponent<RectTransform>(), false);
        newTooltip.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        newTooltip.GetComponent<RectTransform>().offsetMin = Vector2.zero;


       // EditorApplication.isPaused = true;
        newTooltip.timed = false;



        newTooltip.gameObject.SetActive(false);
        newTooltip.SetTTVisibility(visibleCaptain, visibleNavigator);
        newTooltip.GetComponent<Tooltip_ConfirmButton>().confirmObjective = objective;
        //newTooltip.SetTTArrowTarget(target);

        if (IngameTexts.Instance.textHashtable[text] != null)
        {
            newTooltip.SetContent(IngameTexts.Instance.textHashtable[text].ToString(), image);
        }
        else
        {
            newTooltip.SetContent(text, image);
        }

        


        AddTooltipToQueue(newTooltip);

        return newTooltip;
    }


    public Tooltip NewTooltip(string text, Sprite image = null, float ttDuration = 0, Transform target = null, bool visibleCaptain = true, bool visibleNavigator = true)
    {

        //Debug.Log("Create new Tooltip " + text + "|" + ttDuration);

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
        // UpdateTooltips();

       // Debug.Log("Remove Tooltip from queue: " + removedTt.ttText);

       Destroy(removedTt.gameObject);
       // removedTt.GetComponent<RectTransform>().localPosition = Vector3.zero;
    }

    
    IEnumerator DelayedUpdateTooltips()
    {
        yield return null;
        UpdateTooltips();
    }


    public void UpdateTooltips()
    {

        //Debug.Log("updateTooltips");
        //Debug.Log("\nNumber of Tooltips In queue: " + tooltipQueue.Count);

        //EditorApplication.isPaused = true;

        int n = 0;

        bool removedTooltip = false;


        foreach (Tooltip tt in tooltipQueue)
        {
            if (tt == null)
            {
                //Debug.Log("Tooltip is null");
                tooltipQueue.Remove(tt);
                removedTooltip = true;
                break;
            }
        }

        foreach (Tooltip tt in tooltipQueue)
        {
            if (tt != null)
            {


                if (Application.isEditor)
                {
#if UNITY_EDITOR
                    tt.GetComponent<RectTransform>().anchoredPosition = new Vector2(-50 * (GetMainGameViewSize().x / 1920f), n * -200 * (GetMainGameViewSize().x / 1920f));
#endif
                }
                else
                {
                    tt.GetComponent<RectTransform>().anchoredPosition = new Vector2(-50, n * -200);
                }

                tt.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.8f, 0.8f);

                if (n < maxTooltipSlots)
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

                   // Debug.Log("TooltipText " + n + ": " + tt.ttText);




                    n++;

                }
            } else
            {
                Debug.Log("Destroyed Tooltip in queue!");
            }

            if (removedTooltip)
            {
                UpdateTooltips();
            }

            

            
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

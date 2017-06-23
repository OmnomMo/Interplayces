/*******************************************************
 * Copyright (C) 2017 Doron Weiss  - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of unity license.
 * 
 * See https://abnormalcreativity.wixsite.com/home for more info
 *******************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Dweiss {
	[System.Serializable]
	public class Settings : ASettings {

        [Header("--DarkBackground--")]

        public float absoluteFactorThreshold = 1.1f;
        public float specificFactorThreshold = 1.8f;

        public float lightBackgroundThreshold = 1.7f;
        public float darkBackgroundThreshold = 1.4f;

        public float darkBG_MagentaRed_threshhold = 0.7f;

        public float darkBG_Magenta_BgtG = 1.15f;
        public float darkBG_Magenta_BgtG2 = 0.1f;

        public float darkBG_blue_BgtG = 1.1f;
        public float darkBG_blue_BgtR = 1.3f;

        public float darkBG_blue_Bgt = 0.6f;

        public float darkBG_green_GgtR = 1f;
        public float darkBG_green_GgtB = 1f;




        [Header("--Magenta--")]

        public float Magenta_RgtG = 1.2f;
        public float Magenta_BgtG = 1f;
        public float Magenta_Rgt = 0.4f;
        public float Magenta_Bgt = 0.35f;


        [Header("--Red--")]

        public float Red_RgtG = 1.2f;
        public float Red_RgtB = 1.2f;
        public float Red_Rgt = 0f;

        [Header("--Green--")]

        public float Green_GgtR = 1.1f;
        public float Green_GgtB = 1.1f;
        public float Green_Rst = 0.38f;
        public float Green_Gst = 0.48f;
        public float Green_Gbt = 0f;

        [Header("--Blue--")]

        public float Blue_BgtG = 1.2f;
        public float Blue_BgtR = 1.2f;
        public float Blue_Bgt = 0f;



        //[Header("--Simple primitive example--")]
        //public bool show = true;

        //public float colorChangeCycle = 1.5f;
        //public int qualitySettings = 2;


        //[Header("--Lists and arrays--")]
        //public float[] arrayExample;

        //public List<float> listExample;


        //[Header("--Enum and Class--")]
        //public EnumExample enumExample;
        //public MySpecialClassExample classExample;



        #region Enums and classes for serialization

        public enum EnumExample {
            Enum1,Enum2
        }


        [System.Serializable]
        public class MySpecialClassExample
        {
            public string txt = "abcd";
        }
        #endregion


        private new void Awake() {
			base.Awake ();
            SetupSingelton();
        }


        #region  Singelton
        public static Settings _instance;
        public static Settings Instance { get { return _instance; } }
        private void SetupSingelton()
        {
            if (_instance != null)
            {
                Debug.LogError("Error in settings. Multiple singeltons exists: " + _instance.name + " and now " + this.name);
            }
            else
            {
                _instance = this;
            }
        }
        #endregion



    }
}
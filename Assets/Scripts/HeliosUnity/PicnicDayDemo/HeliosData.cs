using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HeliosUnity.PicnicDayDemo
{

    public class HeliosData
    {

        public HeliosDataType HeliosDataType = HeliosDataType.NONE;
        private float maxDataValue = 0.0f;
        private List<float> data = new List<float>();
        public List<float> normalizedData = new List<float>();
        
        public Gradient dataGradient;
        private GradientColorKey[] colorKeys;
        private GradientAlphaKey[] alphaKeys;

        

        public HeliosData(List<float> data, List<float> normalizedData, float maxDataValue)
        {
            this.maxDataValue = maxDataValue;
            this.data = data;
            this.normalizedData = normalizedData;
            dataGradient = new Gradient();
        }

        public void SetDataType(HeliosDataType dataType)
        {
            this.HeliosDataType = dataType;

            switch(dataType)
            {
                case HeliosDataType.LEST:
                    SetGradientHot();
                    break;

                case HeliosDataType.PSYNTH:
                    SetGradientGreen();
                    break;

                case HeliosDataType.WUE:
                    SetGradientParula();
                    break;
            }

            dataGradient.SetKeys(colorKeys, alphaKeys);
            
        }

        private void SetGradientHot()
        {
            colorKeys = new GradientColorKey[5];

            colorKeys[0].color = new Color(0, 0, 0);
            colorKeys[0].time = 0.0f;

            colorKeys[1].color = new Color(0.5f, 0, 0.5f);
            colorKeys[1].time = 0.25f;

            colorKeys[2].color = new Color(1, 0, 0);
            colorKeys[2].time = 0.5f;
            
            colorKeys[3].color = new Color(1, 0.5f, 0);
            colorKeys[3].time = 0.75f;

            colorKeys[4].color = new Color(1, 1, 0);
            colorKeys[4].time = 1f;

            alphaKeys = new GradientAlphaKey[5];

            alphaKeys[0].alpha = 1.0f;
            alphaKeys[0].time = 0.0f;

            alphaKeys[1].alpha = 1.0f;
            alphaKeys[1].time = 0.25f;

            alphaKeys[2].alpha = 1.0f;
            alphaKeys[2].time = 0.5f;

            alphaKeys[3].alpha = 1.0f;
            alphaKeys[3].time = 0.75f;

            alphaKeys[4].alpha = 1.0f;
            alphaKeys[4].time = 1.0f;


        }

        private void SetGradientGreen()
        {
            colorKeys = new GradientColorKey[5];

            colorKeys[0].color = new Color(0.414f, 0.292f, 0.082f);
            colorKeys[0].time = 0.0f;

            colorKeys[1].color = new Color(0.730f, 0.679f, 0.160f);
            colorKeys[1].time = 0.25f;

            colorKeys[2].color = new Color(0.730f, 0.890f, 0.0742f);
            colorKeys[2].time = 0.5f;

            colorKeys[3].color = new Color(0.378f ,0.89f ,0.0742f); ;
            colorKeys[3].time = 0.75f;

            colorKeys[4].color = Color.green;
            colorKeys[4].time = 1f;

            alphaKeys = new GradientAlphaKey[5];

            alphaKeys[0].alpha = 1.0f;
            alphaKeys[0].time = 0.0f;

            alphaKeys[1].alpha = 1.0f;
            alphaKeys[1].time = 0.25f;

            alphaKeys[2].alpha = 1.0f;
            alphaKeys[2].time = 0.5f;

            alphaKeys[3].alpha = 1.0f;
            alphaKeys[3].time = 0.75f;

            alphaKeys[4].alpha = 1.0f;
            alphaKeys[4].time = 1.0f;
        }

        private void SetGradientParula()
        {
            colorKeys = new GradientColorKey[4];

            colorKeys[0].color = new Color(0, 0, 0.5f);
            colorKeys[0].time = 0.0f;

            colorKeys[1].color = new Color(0, 0.6f, 0.6f);
            colorKeys[1].time = 0.5f;

            colorKeys[2].color = new Color(0.855f, 0.647f, 0.126f);
            colorKeys[2].time = 0.7f;

            colorKeys[3].color = new Color(1, 1, 0);
            colorKeys[3].time = 1f;

            alphaKeys = new GradientAlphaKey[4];

            alphaKeys[0].alpha = 1.0f;
            alphaKeys[0].time = 0.0f;

            alphaKeys[1].alpha = 1.0f;
            alphaKeys[1].time = 0.4f;

            alphaKeys[2].alpha = 1.0f;
            alphaKeys[2].time = 0.7f;

            alphaKeys[3].alpha = 1.0f;
            alphaKeys[3].time = 1.0f;

        }


    }

}
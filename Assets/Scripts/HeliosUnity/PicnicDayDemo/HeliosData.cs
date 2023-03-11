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
        private List<float> normalizedData = new List<float>();
        
        public Gradient dataGradient;
        private GradientColorKey[] colorKey;
        private GradientAlphaKey[] alphaKey;

        public HeliosData(List<float> data, List<float> normalizedData, float maxDataValue)
        {
            this.maxDataValue = maxDataValue;
            this.data = data;
            this.normalizedData = normalizedData;
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
        }

        private void SetGradientHot()
        {
            colorKey = new GradientColorKey[5];

            colorKey[0].color = new Color(0, 0, 0);
            colorKey[0].time = 0.0f;

            colorKey[1].color = new Color(128, 0, 128);
            colorKey[1].time = 0.25f;

            colorKey[2].color = new Color(255, 0, 0);
            colorKey[2].time = 0.5f;

            colorKey[3].color = new Color(255, 128, 0);
            colorKey[3].time = 0.75f;

            colorKey[4].color = new Color(255, 255, 0);
            colorKey[4].time = 1f;

            alphaKey = new GradientAlphaKey[5];

            alphaKey[0].alpha = 1.0f;
            alphaKey[0].time = 0.0f;

            alphaKey[1].alpha = 1.0f;
            alphaKey[1].time = 0.25f;

            alphaKey[2].alpha = 1.0f;
            alphaKey[2].time = 0.5f;

            alphaKey[3].alpha = 1.0f;
            alphaKey[3].time = 0.75f;

            alphaKey[4].alpha = 1.0f;
            alphaKey[4].time = 1.0f;


        }

        private void SetGradientGreen()
        {
            colorKey = new GradientColorKey[5];

            colorKey[0].color = new Color(106, 75, 21);
            colorKey[0].time = 0.0f;

            colorKey[1].color = new Color(187, 174, 41);
            colorKey[1].time = 0.25f;

            colorKey[2].color = new Color(188, 228, 19);
            colorKey[2].time = 0.5f;

            colorKey[3].color = new Color(97,228,19); ;
            colorKey[3].time = 0.75f;

            colorKey[4].color = Color.green;
            colorKey[4].time = 1f;

            alphaKey = new GradientAlphaKey[5];

            alphaKey[0].alpha = 1.0f;
            alphaKey[0].time = 0.0f;

            alphaKey[1].alpha = 1.0f;
            alphaKey[1].time = 0.25f;

            alphaKey[2].alpha = 1.0f;
            alphaKey[2].time = 0.5f;

            alphaKey[3].alpha = 1.0f;
            alphaKey[3].time = 0.75f;

            alphaKey[4].alpha = 1.0f;
            alphaKey[4].time = 1.0f;
        }

        private void SetGradientParula()
        {
            colorKey = new GradientColorKey[4];

            colorKey[0].color = new Color(0, 0, 128);
            colorKey[0].time = 0.0f;

            colorKey[1].color = new Color(0, 152, 152);
            colorKey[1].time = 0.5f;

            colorKey[2].color = new Color(218, 166, 32);
            colorKey[2].time = 0.7f;

            colorKey[3].color = new Color(255, 255, 0);
            colorKey[3].time = 1f;

            alphaKey = new GradientAlphaKey[4];

            alphaKey[0].alpha = 1.0f;
            alphaKey[0].time = 0.0f;

            alphaKey[1].alpha = 1.0f;
            alphaKey[1].time = 0.4f;

            alphaKey[2].alpha = 1.0f;
            alphaKey[2].time = 0.7f;

            alphaKey[3].alpha = 1.0f;
            alphaKey[3].time = 1.0f;

        }


    }

}
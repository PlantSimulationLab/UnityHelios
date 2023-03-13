using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace HeliosUnity.PicnicDayDemo
{

    public enum HeliosDataType
    {
        NONE = 0,
        WUE = 1,
        PSYNTH = 2,
        LEST = 3
    }

    public class HeliosDataLoader
    {

        public static HeliosData Load(string dataFilePath, HeliosDataType dataType)
        {
            if (dataFilePath == "")
                Debug.LogError("Empty data file path.");
            if (dataType == HeliosDataType.NONE)
                Debug.LogError("Invalid Helios Data Type");

            StreamReader reader = new StreamReader(dataFilePath);

            var data = new List<float>();
            float maxDataValue = 0;

            while (!reader.EndOfStream)
            {


                var line = reader.ReadLine();
                var dataValue = float.Parse(line);

                if (dataValue > maxDataValue)
                {
                    maxDataValue = dataValue;
                }

                data.Add(dataValue);
            }

            var normalizedData = NormalizeData(data, maxDataValue);
            var rescaledData = RescaleData(normalizedData);
            var heliosData = new HeliosData(data, rescaledData, maxDataValue);
            heliosData.SetDataType(dataType);
            return heliosData;

        }

        public static List<float> NormalizeData(List<float> data, float maxDataValue)
        {
            var normalizedData = new List<float>(data.Count);



            for (int i = 0; i < data.Count; i++)
                normalizedData.Add(data[i] / maxDataValue);

            return normalizedData;
        }

        public static List<float> RescaleData(List<float> data)
        {
            var rescaledData = new List<float>();
            var min = data.Min();
            var max = data.Max();
            var scaledMin = min / max;

            for(int i=0; i<data.Count; i++)
            {
                var normalizedValue = data[i] / max;
                var rescaledValue = (normalizedValue < scaledMin) ? scaledMin : (normalizedValue > 1) ? 1 : normalizedValue;
                rescaledData.Add(rescaledValue);
            }


            return rescaledData;
        }

    }

}

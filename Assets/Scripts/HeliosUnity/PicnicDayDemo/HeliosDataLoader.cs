using System.IO;
using System.Collections;
using System.Collections.Generic;
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


            return new HeliosData(data, NormalizeData(data, maxDataValue), maxDataValue);

        }

        public static List<float> NormalizeData(List<float> data, float maxDataValue)
        {
            var normalizedData = new List<float>(data.Count);

            for (int i = 0; i < data.Count; i++)
                normalizedData[i] = data[i] / maxDataValue;

            return normalizedData;
        }

    }

}

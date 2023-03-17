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

            return Load(reader, dataType);

        }

        public static HeliosData LoadFromResource(string dataResourcePath, HeliosDataType dataType)
        {
            var data = new List<float>();
            float maxDataValue = 0;

            var dataTextAsset = Resources.Load<TextAsset>(dataResourcePath);
            var dataList = Utilities.Resources.TextAssetToList(dataTextAsset);

            for(int i=0; i<dataList.Count; i++)
            {

                float dataValue;
                if (!float.TryParse(dataList[i], out dataValue))
                    dataValue = 0.0f;
                else
                    dataValue = float.Parse(dataList[i]); ;

                if (dataValue > maxDataValue)
                {
                    maxDataValue = dataValue;
                }

                data.Add(dataValue);
            }

            var normalizedData = NormalizeData(data, maxDataValue);
            var heliosData = new HeliosData(data, normalizedData, maxDataValue);
            heliosData.SetDataType(dataType);

            return heliosData;
        }


        public static List<float> NormalizeData(List<float> data, float maxDataValue)
        {
            var normalizedData = new List<float>(data.Count);

            for (int i = 0; i < data.Count; i++)
            {
                var normalizedValue = data[i] == 0 ? 0 : data[i] / maxDataValue;
                normalizedData.Add(normalizedValue);
            }

            return normalizedData;
        }

        public static HeliosData Load(StreamReader reader, HeliosDataType dataType)
        {
            var data = new List<float>();
            float maxDataValue = 0;

            while (!reader.EndOfStream)
            {

                var line = reader.ReadLine();
                float dataValue;
                if (!float.TryParse(line, out dataValue))
                    dataValue = 0.0f;
                else
                    dataValue = float.Parse(line); ;

                if (dataValue > maxDataValue)
                {
                    maxDataValue = dataValue;
                }

                data.Add(dataValue);
            }

            var normalizedData = NormalizeData(data, maxDataValue);
            //var rescaledData = RescaleData(normalizedData);
            var heliosData = new HeliosData(data, normalizedData, maxDataValue);
            heliosData.SetDataType(dataType);
            return heliosData;
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

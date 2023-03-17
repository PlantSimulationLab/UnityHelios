using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HeliosUnity.Utilities
{

    public class Resources
    {
        public static List<string> TextAssetToList(TextAsset ta)
        {
            var listToReturn = new List<string>();
            var arrayString = ta.text.Split('\n');
            foreach (var line in arrayString)
            {
                listToReturn.Add(line);
            }
            return listToReturn;
        }
    }

}

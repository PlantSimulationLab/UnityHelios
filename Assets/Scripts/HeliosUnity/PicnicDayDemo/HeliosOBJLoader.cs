using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dummiesman;

namespace HeliosUnity.PicnicDayDemo
{
    public class HeliosOBJLoader
    {
        public static OBJLoader loader = new OBJLoader();

        public static GameObject Load(string objFilePath)
        {

            if (!File.Exists(objFilePath))
                Debug.LogError("OBJ File does not exist");

            var loadedObject = loader.Load(objFilePath);

            return loadedObject;
        }
    }
}
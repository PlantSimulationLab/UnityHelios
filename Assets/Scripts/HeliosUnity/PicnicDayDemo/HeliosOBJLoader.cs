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

            loadedObject.transform.Rotate(new Vector3(-90f, 0, 0));

            return loadedObject;
        }

        public static GameObject Load(Stream stream)
        {
            var loadedObject = loader.Load(stream);

            loadedObject.transform.Rotate(new Vector3(-90f, 0, 0));

            return loadedObject;
        }
    }
}
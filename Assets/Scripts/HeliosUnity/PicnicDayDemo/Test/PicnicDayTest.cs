using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Dummiesman;

namespace HeliosUnity.PicnicDayDemo.Test
{

    public class PicnicDayTest : MonoBehaviour
    {
        public HeliosModel[] HeliosModels;
        OBJLoader loader;

        private void Awake()
        {
            loader = new OBJLoader();
        }

        // Start is called before the first frame update
        void Start()
        {

            for(int i=0; i<HeliosModels.Length; i++)
            {
                var dir = Path.Join(Application.streamingAssetsPath, HeliosModels[i].RootFolderPath);
                if(Directory.Exists(dir))
                {
                    var objPath = Path.Join(dir, HeliosModels[i].OBJFileName);
                    if(!File.Exists(objPath))
                    {
                        Debug.LogError("File Does not Exist: " + objPath);
                        
                    }
                    else
                    {
                        loader.Load(objPath);
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

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

        private void Awake()
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {
            var heliosObject = HeliosModelManager.Instance.LoadModel(HeliosModels[1], Vector3.zero, Quaternion.identity);

            heliosObject.ApplyData(HeliosDataType.PSYNTH);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

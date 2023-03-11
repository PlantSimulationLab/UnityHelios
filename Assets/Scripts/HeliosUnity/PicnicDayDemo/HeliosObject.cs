using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeliosUnity.PicnicDayDemo
{ 

    public class HeliosObject : MonoBehaviour
    {
        public HeliosData[] heliosDatas;
        public Transform heliosOBJ;


        public void Awake()
        {
            
        }

        public void Start()
        {
            
        }

        public void ApplyData(HeliosDataType dataType)
        {
            if(heliosDatas[(int)dataType] == null)
            {
                Debug.LogError("Datatype " + dataType.ToString() + " does not exist for Helios Object");
                return;
            }


        }

    }

}
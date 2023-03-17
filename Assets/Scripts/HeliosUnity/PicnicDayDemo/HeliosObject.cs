using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeliosUnity.PicnicDayDemo
{ 

    public class HeliosObject : MonoBehaviour
    {
        public HeliosData[] heliosDatas;
        [SerializeField] private Transform heliosOBJ;

        private MeshFilter objMeshFilter;
        private int vertexCount = 0;
        private int[] triangles = null;
        private Vector3[] vertices = null;
        private Renderer r;
        private Color currentColor;

        [Header("Data Resource Paths")]
        [SerializeField] private string psynthResourcePath;
        [SerializeField] private string wueResourcePath;
        [SerializeField] private string lestResourcePath;
        private string[] dataResourcePaths;

        #region Debug
        [Header("Debug Only")]
        [SerializeField] private bool _toRGB = false;
        [SerializeField] private bool _toWUE = false;
        [SerializeField] private bool _toPSYNTH = false;
        [SerializeField] private bool _toLI = false;
        #endregion

        public void Awake()
        {
            heliosDatas = new HeliosData[3];
            currentColor = new Color();

            BetterStreamingAssets.Initialize();

            dataResourcePaths = new string[3];
            dataResourcePaths[(int)HeliosDataType.LEST - 1] = lestResourcePath;
            dataResourcePaths[(int)HeliosDataType.WUE - 1] = wueResourcePath;
            dataResourcePaths[(int)HeliosDataType.PSYNTH - 1] = psynthResourcePath;

        }

        public void Start()
        {
            PreloadData();
        }

        private void Update()
        {
            if(_toRGB)
            {
                ResetColors();
                _toRGB = false;
            }
            if(_toWUE)
            {
                ApplyData(HeliosDataType.WUE);
                _toWUE = false;
            }
            if(_toPSYNTH)
            {
                ApplyData(HeliosDataType.PSYNTH);
                _toPSYNTH = false;
            }
            if(_toLI)
            {
                ApplyData(HeliosDataType.LEST);
                _toLI = false;
            }
        }

        public void ApplyData(HeliosDataType dataType)
        {
            if(dataResourcePaths[(int)dataType - 1] == null)
            {
                Debug.LogError("Datatype " + dataType.ToString() + " does not exist for Helios Object");
                return;
            }



            ApplyColors(dataType);

        }

        private void ApplyColors(HeliosDataType dataType)
        {
         
            for (int i = 0; i < r.sharedMaterials.Length; i++)
            {
                r.sharedMaterials[i].SetFloat("_Gradient", 1.0f);
            }

            var dataModel = heliosDatas[(int)dataType - 1];
            var colors = new Color[vertices.Length];

            for(int i=0; i<triangles.Length; i++)
            {

                try
                {
                    var normalizedValue = dataModel.normalizedData[(int)(i / 3)];
                    if (triangles[i] % 3 == 0)
                    {
                        currentColor = dataModel.dataGradient.Evaluate(normalizedValue);
                    }
                    colors[triangles[i]] = currentColor;

                }
                catch(System.Exception e)
                {
                    Debug.Log("Exception Occurred: " + e.ToString());
                }
            }

            objMeshFilter.mesh.colors = colors;

        }

        public void ResetColors()
        {
            for (int i = 0; i < r.sharedMaterials.Length; i++)
            {
                r.sharedMaterials[i].SetFloat("_Gradient", 0.0f);
            }
        }

        private void PreloadData()
        {

            objMeshFilter = heliosOBJ.GetComponentInChildren<MeshFilter>();
            triangles = objMeshFilter.mesh.triangles;
            vertices = objMeshFilter.mesh.vertices;
            vertexCount = objMeshFilter.mesh.vertexCount;
            r = objMeshFilter.GetComponentInChildren<Renderer>();

            heliosDatas[(int)HeliosDataType.LEST - 1] = HeliosDataLoader.Load(BetterStreamingAssets.OpenText(lestResourcePath + ".dat"), HeliosDataType.LEST);
            heliosDatas[(int)HeliosDataType.PSYNTH - 1] = HeliosDataLoader.Load(BetterStreamingAssets.OpenText(psynthResourcePath + ".dat"), HeliosDataType.PSYNTH);
            heliosDatas[(int)HeliosDataType.WUE - 1] = HeliosDataLoader.Load(BetterStreamingAssets.OpenText(wueResourcePath + ".dat"), HeliosDataType.WUE);

            /*
            heliosDatas[(int)HeliosDataType.LEST] = HeliosDataLoader.LoadFromResource(lestResourcePath, HeliosDataType.LEST);
            heliosDatas[(int)HeliosDataType.PSYNTH] = HeliosDataLoader.LoadFromResource(lestResourcePath, HeliosDataType.PSYNTH);
            heliosDatas[(int)HeliosDataType.WUE] = HeliosDataLoader.LoadFromResource(lestResourcePath, HeliosDataType.WUE);
            */
        }

    }

}
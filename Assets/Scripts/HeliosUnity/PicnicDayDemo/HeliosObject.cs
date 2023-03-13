using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeliosUnity.PicnicDayDemo
{ 

    public class HeliosObject : MonoBehaviour
    {
        public HeliosData[] heliosDatas;
        public Transform heliosOBJ;

        private MeshFilter objMeshFilter;
        private int vertexCount = 0;
        private int[] triangles = null;
        private Vector3[] vertices = null;

        private Renderer r;

        private Color currentColor;

        public void Awake()
        {
            heliosDatas = new HeliosData[3];
            currentColor = new Color();
        }

        public void Start()
        {
            
        }

        public void ApplyData(HeliosDataType dataType)
        {
            if(heliosDatas[(int)dataType - 1] == null)
            {
                Debug.LogError("Datatype " + dataType.ToString() + " does not exist for Helios Object");
                return;
            }

            objMeshFilter = heliosOBJ.GetComponentInChildren<MeshFilter>();
            triangles = objMeshFilter.mesh.triangles;
            vertices = objMeshFilter.mesh.vertices;
            vertexCount = objMeshFilter.mesh.vertexCount;
            r = objMeshFilter.GetComponentInChildren<Renderer>();

            ApplyColors(dataType);

        }

        private void ApplyColors(HeliosDataType dataType)
        {

            // Apply Shader
            for (int i = 0; i < r.sharedMaterials.Length; i++)
            {
                r.sharedMaterials[i].shader = Resources.Load<Shader>("VertexColor");
            }


            var dataModel = heliosDatas[(int)dataType - 1];
            var colors = new Color[vertices.Length];

            for(int i=0; i<triangles.Length; i++)
            {
                var normalizedValue = dataModel.normalizedData[(int)(i / 3)];
                if(triangles[i] % 3 == 0)
                {
                    currentColor = dataModel.dataGradient.Evaluate(normalizedValue);
                }
                colors[triangles[i]] = currentColor;
            }

            objMeshFilter.mesh.colors = colors;

        }
    }

}
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeliosUnity.Utilities;

namespace HeliosUnity.PicnicDayDemo
{

    public class HeliosModelManager : MonoBehaviourSingleton<HeliosModelManager>
    {
        [SerializeField] private GameObject heliosModelPrefab;
        private List<HeliosObject> loadedHeliosObjects;

        private void Awake()
        {
            loadedHeliosObjects = new List<HeliosObject>();
        }

        public HeliosObject LoadModel(HeliosModel model, Vector3 position, Quaternion rotation)
        {
            var heliosObject = Instantiate(heliosModelPrefab, position, rotation).GetComponent<HeliosObject>();

            if (heliosObject == null) Debug.LogError("Invalid prefab.");

            var modelRootFolder = Path.Join(Application.streamingAssetsPath, model.RootFolderPath);

            heliosObject.heliosOBJ = HeliosOBJLoader.Load(Path.Join(modelRootFolder, model.OBJFileName)).transform;
            
            heliosObject.heliosOBJ.transform.parent = heliosObject.transform;

            heliosObject.heliosDatas[(int)HeliosDataType.WUE - 1] = HeliosDataLoader.Load(Path.Join(modelRootFolder, model.WUEDatFileName), HeliosDataType.WUE);

            heliosObject.heliosDatas[(int)HeliosDataType.PSYNTH - 1] = HeliosDataLoader.Load(Path.Join(modelRootFolder, model.PSynthFileName), HeliosDataType.PSYNTH);

            heliosObject.heliosDatas[(int)HeliosDataType.LEST - 1] = HeliosDataLoader.Load(Path.Join(modelRootFolder, model.LEstFileName), HeliosDataType.LEST);

            loadedHeliosObjects.Add(heliosObject);

            return heliosObject;
            
        }
    }

}

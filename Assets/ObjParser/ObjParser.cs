﻿
using System.Linq;

namespace Obj
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;

    public static class ObjParser
    {
        public static bool logTime = false;
        public static List<List<Color>> colorList = new List<List<Color>>();
        public static List<string> dataNames = new List<string>();
        public static GameObject Parse(string path, float scale = 1, Material material = null, Material transparentMaterial = null, bool forceTangentsCalculation = false)
        {
            if (material == null) material = Resources.Load<Material>("ObjDefaulOpaque");
            if (transparentMaterial == null) transparentMaterial = Resources.Load<Material>("ObjDefaulTransparent");

            path = ProcessPath(path);

            var streamReader = GetStreamReader(path);
            if (streamReader == null) return null;

            System.Diagnostics.Stopwatch stopwatch = null;
            if (logTime)
            {
                stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();
            }
            
            DataProcess.datList.Clear();
            
            string[] datFiles = Directory.GetFiles(Path.Combine(Path.GetDirectoryName(path)), "*.dat");

            if (datFiles.Length > 0)
            {
                for (int i = 0; i < datFiles.Length; i++)
                {
                    var datStreamReader = GetStreamReader(datFiles[i]);
                    DataProcess.SortDat(datStreamReader);
                }
                
            }
            
            var modelData = ObjGeometryProcessor.ProcessStream(streamReader, scale);
            
            colorList = modelData.datColors;
            dataNames = DataProcess.parseDatList(datFiles);

            ParseMaterials(path, modelData, material, transparentMaterial);

            var result = CreateGameObjects(modelData, Path.GetFileNameWithoutExtension(path), forceTangentsCalculation);
            
            if (stopwatch != null)
            {
                stopwatch.Stop();
                Debug.Log($"Loading time = {stopwatch.Elapsed}");
            }

            return result;
        }
        
        async public static Task<GameObject> ParseAsync(string path, float scale = 1, Material material = null, Material transparentMaterial = null, bool forceTangentsCalculation = false)
        {
            if (material == null) material = Resources.Load<Material>("ObjDefaulOpaque");
            if (transparentMaterial == null) transparentMaterial = Resources.Load<Material>("ObjDefaulTransparent");

            //path = ProcessPath(path);

            var streamReader = GetStreamReader(path);
            if (streamReader == null) return null;

            System.Diagnostics.Stopwatch stopwatch = null;
            if (logTime)
            {
                stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();
            }

#if UNITY_EDITOR
            // When in editor, tasks need to be canceled on exiting play mode

            var cancellationTokenSource = new CancellationTokenSource();
            Action<UnityEditor.PlayModeStateChange> lambda = state => { if (state == UnityEditor.PlayModeStateChange.ExitingPlayMode) cancellationTokenSource.Cancel(); };
            UnityEditor.EditorApplication.playModeStateChanged += lambda;

            ModelData modelData;
            try
            {
                var datPath = Path.Combine(Path.GetDirectoryName(path), "rad_data.dat");
                var datStreamReader = GetStreamReader(datPath);
                DataProcess.SortDat(datStreamReader);
                modelData = await Task.Run(() => ObjGeometryProcessor.ProcessStream(streamReader, scale, cancellationTokenSource.Token), cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                UnityEditor.EditorApplication.playModeStateChanged -= lambda;
                cancellationTokenSource.Dispose();
                return null;
            }
            UnityEditor.EditorApplication.playModeStateChanged -= lambda;
            cancellationTokenSource.Dispose();

#else
        var modelData = await Task.Run(() => ObjGeometryProcessor.ProcessStream(streamReader, scale));
#endif

            ParseMaterials(path, modelData, material, transparentMaterial);

            var result = CreateGameObjects(modelData, Path.GetFileNameWithoutExtension(path), forceTangentsCalculation);

            if (stopwatch != null)
            {
                stopwatch.Stop();
                Debug.Log($"Loading time = {stopwatch.Elapsed}");
            }

            return result;
        }
        
        private static void ParseMaterials(string path, ModelData modelData, Material material, Material transparentMaterial)
        {
            if (string.IsNullOrEmpty(modelData.materialsLibraryName))
            {
                modelData.materials.Add("Material", new Material(material));
            }
            else
            {
                var mtlPath = Path.Combine(Path.GetDirectoryName(path), modelData.materialsLibraryName);
                var mtlStreamReader = GetStreamReader(mtlPath);
                modelData.materials = MtlProcessor.ProcessMtl(mtlStreamReader, material, transparentMaterial, Path.GetDirectoryName(path));
            }
        }

        private static string ProcessPath(string path)
        {
            if (!path.Contains(@":\"))
            {
                var replaced = Application.dataPath.Replace("/", @"\") + @"\";
                return (replaced + path).Replace(@"\\", @"\");
            }

            return path;
        }

        private static StreamReader GetStreamReader(string path)
        {
            if (!File.Exists(path))
            {
                Debug.LogError($"File does not exist; Path = {path}");
                return null;
            }

            return File.OpenText(path);
        }

        private static GameObject CreateGameObjects(ModelData modelData, string rootName, bool forceTangentsCalculation)
        {
            var root = new GameObject(Path.GetFileNameWithoutExtension(rootName));

            foreach (var meshData in modelData.meshes)
            {
                var go = new GameObject(meshData.name ?? meshData.group);
                go.name = rootName;
                
                if (!string.IsNullOrEmpty(meshData.name) && !string.IsNullOrEmpty(meshData.group) && meshData.name != meshData.group)
                {
                    Transform groupTransform = null;
                    foreach (Transform child in root.transform)
                    {
                        if (child.name == meshData.group)
                        {
                            groupTransform = child;
                            break;
                        }
                    }
                    if (groupTransform == null)
                    {
                        groupTransform = new GameObject(meshData.group).transform;
                        groupTransform.parent = root.transform;
                    }
                    go.transform.parent = groupTransform;
                }
                else
                {
                    //added this to rotate object accordingly
                    go.transform.parent = root.transform;
                    go.transform.rotation = Quaternion.Euler(new Vector3(-90,0,0));
                }

                var materials = GetMaterials(meshData.materialNames, modelData.materials);

                bool needsTangents = forceTangentsCalculation;
                if (!forceTangentsCalculation)
                {
                    foreach (var material in materials)
                    {
                        if (material.GetTexture("_BumpMap") != null || material.GetTexture("_DetailNormalMap") != null)
                        {
                            needsTangents = true;
                            break;
                        }
                    }
                }
                
                Shader shader = Resources.Load<Shader>("LitPlantShader");
                var meshFilter = go.AddComponent<MeshFilter>();
                meshFilter.sharedMesh = meshData.ToMesh(needsTangents);

                var meshRenderer = go.AddComponent<MeshRenderer>();
                
                meshRenderer.sharedMaterials = materials;
                
                for (int i = 0; i < materials.Length; i++)
                {
                    meshRenderer.materials[i].shader = shader;
                }
            }
            
            return root;
        }

        private static Material[] GetMaterials(List<string> materialNames, Dictionary<string, Material> materials)
        {
            var result = new Material[materialNames.Count];

            for (int i = 0; i < materialNames.Count; i++)
            {
                result[i] = materials[materialNames[i]];
            }

            return result;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

[CreateAssetMenu(fileName = "HeliosModel", menuName = "Helios/HeliosModelScriptableObject", order = 1)]
public class HeliosModel :ScriptableObject
{
    public string RootFolderPath;
    public string OBJFileName;
    public string WUEDatFileName;
    public string PSynthFileName;
    public string LEstFileName;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

public class XRManager : MonoBehaviour
{

    [SerializeField] private CharacterMove characterMove;
    [SerializeField] private List<GameObject> xrObjects;

    public IEnumerator StartXRCoroutine()
    {
        Debug.Log("Initializing XR...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed. Check Editor or Player log for details.");
        }
        else
        {
            Debug.Log("Starting XR...");
            XRGeneralSettings.Instance.Manager.StartSubsystems();
        }
    }

    void StopXR()
    {


        Debug.Log("Stopping XR...");

        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("XR stopped completely.");
        foreach (GameObject go in xrObjects) go.SetActive(false);
        characterMove.enabled = true;
    }
    void StartXR()
    {
        characterMove.enabled = false;
        foreach (GameObject go in xrObjects) go.SetActive(true);
        StartCoroutine(StartXRCoroutine());
    }
    
    public void SwitchModes(bool isVR)
    {
        if (isVR) StartXR(); else StopXR();
    }
}

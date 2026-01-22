using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR.Management;

public class VRToggleManager : MonoBehaviour
{

    public GameObject desktopCamera;
    public GameObject xrOrigin;

    public bool VRActive;
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        desktopCamera.SetActive(true);
        xrOrigin.SetActive(false);
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (!VRActive)
            {
                StartCoroutine(StartVR());
            }
            else {
                StartCoroutine(StopVR());
            }
        }
    }

    private System.Collections.IEnumerator StartVR()
    {
        desktopCamera.SetActive(false);
        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Failed to initialise VR! (StartVR coroutine)");
            desktopCamera.SetActive(true);
            yield break;
        }

        XRGeneralSettings.Instance.Manager.StartSubsystems();

        xrOrigin.SetActive(true);

        VRActive = true;
        player.GetComponent<FirstPersonController>().enabled = false;
        yield return null;

    }

    private System.Collections.IEnumerator StopVR()
    {
        
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();

        desktopCamera.SetActive(true);
        xrOrigin.SetActive(false);
        VRActive = false;
        yield return null;

    }
}

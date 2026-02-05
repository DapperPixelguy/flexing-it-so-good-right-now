using UnityEngine;

public class DistanceCuller : MonoBehaviour
{
    private Renderer[] renderers;

    void Awake()
    {        
        renderers = GetComponentsInChildren<Renderer>(true);
    }

    private void Start()
    {
        if (!gameObject.CompareTag("StartingRoom"))
        {
            SetRoom(false);
        }
    }

    public void SetRoom(bool state)
    {
        Debug.Log($"{name} SetRoom({state})");

        foreach (Renderer r in renderers)
            r.enabled = state;
    }
}

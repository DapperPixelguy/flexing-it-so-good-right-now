using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class TeleportBlock : MonoBehaviour
{

    public Transform teleportTarget;    

    public DistanceCuller currentRoom;
    public DistanceCuller exitRoom;
    public bool swappable;
    private RoomSwap Roomswap;

    private bool isTeleporting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (!isTeleporting && other.CompareTag("Player"))
        {
            Roomswap = other.GetComponent<RoomSwap>();
            if (Roomswap.swapping)
            {
                return;
            }
            StartCoroutine(Teleport(other));
        }
    }


    IEnumerator Teleport(Collider other)
    {
        Roomswap.swappable = swappable;
        CharacterController controller = other.GetComponent<CharacterController>();

        if (controller != null)
        {

            yield return StartCoroutine(FadeController.Instance.FadeInOut(0f, 0.25f, () =>
            {
                currentRoom.SetRoom(false);
                exitRoom.SetRoom(true);

                
                controller.enabled = false;
                other.transform.position = teleportTarget.position;
                other.transform.rotation = teleportTarget.rotation;
                controller.enabled = true;
                })
            );                    
        }
        else
        {
            other.transform.position = teleportTarget.position;
        }
    }
}

using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class OutOfBoundsTrigger : MonoBehaviour
{

    private bool isTeleporting;
    private RoomSwap room;
    private void OnTriggerEnter(Collider other)    
    {
        if (!isTeleporting && other.CompareTag("Player"))
        {
            StartCoroutine(ReturnToBounds(other));
        }
    }
    
    IEnumerator ReturnToBounds(Collider other)
    {
        CharacterController controller = other.GetComponent<CharacterController>();
        room = other.GetComponent<RoomSwap>();
        if (controller != null) {
            yield return FadeController.Instance.FadeInOut(0f, 0.5f, () =>
            {
                controller.enabled = false;
                other.transform.position = new Vector3(-62.9380836f, -2.654549f, -13.7584381f);
                other.transform.rotation = new Quaternion(0, 0, 0, 0);
                other.GetComponent<FirstPersonController>().ResetMouseLook();
                controller.enabled = true;
                room.clean = false;

            });
        
        }
    }
        
}


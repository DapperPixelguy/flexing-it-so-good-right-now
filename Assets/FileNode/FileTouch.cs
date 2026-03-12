using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class FileTouch : MonoBehaviour
{
    public VideoClip file;
    public GameObject videoPlayerParent;
    private VideoPlayer videoPlayer;
    private GameObject canvas;
    private void Start()
    {              
        videoPlayer = videoPlayerParent.GetComponent<VideoPlayer>();
        canvas = videoPlayerParent.transform.parent.Find("Canvas").gameObject;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {         
        FirstPersonController controller = other.GetComponent<FirstPersonController>();
        canvas.SetActive(true);
        controller.locked = true;        
        videoPlayer.clip = file;
        videoPlayer.Play();
        Destroy(gameObject);      
    }
}

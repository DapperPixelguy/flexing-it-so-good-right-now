using UnityEngine;
using UnityEngine.Video;

public class FileTouch : MonoBehaviour
{
    public Object file;
    public GameObject videoPlayerParent;
    private VideoPlayer videoPlayer;
    private void Start()
    {              
        videoPlayer = videoPlayerParent.GetComponent<VideoPlayer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        videoPlayer.Play();
        Destroy(gameObject);      
    }
}

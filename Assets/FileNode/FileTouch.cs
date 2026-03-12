using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class FileTouch : MonoBehaviour
{
    public VideoClip file;
    public GameObject videoPlayerParent;
    private VideoPlayer videoPlayer;
    private GameObject canvas;
    private FirstPersonController controller;
    private void Start()
    {              
        videoPlayer = videoPlayerParent.GetComponent<VideoPlayer>();
        canvas = videoPlayerParent.transform.parent.Find("Canvas").gameObject;
    }
    private void Update()
    {
        if (videoPlayer.time >= (float)videoPlayer.length - 0.1f)
        {
            Debug.Log("Video concluded");
            videoPlayer.Stop();
        }
        if (controller != null && controller.locked) {
        if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Space pressed");
                if (videoPlayer.isPlaying)
                {
                    Debug.Log("Paused with space");
                    videoPlayer.Pause(); 
                }
                else
                {
                    Debug.Log("Played with space");
                    videoPlayer.Play();
                }
            }
        if (Input.GetKeyDown(KeyCode.Escape))
            {
                controller.locked = false;
                videoPlayer.Stop();
                canvas.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {         
        controller = other.GetComponent<FirstPersonController>();       
        if (!controller.locked)
        {
            controller.locked = true;
            Debug.Log("started");
            canvas.SetActive(true);
            videoPlayer.clip = file;
            videoPlayer.Play();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

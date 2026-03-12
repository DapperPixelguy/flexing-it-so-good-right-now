using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerSlider : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Slider slider;
    private bool isDragging = false;
    void Start()
    {
        slider.minValue = 0;
        slider.maxValue = (float)videoPlayer.length;

        slider.onValueChanged.AddListener(OnSliderChanged);
    }
    
    void Update()
    {
        if (!isDragging && videoPlayer.isPlaying)
        {
            slider.value = (float)videoPlayer.time;
        }        
    }

    public void OnSliderChanged(float value)
    {
        if (videoPlayer.canSetTime)
        {
            videoPlayer.time = value;
        }
    }

    public void OnPointerDown()
    {
        isDragging = true;
    }

    public void OnPointerUp()
    {
        isDragging = false;
        videoPlayer.time = slider.value;
    }
}

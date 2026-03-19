using UnityEngine;

public class ToggleSecret : MonoBehaviour
{
    private MeshRenderer display;
    private ParticleSystem particle;
    private bool secretActive;
    void Start()
    {
        display = GetComponent<MeshRenderer>();
        particle = display.GetComponent<ParticleSystem>();
        if (particle != null)
        {
            particle.Stop();
        }
        secretActive = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            print("click");
            display.enabled = secretActive ? false : true;
            if (secretActive)
            {
                if (particle != null)
                {
                    particle.Stop();
                }
            } else
            {
                if (particle != null)
                {
                    particle.Play();
                }
            }
                secretActive = !secretActive;
        }
    }
}

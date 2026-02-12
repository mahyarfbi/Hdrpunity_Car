using UnityEngine;

public class Render_Camera : MonoBehaviour
{
    public Camera[] cam;
  
    public float updateInterval = 0.1f;
    float timer;
    private void Start()
    {
        for (int i = 0; i < cam.Length; i++)
        {
            cam[i].enabled = false;
            cam[i].useOcclusionCulling = false;
        }
     


    }

    void LateUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            for (int i = 0; i < cam.Length; i++)
            {
                cam[i].Render();    
            
            }
            timer = 0f;
        }
    }
}

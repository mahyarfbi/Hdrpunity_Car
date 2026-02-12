using System.Collections;
using UnityEngine;

public class Traffic_Light : MonoBehaviour
{
    [HideInInspector] public bool bol_Red, bol_Green, bol_Yellow;
    int lent = 1;// Grren = 1 // Yelow = 3 // red = 2
    public Texture2D texture_Red,Texture_Green,Texture_Yelow;
    Material material;
    private Renderer rend;
    private void Start()
    {
        rend = GetComponent<Renderer>();
        material = rend.materials[1];
        material.EnableKeyword("_EMISSION");
        Debug.Log(rend.materials[1]);
        StartCoroutine(enum_Teraffic_Light());

    }
    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator enum_Teraffic_Light()
    {
        material.SetTexture("_EmissiveColorMap", Texture_Yelow);
        Fun_Yellow();
      //  material.SetTexture("_BaseColorMap", Texture_Yelow);
        lent++;
        yield return new WaitForSeconds(1);
        if (lent == 3)
        {
            lent = 1;
        }

        if (lent == 1)
        {
            material.SetTexture("_EmissiveColorMap", Texture_Green);
            Fun_Green();
          
        }

        else if (lent == 2)
        {
            material.SetTexture("_EmissiveColorMap", texture_Red);
            Fun_Red();
        }
        
            yield return new WaitForSeconds(10);
        StartCoroutine(enum_Teraffic_Light());
    }
    public void Fun_Red()
    {
        bol_Red = true;
        bol_Yellow = false;
        bol_Green = false;
    }
    public void Fun_Green()
    {
        bol_Red = false;
        bol_Yellow = false;
        bol_Green = true;
    }
    public void Fun_Yellow()
    {
        bol_Red = false;
        bol_Yellow = true;
        bol_Green = false;
    }
}

using System.Collections;
using UnityEngine;

public class Manage_Camera : MonoBehaviour
{
    bool bol_Freelocak ,bol_enum_cmera;
  public  GameObject Camera_Tpp_;
    public GameObject[] All_Camear; // 0 = free 1 = pov
    void Start()
    {
        Camera_Tpp_.SetActive(false);
        All_Camear[0].SetActive(true);
        All_Camear[1].SetActive(false);
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(enum_Camera());
        }
    }
    IEnumerator enum_Camera()
    {
        bol_enum_cmera = true;
        if (bol_Freelocak == false)
        {
            Camera_Tpp_.SetActive(true);
           
            All_Camear[1].SetActive(true);
            All_Camear[0].SetActive(false); 
            bol_Freelocak =true;
        } else if (bol_enum_cmera == true)
        {
            Camera_Tpp_.SetActive(false);
          
            All_Camear[1].SetActive(false); 
            All_Camear[0].SetActive(true);
            bol_Freelocak = false; 
        } 
        yield return new WaitForSeconds(1);
    }
}

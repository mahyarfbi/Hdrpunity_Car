using FCG;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Ai_Bot_Car : MonoBehaviour
{
    List<Transform> Wheel = new List<Transform>();
    public List<Transform> Target = new List<Transform>();
    public Transform Parnet_Target;
    int len_Target;
    public float Speed_Car;
    float Defult_Speed;
    [SerializeField] float distanse;
    float rayTimer;
    void Start()
    {
        Defult_Speed = Speed_Car;
        for (int i = 0; i < transform.childCount; i++)
        {
            Wheel.Add(transform.GetChild(i));
        }
        for (int i = 0; i < Parnet_Target.childCount; i++)
        {
            Target.Add(Parnet_Target.GetChild(i));
        }
        len_Target = FindNearestTarget();
    }
    private void OnDrawGizmos()
    {
        if (Parnet_Target == null)
            return;

        int count = Parnet_Target.childCount;
        if (count < 2)
            return;

        Gizmos.color = Color.blue;

        for (int i = 0; i < count - 1; i++)
        {
            Transform a = Parnet_Target.GetChild(i);
            Transform b = Parnet_Target.GetChild(i + 1);

            if (a == null || b == null)
                continue;

            Gizmos.DrawLine(a.position, b.position);
        }
    }
    
   
    int FindNearestTarget()
    {
        int nearestIndex = 0;
        float minSqrDist = Mathf.Infinity;

        Vector3 carPos = transform.position;

        for (int i = 0; i < Target.Count; i++)
        {
            float sqrDist = (Target[i].position - carPos).sqrMagnitude;
            if (sqrDist < minSqrDist)
            {
                minSqrDist = sqrDist;
                nearestIndex = i;
            }
        }

        return nearestIndex;
    }
    private void FixedUpdate()
    {
        Ai_Patrol();
        Ray_view_bot();
        Update_Wheel(Wheel[0]);
        Update_Wheel(Wheel[1]);
        Update_Wheel(Wheel[2]);
        Update_Wheel(Wheel[3]);
        rayTimer += Time.fixedDeltaTime;
        if (rayTimer > 0.1f) 
        {
            Ray_view_bot();
            rayTimer = 0;
        }
    }
    public void Update_Wheel(Transform wheel)
    {
        wheel.Rotate(new Vector3(Speed_Car * 1.5f, 0, 0));
    }
    public void Ai_Patrol()
    {
      
        if (len_Target >= Target.Count) len_Target = 0;
        Founc_Smoth_AngelforTarget();
        distanse = Vector3.Distance(transform.position, Target[len_Target].position);

        if (distanse < 2.5f)
        {
          
                len_Target++;
          
        }
        if (Target[len_Target].position != null)
            transform.position = Vector3.MoveTowards(transform.position, Target[len_Target].position, Speed_Car * Time.fixedDeltaTime);

       
    }
    RaycastHit hit; float[] angles = { 0f, 15, -15f}; bool bol_Break; float Lerp_Speed;

    public void Ray_view_bot()
    {

        bol_Break = false;
        foreach (float angle in angles)
        {
            Vector3 dir = Quaternion.Euler(0, angle, 0) * transform.forward;

            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, out hit, 5))
            {

                Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir * 5, Color.red);

                if (hit.collider.CompareTag("Player"))
                {

                    bol_Break = true;

                }
                else if (hit.collider.CompareTag("trafficlight"))
                    {
                     Traffic_Light traffic_Light =  hit.collider.gameObject.GetComponent<Traffic_Light>();
                     if(traffic_Light.bol_Red == true || traffic_Light.bol_Yellow)
                    {
                        bol_Break = true;
                    }
                     else if(traffic_Light.bol_Green == true)
                    {
                        bol_Break = false;
                    }
                    }
            }
            else
            {

                Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir * 5, Color.green);
            }

            if (bol_Break == true)
            {

                if (Speed_Car >= 0) Speed_Car -= 1.75f *  Time.fixedDeltaTime;
            }
            else
            {
                if (Speed_Car <= Defult_Speed) Speed_Car +=  0.1f *  Time.fixedDeltaTime;
            }

        }
    }
    public void Founc_Smoth_AngelforTarget()
    {
        Vector3 direction = Target[len_Target].position - transform.position;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2);
    }
}

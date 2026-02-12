using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
public class Movement_Test : MonoBehaviour
{
    public CanvasGroup canvas_War_Motor, Canvas_War_RPM;
    public GameObject light_Breake, lights_Front;
    public GameObject Lent_Front_R, Lent_Front_L;
    public GameObject Front_L, Front_R, Back_L, Back_R;
    public WheelCollider Wheel_Front_L, Wheel_Front_R, Wheel_Back_L, Wheel_Back_R;
    public float angel, speed_Angel = 1;
    public Transform Farmun;
    [Range(500, 1000)]
    float Min_Rpm = 700, Max_RPM = 7000;
    [SerializeField] float speed_Turqe, enginRpm, Timer_Lent = 0;
    public float vertivcal, horizntal, Speed_Angel_Lerping = 10;
    float Speed_Car, kilumetr, Max_Speed;
    int Dande = 1;
    Rigidbody rb;
    public Text Text_Km, Text_Dande;
    [HideInInspector] public bool bol_Kelaj;
    Color color_Lent;
    public Material material_Lent;
    public Transform RightTest, LeftTest;
    public float Test;
 //   public TrailRenderer trailRenderer_Break, trailRenderer_Break_1;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //  rb.centerOfMass = new Vector3(0, -0.5f, 0);
        Text_Dande.text = "Dande = " + Dande.ToString();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb.angularDamping = 3f;
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P)) transform.localRotation = quaternion.Euler(0, 0, 0);
        Func_UI();
        Dande = math.clamp(Dande, 1, 5);
        Update_Clamp_Velocity();
        Debug.Log(color_Lent.r.ToString());
        break_car();
        Anti_Roll();
        updatewheel(Wheel_Front_L, Front_L.transform);
        updatewheel(Wheel_Front_R, Front_R.transform);
        updatewheel(Wheel_Back_L, Back_L.transform);
        updatewheel(Wheel_Back_R, Back_R.transform);
        updateLent(Wheel_Front_L, Lent_Front_L.transform);
        updateLent(Wheel_Front_R, Lent_Front_R.transform);

        float rotate_farmun = Wheel_Front_L.steerAngle;
        Light_Car();
        //   Farmun.localRotation = quaternion.Euler(transform.localRotation.x + -1.4f, 0, rotate_farmun / 10);
        Kelaj();
    }
   
    public void Anti_Roll()
    {

        Vector3 angVel = rb.angularVelocity;
        angVel.z *= 0.4f;
        rb.angularVelocity = angVel;
    }
    public void Kelaj()
    {
        bol_Kelaj = Input.GetKey(KeyCode.Q);
        if (bol_Kelaj == true)
        {
            speed_Turqe *= 0;
        }
        else if (bol_Kelaj == false)
        {
            speed_Turqe *= 1;
        }
    }
    public void Func_UI()
    {
        kilumetr = Speed_Car * 3.6f;
        Text_Km.text = "Km/h" + math.abs((int)kilumetr).ToString();
    }

    public void Light_Car()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

        }
        if (Input.GetKey(KeyCode.Space) && bol_breake_Down == false)
        {
            StartCoroutine(Down_Breake_enum());
        }
        else if (bol_breake_Up == false)
        {
            StartCoroutine(Up_Breake_enum());
        }
    }
    bool bol_breake_Down, bol_breake_Up;
    IEnumerator Up_Breake_enum()
    {
        bol_breake_Up = true;
        Debug.Log("Up");
        yield return null;


        light_Breake.gameObject.SetActive(false);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        bol_breake_Down = false;
    }
    IEnumerator Down_Breake_enum()
    {
        bol_breake_Down = true;
        Debug.Log("Down");
        yield return null;
        light_Breake.gameObject.SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space));
        bol_breake_Up = false;
    }

    void updatewheel(WheelCollider w, Transform t)
    {
        Vector3 p;
        Quaternion q;
        w.GetWorldPose(out p, out q);
        t.position = p;
        t.rotation = q;
    }
    void updateLent(WheelCollider w, Transform t)
    {
        Vector3 pos;
        Quaternion rot;
        w.GetWorldPose(out pos, out rot);
        float steer = w.steerAngle;
        steer = Mathf.Clamp(steer, -40, 40);
        t.localRotation = Quaternion.Euler(transform.localRotation.x, 0, steer);



    }
    public void Manage_Gearbox()
    {
        float gearFactor = 800 / Dande;
        speed_Turqe = gearFactor * vertivcal;


        ////if ((Dande == 2 && kilumetr < 28) || (Dande == 3 && kilumetr < 68) || (Dande == 4 && kilumetr < 108) || (Dande == 5 && kilumetr < 218) && bol_Kelaj == false)
        ////{

        ////    if (Canvas_War_RPM.alpha <= 1 ) Canvas_War_RPM.alpha += Time.deltaTime * 2;
        ////}
        ////else if (Canvas_War_RPM.alpha >= 0) Canvas_War_RPM.alpha -= Time.deltaTime * 2;
        //if(bol_Kelaj)
        //{
        //    if (Canvas_War_RPM.alpha <= 1) Canvas_War_RPM.alpha += Time.deltaTime * 2;
        //}
        //else 
        //{
        //    Canvas_War_RPM.alpha -= Time.deltaTime * 2;
        //}

    }
    public void Update_Clamp_Velocity()
    {
        if (Speed_Car >= -0.1f)
        {
            if (Input.GetKeyDown(KeyCode.R) && Dande < 5)
            {
                Dande++;
                Text_Dande.text = "Dande = " + Dande.ToString();

            }
            if (Input.GetKeyDown(KeyCode.E) && Dande > 1)
            {
                Dande--;
                Text_Dande.text = "Dande = " + Dande.ToString();
            }
        }
        else if (Speed_Car <= -1)
        {
            Dande = 1;

            Text_Dande.text = "Dande = " + "R";
        }
        else
        {
            Text_Dande.text = "Dande = " + Dande.ToString();
        }



        if (Dande == 0)
        {
            Dande = 1;
        }
        if (Dande == 1)
        {
            Max_Speed = 30;
        }
        else if (Dande == 2)
        {
            Max_Speed = 70;
        }
        else if (Dande == 3)
        {

            Max_Speed = 110;
        }
        else if (Dande == 4)
        {
            Max_Speed = 160;
        }
        else if (Dande == 5)
        {
            Max_Speed = 220;
        }


        //   rb.linearVelocity = new Vector3(math.clamp(rb.linearVelocity.x, -Max_Speed, Max_Speed), math.clamp(rb.linearVelocity.y, -Max_Speed, Max_Speed), math.clamp(rb.linearVelocity.z, -Max_Speed, Max_Speed));
        Speed_Car = Vector3.Dot(transform.forward, rb.linearVelocity);

    }







    public void angel_()
    {

        if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.D)))
        {
            angel = math.lerp(angel, 35, Time.deltaTime);

        }
        else
        {
            angel = math.lerp(angel, 0, Time.deltaTime);
        }
        Wheel_Front_L.steerAngle = horizntal * angel;
        Wheel_Front_R.steerAngle = horizntal * angel;
    }
    public void break_car()
    {
        if (kilumetr < Max_Speed + 5)
        {
            if (canvas_War_Motor.alpha >= 0) canvas_War_Motor.alpha -= Time.deltaTime * 2;
        }
        if (Input.GetKey(KeyCode.Space))
        {
      //      trailRenderer_Break.emitting = true;
     //       trailRenderer_Break_1.emitting = true;
            Wheel_Front_L.brakeTorque = 1200;
            Wheel_Front_R.brakeTorque = 1200;
            Wheel_Back_L.brakeTorque = 1200;
            Wheel_Back_R.brakeTorque = 1200;
           

        }
        else if (kilumetr > Max_Speed + 5 && bol_Kelaj == false)
        {
            if (canvas_War_Motor.alpha <= 1) canvas_War_Motor.alpha += Time.deltaTime * 2;
            Wheel_Front_L.brakeTorque = 1000;
            Wheel_Front_R.brakeTorque = 1000;
            Wheel_Back_L.brakeTorque = 1000;
            Wheel_Back_R.brakeTorque = 1000;
          //  trailRenderer_Break.emitting = true;
      //      trailRenderer_Break_1.emitting = true;


        }
        else if (kilumetr < Max_Speed)
        {
        //    trailRenderer_Break.emitting = false;
        //    trailRenderer_Break_1.emitting = false;
            Wheel_Front_L.brakeTorque = 0;
            Wheel_Front_R.brakeTorque = 0;
            Wheel_Back_L.brakeTorque = 0;
            Wheel_Back_R.brakeTorque = 0;
        }


        if (Input.GetKey(KeyCode.Space) && kilumetr > 2)
        {
            Timer_Lent += 5 * Time.deltaTime;
            if (Timer_Lent > 8f)
            {
                color_Lent.r = Mathf.Clamp01(color_Lent.r + Time.deltaTime * 2);
                material_Lent.color = color_Lent;
            }
        }
        else
        {
            Timer_Lent = 0;
            color_Lent.r = Mathf.Clamp01(color_Lent.r - Time.deltaTime * 2);
            material_Lent.color = color_Lent;

        }

    }

    
    public void Run_car()
    {
        vertivcal = Input.GetAxis("Vertical");
        horizntal = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.W)) enginRpm += 50;
        else enginRpm -= 50;
        enginRpm = math.clamp(enginRpm, Min_Rpm, Max_RPM);

        Wheel_Back_L.motorTorque = speed_Turqe;
        Wheel_Back_R.motorTorque = speed_Turqe;
        Wheel_Front_L.motorTorque = speed_Turqe;
        Wheel_Front_R.motorTorque = speed_Turqe;
        if (math.abs(kilumetr) <= Max_Speed)
        {
            Manage_Gearbox();
            //speed_Turqe = 700 * vertivcal;
        }
        else if (math.abs(kilumetr) > Max_Speed)
        {

            speed_Turqe = 0;
        }




    }

    void FixedUpdate()
    {
        angel_();
        Vector3 angVel = rb.angularVelocity;
        angVel.y = Mathf.Clamp(angVel.y, -0.5f, 0.5f);
        rb.angularVelocity = angVel;
        AntiRollBar(Wheel_Back_L, Wheel_Back_R);
        AntiRollBar(Wheel_Front_L, Wheel_Front_R);
        Run_car();
    }

    void AntiRollBar(WheelCollider wheelL, WheelCollider wheelR)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        float travelL = 1.0f, travelR = 1.0f;
        bool groundedL = wheelL.GetGroundHit(out WheelHit hitL);
        bool groundedR = wheelR.GetGroundHit(out WheelHit hitR);

        if (groundedL)
            travelL = (-wheelL.transform.InverseTransformPoint(hitL.point).y - wheelL.radius) / wheelL.suspensionDistance;

        if (groundedR)
            travelR = (-wheelR.transform.InverseTransformPoint(hitR.point).y - wheelR.radius) / wheelR.suspensionDistance;

        float rawForce = (travelL - travelR) * 4000;
        float antiRollForce = Mathf.Clamp(rawForce, -4000, 4000);

        if (groundedL)
            rb.AddForceAtPosition(wheelL.transform.up * -antiRollForce, wheelL.transform.position);

        if (groundedR)
            rb.AddForceAtPosition(wheelR.transform.up * antiRollForce, wheelR.transform.position);
    }

}


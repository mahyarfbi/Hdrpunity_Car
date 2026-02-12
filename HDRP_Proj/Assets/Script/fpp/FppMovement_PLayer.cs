using UnityEngine;

public class FppMovement_PLayer : MonoBehaviour
{
    [SerializeField] float horizntal, Vertical,MouseX,MouseY;
    public float speed_Move, Speed_Rotate;
    Rigidbody rb;
    public Transform Body;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Movement();
    }
    public void Movement()
    {
        horizntal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");
        rb.linearVelocity = new Vector3(Vertical * speed_Move * Time.fixedDeltaTime, 0, horizntal * speed_Move * Time.fixedDeltaTime);
        transform.Rotate(Vector3.up * MouseX);
        Body.Rotate(new Vector3(-MouseY, 0, 0) * Speed_Rotate * Time.fixedDeltaTime);
    }
}

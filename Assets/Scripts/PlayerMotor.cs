using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private Camera cam;

	private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;

    private Rigidbody rb;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}
    //get a movement vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    //get a rotational vector3
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }
    //get a rotational vector3 for Camera
    public void rotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }
    //run every physcis iteration
    void FixedUpdate()
    {
        performMovement();
        performRotation();
    }
    //perform movement on velocity varaiable
    void performMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.transform.position + velocity * Time.fixedDeltaTime);
        }
    }
    //perform rotaion on rotation varaiable
    void performRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if(cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }
}

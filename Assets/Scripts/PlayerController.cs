using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensivity = 3f;
	private PlayerMotor motor;

	void Start()
	{
		motor = GetComponent<PlayerMotor> ();
	}

	void Update()
	{
		//calculate player movement velocity as a 3D vector
		float _Xmov = Input.GetAxisRaw("Horizontal");
		float _Zmov = Input.GetAxisRaw ("Vertical");

		Vector3 _moveHorizontal = transform.right * _Xmov;
		Vector3 _moveVertical = transform.forward * _Zmov;

		//final movement vector
		Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * speed;

		//apply movement 

		motor.Move (_velocity);

        //Calculate rotation as a 3d vector: turning around

        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensivity;

        //apply rotaion
        motor.Rotate(_rotation);


        //Calculate camera rotation as a 3d vector: turning around

        float _xRot  = Input.GetAxisRaw("Mouse Y");

        Vector3 _cmeraRotation = new Vector3(_xRot, 0f, 0f) * lookSensivity;

        //apply rotaion
        motor.rotateCamera(_cmeraRotation);
    }
}

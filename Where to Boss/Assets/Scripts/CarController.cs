using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
public class CarController : MonoBehaviour
{
    private Rigidbody carRB;
    private float gasInput;
    private float steeringInput;
    private float brakeInput;
    private PlayerInputActions carControls;
    private InputAction move;
    private float slipAngle;


    [SerializeField] private float brakePower;
    [SerializeField] private AnimationCurve steeringCurve;
    [SerializeField] private float motorPower;
    private float carSpeed;


    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider FRWheelcol;
    [SerializeField] private WheelCollider FLWheelcol;
    [SerializeField] private WheelCollider RLWheelcol;
    [SerializeField] private WheelCollider RRWheelcol;
    [Space(20)]
    [Header("Wheel Meshes")]
    [SerializeField] private MeshRenderer FRWheelMesh;
    [SerializeField] private MeshRenderer FLWheelMesh;
    [SerializeField] private MeshRenderer RLWheelMesh;
    [SerializeField] private MeshRenderer RRWheelMesh;

    private void Awake()
    {
        carControls = new PlayerInputActions();
    }
    private void OnEnable()
    {
        move = carControls.Player.Move;
        move.Enable();
    }
    private void OnDisable()
    {
        move.Disable();
    }
    void Start()
    {
        carRB = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        carSpeed = carRB.linearVelocity.magnitude;
        UpdateWheelMesh(FRWheelcol, FRWheelMesh);
        UpdateWheelMesh(FLWheelcol, FLWheelMesh);
        UpdateWheelMesh(RLWheelcol, RLWheelMesh);
        UpdateWheelMesh(RRWheelcol, RRWheelMesh);
        CheckInput();
        ApplyBrake();
        AddMotorPower();
        AddSteering();
    }

    private void AddSteering()
    {
        float steeringAngle = steeringInput * steeringCurve.Evaluate(carSpeed);
        FRWheelcol.steerAngle = steeringAngle;
        FLWheelcol.steerAngle = steeringAngle;
    }

    private void AddMotorPower()
    {
        RRWheelcol.motorTorque = motorPower * gasInput;
        RLWheelcol.motorTorque = motorPower * gasInput;
    }

    private void ApplyBrake()
    {
        FRWheelcol.brakeTorque = brakeInput * brakePower * 0.7f;
        FLWheelcol.brakeTorque = brakeInput * brakePower * 0.7f;

        RRWheelcol.brakeTorque = brakeInput * brakePower * 0.3f;
        RLWheelcol.brakeTorque = brakeInput * brakePower * 0.3f;

    }
    private void CheckInput()
    {
        gasInput = move.ReadValue<Vector2>().y;
        steeringInput = move.ReadValue<Vector2>().x;
        slipAngle = Vector3.Angle(transform.forward, carRB.linearVelocity-transform.forward);
        if(slipAngle < 120f)
        {
            if (gasInput < 0)
            {
                brakeInput = Mathf.Abs(gasInput);
                gasInput = 0;
            }
        }
        else
        {
            brakeInput = 0;
        }
    }

    private void UpdateWheelMesh(WheelCollider wheelCollider, MeshRenderer wheelMesh)
    {
        Vector3 wheelPosition;
        Quaternion wheelRotation;
        wheelCollider.GetWorldPose(out wheelPosition, out wheelRotation);
        wheelMesh.transform.position = wheelPosition;
        wheelMesh.transform.rotation = wheelRotation;
        
    }



}

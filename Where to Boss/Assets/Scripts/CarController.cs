using System.Collections;
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
    private InputAction horn;
    private float slipAngle;
    private bool isBraking = true;
    private bool isBossSpeaking = true;
    [SerializeField] private float playerSpeechTimer = 14f;
    [SerializeField] private float musicKickInTimer = 20f;

    [SerializeField] private float brakePower;
    [SerializeField] private AnimationCurve steeringCurve;
    [SerializeField] private AnimationCurve EngineCurve;
    [SerializeField] private float motorPower;
    [SerializeField] private float speedToBreakSound;
    [SerializeField ] private float carSpeed;


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
        horn = carControls.Player.Horn;
        move = carControls.Player.Move;
        move.Enable();
        horn.Enable();

        horn.performed += Horn_performed;
    }

    private void Horn_performed(InputAction.CallbackContext obj)
    {
        //Pressed H
        SoundManager.PlaySound(SoundManager.Sound.CarHorn, transform.position);
    }

    private void OnDisable()
    {
        horn.Disable();
        move.Disable();
    }
    void Start()
    {
        carRB = gameObject.GetComponent<Rigidbody>();
        StartCoroutine(SpeechTimer());
        StartCoroutine(MusicTimer());
    }

    IEnumerator MusicTimer()
    {
        yield return new WaitForSeconds(musicKickInTimer);
        GameManager.Instance.stageMusic.Play();
    }

    IEnumerator SpeechTimer()
    {
       yield return new WaitForSeconds(playerSpeechTimer);
       SoundManager.PlaySound(SoundManager.Sound.PlayerVoiceLine, transform.position);
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
        EngineNoise();
        AdjustWheelFriction();
        ApplyTurningTorque();
    }
    private void ApplyTurningTorque()
    {
        if (Mathf.Abs(steeringInput) > 0.1f && carSpeed > 5f) // Only apply at moderate speeds
        {
            float torqueStrength = steeringInput * carSpeed * 0.05f;
            carRB.AddTorque(Vector3.up * torqueStrength, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (carSpeed > 12f && collision.gameObject.CompareTag("Building") && isBossSpeaking)
        {
            int voiceIndex = (int)UnityEngine.Random.Range(1, 4);
            switch (voiceIndex)
            {
                case 1:
                    SoundManager.PlaySound(SoundManager.Sound.Crash1,transform.position);
                    break;
                case 2:
                    SoundManager.PlaySound(SoundManager.Sound.Crash2, transform.position);
                    break;
                case 3:
                    SoundManager.PlaySound(SoundManager.Sound.Crash3, transform.position);
                    break;
                case 4:
                    SoundManager.PlaySound(SoundManager.Sound.Crash4, transform.position);
                    break;
            }

            StartCoroutine(BossLineTimer());

        }
    }

    IEnumerator BossLineTimer()
    {
        yield return new WaitForSeconds(2f);
    }

    private void EngineNoise()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (gasInput >= 1 || gasInput < 0)
            audioSource.pitch = EngineCurve.Evaluate(carSpeed);
        else
            audioSource.pitch = .8f;
       

        if (brakeInput> 0 && carSpeed > speedToBreakSound && isBraking)
        {
            SoundManager.PlaySound(SoundManager.Sound.CarBreak, transform.position);
            isBraking = false;
            StartCoroutine(BrakeSoundYield());
        }

    }

    IEnumerator BrakeSoundYield()
    {
        Debug.Log("Started timer");
        yield return new WaitForSeconds(2.5f);
        isBraking=true;
        Debug.Log("Timer ended");
    }

    private void AddSteering()
    {
        float steeringAngle = steeringInput * steeringCurve.Evaluate(carSpeed); // Ensure a minimum steer value
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

    private void AdjustWheelFriction()
    {
        WheelFrictionCurve friction = RLWheelcol.sidewaysFriction;
        float frictionFactor = Mathf.Lerp(1f, 0.5f, carSpeed / 50f); // Reduces sideways grip as speed increases

        friction.stiffness = frictionFactor;
        RLWheelcol.sidewaysFriction = friction;
        RRWheelcol.sidewaysFriction = friction;
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

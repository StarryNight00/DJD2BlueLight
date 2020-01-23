using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Constants
    private const float MAX_FORWARD_ACCELERATION = 8.0f;
    private const float MAX_BACKWARDS_ACCELERATION = 2.0f;
    private const float MAX_STRAFE_ACCELERATION = 8.0f;
    private const float JUMP_ACCELARATION = 350.0f;
    private const float GRAVITY_ACCELARATION = 20.0f;

    private const float MAX_FORWARD_VELOCITY = 4.0f;
    private const float MAX_BACKWARDS_VELOCITY = 3.0f;
    private const float MAX_STRAFE_VELOCITY = 2.0f;
    private const float MAX_JUMP_VELOCITY = 60.0f;
    private const float MAX_FALL_VELOCITY = 100.0f;

    private const float ANGULAR_VELOCITY_FACTOR = 3.0f;
    private const float WALK_VELOCITY_FACTOR = 1.0f;
    private const float RUN_VELOCITY_FACTOR = 2.0f;


    private const float MIN_HEADLOOK_ROTATION = 300.0f;
    private const float MAX_HEADLOOK_ROTATION = 60.0f;


    //Variables
    private CharacterController controller;
    private PlayerInteraction plyrInteraction;
    private Transform cameraTransform;
    private Karma _playerKarma;
    private Faction _playerFaction;

    private Vector3 velocity;
    private Vector3 acceleration;
    private float velocityFactor;

    private bool jump;
    [SerializeField] private bool _interacting;

    // Properties
    public bool Interacting { get; }
    public Karma PlayerKarma => _playerKarma;
    public Faction PlayerFaction => _playerFaction;

    //Game Updates
    private void UpdateAccelaration()
    {
        acceleration.z = Input.GetAxis("Forward");

        if (acceleration.z > 0)
            acceleration.z *= MAX_FORWARD_ACCELERATION * velocityFactor;
        else
            acceleration.z *= MAX_BACKWARDS_ACCELERATION * velocityFactor;

        acceleration.x = Input.GetAxis("Horizontal/Strafe") * MAX_STRAFE_ACCELERATION * velocityFactor;

        if (jump)
        {
            acceleration.y = JUMP_ACCELARATION;
            jump = false;
        }
        else if (controller.isGrounded)
        {
            acceleration.y = 0;
        }
        else
            acceleration.y = -GRAVITY_ACCELARATION;
    }

    private void UpdateVelocity()
    {
        velocity += acceleration * Time.fixedDeltaTime;

        velocity.z = acceleration.z == 0.0f ? velocity.z = 0.0f : Mathf.Clamp(velocity.z, -MAX_BACKWARDS_VELOCITY * velocityFactor, MAX_FORWARD_VELOCITY * velocityFactor);
        velocity.x = acceleration.x == 0.0f ? velocity.x = 0.0f : Mathf.Clamp(velocity.x, -MAX_STRAFE_VELOCITY * velocityFactor, MAX_STRAFE_VELOCITY * velocityFactor);
        velocity.y = acceleration.y == 0.0f ? velocity.y = -0.1f : Mathf.Clamp(velocity.y, -MAX_FALL_VELOCITY, MAX_JUMP_VELOCITY);
    }

    private void UpdateVelocityFactor()
    {
        velocityFactor = Input.GetButton("Run") ? RUN_VELOCITY_FACTOR : WALK_VELOCITY_FACTOR;
    }

    private void UpdatePosition()
    {
        Vector3 motion = velocity * Time.fixedDeltaTime;
        controller.Move(transform.TransformVector(motion));
    }

    private void UpdateRotation()
    {
        float xRotation = Input.GetAxis("Mouse X") * ANGULAR_VELOCITY_FACTOR;

        transform.Rotate(0f, xRotation, 0f);
    }

    private void UpdateHeadLook()
    {
        Vector3 cameraRotation = cameraTransform.localEulerAngles;

        cameraRotation.x -= Input.GetAxis("Mouse Y") * ANGULAR_VELOCITY_FACTOR;

        if (cameraRotation.x > 180.0f)
            cameraRotation.x = Mathf.Max(cameraRotation.x, MIN_HEADLOOK_ROTATION);
        else
            cameraRotation.x = Mathf.Min(cameraRotation.x, MAX_HEADLOOK_ROTATION);

        cameraTransform.localEulerAngles = cameraRotation;
    }

    public void SetInteractionState(bool state)
    {
        _interacting = state;
    }

    public void SetPlayerFaction(Faction faction)
    {
        _playerFaction = faction;
    }

    //Engine Start
    public void Start()
    {
        controller = GetComponent<CharacterController>();
        plyrInteraction = GetComponent<PlayerInteraction>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
        acceleration = Vector3.zero;
        velocity = Vector3.zero;
        velocityFactor = WALK_VELOCITY_FACTOR;
        _playerKarma = new Karma(this);
        _playerFaction = Faction.None;
    }

    //Engine Updates
    public void FixedUpdate()
    {
        if (!_interacting)
        {
            UpdateVelocityFactor();
            UpdateAccelaration();
            UpdateVelocity();
        }
        if (_interacting)
        {
            velocity = new Vector3();
            acceleration = new Vector3();
        }
        UpdatePosition();
    }

    public void Update()
    {
        if (!_interacting)
        {
            UpdateRotation();
            UpdateHeadLook();
        }
    }
}

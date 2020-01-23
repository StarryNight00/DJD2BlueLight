using UnityEngine;

/// <summary>
/// Class responsible for handling player movement and karma.
/// </summary>
public class Player : MonoBehaviour
{
    // Constants for various movement and camera movement restrictions
    private const float MAX_FORWARD_ACCELERATION = 8.0f;
    private const float MAX_BACKWARDS_ACCELERATION = 2.0f;
    private const float MAX_STRAFE_ACCELERATION = 8.0f;

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


    // private CharacterController variable for the character's controller
    private CharacterController controller;
    // private PlayerInteraction variable for playerInteraction
    private PlayerInteraction plyrInteraction;
    // private Transform variable for camera's transform
    private Transform cameraTransform;
    // private Karma variable for the player's associated karma
    private Karma _playerKarma;
    // private Faction variable for the player's current Faction
    private Faction _playerFaction;

    // private Vector3 for the player's velocity
    private Vector3 velocity;
    // private Vector3 for the player's acceleration
    private Vector3 acceleration;
    // private float for the player's velocity factor
    private float velocityFactor;

    [SerializeField] private bool _interacting;

    // public property of Karma type, used to access _playerKarma
    public Karma PlayerKarma => _playerKarma;

    /// <summary>
    /// Responsible for updating the player's acceleration
    /// </summary>
    private void UpdateAcceleration()
    {
        // set acceleration on z to input given on forward axis
        acceleration.z = Input.GetAxis("Forward");

        // if statements that checks if the acceleration on z is greater than 0
        if (acceleration.z > 0)
            // set acceleration on z to it's value times max acceleration
            // on forward times velocity factor
            acceleration.z *= MAX_FORWARD_ACCELERATION * velocityFactor;
        else
            // set acceleration on z to it's value times max acceleration
            // on bakcwards times velocity factor
            acceleration.z *= MAX_BACKWARDS_ACCELERATION * velocityFactor;

        // set acceleration on x to input given on Horizontal/Strafe axis
        // times max strafe acceleration times velocity factor
        acceleration.x = Input.GetAxis("Horizontal/Strafe") * 
            MAX_STRAFE_ACCELERATION * velocityFactor;
    }

    /// <summary>
    /// Responsible for updating the player's velocity on various axis.
    /// </summary>
    private void UpdateVelocity()
    {
        // set velocity to acceleration times fixedDeltaTime
        velocity += acceleration * Time.fixedDeltaTime;

        // set velocity on z to acceleration on z 0f if acceleration on z
        // equals 0 or to velocity on z, clamped between negative max backwards
        // velocity times velocity factor and max forward velocity times
        // velocity factor
        velocity.z = acceleration.z == 0.0f ? velocity.z =
            0.0f : Mathf.Clamp(velocity.z, -MAX_BACKWARDS_VELOCITY *
            velocityFactor, MAX_FORWARD_VELOCITY * velocityFactor);

        // set velocity on x to acceleration on x 0f if acceleration on x
        // equals 0 or to velocity on x, clamped between negative max strafe
        // velocity times velocity factor and max strafe velocity times
        // velocity factor
        velocity.x = acceleration.x == 0.0f ? velocity.x =
            0.0f : Mathf.Clamp(velocity.x, -MAX_STRAFE_VELOCITY *
            velocityFactor, MAX_STRAFE_VELOCITY * velocityFactor);
    }

    /// <summary>
    /// Responsible for updating velocity factor.
    /// </summary>
    private void UpdateVelocityFactor()
    {
        // assing run velocity factor to velocityFactor if Run button is down
        // or assign walk velocity factor to velocity factor otherwise
        velocityFactor = Input.GetButton("Run") ? RUN_VELOCITY_FACTOR : 
            WALK_VELOCITY_FACTOR;
    }

    /// <summary>
    /// Responsible for updating the player's position.
    /// </summary>
    private void UpdatePosition()
    {
        // declare and initialize motion with value of velocity times
        // fixedDeltaTime
        Vector3 motion = velocity * Time.fixedDeltaTime;
        // call move method of controller, giving it the motion Vector3
        controller.Move(transform.TransformVector(motion));
    }

    /// <summary>
    /// Responsible for updating the player's rotation in x axis based on
    /// Mouse X axis.
    /// </summary>
    private void UpdateRotation()
    {
        // declare and initialize xRotationm assigning to it the Mouse X axis
        // times angular velocity factor
        float xRotation = Input.GetAxis("Mouse X") * ANGULAR_VELOCITY_FACTOR;
        // rotate player with xRotation
        transform.Rotate(0f, xRotation, 0f);
    }
    /// <summary>
    /// Responsible for updating the player's head look angle in y axis, based
    /// on the Mouse Y axis.
    /// </summary>
    private void UpdateHeadLook()
    {
        // declare and initialize cameraRotation with localEulerAngles from
        // cameraTransform
        Vector3 cameraRotation = cameraTransform.localEulerAngles;
        // assign input from Mouse Y axis times angular velocity factor to
        // cameraRotation.x
        cameraRotation.x -= Input.GetAxis("Mouse Y") * 
            ANGULAR_VELOCITY_FACTOR;
        // if statement that checks if cameraRotation.x  is greater than 180.0f
        if (cameraRotation.x > 180.0f)
            // assign maximum value between cameraRotation.x or min headlook
            // rotation to cameraRotation.x
            cameraRotation.x = Mathf.Max(cameraRotation.x, 
                MIN_HEADLOOK_ROTATION);
        else
            // assign minimum value between cameraRotation.x or max headlook
            // rotation to cameraRotation.x
            cameraRotation.x = Mathf.Min(cameraRotation.x, 
                MAX_HEADLOOK_ROTATION);
        // assign cameraRotation to cameraTransform.localEulerAngles
        cameraTransform.localEulerAngles = cameraRotation;
    }

    /// <summary>
    /// Responsible for setting the player's interaction state, using a bool
    /// given as a parameter.
    /// </summary>
    /// <param name="state">Boolean used to set the _interacting bool value
    /// </param>
    public void SetInteractionState(bool state)
    {
        _interacting = state;
    }

    /// <summary>
    /// Responsible for initializing the necessary variables on start of
    /// running time.
    /// </summary>
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

    /// <summary>
    /// Responsible for updating the state of the object during runtime in
    /// tandem with real time.
    /// </summary>
    public void FixedUpdate()
    {
        // if statememnt that checks if _interacting bool is false
        if (!_interacting)
        {
            // in case _interacting bool is false, update player's velocity
            // factor, acceleration and velocity
            UpdateVelocityFactor();
            UpdateAcceleration();
            UpdateVelocity();
        }
        // if statement that checks if _interacting bool is true
        if (_interacting)
        {
            // in case _interacting bool is true, velocity and acceleration are
            // reset, as to stop the player's movement
            velocity = new Vector3();
            acceleration = new Vector3();
        }
        // Update player's position
        UpdatePosition();
    }

    /// <summary>
    /// Responsible for updating the state of the object during runtime.
    /// </summary>
    public void Update()
    {
        // if statement that checks if _interacting bool is false
        if (!_interacting)
        {
            // only if _interacting bool is false, update Rotation and 
            // HeadLook, in order to stop the player from looking around while
            // interacting with NPCs
            UpdateRotation();
            UpdateHeadLook();
        }
    }
}

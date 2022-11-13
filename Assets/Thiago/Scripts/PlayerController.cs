using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{

    public bool canMove = true;
    public RaycastHit hitinfo;
    public bool canOpenDoor = false;
    public int life = 100;
    public Collider Stairs;
    public PhysicMaterial StairsMaterial;
    private FadeInAndOut fadeInAndOut;

    Transform doorTransform;

    [SerializeField] LayerMask doorLayer;
    [SerializeField] LayerMask itemLayer;
    public bool canGrab = false;
    [SerializeField] HumanoidLandInput _input;
    Animator anim;
    Rigidbody MyRb;
    [SerializeField] float BlendTreeAcceleration = 0.1f;
    [SerializeField] float BlendTreeDeceleration = 0.1f;
    private float horizontalMove;
    private float verticalMove;
    [SerializeField] bool isTank = true;
    public bool isMoving;
    private bool isRunning = false;
    CapsuleCollider _capsuleCollider = null;

    [Header("TankController")]
    private bool isBacking;
    private bool canStopAnim;
    private float verticalSpeed = 3.8f;
    private float horizontalSpeed = 150f;
    public float velocityX = 0.0f;
    public float velocityY = 0.0f;

    [Header("ModernController")]
    private GameObject currentCamera;
    Vector3 moveDirection;
    [SerializeField] Vector3 _playerMoveInput = Vector3.zero;
    private Vector3 viewForwardModern = Vector3.zero;
    private Vector3 viewRight = Vector3.zero;
    private float speed;
    private float velocity = 0.0f;

    [Header("Ground Check")]
    [SerializeField] bool _playerIsGrounded = true;
    [SerializeField][Range(0.0f, 1.8f)] float _groundCheckRadiusMultiplier = 0.9f;
    [SerializeField][Range(-0.95f, 1.05f)] float _groundCheckDistanceTolerance = 0.05f;
    [SerializeField] float _playerCenterToGroundDistance = 0.0f;
    RaycastHit _groundCheckHit = new RaycastHit();

    [Header("Gravity")]
    [SerializeField] float _gravityFallCurrent = 0.0f;
    [SerializeField] float _gravityFallMin = 0.0f;
    [SerializeField] float _gravityFallIncrementTime = 0.05f;
    [SerializeField] float _playerFallTimer = 0.0f;
    [SerializeField] float _gravityGrounded = -1.0f;
    [SerializeField] float _maxSlopeAngle = 47.5f;

    [Header("Stairs")]
    [SerializeField][Range(0.0f, 1.0f)] float _maxStepHeight = 0.5f;
    [SerializeField][Range(0.0f, 1.0f)] float _minStepDepth = 0.3f;
    [SerializeField] float _stairHeightPaddingMultiplier = 1.5f;
    [SerializeField] bool _isFirstStep = true;
    [SerializeField] float _firstStepVelocityDistanceMultiplier = 0.1f;
    [SerializeField] bool _playerIsAscendingStairs = false;
    [SerializeField] bool _playerIsDescendingStairs = false;
    [SerializeField] float _ascendingStairsMovementMultiplier = 0.35f;
    [SerializeField] float _descendingStairsMovementMultiplier = 0.7f;
    [SerializeField] float _maximumAngleOfApproachToAscend = 45.0f;
    float _playerHalfHeightToGround = 0.0f;
    float _maxAscendRayDistance = 0.0f;
    float _maxDescendRayDistance = 0.0f;
    int _numberOfStepDetectRays = 0;
    float _rayIncrementAmount = 0.0f;
    Vector3 _playerCenterPoint = Vector3.zero;




    #region StartAndUpdate

    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _input = GetComponent<HumanoidLandInput>();

        _maxAscendRayDistance = _maxStepHeight / Mathf.Cos(_maximumAngleOfApproachToAscend * Mathf.Deg2Rad);
        _maxDescendRayDistance = _maxStepHeight / Mathf.Cos(80.0f * Mathf.Deg2Rad);

        _numberOfStepDetectRays = Mathf.RoundToInt(((_maxStepHeight * 100.0f) * 0.5f) + 1.0f);
        _rayIncrementAmount = _maxStepHeight / _numberOfStepDetectRays;
    }
    private void Start()
    {
        fadeInAndOut = GameObject.Find("FadeInAndOutManager").GetComponent<FadeInAndOut>();
        currentCamera = GameObject.FindGameObjectWithTag("CurrentCamera");
        MyRb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        
    }

    private void Update()
    {
        if (isTank)
        {
            TankControlUpdate();
        }
        else
        {
            ModernControllerUpdate();
        }
        if (life <= 0)
        {
            Debug.Log("You are dead");
        }

        RaycastForDoor();

    }
    private void FixedUpdate()
    {
        _playerMoveInput = GetMoveInput();
        _playerCenterPoint = MyRb.position + _capsuleCollider.center;
       // _playerMoveInput = PlayerStairs();
       // _playerMoveInput = PlayerSlope();
        _playerIsGrounded = PlayerGroundCheck();
      //  _playerMoveInput.y = PlayerFallGravity();


        if (!isMoving)
        {
            // Stairs.material.staticFriction = 5;
            StairsMaterial.staticFriction = 5;
            StairsMaterial.frictionCombine = PhysicMaterialCombine.Maximum;
        }
        else
        {
            // Stairs.material.staticFriction = 0;
            StairsMaterial.staticFriction = 0;
            StairsMaterial.frictionCombine = PhysicMaterialCombine.Minimum;
        }

        if (isRunning)
        {
            _ascendingStairsMovementMultiplier = 1000.0f;
        }
        else
        {
            _ascendingStairsMovementMultiplier = 3500.0f;
        }





        Debug.DrawRay(MyRb.position, MyRb.transform.TransformDirection(_playerMoveInput), Color.red, 1.0f);

        MyRb.AddRelativeForce(_playerMoveInput, ForceMode.Force);


        if (!isTank)
        {
            ModernControllerFixedUpdate();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyHand")
        {
            life -= 33;
        }
    }

    #endregion



    public void SetDoorTransform(Transform Door)
    {
        doorTransform = Door;
    }
    public Transform GetDoorTransform()
    {
        return doorTransform;
    }
    private bool PlayerGroundCheck()
    {
        float sphereCastRadius = _capsuleCollider.radius * _groundCheckRadiusMultiplier;
        Physics.SphereCast(_playerCenterPoint, sphereCastRadius, Vector3.down, out _groundCheckHit);
        _playerCenterToGroundDistance = _groundCheckHit.distance + sphereCastRadius;
        return ((_playerCenterToGroundDistance >= _capsuleCollider.bounds.extents.y - _groundCheckDistanceTolerance) &&
                (_playerCenterToGroundDistance <= _capsuleCollider.bounds.extents.y + _groundCheckDistanceTolerance));
    }

    private Vector3 AscendStairs(Vector3 calculatedStepInput)
    {
        if (isMoving)
        {
            float calculatedVelDistance = _isFirstStep ? (MyRb.velocity.magnitude * _firstStepVelocityDistanceMultiplier) + _capsuleCollider.radius : _capsuleCollider.radius;

            float ray = 0.0f;
            List<RaycastHit> raysThatHit = new List<RaycastHit>();
            for (int x = 1;
                 x <= _numberOfStepDetectRays;
                 x++, ray += _rayIncrementAmount)
            {
                Vector3 rayLower = new Vector3(_playerCenterPoint.x, ((_playerCenterPoint.y - _playerHalfHeightToGround) + ray), _playerCenterPoint.z);
                RaycastHit hitLower;
                if (Physics.Raycast(rayLower, MyRb.transform.TransformDirection(_playerMoveInput), out hitLower, calculatedVelDistance + _maxAscendRayDistance))
                {
                    float stairSlopeAngle = Vector3.Angle(hitLower.normal, MyRb.transform.up);
                    if (stairSlopeAngle == 90.0f)
                    {
                        raysThatHit.Add(hitLower);
                    }
                }
            }
            //  Debug.Log(raysThatHit.Count);
            if (raysThatHit.Count > 0)
            {
                Vector3 rayUpper = new Vector3(_playerCenterPoint.x, (((_playerCenterPoint.y - _playerHalfHeightToGround) + _maxStepHeight) + _rayIncrementAmount), _playerCenterPoint.z);
                RaycastHit hitUpper;
                Physics.Raycast(rayUpper, MyRb.transform.TransformDirection(_playerMoveInput), out hitUpper, calculatedVelDistance + (_maxAscendRayDistance * 2.0f));
                if (!(hitUpper.collider) ||
                     (hitUpper.distance - raysThatHit[0].distance) > _minStepDepth)
                {
                    if (Vector3.Angle(raysThatHit[0].normal, MyRb.transform.TransformDirection(-_playerMoveInput)) <= _maximumAngleOfApproachToAscend)
                    {
                        Debug.DrawRay(rayUpper, MyRb.transform.TransformDirection(_playerMoveInput), Color.yellow, 5.0f);

                        _playerIsAscendingStairs = true;
                        Vector3 playerRelX = Vector3.Cross(_playerMoveInput, Vector3.up);

                        if (_isFirstStep)
                        {
                            calculatedStepInput = Quaternion.AngleAxis(45.0f, playerRelX) * calculatedStepInput;
                            _isFirstStep = false;
                        }
                        else
                        {
                            float stairHeight = raysThatHit.Count * _rayIncrementAmount * _stairHeightPaddingMultiplier;

                            float avgDistance = 0.0f;
                            foreach (RaycastHit r in raysThatHit)
                            {
                                avgDistance += r.distance;
                            }
                            avgDistance /= raysThatHit.Count;

                            float tanAngle = Mathf.Atan2(stairHeight, avgDistance) * Mathf.Rad2Deg;
                            calculatedStepInput = Quaternion.AngleAxis(tanAngle, playerRelX) * calculatedStepInput;
                            calculatedStepInput *= _ascendingStairsMovementMultiplier;
                        }

                    }
                    else
                    {
                        _playerIsAscendingStairs = false;
                        _isFirstStep = true;
                    }
                }
                else
                {
                    _playerIsAscendingStairs = false;
                    _isFirstStep = true;
                }
            }
            else
            {
                _playerIsAscendingStairs = false;
                _isFirstStep = true;
            }
        }
        else
        {
            _playerIsAscendingStairs = false;
            _isFirstStep = true;
        }
        return calculatedStepInput;
    }

    private Vector3 DescendStairs(Vector3 calculatedStepInput)
    {
        if (isMoving)
        {
            float ray = 0.0f;
            List<RaycastHit> raysThatHit = new List<RaycastHit>();
            for (int x = 1;
                 x <= _numberOfStepDetectRays;
                 x++, ray += _rayIncrementAmount)
            {
                Vector3 rayLower = new Vector3(_playerCenterPoint.x, ((_playerCenterPoint.y - _playerHalfHeightToGround) + ray), _playerCenterPoint.z);
                RaycastHit hitLower;
                if (Physics.Raycast(rayLower, MyRb.transform.TransformDirection(-_playerMoveInput), out hitLower, _capsuleCollider.radius + _maxDescendRayDistance))
                {
                    float stairSlopeAngle = Vector3.Angle(hitLower.normal, MyRb.transform.up);
                    if (stairSlopeAngle == 90.0f)
                    {
                        raysThatHit.Add(hitLower);
                    }
                }
            }
            if (raysThatHit.Count > 0)
            {
                Vector3 rayUpper = new Vector3(_playerCenterPoint.x, (((_playerCenterPoint.y - _playerHalfHeightToGround) + _maxStepHeight) + _rayIncrementAmount), _playerCenterPoint.z);
                RaycastHit hitUpper;
                Physics.Raycast(rayUpper, MyRb.transform.TransformDirection(-_playerMoveInput), out hitUpper, _capsuleCollider.radius + (_maxDescendRayDistance * 2.0f));
                if (!(hitUpper.collider) ||
                     (hitUpper.distance - raysThatHit[0].distance) > _minStepDepth)
                {
                    if (!(_playerIsGrounded) && hitUpper.distance < _capsuleCollider.radius + (_maxDescendRayDistance * 2.0f))
                    {
                        Debug.DrawRay(rayUpper, MyRb.transform.TransformDirection(-_playerMoveInput), Color.yellow, 5.0f);

                        _playerIsDescendingStairs = true;
                        Vector3 playerRelX = Vector3.Cross(_playerMoveInput, Vector3.up);

                        float stairHeight = raysThatHit.Count * _rayIncrementAmount * _stairHeightPaddingMultiplier;

                        float avgDistance = 0.0f;
                        foreach (RaycastHit r in raysThatHit)
                        {
                            avgDistance += r.distance;
                        }
                        avgDistance /= raysThatHit.Count;

                        float tanAngle = Mathf.Atan2(stairHeight, avgDistance) * Mathf.Rad2Deg;
                        calculatedStepInput = Quaternion.AngleAxis(tanAngle - 90.0f, playerRelX) * calculatedStepInput;
                        calculatedStepInput *= _descendingStairsMovementMultiplier;
                    }
                    else
                    {
                        _playerIsDescendingStairs = false;
                    }
                }
                else
                {
                    _playerIsDescendingStairs = false;
                }
            }
            else
            {
                _playerIsDescendingStairs = false;
            }
        }
        else
        {
            _playerIsDescendingStairs = false;
        }
        return calculatedStepInput;
    }

    private Vector3 PlayerStairs()
    {
        Vector3 calculatedStepInput = _playerMoveInput;

        _playerHalfHeightToGround = _capsuleCollider.bounds.extents.y;
        if (_playerCenterToGroundDistance < _capsuleCollider.bounds.extents.y)
        {
            _playerHalfHeightToGround = _playerCenterToGroundDistance;
        }

        calculatedStepInput = AscendStairs(calculatedStepInput);
        if (!(_playerIsAscendingStairs))
        {
            calculatedStepInput = DescendStairs(calculatedStepInput);
        }
        return calculatedStepInput;

    }

    private float PlayerFallGravity()
    {
        float gravity = _playerMoveInput.y;
        if (_playerIsGrounded || _playerIsAscendingStairs || _playerIsDescendingStairs)
        {
            _gravityFallCurrent = _gravityFallMin; // Reset
        }
        else
        {
            _playerFallTimer -= Time.fixedDeltaTime;
            if (_playerFallTimer < 0.0f)
            {
                float gravityFallMax = 10000.0f;
                float gravityFallIncrementAmount = (gravityFallMax - _gravityFallMin) * 0.1f;
                if (_gravityFallCurrent < gravityFallMax)
                {
                    _gravityFallCurrent += gravityFallIncrementAmount;
                }
                _playerFallTimer = _gravityFallIncrementTime;
            }
            gravity = -_gravityFallCurrent;
        }
        return gravity;
    }

    private Vector3 PlayerSlope()
    {
        Vector3 calculatedPlayerMovement = _playerMoveInput;

        if (_playerIsGrounded && !_playerIsAscendingStairs && !_playerIsDescendingStairs)
        {
            Vector3 localGroundCheckHitNormal = MyRb.transform.InverseTransformDirection(_groundCheckHit.normal);

            float groundSlopeAngle = Vector3.Angle(localGroundCheckHitNormal, MyRb.transform.up);
            if (groundSlopeAngle == 0.0f)
            {
                if (_input.MoveIsPressed)
                {
                    RaycastHit rayHit;
                    float rayCalculatedRayHeight = _playerCenterPoint.y - _playerCenterToGroundDistance + _groundCheckDistanceTolerance;
                    Vector3 rayOrigin = new Vector3(_playerCenterPoint.x, rayCalculatedRayHeight, _playerCenterPoint.z);
                    if (Physics.Raycast(rayOrigin, MyRb.transform.TransformDirection(calculatedPlayerMovement), out rayHit, 0.75f))
                    {
                        if (Vector3.Angle(rayHit.normal, MyRb.transform.up) > _maxSlopeAngle)
                        {
                            Debug.Log(_playerMoveInput.y);
                            calculatedPlayerMovement.y = --verticalSpeed;
                        }
                    }
                    //Debug.DrawRay(rayOrigin, _rigidbody.transform.TransformDirection(calculatedPlayerMovement), Color.green, 1.0f);
                }

                if (calculatedPlayerMovement.y == 0.0f)
                {
                    calculatedPlayerMovement.y = _gravityGrounded;
                }
            }
            else
            {
                Quaternion slopeAngleRotation = Quaternion.FromToRotation(MyRb.transform.up, localGroundCheckHitNormal);
                calculatedPlayerMovement = slopeAngleRotation * calculatedPlayerMovement;

                float relativeSlopeAngle = Vector3.Angle(calculatedPlayerMovement, MyRb.transform.up) - 90.0f;
                calculatedPlayerMovement += calculatedPlayerMovement * (relativeSlopeAngle / 90.0f);

                if (groundSlopeAngle < _maxSlopeAngle)
                {
                    if (_input.MoveIsPressed)
                    {
                        calculatedPlayerMovement.y += _gravityGrounded;
                    }
                }
                else
                {
                    float calculatedSlopeGravity = groundSlopeAngle * -0.2f;
                    if (calculatedSlopeGravity < calculatedPlayerMovement.y)
                    {
                        calculatedPlayerMovement.y = calculatedSlopeGravity;
                    }
                }
            }
            //#if UNITY_EDITOR
            //            Debug.DrawRay(_rigidbody.position, _rigidbody.transform.TransformDirection(calculatedPlayerMovement), Color.red, 0.5f);
            //#endif
        }

        return calculatedPlayerMovement;
    }

    private void TankControlUpdate()
    {
        MovementAnimationTank();
        anim.SetFloat("Velocity", velocityY);
        anim.SetFloat("VelocityX", velocityX);
        if (canMove)
        {






            if (Input.GetButton("Run"))
            {

                if (Input.GetButton("Run") && isBacking)
                {
                    StartCoroutine(stopMove());
                    this.GetComponent<Animator>().Play("QuickTurn");
                }
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }


            if (Input.GetButton("Horizontal") && !canStopAnim)
            {
                horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * horizontalSpeed;
                this.transform.Rotate(0, horizontalMove, 0);
            }


            if (Input.GetButton("Vertical") && !canStopAnim)
            {
                isMoving = true;
                if (Input.GetButton("SKey"))
                {
                    verticalSpeed = 100f;
                    isBacking = true;
                }
                else
                {
                    isBacking = false;
                    if (!isRunning)
                    {
                        verticalSpeed = 250f;
                    }
                    else
                    {
                        verticalSpeed = 600f;
                    }
                }
                verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * verticalSpeed;

                if (isBacking)
                {
                    var v2 = transform.forward * -verticalSpeed * Time.deltaTime;
                    v2.y = MyRb.velocity.y;
                    MyRb.velocity = v2;
                }
                else
                {
                    var v1 = transform.forward * verticalSpeed * Time.deltaTime;
                    v1.y = MyRb.velocity.y;
                    MyRb.velocity = v1;

                }
            }
            else
            {
                isBacking = false;
                isMoving = false;
            }


            IEnumerator stopMove()
            {
                canStopAnim = true;
                yield return new WaitForSeconds(1.18f);
                canStopAnim = false;
            }

        }


    }

    private void MovementAnimationTank()
    {

        bool forwardPressed = Input.GetKey("w");
        bool rightPressed = Input.GetKey("d");
        bool leftpressed = Input.GetKey("a");
        bool runPressed = Input.GetKey("left shift");



        if (forwardPressed && velocityY < 2.0f)
        {
            velocityY += Time.deltaTime * BlendTreeAcceleration;
        }
        if (forwardPressed && runPressed && velocityY < 3.0f)
        {
            velocityY += Time.deltaTime * BlendTreeAcceleration;
        }
        if (forwardPressed && !isRunning && velocityY > 2.0f)
        {
            velocityY -= Time.deltaTime * BlendTreeDeceleration;
        }
        if (!forwardPressed && velocityY > 1.0f)
        {
            velocityY -= Time.deltaTime * BlendTreeDeceleration;
        }
        if (!forwardPressed && !isBacking && velocityY < 1.0f)
        {
            velocityY += Time.deltaTime * BlendTreeAcceleration;
        }
        if (isBacking && velocityY > 0.0f)
        {
            velocityY -= Time.deltaTime * BlendTreeDeceleration;
        }



        if (rightPressed && !leftpressed && !forwardPressed && !isBacking && velocityX < 1.0f)
        {
            velocityX += Time.deltaTime * BlendTreeAcceleration;
        }
        if (!rightPressed && leftpressed && !forwardPressed && velocityX > -1.0f)
        {
            velocityX -= Time.deltaTime * BlendTreeDeceleration;
        }
        if (!rightPressed && !leftpressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * BlendTreeAcceleration;
        }
        if (!rightPressed && !leftpressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * BlendTreeDeceleration;
        }
        if (!canMove && velocityX != 0.0f)
        {
            velocityX = 0.0f;

        }
        if (!canMove && velocityY != 0.0f)
        {
            velocityY = 1.0f;
        }



        {

        }

    }

    private void ModernControllerUpdate()
    {
        if (canMove)
        {


            if (Input.GetButton("Run") && isMoving)
            {
                isRunning = true;
                speed = 550f;
            }
            else
            {
                isRunning = false;
                speed = 250f;
            }

            MovementAnimationModernController();
            anim.SetFloat("Velocity", velocity);
            horizontalMove = Input.GetAxisRaw("Horizontal");
            verticalMove = Input.GetAxisRaw("Vertical");

            if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
            {
                currentCamera = GameObject.FindGameObjectWithTag("CurrentCamera");
                viewForwardModern = Vector3.Scale(currentCamera.transform.forward, new Vector3(1f, 0f, 1f)).normalized;
                viewRight = Vector3.Scale(currentCamera.transform.right, new Vector3(1f, 0f, 1f)).normalized;
                viewRight = new Vector3(viewForwardModern.z, 0f, viewForwardModern.x * -1f);
            }

            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

            var v1 = moveDirection * speed * Time.deltaTime;
            v1.y = MyRb.velocity.y;
            MyRb.velocity = v1;
        }
    }

    private void ModernControllerFixedUpdate()
    {
        moveDirection = verticalMove * viewForwardModern + horizontalMove * viewRight;
        if (moveDirection.sqrMagnitude > 1f)
        {
            moveDirection.Normalize();
        }
        if (moveDirection.sqrMagnitude != 0f)
        {
            base.transform.forward = moveDirection;
        }
    }

    private void MovementAnimationModernController()
    {
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool downPressed = Input.GetKey("s");
        bool runPressed = Input.GetKey("left shift");
        if ((forwardPressed || leftPressed || rightPressed || downPressed) && velocity < 2.0f)
        {
            velocity += Time.deltaTime * BlendTreeAcceleration;
        }
        if ((forwardPressed || leftPressed || rightPressed || downPressed) && runPressed && velocity < 3.0f)
        {
            velocity += Time.deltaTime * BlendTreeAcceleration;
        }

        if ((!forwardPressed && !leftPressed && !rightPressed && !downPressed) && velocity > 1.0f)
        {
            velocity -= Time.deltaTime * BlendTreeDeceleration;
        }
        if ((forwardPressed || leftPressed || rightPressed || downPressed) && !isRunning && velocity > 2.0f)
        {
            velocity -= Time.deltaTime * BlendTreeDeceleration;
        }
        if (velocity < 1.0f)
        {
            velocity = 1.0f;
        }
    }

    private void RaycastForDoor()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1.5f, Color.green);

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitinfo, 1.5f, doorLayer))
        {

            canOpenDoor = true;
    
           
            Debug.Log("DoorAhead");

        }
        else
        {
            canOpenDoor = false;
        }
    }

    private Vector3 GetMoveInput()
    {
        return new Vector3(_input.MoveInput.x, 0.0f, _input.MoveInput.y);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Debug.Log("KEY HERE");
            other.GetComponent<Key>().canGrab = true;
            if (other.GetComponent<Key>().canDestroy)
            {
                other.GetComponent<Key>().canDestroy = false;
                StartCoroutine(StartAnimation(other));
               
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Debug.Log("KEY OUT");
            other.GetComponent<Key>().canGrab = false;
        }
    }
    IEnumerator PickUpItem(Collider other)
    {
        
        yield return new WaitForSeconds(1.5f);
      //  fadeInAndOut.act = false;
        if (other != null)
        {
            Destroy(other.gameObject);
        }
      
        canMove = true;
    }
    IEnumerator StartAnimation(Collider other)
    {
        canMove = false;
        anim.SetTrigger("PickItem");
        yield return new WaitForSeconds(1.2f);
        anim.speed = 0;
        fadeInAndOut.act = true;
        StartCoroutine(PickUpItem(other));       
    }
}

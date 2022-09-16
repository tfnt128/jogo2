using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Controls;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    Rigidbody MyRb;
    [SerializeField] float BlendTreeAcceleration = 0.1f;
    [SerializeField] float BlendTreeDeceleration = 0.1f;
    private float horizontalMove;
    private float verticalMove;
    [SerializeField] bool isTank = true;
    private bool isMoving;
    private bool isRunning = false;
    CapsuleCollider _capsuleCollider = null;

    [Header("TankController")]
    private bool isBacking;
    private bool canStopAnim;
    private float verticalSpeed = 3.8f;
    private float horizontalSpeed = 150f;
    private float velocityX = 0.0f;
    private float velocityY = 0.0f;

    [Header("ModernController")]
    private GameObject currentCamera;
    Vector3 moveDirection;
    Vector3 _playerMoveInput = Vector3.zero;
    private Vector3 viewForwardModern = Vector3.zero;
    private Vector3 viewRight = Vector3.zero;
    private float speed;
    private float velocity = 0.0f;

    [Header("GroundCheck")]
    [SerializeField] bool _playerIsGrounded = true;
    [SerializeField][Range(0.0f, 1.8f)] float _groundCheckRadiusMustiplier = 0.9f;
    [SerializeField][Range(-0.95f, 1.05f)] float _groundCheckDistance = 0.05f;
    [SerializeField] float _playerCenterToGround = -20.0f;
    RaycastHit _groundCheckHit = new RaycastHit();

    [Header("Gravity")]
    [SerializeField] float _gravityFallCurrent = -100.0f;
    [SerializeField] float _gravityFallMin = -100.0f;
    [SerializeField] float _gravittFallMax = -500.0f;
    [SerializeField][Range(-5.0f, -35.0f)] float _gravityFallIncrementAmount = -20.0f;
    [SerializeField] float _gravityFallIncrementTime = 0.05f;
    [SerializeField] float _playerFallTimer = 0.0f;
    [SerializeField] float _gravity = 0.0f;


    #region StartAndUpdate

    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }
    private void Start()
    {
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
        
    }
    private void FixedUpdate()
    {
        _playerIsGrounded = PlayerGroundCheck();
        _playerMoveInput.y = PlayerGravity();
        _playerMoveInput = PlayerMoveByGravity();

        MyRb.AddRelativeForce(_playerMoveInput, ForceMode.Force);
        if (!isTank)
        {
            ModernControllerFixedUpdate();
        }
    }
    #endregion

    private bool PlayerGroundCheck()
    {
        float sphereCastRadius = _capsuleCollider.radius * _groundCheckRadiusMustiplier;
        float sphereCastTravelDistance = _capsuleCollider.bounds.extents.y * sphereCastRadius + _groundCheckDistance;
        return Physics.SphereCast(MyRb.position, sphereCastRadius, Vector3.down, out _groundCheckHit, sphereCastTravelDistance);
    }


    private float PlayerGravity()
    {
        if (_playerIsGrounded)
        {
            _gravity = 0.0f;
            _gravityFallCurrent = _gravityFallMin;
        }
        else
        {
            _playerFallTimer -= Time.fixedDeltaTime;
            if(_playerFallTimer < 0.0f)
            {
                if(_gravityFallCurrent > _gravittFallMax)
                {
                    _gravityFallCurrent += _gravityFallIncrementAmount;
                }
                _playerFallTimer = _gravityFallIncrementTime;
                _gravity = _gravityFallCurrent;
            }
        }
        return _gravity;
    }


    private Vector3 PlayerMoveByGravity()
    {
        Vector3 calculate = (new Vector3(_playerMoveInput.x,_playerMoveInput.y * MyRb.mass,_playerMoveInput.z));
        return calculate;
    }


    private void TankControlUpdate()
    {
        MovementAnimationTank();
        anim.SetFloat("Velocity", velocityY);
        anim.SetFloat("VelocityX", velocityX);


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

            if (Input.GetButton("SKey"))
            {
                verticalSpeed = 120f;
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
                    verticalSpeed = 550f;
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
        }

   
    private void ModernControllerUpdate()
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
}

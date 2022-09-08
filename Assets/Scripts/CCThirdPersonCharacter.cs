using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CCThirdPersonCharacter : MonoBehaviour
{
    private CharacterController cc;

    private Quaternion inputAngle;

    private float speed = 4f;

    public Vector3 velocity;


    public Vector3 moveDirection;

    public Vector3 pushDisplacement;

    private Vector3 viewForward = Vector3.zero;

    private Vector3 viewRight = Vector3.zero;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        velocity = Vector3.zero;
        pushDisplacement = Vector3.zero;
    }

    private void Update()
    {
        float axis = Input.GetAxis("Horizontal");
        float axis2 = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            viewForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1f, 0f, 1f)).normalized;
            viewRight = Vector3.Scale(Camera.main.transform.right, new Vector3(1f, 0f, 1f)).normalized;
            viewRight = new Vector3(viewForward.z, 0f, viewForward.x * -1f);
        }
        moveDirection = axis2 * viewForward + axis * viewRight;
        if (moveDirection.sqrMagnitude > 1f)
        {
            moveDirection.Normalize();
        }
        if (moveDirection.sqrMagnitude != 0f)
        {
            base.transform.forward = moveDirection;
        }
        
        velocity += Physics.gravity * Time.deltaTime;
        cc.Move((moveDirection * speed + velocity) * Time.deltaTime);
        pushDisplacement *= 0.94f;
    }
}

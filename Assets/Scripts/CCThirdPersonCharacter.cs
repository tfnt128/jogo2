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



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.DrawRay(hit.point, hit.normal, Color.magenta);
        pushDisplacement += hit.moveDirection;
        if (velocity.y < 0f && Vector3.Dot(hit.normal, Vector3.down) < -0.5f)
        {
            if (velocity.y < -5f)
            {
                GameObject.Find("Dust").GetComponent<ParticleSystem>().Play();
                GameObject.Find("Dust").transform.position = base.transform.position + Vector3.up * 0.1f;
            }
            velocity.y = 0f;
        }
        float num = 20f;
        Rigidbody attachedRigidbody = hit.collider.attachedRigidbody;
        if (!(attachedRigidbody == null) && !attachedRigidbody.isKinematic && !(hit.moveDirection.y < -0.3f))
        {
            Vector3 vector = Vector3.Scale(hit.normal, new Vector3(-1f, -1f, -1f));
            attachedRigidbody.AddForceAtPosition(vector * num, hit.point, ForceMode.Force);
        }
    }
}

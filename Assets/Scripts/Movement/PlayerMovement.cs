using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{

    //Essentials
    Rigidbody rb;
    [SerializeField] Transform orientation;
    public LayerMask whatIsGround;

    Vector3 movDir;
    float hInput;
    float vInput;
    internal bool grounded;

    //scalling var
    public float moveSpeed;
    public float groundDrag;
    public float height;

    //Scalling for crouch
    float scaleYStart;
    float crouchScale;

    public KeyCode crouch = KeyCode.LeftControl;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        scaleYStart = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        myInputs();
        speedControl();
        addDrag();
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    void myInputs()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(crouch))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        if (Input.GetKeyUp(crouch))
        {
            transform.localScale = new Vector3(transform.localScale.x, scaleYStart, transform.localScale.z);
        }
    }

    void movePlayer()
    {
        movDir = orientation.forward * vInput + orientation.right * hInput;

        rb.AddForce(movDir.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    void speedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitVel.x, rb.linearVelocity.y, limitVel.z);
        }
    }
    void addDrag()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, height * 0.5f + 0.2f, whatIsGround);

        if (grounded)
        {
            rb.linearDamping = groundDrag;
        }
    }
}


using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class State : MonoBehaviour
{

    public KeyCode crouch;

    public LayerMask mask;

    public enum state
    {
        walking,
        crouching,
        jumping,
        idle
    }

    private state playerState;

    public state PlayerState 
    {
        set { playerState = value; }
        get { return playerState; } 
    }

    private Rigidbody player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player = this.gameObject.GetComponent<Rigidbody>();
        crouch = KeyCode.LeftControl;
    }

    private void FixedUpdate()
    {
        updateState();
    }

    private void updateState()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, -1, 0)), .1f, mask))
        {
            playerState = state.jumping;
        }
        else if (Input.GetKey(crouch))
        {
            playerState = state.crouching;
        }
        else if(player.linearVelocity.x>0 || player.linearVelocity.z > 0)
        {
            playerState = state.walking;
        }
    }
}

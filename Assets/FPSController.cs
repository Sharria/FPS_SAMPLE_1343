using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] Camera cam;
    [SerializeField] float movementSpeed = 2.0f;
    [SerializeField] float lookSensitivityX = 1.0f;
    [SerializeField] float lookSensitivityY = 1.0f;

    Vector3 velocity;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpForce = 10;
    bool grounded;
    float xRotation;

    public Camera Cam { get { return cam; } }

    List<Gun> equippedGuns = new List<Gun>();
    public Gun currentGun = null;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Look();

        FireGun();
    }

    void Movement()
    {
        grounded = controller.isGrounded;

        if(grounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        Vector2 movement = GetPlayerMovementVector();
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;
        controller.Move(move * movementSpeed * (GetSprint() ? 3 : 1) * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y += Mathf.Sqrt (jumpForce * -1 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void Look()
    {
        Vector2 looking = GetPlayerLook();
        float lookX = looking.x * lookSensitivityX * Time.deltaTime;
        float lookY = looking.y * lookSensitivityY * Time.deltaTime;

        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * lookX);
    }

    void FireGun()
    {
        if(GetPressFire())
        {
            currentGun?.Fire();
        }
    }

    bool GetPressFire()
    {
        return Input.GetButtonDown("Fire1");
    }

    bool GetHoldFire()
    {
        return Input.GetButton("Fire1");
    }

    Vector2 GetPlayerMovementVector()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    Vector2 GetPlayerLook()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    bool GetSprint()
    {
        return Input.GetButton("Sprint");
    }

    
}

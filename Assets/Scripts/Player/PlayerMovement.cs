using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Public var
    public float walkSpeed, runSpeed, rotationSpeed, minRotation, maxRotation;
    public bool canMove, canRotate;

    public Animator cameraJointAnim;

    // Priv var
    private CharacterController charController;
    private Vector3 movVector;
    
    private float xRot, yRot, speed, stamina;
    private bool isRunning, canRun;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize var
        charController = GetComponent<CharacterController>();
        movVector = new Vector3(0f, 0f, 0f);
        speed = walkSpeed;
        stamina = 85f;
        isRunning = false;
        canRun = true;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotation();
    }

    // Fn movement
    void Movement()
    {
        if (canMove)
        {
            // Restart mov vector
            movVector = new Vector3(0f, -1f, 0f);

            // Get WASD inputs
            float xMovement = Input.GetAxis("Horizontal");
            float zMovement = Input.GetAxis("Vertical");

            // Pass value to mov vector
            movVector.x = xMovement;
            movVector.z = zMovement;

            // Move to mov vector in player direction
            movVector = transform.TransformDirection(movVector);

            // Verify if running
            if (Input.GetKeyDown(KeyCode.LeftShift) && canRun)
            {
                isRunning = true;
                cameraJointAnim.SetBool("isRunning", true);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isRunning = false;
                cameraJointAnim.SetBool("isRunning", false);
            }

            // Control stamina
            Stamina();

            // Move character
            charController.Move(movVector.normalized * speed * Time.deltaTime);
        }
    }

    // Fn stamina
    void Stamina()
    {
        // If running
        if(isRunning)
        {
            speed = runSpeed;
            stamina -= 10f * Time.deltaTime;

            if (stamina < 15f)
            {
                canRun = false;
            }

            if (stamina <= 0f)
            {
                isRunning = false;
                cameraJointAnim.SetBool("isRunning", false);
            }
        }
        // If NOT running
        else
        {
            speed = walkSpeed;
            if(stamina <= 100f)
            {
                stamina += 10f * Time.deltaTime;
            }

            if (stamina >= 15f)
            {
                canRun = true;
            }
        }
    }

    // Fn rotation camera
    void Rotation()
    {
        if (canRotate)
        {
            // Get mouse inputs
            xRot += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            yRot += Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            // Add Y rotation limits
            yRot = Mathf.Clamp(yRot, minRotation, maxRotation);

            // Apply rot
            transform.localRotation = Quaternion.Euler(0f, xRot, 0f);
            Camera.main.transform.localRotation = Quaternion.Euler(-yRot, 0f, 0f);
        }
    }
}

using UnityEngine;
using System.Collections;

[AddComponentMenu("IntroToUnity/PlayerController")]
public class PlayerController : MonoBehaviour 
{
    public Transform CameraTransform;
    public Transform CameraTarget;
    public float CameraDistance = 2;

    private Animator animator;
    private CharacterController characterController;

    public float Speed { get; private set; }
    public bool Walk { get; private set; }

    public int Score { get; set; }

    public GameObject HUD { get; private set; }

    void Start()
    {
        Screen.lockCursor = true;

        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        HUD = GameObject.Find("HUD");
    }

    void Update()
    {
        // exit
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        //ControlCamera();

        if (animator == null || characterController == null) return;

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        animator.SetFloat("Speed", move.sqrMagnitude * 0.5f);

        bool walk = Input.GetButton("Walk");
        animator.SetBool("Walk", walk);

        Speed = walk ? 1f : 4f;
        //float speed = walk ? 1.0f : 2.5f;

        if (move != Vector3.zero)
        {
            move = CameraTransform.TransformDirection(move);
            move.y = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(move), .1f);
            //characterController.SimpleMove(transform.forward * Speed);
        }
    }

    void LateUpdate()
    {
       
        //UpdateCamera();
    }

    void FixedUpdate()
    {

    }

    private void UpdateCamera()
    {
        if (!CameraTransform || !CameraTarget) return;

        CameraTransform.LookAt(CameraTarget);
        float currentDistance = Vector3.Distance(CameraTransform.position, CameraTarget.position);
        float deltaDistance = currentDistance - CameraDistance;

        CameraTransform.Translate(Vector3.forward * deltaDistance);

        RaycastHit hit = new RaycastHit();
        //if (Physics.Raycast(CameraTarget.position, CameraTransform.position - CameraTarget.position, out hit))
        if (Physics.SphereCast(CameraTarget.position, characterController.radius, CameraTransform.position - CameraTarget.position, out hit, CameraDistance))
        {
            Debug.DrawLine(CameraTarget.position, hit.point, Color.green);
            Debug.DrawLine(CameraTarget.position, CameraTransform.position, Color.red);

            Vector3 position = CameraTarget.position + Vector3.Project(hit.point - CameraTarget.position, CameraTransform.position - CameraTarget.position);
            Debug.DrawLine(CameraTarget.position, position);
            CameraTransform.position = position;

            Debug.Log(hit.collider.ToString());
        }

        //*
        bool visible = Vector3.Distance(characterController.ClosestPointOnBounds(CameraTransform.position), CameraTransform.position) <= characterController.radius ? false : true;
        foreach (Renderer render in gameObject.GetComponentsInChildren<Renderer>())
        {
            render.enabled = visible;
        }
        //*/
    }

    private void ControlCamera()
    {
        if (!CameraTransform || !CameraTarget) return;

        /*
        float yaw = Input.GetAxis("Mouse X") * Time.deltaTime * 90;
        float pitch = Input.GetAxis("Mouse Y") * Time.deltaTime * 90;

        CameraTransform.RotateAround(CameraTarget.position, Vector3.up, yaw);
        CameraTransform.RotateAround(CameraTarget.position, CameraTransform.right, pitch);
        //*/
    }

    /*
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Contains("Item"))
        {
            ItemBehaviour item = collider.gameObject.GetComponent<ItemBehaviour>();
            if (item == false) return;

            if (item.Pick())
            {
                Score++;
            }
        }
    }
    //*/

    void OnGUI()
    {
        if (HUD != null && HUD.guiText != null)
        {
            HUD.guiText.text = "Score : " + Score;
        }
        else
        {
            GUILayout.Label("Score : " + Score);
        }
    }
}

using UnityEngine;
using System.Collections;

[AddComponentMenu("IntroToUnity/PlayerController")]
public class PlayerController : MonoBehaviour
{
    public GameObject HUD;

    public int Score { get; set; }

    private Animator _animator;
    private CharacterController _characterController;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        
        if (_animator == null || _characterController == null) enabled = false;
    }

    void Update()
    {
        // exit
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _animator.SetFloat("Speed", move.sqrMagnitude * 0.5f);

        bool walk = Input.GetButton("Walk");
        _animator.SetBool("Walk", walk);

        if (move != Vector3.zero)
        {
            if (Camera.main != null)
            {
                move = Camera.main.transform.TransformDirection(move);
            }
            move.y = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(move), .1f);
        }
    }

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

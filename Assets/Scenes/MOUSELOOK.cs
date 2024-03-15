using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOUSELOOK : MonoBehaviour
{
    [Tooltip("Controls how much the camera will move in response to detected mouse input.")]
    [SerializeField] private float sensitivity = 2f;
    [Tooltip("Controls how much the camera will continue to move after mouse ceases.")]
    [SerializeField] private float drag = 3f;

    private Vector2 MouseDir;
    private Vector2 smoothing;
    private Vector2 result;
    private Transform character;

    /// <summary>
    /// Enables/disables the ability to move the camera by moving the mouse
    /// </summary>
    public bool LookEnabled { get; set; } = true;

    /// <summary>
    /// Awake is called before Start.
    /// </summary>
    void Awake()
    {
        character = transform.parent;
        LookEnabled = true;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        if(LookEnabled == true) 
        { 
            MouseDir = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            MouseDir *= sensitivity;
            smoothing = Vector2.Lerp(smoothing, MouseDir, 1 / drag);
            result += smoothing;
            result.y = Mathf.Clamp(result.y, -70, 70);

            character.rotation = Quaternion.AngleAxis(result.x, character.up);
            transform.localRotation = Quaternion.AngleAxis(-result.y, Vector3.right);
        }
    }
}

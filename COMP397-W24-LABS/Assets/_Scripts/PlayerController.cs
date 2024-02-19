using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : Subject
{
    #region fields
    PlayerControl _inputs;
    Vector2 _move;
    Camera _camera;
    Vector3 _camForward, _camRight;
    #endregion

    #region serialized fields
    [SerializeField] float _speed;

    [Header ("Character Controller")]
    [SerializeField] CharacterController _controller;

    [Header("Movement")]
    [SerializeField] float _gravity = -30.0f;
    [SerializeField] float _jumpHeight = 3.0f;

    [SerializeField] Vector3 _velocity;

    [Header("Ground Detection")]
    [SerializeField] Transform _groundCheck;

    [SerializeField] float _groundRadius = 0.5f;
    [SerializeField] LayerMask _groundMask;

    [SerializeField] bool _isGrounded;

    [Header("Respawn Transform")]
    [SerializeField] Transform _respawn;

    #endregion

    public void Awake() {
        _camera = Camera.main;
        _controller = GetComponent<CharacterController>();
        _inputs = new PlayerControl();
        _inputs.Player.Move.performed += Move_performed;
        _inputs.Player.Jump.performed += Jump_performed;
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        if (_isGrounded) {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2.0f * _gravity);
        }
    }

    public void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        _move = obj.ReadValue<Vector2>();
        Debug.Log($"move performed {obj.ReadValue<Vector2>().x}, {obj.ReadValue<Vector2>().y}");
    }

    public void OnEnable() {
        _inputs.Enable();
    }

    public void OnDisable() {
        _inputs.Disable();
    }

    public void FixedUpdate() {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundRadius, _groundMask);
        if( _isGrounded && _velocity.y < 0.0f) {
            _velocity.y = 2.0f;
        }
        _camForward = _camera.transform.forward;
        _camRight = _camera.transform.right;
        _camForward.y = 0.0f;
        _camRight.y = 0.0f;

        _camForward.Normalize();
        _camRight.Normalize();

        Vector3 movement = (_camRight * _move.x + _camForward * _move.y) * _speed * Time.fixedDeltaTime;
        _controller.Move(movement);
        _velocity.y += _gravity * Time.fixedDeltaTime;
        _controller.Move(_velocity * Time.fixedDeltaTime);
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, _groundRadius);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log($"Colliding with {other.tag}");

        if (other.CompareTag("deathZone")) {
            _controller.enabled = false;
            transform.position = _respawn.position;
            _controller.enabled = true;
            NotifyObservers();
        }
    }
}

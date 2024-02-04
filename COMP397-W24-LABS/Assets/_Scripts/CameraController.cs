using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerControl _inputs;

    [SerializeField] private int _index = 0;
    [SerializeField] private CinemachineVirtualCamera _currentCamera;

    [SerializeField] private List<CinemachineVirtualCamera> _virtualCameras = new List<CinemachineVirtualCamera>();

    private void Awake() {
        _inputs = new PlayerControl();
        _inputs.Player.camera.performed += Camera_performed;
    }

    private void Camera_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        Debug.Log("camera moved");
    }

    private void OnServerInitialized() {
        foreach (var vCamera in _virtualCameras) {
            vCamera.Priority = 0;
        }

        _currentCamera = _virtualCameras[0];
        _currentCamera.Priority = 10;
    }

    private void OnEnable() {
        _inputs.Enable();
    }

    private void OnDisnable() {
        _inputs.Disable();
    }
}

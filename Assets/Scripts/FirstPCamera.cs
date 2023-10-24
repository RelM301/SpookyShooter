using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPCamera : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;
    public float maxCameraRotation = 90f;

    // Reference to the game over panel
    public GameObject gameOverPanel; // Assign your game over panel in the Inspector
    public GameObject pauseGamePanel;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the game over panel is active
        bool isGameOverPanelActive = gameOverPanel.activeInHierarchy;
        bool isPauseGamePanelActive = pauseGamePanel.activeInHierarchy;

        if (isGameOverPanelActive || isPauseGamePanelActive)
        {
            // Unlock the cursor and make it visible
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            // Lock the cursor
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Collect mouse input
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the camera around its local X axis
        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -maxCameraRotation, maxCameraRotation);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        transform.localEulerAngles = new Vector3(cameraVerticalRotation, 0f, 0f);
        player.Rotate(Vector3.up * inputX);
    }
}

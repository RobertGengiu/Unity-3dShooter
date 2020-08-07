using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private Transform playerRoot, lookRoot;
    // Start is called before the first frame update

    [SerializeField]
    private bool invert;

    [SerializeField]
    private bool canUnlock = true;

    [SerializeField]
    private float sensivity = 5f;

    [SerializeField]
    private int smoothSteps = 10;

    [SerializeField]
    private float smoothWeight = 0.4f;

    [SerializeField]
    private float rollAngle = 10f;

    [SerializeField]
    private float rollSpeed = 3f;

    [SerializeField]
    private Vector2 defaultLookLimits = new Vector2(-70f, 80f);

    private Vector2 currentMouseLook;
    private Vector2 lookAngles;
    
    private float currentRollAngles;
    
    private int lastLookFrame;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        LockAndUnlockCursor();
        if (Cursor.lockState == CursorLockMode.Locked) {
            LookAround();
        }
    }

    void LockAndUnlockCursor() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (Cursor.lockState == CursorLockMode.Locked) {
                Cursor.lockState = CursorLockMode.None;
            } else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void LookAround() {
        currentMouseLook = new Vector2(Input.GetAxis(MouseAxis.MOUSEY), Input.GetAxis(MouseAxis.MOUSEX));

        lookAngles.x += currentMouseLook.x * sensivity * (invert ? 1f : -1f);
        lookAngles.y += currentMouseLook.y * sensivity;
        lookAngles.x = Mathf.Clamp(lookAngles.x, defaultLookLimits.x, defaultLookLimits.y);

        currentRollAngles = Mathf.Lerp (currentRollAngles, Input.GetAxisRaw(MouseAxis.MOUSEX) * rollAngle, Time.deltaTime * rollSpeed);

        lookRoot.localRotation = Quaternion.Euler(lookAngles.x, 0f, currentRollAngles);
        playerRoot.localRotation = Quaternion.Euler(0f, lookAngles.y, 0f);
    }
}

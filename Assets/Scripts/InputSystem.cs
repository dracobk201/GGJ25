using ScriptableObjectArchitecture;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    [SerializeField] private Vector2GameEvent tapped;
    private InputActions touchControls;
    private Camera cam;

    private void Awake()
    {
        touchControls = new InputActions();
        cam = Camera.main;
    }

    private void OnEnable()
    {
        touchControls.Enable();
        touchControls.Mobile.Tap.performed += context => tapped.Raise(cam.ScreenToWorldPoint(context.ReadValue<Vector2>()));
    }

    private void OnDisable()
    {
        touchControls.Disable();
        touchControls.Mobile.Tap.performed -= context => tapped.Raise(cam.ScreenToWorldPoint(context.ReadValue<Vector2>()));
    }
}
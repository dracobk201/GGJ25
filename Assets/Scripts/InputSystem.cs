using ScriptableObjectArchitecture;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    [SerializeField] private Vector2Reference tapLocation;
    //[SerializeField] private BoolReference tapping;
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
        //touchControls.Mobile.Tap.performed += context => Debug.Log(context.ReadValue<Vector2>());
        //touchControls.Mobile.Tap.canceled += context => shooting.Value = false;
    }

    private void OnDisable()
    {
        touchControls.Disable();
        touchControls.Mobile.Tap.performed -= context => tapped.Raise(cam.ScreenToWorldPoint(context.ReadValue<Vector2>()));
        //touchControls.Mobile.Tap.canceled -= context => shooting.Value = false;
    }
}
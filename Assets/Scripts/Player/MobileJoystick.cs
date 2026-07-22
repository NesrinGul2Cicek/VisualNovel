using UnityEngine;
using UnityEngine.EventSystems;

// On-screen virtual joystick for mobile movement.
//
// This intentionally does NOT go through the Input System's action bindings.
// Unity's own "On-Screen Stick" component creates a virtual Gamepad device,
// which is more fragile here (it keeps fighting with the raw <Touchscreen>
// bindings already used elsewhere in this project, e.g. Interact/ContinueT).
// Reading drag input straight from uGUI's event interfaces and feeding it to
// PlayerController.SetVirtualMove() is the simpler, more robust route, and it
// coexists fine with the Input System driving keyboard/gamepad movement.
//
// Setup in the Unity Editor (MainScene):
// 1. Canvas > create UI > Image, name it "JoystickBackground" (a translucent
//    circle sprite works well). Anchor it bottom-left, e.g. size 200x200.
// 2. As a child of JoystickBackground, create another Image "JoystickHandle"
//    (a smaller filled circle), size ~80x80, centered.
// 3. Add this script (MobileJoystick) to JoystickBackground.
// 4. Drag JoystickBackground's own RectTransform into "Background",
//    JoystickHandle's RectTransform into "Handle", and the Player object
//    (the one with PlayerController) into "Player".
// 5. Make sure JoystickBackground's Image has "Raycast Target" enabled so it
//    receives pointer/drag events.
public class MobileJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("References")]
    public RectTransform background;
    public RectTransform handle;
    public PlayerController player;

    [Header("Settings")]
    [Tooltip("How far the handle can travel from center, in UI units.")]
    public float handleRange = 70f;
    [Tooltip("Inputs smaller than this magnitude are treated as zero, to avoid drift.")]
    public float deadZone = 0.1f;

    private Vector2 inputVector = Vector2.zero;
    private Camera uiCamera;

    private void Awake()
    {
        if (background == null)
            background = GetComponent<RectTransform>();

        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            uiCamera = canvas.worldCamera;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (background == null) return;

        Vector2 localPoint;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                background, eventData.position, uiCamera, out localPoint))
            return;

        localPoint.x /= background.sizeDelta.x;
        localPoint.y /= background.sizeDelta.y;

        inputVector = new Vector2(localPoint.x * 2f, localPoint.y * 2f);
        if (inputVector.magnitude > 1f)
            inputVector = inputVector.normalized;

        if (handle != null)
            handle.anchoredPosition = inputVector * handleRange;

        Vector2 finalInput = inputVector.magnitude < deadZone ? Vector2.zero : inputVector;

        if (player != null)
            player.SetVirtualMove(finalInput);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;

        if (handle != null)
            handle.anchoredPosition = Vector2.zero;

        if (player != null)
            player.SetVirtualMove(Vector2.zero);
    }
}

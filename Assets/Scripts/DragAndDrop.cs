using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour {
    
    [SerializeField] private InputAction mouseClick;
    
    [SerializeField] private float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    private Camera mainCamera;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private GameObject selectedObject;
    private Slot selectedSlot;

    [SerializeField] private SlotManager slotManager;


    private void Awake() {
        this.mainCamera = Camera.main;
    }

    private void OnEnable() {
        this.mouseClick.Enable();
        mouseClick.performed += OnMouseClick;
    }

    private void OnDisable() {
        this.mouseClick.Disable();
        mouseClick.performed -= OnMouseClick;
    }

    private void OnMouseClick(InputAction.CallbackContext context) {
        
        Debug.Log("Mouse Click");
        Ray ray = this.mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit)) {
            if (hit.collider != null) {

                selectedObject = hit.collider.gameObject;
                selectedSlot = selectedObject.GetComponent<Slot>();
                StartCoroutine(MoveObject(selectedObject));
            }
        }
    }

    // Coroutine for dragging the card
    private IEnumerator MoveObject(GameObject selectedObject) {
        
        float initialDistance = Vector3.Distance(selectedObject.transform.position, this.mainCamera.transform.position);

        while(mouseClick.ReadValue<float>() > 0) { // drag
            
            Ray ray = this.mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            StartCardParallax(selectedObject);
            selectedObject.transform.position = Vector3.SmoothDamp(selectedObject.transform.position, ray.GetPoint(initialDistance), ref velocity, smoothTime);
            yield return waitForFixedUpdate;
        }
        // drop
        CheckOverlappingSlots();
        EndCardParallax(selectedObject);

    }


    // Check if the card is overlapping with another slot
    private void CheckOverlappingSlots() {
        
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Slot overlapSlot = hit.collider.GetComponent<Slot>();
            if (overlapSlot != null && overlapSlot != selectedSlot) {
                Debug.Log("Overlapping slot");
                slotManager.Swap(selectedSlot, overlapSlot);
            } else {
                // snapping back to previous position
                selectedSlot.transform.position = selectedSlot.GetCurrentPosition();
            }
        }
    }

    // Parallax feature
    // when a card is being dragged, it rotates on itself
    private void StartCardParallax(GameObject selectedObject) {
        
        float maxRotationAngle = 10f;
        float rotationSpeed = 0.5f;

        float additionalRotation = Mathf.Sin(Time.time * rotationSpeed) * maxRotationAngle;
        Quaternion rotation = Quaternion.Euler(0, additionalRotation, 0);

        selectedObject.transform.rotation = rotation;
    }

    private void EndCardParallax(GameObject selectedObject) {
        selectedObject.transform.rotation = Quaternion.identity;
    }


}

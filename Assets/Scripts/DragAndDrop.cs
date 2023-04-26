using System.Collections;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class DragAndDrop : MonoBehaviour {
    
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
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += OnTouch;
    }

    private void OnDisable() {
        TouchSimulation.Disable();
        EnhancedTouchSupport.Disable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= OnTouch;
    } 

    private void OnTouch(Finger finger) {

        Ray ray = this.mainCamera.ScreenPointToRay(finger.screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit)) {
            if (hit.collider != null) {

                selectedObject = hit.collider.gameObject;
                selectedSlot = selectedObject.GetComponent<Slot>();
                StartCoroutine(MoveObject(selectedObject, finger));
            }
        }
    }

    // Coroutine for dragging the card
    private IEnumerator MoveObject(GameObject selectedObject, Finger finger) {

        float initialDistance = Vector3.Distance(selectedObject.transform.position, this.mainCamera.transform.position);

        while(finger.isActive) { // drag 
        
            Ray ray = this.mainCamera.ScreenPointToRay(finger.screenPosition);
            StartCardParallax(selectedObject);
            selectedObject.transform.position = Vector3.SmoothDamp(selectedObject.transform.position, ray.GetPoint(initialDistance), ref velocity, smoothTime);
            yield return waitForFixedUpdate;
        }
        // drop
        CheckOverlappingSlots(finger);
        EndCardParallax(selectedObject);

    }


    // Check if the card is overlapping with another slot
    private void CheckOverlappingSlots(Finger finger) {

        Ray ray = Camera.main.ScreenPointToRay(finger.screenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {

            Slot overlapSlot = hit.collider.GetComponent<Slot>();
            if (overlapSlot != null && overlapSlot != selectedSlot) {
                // avoiding cards overlapping opposite player's cards
                if (slotManager.GetCurrentPlayer(selectedSlot, overlapSlot) == 0) {
                    SnapBack();
                } else {
                    slotManager.Swap(selectedSlot, overlapSlot);
                }
    
            } else {
                // snapping back to previous position
                SnapBack();
            }
        }
    }

    // Parallax feature
    // when a card is being dragged, it rotates on itself
    private void StartCardParallax(GameObject selectedObject) {
        
        float maxRotationAngle = 10f;
        float rotationSpeed = 2f;

        float additionalRotation = Mathf.Sin(Time.time * rotationSpeed) * maxRotationAngle;
        Quaternion rotation = Quaternion.Euler(0, additionalRotation, 0);

        selectedObject.transform.rotation = rotation;
    }

    private void EndCardParallax(GameObject selectedObject) {
        selectedObject.transform.rotation = Quaternion.identity;
    }

    private void SnapBack() {
        
        Vector3 snapPosition = selectedSlot.GetCurrentPosition();
        if (selectedObject != null && selectedSlot != null && Vector3.Distance(selectedObject.transform.position, snapPosition) > 0.5f) {
            iTween.MoveTo(selectedObject, iTween.Hash("position", snapPosition, "time", 0.2f, "easetype", iTween.EaseType.easeOutBack));
        }

    }


}

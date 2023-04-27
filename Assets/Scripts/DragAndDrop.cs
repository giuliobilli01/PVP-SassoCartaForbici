using System.Collections;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class DragAndDrop : MonoBehaviour {
    
    [SerializeField] private float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    private Camera mainCamera;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private GameObject selectedObject;
    private Slot selectedSlot;

    [SerializeField] private SlotManager slotManager;

    private bool isDraggable = false;
    private bool parallax = false;

    private void Awake() {
        this.mainCamera = Camera.main;
    } 

    private void OnEnable() {
        EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += OnTouch;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove += MoveObject;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += CheckOverlappingSlots;
    }

    private void OnDisable() {
        EnhancedTouchSupport.Disable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= OnTouch;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove -= MoveObject;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp -= CheckOverlappingSlots;
    } 

    private void OnTouch(Finger finger) {

        //Debug.Log("Finger down at " + finger.screenPosition + " with index " + finger.index);
        Ray ray = this.mainCamera.ScreenPointToRay(finger.screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit)) {
            if (hit.collider != null) {

                this.selectedObject = hit.collider.gameObject;
                this.selectedSlot = selectedObject.GetComponent<Slot>();
                isDraggable = true;
            } else {
                isDraggable = false;
            }
        } 
    } 

    private void MoveObject(Finger finger) {

        float initialDistance = Vector3.Distance(selectedObject.transform.position, this.mainCamera.transform.position);

        if (isDraggable) {
            parallax = true;
            Ray ray = this.mainCamera.ScreenPointToRay(finger.screenPosition);
            selectedObject.transform.position = Vector3.SmoothDamp(selectedObject.transform.position, ray.GetPoint(initialDistance), ref velocity, smoothTime);
        } 
    }


    // Check if the card is overlapping with another slot
    private void CheckOverlappingSlots(Finger finger) {

        parallax = false;

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
    private void StartCardParallax() {
        
        float maxRotationAngle = 10f;
        float rotationSpeed = 2f;

        float additionalRotation = Mathf.Sin(Time.time * rotationSpeed) * maxRotationAngle;
        Quaternion rotation = Quaternion.Euler(0, additionalRotation, 0);

        this.selectedObject.transform.rotation = rotation;
    }

    private void EndCardParallax() {
        this.selectedObject.transform.rotation = Quaternion.identity;
    }

    private void SnapBack() {
        
        Vector3 snapPosition = selectedSlot.GetCurrentPosition();
        if (selectedObject != null && selectedSlot != null && Vector3.Distance(selectedObject.transform.position, snapPosition) > 0.5f) {
            iTween.MoveTo(selectedObject, iTween.Hash("position", snapPosition, "time", 0.2f, "easetype", iTween.EaseType.easeOutBack));
        }
    }

    /* void Update() {
        
        if (parallax && selectedObject != null) {
            StartCardParallax();
        } else if (!parallax && selectedObject != null) {
            EndCardParallax();
        }

    } */


}

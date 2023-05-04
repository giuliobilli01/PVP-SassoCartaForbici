using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class DragAndDrop : MonoBehaviour {

    [SerializeField] private int fingerID;
    
    [SerializeField] private float smoothTime = 0.001f;
    private Vector3 velocity = Vector3.zero;

    private Camera mainCamera;

    private GameObject selectedObject;
    private Slot selectedSlot;

    [SerializeField] private SlotManager slotManager;

    private bool isDraggable = false;

    private Vector3 minScale = new Vector3(0.08314686f, 0.08314686f, 0.08314686f);
    private Vector3 maxScale = new Vector3(0.1171851f, 0.1171851f, 0.1171851f);

    private void Awake() {
        this.mainCamera = Camera.main;
    } 

    private void Start() {
        EnhancedTouchSupport.Enable();
    }

    void OnEnable() {
        EnhancedTouchSupport.Enable();
        Touch.onFingerDown += OnTouch;
        Touch.onFingerMove += MoveObject;
        Touch.onFingerUp += CheckOverlappingSlots;
    }

    void OnDisable() {
        EnhancedTouchSupport.Disable();
        Touch.onFingerDown -= OnTouch;
        Touch.onFingerMove -= MoveObject;
        Touch.onFingerUp -= CheckOverlappingSlots;
    } 

    private void OnTouch(Finger finger) {
        
        if (finger.index != fingerID) {
            return;
        }

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

        if (finger.index != fingerID) {
            return;
        }

        float initialDistance = Vector3.Distance(selectedObject.transform.position, this.mainCamera.transform.position);

        if (isDraggable) {
            Ray ray = this.mainCamera.ScreenPointToRay(finger.screenPosition);

            Vector3 newPosition = ray.GetPoint(initialDistance);
            //newPosition.z = -0.1f;
            selectedObject.transform.position = Vector3.SmoothDamp(selectedObject.transform.position, newPosition, ref velocity, smoothTime);

            int currentPlayer = slotManager.GetCurrentPlayer(selectedSlot, selectedSlot);
            bool isMovingUp = selectedObject.transform.position.y > selectedSlot.GetCurrentPosition().y;
            bool isMovingDown = selectedObject.transform.position.y < selectedSlot.GetCurrentPosition().y;
            bool shouldScaleUp = (currentPlayer == 1 && isMovingUp) || (currentPlayer == 2 && isMovingDown);
            
            selectedObject.transform.localScale = Vector3.Lerp(selectedObject.transform.localScale, shouldScaleUp ? maxScale : minScale, 0.1f);
        } 
    }


    // Check if the card is overlapping with another slot
    private void CheckOverlappingSlots(Finger finger) {

        if (finger.index != fingerID) {
            return;
        }

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

    private void SnapBack() {
        
        Vector3 snapPosition = selectedSlot.GetCurrentPosition();
        Vector3 snapScale = selectedSlot.GetCurrentScale();

        if (selectedObject != null && selectedSlot != null && Vector3.Distance(selectedObject.transform.position, snapPosition) > 0.5f) {
            selectedObject.transform.localScale = snapScale;
            iTween.MoveTo(selectedObject, iTween.Hash("position", snapPosition, "time", 0.2f, "easetype", iTween.EaseType.easeOutBack));
        }
    }

}

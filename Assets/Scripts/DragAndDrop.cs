using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class DragAndDrop : MonoBehaviour

{
    [SerializeField] private InputAction inputAction;
    [SerializeField] private float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    private Camera mainCamera;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private GameObject selectedObject;
    private Slot selectedSlot;

    [SerializeField] private SlotManager slotManager;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
        Touch.onFingerDown += OnScreenTouch;
        Touch.onFingerUp += OnFingerUp;
    }

    private void OnDisable()
    {
        Touch.onFingerDown -= OnScreenTouch;
        Touch.onFingerUp -= OnFingerUp;
    }

    private void OnScreenTouch(Finger finger)
    {
        Ray ray = mainCamera.ScreenPointToRay(finger.screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider != null)
            {
                selectedObject = hit.collider.gameObject;
                selectedSlot = selectedObject.GetComponent<Slot>();
                StartCoroutine(MoveObject(selectedObject));
            }
        }
    }

    private void OnFingerUp(Finger finger)
    {
        if (selectedObject != null)
        {
            CheckOverlappingSlots();
            EndCardParallax(selectedObject);
        }
    }

    private IEnumerator MoveObject(GameObject selectedObject)
    {
        float initialDistance = Vector3.Distance(selectedObject.transform.position, mainCamera.transform.position);

        while (Touch.activeFingers.Count > 0 &&
               selectedObject != null &&
               selectedSlot != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Touch.activeFingers[0].screenPosition);
            StartCardParallax(selectedObject);
            selectedObject.transform.position = Vector3.SmoothDamp(selectedObject.transform.position, ray.GetPoint(initialDistance), ref velocity, smoothTime);
            yield return waitForFixedUpdate;
        }
    }

    private void CheckOverlappingSlots()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Slot overlapSlot = hit.collider.GetComponent<Slot>();
            if (overlapSlot != null && overlapSlot != selectedSlot)
            {
                if (slotManager.GetCurrentPlayer(selectedSlot, overlapSlot) == 0)
                {
                    SnapBack();
                }
                else
                {
                    slotManager.Swap(selectedSlot, overlapSlot);
                }
            }
            else
            {
                SnapBack();
            }
        }
    }

    private void StartCardParallax(GameObject selectedObject)
    {
        float maxRotationAngle = 10f;
        float rotationSpeed = 2f;
        float additionalRotation = Mathf.Sin(Time.time * rotationSpeed) * maxRotationAngle;
        Quaternion rotation = Quaternion.Euler(0, additionalRotation, 0);
        selectedObject.transform.rotation = rotation;
    }

    private void EndCardParallax(GameObject selectedObject)
    {
        selectedObject.transform.rotation = Quaternion.identity;
    }

    private void SnapBack()
    {
        Vector3 snapPosition = selectedSlot.GetCurrentPosition();
        if (selectedObject != null && selectedSlot != null && Vector3.Distance(selectedObject.transform.position, snapPosition) > 0.5f) {
            iTween.MoveTo(selectedObject, iTween.Hash("position", snapPosition, "time", 0.2f, "easetype", iTween.EaseType.easeOutBack));
        }
    }


}

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour {
    
    [SerializeField] private InputAction mouseClick;
    
    [SerializeField] private float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    private Camera mainCamera;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

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
                StartCoroutine(MoveObject(hit.collider.gameObject));
            }
        }
    }

    private IEnumerator MoveObject(GameObject selectedObject) {
        
        float initialDistance = Vector3.Distance(selectedObject.transform.position, this.mainCamera.transform.position);

        while(mouseClick.ReadValue<float>() > 0) { // drag
            
            Ray ray = this.mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            selectedObject.transform.position = Vector3.SmoothDamp(selectedObject.transform.position, ray.GetPoint(initialDistance), ref velocity, smoothTime);
            yield return waitForFixedUpdate;
        }
        // drop
        // check if the object is over another object
        if (Physics.Raycast(selectedObject.transform.position, Vector3.down, out RaycastHit hit, 1f)) {
           // if (hit.collider.gameObject != selectedObject) {

                Debug.Log("Swap");
          //  }
        } 
    }
}

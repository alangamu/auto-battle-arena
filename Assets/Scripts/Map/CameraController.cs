using AutoFantasy.Scripts.ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutoFantasy.Scripts.Map
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private GameObjectVariable _activeMapHero;
        [SerializeField]
        private Transform _viewPoint;

        delegate void UpdateFunc();
        UpdateFunc Update_CurrentFunc;
        Vector3 cameraTargetOffset;
        Vector3 lastMouseGroundPlanePosition;
        Vector3 lastMousePosition;  // From Input.mousePosition
        int mouseDragThreshold = 1; // Threshold of mouse movement to start a drag

        private void OnEnable()
        {
            _activeMapHero.OnValueChanged += NewActiveMapHero;
        }

        private void OnDisable()
        {
            _activeMapHero.OnValueChanged -= NewActiveMapHero;
        }

        private void NewActiveMapHero(GameObject obj)
        {
            CenterOnObject(obj.transform);
        }

        public void CancelUpdateFunc()
        {
            Update_CurrentFunc = Update_DetectModeStart;

            // Also do cleanup of any UI stuff associated with modes.
            //hexPath = null;
        }

        private void Start()
        {
            Update_CurrentFunc = Update_DetectModeStart;
        }

        private void Update()
        {
            Update_CurrentFunc();
            Update_ScrollZoom();
            lastMousePosition = Input.mousePosition;
        }

        private Vector3 MouseToGroundPlane(Vector3 mousePos)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);
            // What is the point at which the mouse ray intersects Y=0
            if (mouseRay.direction.y >= 0)
            {
                //Debug.LogError("Why is mouse pointing up?");
                return Vector3.zero;
            }
            float rayLength = (mouseRay.origin.y / mouseRay.direction.y);
            return mouseRay.origin - (mouseRay.direction * rayLength);
        }

        private void Update_DetectModeStart()
        {
            // Check here(?) to see if we are over a UI element,
            // if so -- ignore mouse clicks and such.
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // TODO: Do we want to ignore ALL GUI objects?  Consider
                // things like unit health bars, resource icons, etc...
                // Although, if those are set to NotInteractive or Not Block
                // Raycasts, maybe this will return false for them anyway.
                return;
            }

            //if (Input.GetMouseButtonDown(0))
            //{
            //    // Left mouse button just went down.
            //    // This doesn't do anything by itself, really.
            //    Debug.Log("MOUSE DOWN");
            //}
            //else if (Input.GetMouseButtonUp(0))
            //{
            //    Debug.Log("MOUSE UP -- click!");

            //    // TODO: Are we clicking on a hex with a unit?
            //    //          If so, select it

            //    //Unit[] us = hexUnderMouse.Units;

            //    // TODO: Implement cycling through multiple units in the same tile

            //    //if (us.Length > 0)
            //    //{
            //    //    selectionController.SelectedUnit = us[0];

            //    //    // NOTE: Selecting a unit does NOT change our mouse mode

            //    //    //Update_CurrentFunc = Update_UnitMovement;
            //    //}

            //}
            ////else if (selectionController.SelectedUnit != null && Input.GetMouseButtonDown(1))
            ////{
            ////    // We have a selected unit, and we've pushed down the right
            ////    // mouse button, so enter unit movement mode.
            ////    Update_CurrentFunc = Update_UnitMovement;

            ////}
            //else 
            if (Input.GetMouseButton(0) &&
                Vector3.Distance(Input.mousePosition, lastMousePosition) > mouseDragThreshold)
            {
                // Left button is being held down AND the mouse moved? That's a camera drag!
                Update_CurrentFunc = Update_CameraDrag;
                lastMouseGroundPlanePosition = MouseToGroundPlane(Input.mousePosition);
                Update_CurrentFunc();
            }
            //else if (selectionController.SelectedUnit != null && Input.GetMouseButton(1))
            //{
            //    // We have a selected unit, and we are holding down the mouse
            //    // button.  We are in unit movement mode -- show a path from
            //    // unit to mouse position via the pathfinding system.
            //}
        }

        private void Update_CameraDrag()
        {
            if (Input.GetMouseButtonUp(0))
            {
                CancelUpdateFunc();
                return;
            }

            Vector3 hitPos = MouseToGroundPlane(Input.mousePosition);

            Vector3 diff = lastMouseGroundPlanePosition - hitPos;
            _viewPoint.Translate(diff, Space.World);

            lastMouseGroundPlanePosition = hitPos = MouseToGroundPlane(Input.mousePosition);
        }

        private void Update_ScrollZoom()
        {
            // Zoom to scrollwheel
            float scrollAmount = Input.GetAxis("Mouse ScrollWheel");
            float minHeight = 10;
            float maxHeight = 30;
            // Move camera towards hitPos
            Vector3 hitPos = MouseToGroundPlane(Input.mousePosition);
            Vector3 dir = hitPos - Camera.main.transform.position;

            Vector3 p = Camera.main.transform.position;

            // Stop zooming out at a certain distance.
            // TODO: Maybe you should still slide around at 20 zoom?
            if (scrollAmount > 0 || p.y < (maxHeight - 0.1f))
            {
                cameraTargetOffset += dir * scrollAmount;
            }
            Vector3 lastCameraPosition = Camera.main.transform.position;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Camera.main.transform.position + cameraTargetOffset, Time.deltaTime * 5f);
            cameraTargetOffset -= Camera.main.transform.position - lastCameraPosition;


            p = Camera.main.transform.position;
            if (p.y < minHeight)
            {
                p.y = minHeight;
            }
            if (p.y > maxHeight)
            {
                p.y = maxHeight;
            }
            Camera.main.transform.position = p;

            // Change camera angle
            Camera.main.transform.rotation = Quaternion.Euler(
                Mathf.Lerp(30, 75, Camera.main.transform.position.y / maxHeight),
                Camera.main.transform.rotation.eulerAngles.y,
                Camera.main.transform.rotation.eulerAngles.z
            );
        }

        private void CenterOnObject(Transform target)
        {
            _viewPoint.position = target.position;
        }
    }
}

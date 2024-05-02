using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 Origin;
    private Vector3 Difference;

    private bool drag = false;
    [SerializeField] private float x_min, y_min, x_max, y_max;
    [SerializeField] private Transform cameraBox;
    Camera mainCamera;

    private void Start()
    {
        mainCamera = CoreManager.Instance.GameplayCamera;

        float baseRatio = 1080 / 1920f;
        float currentRatio = Screen.width / (Screen.height * 1f);
        cameraBox.localScale = new Vector3(currentRatio / baseRatio, 1, 1);
        float cameraVerticalSize = mainCamera.orthographicSize;
        float unit_size = Screen.height / (cameraVerticalSize * 2);
        x_min =  - GameConstant.TileSize / 2;
        y_min =  - GameConstant.TileSize / 2;
    }

    public void SetMaxPosition(int width, int height)
    {
        float cameraVerticalSize = mainCamera.orthographicSize;
        float unit_size = Screen.height / cameraVerticalSize;

        x_max = width * GameConstant.TileSize - GameConstant.TileSize / 2;
        y_max = height * GameConstant.TileSize - GameConstant.TileSize / 2;
    }

    private void Update()
    {
        if (!InputManager.Instance.IsBlockInput)
        {
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;

                Zoom(difference * 0.01f);
            }
            else if (Input.GetMouseButton(0))
            {
                Difference = (mainCamera.ScreenToWorldPoint(Input.mousePosition)) - transform.position;
                if (drag == false)
                {
                    drag = true;
                    Origin = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                }

            }
            else
            {
                drag = false;
            }

            if (drag)
            {
                transform.position = Origin - Difference;
            }

            float x = Mathf.Clamp(transform.position.x, x_min, x_max);
            float y = Mathf.Clamp(transform.position.y, y_min, y_max);
            transform.position = new Vector3(x, y, -10);
        }
    }

    void Zoom(float zoomValue)
    {
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize - zoomValue, 5, 15);
    }
}


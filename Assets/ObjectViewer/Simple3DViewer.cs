using UnityEngine;
using UnityEngine.EventSystems;

public class Simple3DViewer : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 5f;
    public float movementSpeed = 0.1f;
    public float zoomSpeed = 1f;
    public bool ifDimensionShown = false;

    public GameObject dimSide_left_1, dimSide_left_2, dimSide_right_1, dimSide_right_2, dimUpper_left, dimUpper_right;

    private Vector3 lastMousePosition;

    private void Start()
    {
        if (target == null)
        {
            target = transform; // ���� ��ũ��Ʈ�� ����� ������Ʈ�� Ÿ������ ����
        }
    }

    private void Update()
    {

        if (ifDimensionShown)
            SetVisibleObject(transform.localEulerAngles.x, transform.localEulerAngles.y - 20f);

        // ���콺 �Է��� �ޱ� ���� Event System���� ���� �Է��� ���� (UIŬ���� ���� �ȵǰ�)
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // ���콺 ���� ��ư�� ó�� ���� ������ ȸ�� �����ϵ��� ����
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        // ���콺 ���� ��ư�� ���� ���¿���'��' ȸ���� �����ϵ���
        if (Input.GetMouseButton(0))
        {
            float deltaX = Input.mousePosition.x - lastMousePosition.x;
            float deltaY = Input.mousePosition.y - lastMousePosition.y;

            // ī�޶� Ÿ�� ������ ȸ��
            transform.RotateAround(target.position, Vector3.up, deltaX * rotationSpeed);
            transform.RotateAround(target.position, transform.right, -deltaY * rotationSpeed);

            //Debug.Log($"deltaX: {deltaX}, deltaY: {deltaY}");
        }

        // ���콺 ������ ��ư �巡�׷� ī�޶� �̵�
        if (Input.GetMouseButton(1))
        {
            float deltaX = Input.mousePosition.x - lastMousePosition.x;
            float deltaY = Input.mousePosition.y - lastMousePosition.y;

            // ī�޶� �����ʰ� ���� �������� �̵�
            transform.Translate(-deltaX * movementSpeed, -deltaY * movementSpeed, 0);
        }


        // ���콺 �ٷ� �� ��/�ƿ�
        float zoomDelta = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(0, 0, zoomDelta * zoomSpeed, Space.Self);

        lastMousePosition = Input.mousePosition;
    }



    public void SetVisibleObject(float angleX, float angleY)
    {
        angleY = Mathf.Repeat(angleY, 360f); // angleY ���� 0���� 360 ������ ������ �ݺ���Ŵ

        if ((angleY >= 0f && angleY < 45f) || (angleY >= 180f && angleY <= 225f))
        {
            SetObjectVisibility(dimSide_left_1, true);
            SetObjectVisibility(dimSide_left_2, false);
            SetObjectVisibility(dimSide_right_1, false);
            SetObjectVisibility(dimSide_right_2, false);
        }
        else if ((angleY >= 45f && angleY < 90f) || (angleY >= 225f && angleY <= 270f))
        {
            SetObjectVisibility(dimSide_left_1, false);
            SetObjectVisibility(dimSide_left_2, true);
            SetObjectVisibility(dimSide_right_1, false);
            SetObjectVisibility(dimSide_right_2, false);
        }
        else if ((angleY >= 90f && angleY < 135f) || (angleY >= 270f && angleY <= 315f))
        {
            SetObjectVisibility(dimSide_left_1, false);
            SetObjectVisibility(dimSide_left_2, false);
            SetObjectVisibility(dimSide_right_1, false);
            SetObjectVisibility(dimSide_right_2, true);
        }
        else if ((angleY >= 135f && angleY <= 180f) || (angleY >= 315f && angleY <= 360f))
        {
            SetObjectVisibility(dimSide_left_1, false);
            SetObjectVisibility(dimSide_left_2, false);
            SetObjectVisibility(dimSide_right_1, true);
            SetObjectVisibility(dimSide_right_2, false);
        }
        else
        {
            Debug.Log($"X : {transform.localEulerAngles.x}, Y : {transform.localEulerAngles.y - 20f}");
        }

        if (angleY >= 0f && angleY < 90f)
        {
            if (angleX >= 0f && angleX < 180f)
            {
                SetObjectVisibility(dimUpper_left, false);
                SetObjectVisibility(dimUpper_right, true);
            }
            else if (angleX >= 180f && angleX < 360f)
            {
                SetObjectVisibility(dimUpper_left, true);
                SetObjectVisibility(dimUpper_right, false);
            }
        }
        else if (angleY >= 90f && angleY < 180f)
        {
            SetObjectVisibility(dimUpper_left, true);
            SetObjectVisibility(dimUpper_right, false);
        }
        else if (angleY >= 270f && angleY < 360f)
        {
            if (angleX >= 0f && angleX < 180f)
            {
                SetObjectVisibility(dimUpper_left, true);
                SetObjectVisibility(dimUpper_right, false);
            }
            else if (angleX >= 180f && angleX < 360f)
            {
                SetObjectVisibility(dimUpper_left, false);
                SetObjectVisibility(dimUpper_right, true);
            }
        }
        else if (angleY >= 180f && angleY < 270f)
        {
            SetObjectVisibility(dimUpper_left, false);
            SetObjectVisibility(dimUpper_right, true);
        }



        //if (angleY >= 0f && angleY < 180f)
        //{
        //    if (angleX >= 0f && angleX < 180f)
        //    {
        //        Debug.Log("1-1");
        //        SetObjectVisibility(dimUpper_left, false);
        //        SetObjectVisibility(dimUpper_right, true);
        //    }
        //    else if (angleX >= 180f && angleX < 360f)
        //    {
        //        Debug.Log("1-2");

        //        SetObjectVisibility(dimUpper_left, true);
        //        SetObjectVisibility(dimUpper_right, false);
        //    }

        //}
        //else if (angleY >= 180f && angleY < 360f)
        //{
        //    if (angleX >= 0f && angleX < 180f)
        //    {
        //        Debug.Log("2-1");
        //        SetObjectVisibility(dimUpper_left, true);
        //        SetObjectVisibility(dimUpper_right, false);
        //    }
        //    else if (angleX >= 180f && angleX < 360f)
        //    {
        //        Debug.Log("2-2");

        //        SetObjectVisibility(dimUpper_left, false);
        //        SetObjectVisibility(dimUpper_right, true);
        //    }
        //}
    }


    private void SetObjectVisibility(GameObject obj, bool visible)
    {
        obj.SetActive(visible);
    }

    public void ResetCam()
    {
        transform.position = new Vector3(0f, 0.86f, -3.42f);
        transform.rotation = Quaternion.Euler(15f, 0f, 0f);
    }
}
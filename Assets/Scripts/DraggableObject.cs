using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public BoardGenerator boardGenerator; // ��������������

    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        // ��λ�����������̱߽���
        cursorPosition.x = Mathf.Clamp(Mathf.Round(cursorPosition.x), 0, boardGenerator.GetBoardSize().x - 1);
        cursorPosition.z = Mathf.Clamp(Mathf.Round(cursorPosition.z), 0, boardGenerator.GetBoardSize().y - 1);
        cursorPosition.y = 1f;
        transform.position = cursorPosition;
    }
}

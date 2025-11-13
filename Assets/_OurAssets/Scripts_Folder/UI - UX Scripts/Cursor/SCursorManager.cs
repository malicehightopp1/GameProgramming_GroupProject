using UnityEngine;
using UnityEngine.EventSystems;

public class SCursorManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Texture2D mDefaultCursor;
    [SerializeField] private Texture2D mPointerCursor;
    private Vector2 mHotSpot = Vector2.zero;
    private void Start()
    {
        Cursor.SetCursor(mDefaultCursor, mHotSpot, CursorMode.Auto);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(mPointerCursor, mHotSpot, CursorMode.Auto);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(mDefaultCursor, mHotSpot, CursorMode.Auto);
    }
}

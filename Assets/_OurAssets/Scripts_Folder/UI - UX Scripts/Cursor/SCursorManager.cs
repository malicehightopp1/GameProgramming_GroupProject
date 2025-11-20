using UnityEngine;
using UnityEngine.EventSystems;

public class SCursorManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler //managing cursor changes on UI hover
{
    [SerializeField] private Texture2D mDefaultCursor;
    [SerializeField] private Texture2D mPointerCursor;
    private Vector2 mHotSpot = Vector2.zero; //setting hotspot to top left of cursor
    private void Start() //initial cursor setup
    {
        Cursor.SetCursor(mDefaultCursor, mHotSpot, CursorMode.Auto);
    }
    public void OnPointerEnter(PointerEventData eventData) //changing cursor when hovering over UI elements
    {
        Cursor.SetCursor(mPointerCursor, mHotSpot, CursorMode.Auto);
    }
    public void OnPointerExit(PointerEventData eventData) //changing cursor back to default when not hovering over UI elements
    {
        Cursor.SetCursor(mDefaultCursor, mHotSpot, CursorMode.Auto);
    }
}

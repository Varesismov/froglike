using UnityEngine;
using UnityEngine.EventSystems;

public class TestDropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("DROP ZONE ZADZIA£A£O!");
    }
}

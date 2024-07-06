using UnityEngine;
using UnityEngine.EventSystems;
public class DropZone : MonoBehaviour, IDropHandler
{
    public UIAlbums inventory;
    public int Size;
    public int Offset{get { return inventory.offset * inventory.SizePerPage; }}
    public int ChildCount { get { return GetComponentsInChildren<Draggable>(false).Length; } }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent<Draggable>(out var drag))
        {
            UIAlbums newInventory = drag.drop.inventory;
            var size = Size - Offset > inventory.SizePerPage ? inventory.SizePerPage : Size - Offset;

            if (drag.Zone_parent == transform)
            {
                newInventory.Delete(drag);
                Debug.Log(drag.Index + " " + Offset);
                inventory.Insert(drag, this, drag.Index + Offset);
            }
            else if (ChildCount >= size)
            {
                int prevIndex = drag.Index;
                drag.Index = FindIndexPosition(drag, size);
                Transform replacedChild = transform.GetChild(drag.Index);

                Draggable replacedDrag = replacedChild.GetComponent<Draggable>();

                newInventory.Delete(drag);
                inventory.Delete(replacedDrag);

                newInventory.Insert(replacedDrag, drag.drop, prevIndex + drag.drop.Offset);
                inventory.Insert(drag, this, drag.Index + Offset);
            }
            else
            {
                newInventory.Delete(drag);
                inventory.Insert(drag, this);
            }
            inventory.Refresh();
        }
    }
    public int FindIndexPosition(Draggable drag, int size)
    {
        float dragposition = drag.transform.position.x;
        float minDistance = Difference(transform.GetChild(0).position.x, dragposition);
        int count = size, j = 0;
        while (j + 1 < count && minDistance > Difference(transform.GetChild(j + 1).position.x, dragposition))
        {
            minDistance = Difference(transform.GetChild(j + 1).position.x, dragposition);
            j++;
        }
        return j;
    }
    public float Difference(float a, float b)
    {
        return Mathf.Abs(Mathf.Abs(a) - Mathf.Abs(b));
    }
}

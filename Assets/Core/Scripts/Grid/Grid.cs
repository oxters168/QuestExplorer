using UnityEngine;
using UnityHelpers;

public class Grid : MonoBehaviour
{
    public float itemSize = 0.2f;
    public float padding = 0.1f;
    public int gridSize = 3;
    private GridItemData[] items;
    
    public void SetData(params GridItemData[] data)
    {
        items = (GridItemData[])data.Clone();
        Refresh();
    }
    private void Refresh()
    {
        var itemsPool = PoolManager.GetPool("Items");
        itemsPool.ReturnAll();
        itemsPool.SetParent(transform);
        for (int i = 0; i < items.Length; i++)
        {
            Transform item = itemsPool.Get();
            int colIndex = i % gridSize;
            int rowIndex = i / gridSize;
            float startHor = -(itemSize * gridSize + padding * (gridSize - 1)) / 2 + itemSize / 2;
            float startVer = (itemSize * gridSize + padding * (gridSize - 1)) / 2 - itemSize  / 2;
            item.localPosition = new Vector3(startHor + colIndex * (itemSize + padding), 0, startVer - rowIndex * (itemSize + padding));

            var gridItem = item.GetComponent<IGridItem>();
            gridItem.SetSize(itemSize);
            gridItem.SetData(items[i]);
        }
    }
}

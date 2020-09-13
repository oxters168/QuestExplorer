using UnityEngine;
using UnityHelpers;

public class Grid : MonoBehaviour
{
    public float itemSize = 0.2f;
    public float padding = 0.1f;
    public int gridSizeX = 3;
    public int gridSizeY = 3;
    private int page = 0;
    private GridItemData[] items;
    
    public Transform[] SetData(params GridItemData[] data)
    {
        items = (GridItemData[])data.Clone();
        page = 0;
        return Refresh();
    }
    public int GetPageCount()
    {
        int itemsCount = items != null ? items.Length : 0;
        int itemsPerPage = gridSizeX * gridSizeY;
        int totalPages = itemsCount / itemsPerPage;
        if (itemsCount % itemsPerPage > 0)
            totalPages++;
        return totalPages;
    }
    public void SetPage(int index)
    {
        page = Mathf.Clamp(index, 0, GetPageCount() - 1);
        Refresh();
    }
    public int GetPage()
    {
        return page;
    }
    public void NextPage()
    {
        SetPage(page + 1);
        Refresh();
    }
    public void PrevPage()
    {
        SetPage(page - 1);
        Refresh();
    }
    private Transform[] Refresh()
    {
        var itemsPool = PoolManager.GetPool("Items");
        itemsPool.ReturnAll();
        //itemsPool.SetParent(transform);
        int itemsCount = items != null ? items.Length : 0;
        Transform[] itemTransforms = new Transform[itemsCount];
        int itemsPerPage = gridSizeX * gridSizeY;
        int startIndex = itemsPerPage * page;
        for (int i = startIndex; i < Mathf.Min(startIndex + itemsPerPage, itemsCount); i++)
        {
            Transform item = itemsPool.Get();
            item.SetParent(transform);
            int colIndex = (i - startIndex) % gridSizeX;
            int rowIndex = (i - startIndex) / gridSizeY;
            float startHor = -(itemSize * gridSizeX + padding * (gridSizeX - 1)) / 2 + itemSize / 2;
            float startVer = (itemSize * gridSizeY + padding * (gridSizeY - 1)) / 2 - itemSize  / 2;
            item.localPosition = new Vector3(startHor + colIndex * (itemSize + padding), 0, startVer - rowIndex * (itemSize + padding));
            itemTransforms[i] = item;

            var gridItem = item.GetComponent<IGridItem>();
            gridItem.SetSize(itemSize);
            gridItem.SetData(items[i]);
        }

        return itemTransforms;
    }
}

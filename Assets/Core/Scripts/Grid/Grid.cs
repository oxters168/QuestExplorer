using UnityEngine;
using UnityHelpers;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    public Transform gridItemPrefab;
    private ObjectPool<Transform> GridItemsPool { get { if (_gridItemsPool == null) _gridItemsPool = new ObjectPool<Transform>(gridItemPrefab, 5, false, true, transform); return _gridItemsPool; } }
    private ObjectPool<Transform> _gridItemsPool;
    public float itemSize = 0.2f;
    public float padding = 0.1f;
    public int gridSizeX = 3;
    public int gridSizeY = 3;
    public float itemsPosY = 0.1f;
    private int page = 0;
    private GridItemData[] items;
    private List<Transform> itemAnchors = new List<Transform>();
    
    public void SetData(int pageNum, params GridItemData[] data)
    {
        items = (GridItemData[])data.Clone();
        SetPage(pageNum);
    }
    public void SetData(params GridItemData[] data)
    {
        SetData(0, data);
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
        page = Mathf.Clamp(index, 0, Mathf.Clamp(GetPageCount() - 1, 0, int.MaxValue));
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
    private void Refresh()
    {
        GridItemsPool.ReturnAll();
        int itemsCount = items != null ? items.Length : 0;
        int itemsPerPage = gridSizeX * gridSizeY;
        int startIndex = itemsPerPage * page;

        int anchorAdditions = itemsPerPage - itemAnchors.Count;
        for (int i = 0; i < anchorAdditions; i++)
        {
            var currentAnchor = new GameObject("Anchor");
            currentAnchor.transform.SetParent(transform);
            itemAnchors.Add(currentAnchor.transform);
        }
        for (int i = startIndex; i < Mathf.Min(startIndex + itemsPerPage, itemsCount); i++)
        {
            Transform item = GridItemsPool.Get();
            item.SetParent(transform);
            int properIndex = (i - startIndex);
            int colIndex = 0;
            int rowIndex = 0;
            if (gridSizeX > 1 && gridSizeY > 1)
            {
                colIndex = properIndex % gridSizeX;
                rowIndex = properIndex / gridSizeY;
            }
            else if (gridSizeX <= 1)
            {
                rowIndex = properIndex;
            }
            else
            {
                colIndex = properIndex;
            }

            float startHor = -(itemSize * gridSizeX + padding * (gridSizeX - 1)) / 2 + itemSize / 2;
            float startVer = (itemSize * gridSizeY + padding * (gridSizeY - 1)) / 2 - itemSize  / 2;
            var gridPos = new Vector3(startHor + colIndex * (itemSize + padding), itemsPosY, startVer - rowIndex * (itemSize + padding));
            var anchor = itemAnchors[properIndex];
            anchor.localPosition = gridPos;
            anchor.localRotation = Quaternion.identity;
            item.localPosition = gridPos;
            var mimic = item.GetComponentInParent<MimicTransform>();
            mimic.other = anchor;

            var gridItem = item.GetComponent<IGridItem>();
            gridItem.SetSize(itemSize);
            gridItem.SetData(items[i]);
        }
    }
}

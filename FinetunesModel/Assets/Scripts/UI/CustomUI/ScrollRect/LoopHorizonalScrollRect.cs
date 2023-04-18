using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class LoopHorizonalScrollRect : LoopScrollRect, IDragHandler
{
    public List<LoopEntry> EntryList { get { return _entryList; } }
    public Action HandleDragBegin = delegate { };
    public Action HandleDragEnd = delegate { };
    public Action<bool> HandleDragging = delegate { };

    public int DataCount { get { return _dataCount; } }

    public void Init(int size, GameObject prefab)
    {
        _size = size;
        _gridLayout = content.GetComponent<GridLayoutGroup> ();
        _horizoanlLayout = content.GetComponent<HorizontalLayoutGroup> ();

        _startIndex = 0;
        _entryList = new List<LoopEntry> ();
        int bufferSize = _bufferSize;
        if (_gridLayout != null)
        {
            bufferSize = _bufferSize * _gridLayout.constraintCount;
        }
        for (int i = 0; i < _size + bufferSize; i++)
        {
            ApendEntry (i, prefab);
        }
    }

    public void DestroyEntry()
    {
        for (int i = 0; i < _entryList.Count; i++)
        {
            Destroy(_entryList[i].gameObject);
        }
    }

    public void UpdateData(int dataCount)
    {
        _dataCount = dataCount;
        Refresh ();
    }

    public LoopEntry InitStartIndex(int dataCount, int startIndex)
    {
        content.anchoredPosition = new Vector2(0, content.anchoredPosition.y);

        _dataCount = dataCount;

        _startIndex = startIndex;
        _endIndex = (_startIndex + _entryList.Count - 1) % _dataCount;

        for (int i = 0; i < _entryList.Count; i++)
        {
            _entryList[i].gameObject.SetActive (true);
        }

        if (_dataCount < _entryList.Count && !EnableDataLoop)
        {
            for (int i = 0; i < _entryList.Count; i++)
            {
                _entryList[i].gameObject.SetActive(i < _dataCount);
            }
        }

        if (_dataCount > 0)
        {
            for (int i = 0; i < _entryList.Count; i++)
            {
                _entryList[i].UpdateIndex ((_startIndex + i) % _dataCount);
            }
        }

        UpdateRealBounds ();

        return _entryList[1];
    }

    public void Refresh()
    {
        StartCoroutine(DelayStartMovement());

        _endIndex = _startIndex + _entryList.Count - 1;
        _endIndex = Mathf.Clamp (_endIndex, 0, _dataCount - 1);
        _startIndex = Mathf.Max(_endIndex - _entryList.Count + 1, 0);

        for (int i = 0; i < _entryList.Count; i++)
        {
            _entryList[i].gameObject.SetActive (true);
        }

        if (_dataCount < _entryList.Count && !EnableDataLoop)
        {
            for (int i = 0; i < _entryList.Count; i++)
            {
                _entryList[i].gameObject.SetActive(i < _dataCount);
            }
        }

        if (_dataCount > 0)
        {
            for (int i = 0; i <= (_endIndex - _startIndex); i++)
            {
                _entryList[i].UpdateIndex (_startIndex + i);
            }
        }

        UpdateRealBounds ();
    }

    private System.Collections.IEnumerator DelayStartMovement()
    {
        yield return null;
        canMovement = true;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        HandleDragBegin();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        HandleDragEnd();
    }

    protected override void SetLoopNormalPosition(float newPosition)
    {
        if (_entryList == null || _entryList.Count == 0)
        {
            return;
        }

        float size = GetSize (_entryList [0]);
        _startIndex = Mathf.CeilToInt(-newPosition / size);
        _endIndex = _startIndex + _entryList.Count - 1;
        _endIndex = Mathf.Clamp (_endIndex, 0, _dataCount - 1);
        _startIndex = Mathf.Max(_endIndex - _entryList.Count + 1, 0);

        if (_dataCount > 0)
        {
            for (int i = 0; i <= (_endIndex - _startIndex); i++)
            {
                _entryList[i].UpdateIndex (_startIndex + i);
            }
        }

        ForceSetContentPosition(new Vector2(
            newPosition + _startIndex * size,
            content.anchoredPosition.y));

        UpdateRealBounds ();
    }

    private void Update()
    {
        if (_dataCount == 0)
        {
            return;
        }
        if (NeedMoveStartToEnd ())
        {
            MoveStartToEnd ();
        }
        else if (NeedMoveEndToStart ())
        {
            MoveEndToStart ();
        }
    }

    private void MoveStartToEnd()
    {
        if (_gridLayout != null) {
            for (int i = 0; i < _gridLayout.constraintCount; i++) {
                LoopEntry entry = _entryList [i];
                entry.transform.SetAsLastSibling ();
                _entryList.Remove (entry);
                _entryList.Add (entry);
                if (!EnableDataLoop && _endIndex == _dataCount - 1)
                {
                    MoveDataIndexDown();
                    entry.gameObject.SetActive (false);
                }
                else
                {
                    MoveDataIndexDown();
                    entry.UpdateIndex(_endIndex);
                }
            }

            ForceSetContentPosition(new Vector2(
                content.anchoredPosition.x + GetSize(_entryList[0]), 
                content.anchoredPosition.y));
        }
        else
        {
            LoopEntry startEntry = _entryList [0];
            startEntry.transform.SetAsLastSibling ();
            _entryList.Remove(startEntry);
            _entryList.Add(startEntry);

            ForceSetContentPosition(new Vector2(
                content.anchoredPosition.x + GetSize(startEntry),
                content.anchoredPosition.y));
            MoveDataIndexDown();
            startEntry.UpdateIndex(_endIndex);
        }

        UpdateRealBounds ();
    }

    private void MoveEndToStart()
    {
        if (_gridLayout != null) {
            for (int i = 0; i < _gridLayout.constraintCount; i++) {
                LoopEntry entry = _entryList [_entryList.Count - 1];
                entry.transform.SetAsFirstSibling ();
                _entryList.Remove (entry);
                _entryList.Insert (0, entry);

                MoveDataIndexUp ();
            }
            Refresh ();
            ForceSetContentPosition(new Vector2(
                content.anchoredPosition.x - GetSize(_entryList[0]),
                content.anchoredPosition.y));
        } else {
            LoopEntry endEntry = _entryList [_entryList.Count - 1];
            endEntry.transform.SetAsFirstSibling ();
            _entryList.Remove(endEntry);
            _entryList.Insert(0, endEntry);

            ForceSetContentPosition(new Vector2(
                content.anchoredPosition.x - GetSize(endEntry),
                content.anchoredPosition.y));
            MoveDataIndexUp();
            endEntry.UpdateIndex(_startIndex);
        }

        UpdateRealBounds ();
    }

    private void MoveDataIndexDown()
    {
        if (EnableDataLoop && _endIndex == _dataCount - 1)
        {
            _endIndex = 0;
        }
        else
        {
            _endIndex++;
        }
        if (EnableDataLoop && _startIndex == _dataCount - 1)
        {
            _startIndex = 0;
        }
        else
        {
            _startIndex++;
        }
    }

    private void MoveDataIndexUp()
    {
        if (EnableDataLoop && _endIndex == 0)
        {
            _endIndex = _dataCount - 1;
        }
        else
        {
            _endIndex--;
        }
        if (EnableDataLoop && _startIndex == 0)
        {
            _startIndex = _dataCount - 1;
        }
        else
        {
            _startIndex--;
        }
    }

    private void UpdateRealBounds()
    {
        float startDelta = 0;
        float endDelta = 0;
        if (_gridLayout != null)
        {
            startDelta = (_startIndex / _gridLayout.constraintCount) * GetSize(_entryList[0]);
            endDelta = ((_dataCount - _endIndex) / _gridLayout.constraintCount) * GetSize (_entryList[0]);
        }
        else
        {
            startDelta = _startIndex * GetSize(_entryList[0]);
            endDelta = (_dataCount - 1 - _endIndex) * GetSize (_entryList[0]);
        }

        UpdateRealContentBoundsInfo (new Vector3 (startDelta, 0, 0), new Vector3 (endDelta, 0, 0));
    }

    private bool NeedMoveStartToEnd()
    {
        if (content.anchoredPosition.x <
            -2 * GetSize (_entryList [_startIndex % _entryList.Count]))
        {
            return EnableDataLoop || (_endIndex < _dataCount - 1);
        }

        return false;
    }

    private bool NeedMoveEndToStart()
    {
        if (content.anchoredPosition.x
            > -GetSize(_entryList[_endIndex % _entryList.Count]))
        {
            if (EnableDataLoop) {
                return true;
            }
            else
            {
                if (_gridLayout != null)
                {
                    return _startIndex / _gridLayout.constraintCount > 0;
                }
                else
                {
                    return _startIndex > 0;
                }
            }
        }
        return false;
    }

    private void ApendEntry(int index, GameObject prefab)
    {
        GameObject gameObject = GameObject.Instantiate (prefab);
        gameObject.transform.SetParent (content.transform);
        gameObject.transform.SetLocalPositionZ(0f);
        gameObject.transform.localScale = Vector3.one;
        gameObject.name = index.ToString();
        _entryList.Add (gameObject.GetComponent<LoopEntry> ());
    }

    private float GetSize (LoopEntry entry)
    {
        float size = 0;
        if (_gridLayout != null)
        {
            size = _gridLayout.cellSize.x + _gridLayout.spacing.x;
        }
        else
        {
            size = entry.GetComponent<LayoutElement>().preferredWidth + _horizoanlLayout.spacing;
        }

        return size;
    }

    private float GetSpacing()
    {
        if (_gridLayout != null)
        {
            return _gridLayout.spacing.x;
        }
        else
        {
            return _horizoanlLayout.spacing;
        }
    }

    private GridLayoutGroup _gridLayout;
    private HorizontalLayoutGroup _horizoanlLayout;

    private List<LoopEntry> _entryList;
    private int _dataCount;
    private int _startIndex;
    private int _endIndex;

    private const int _bufferSize = 3;
    private int _size;
    private bool _flag;
}

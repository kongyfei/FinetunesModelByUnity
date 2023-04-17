using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class LoopVerticalScrollRect : LoopScrollRect, IDragHandler
{
    public bool HaveInited { get { return _haveInited; } }

    public List<LoopEntry> EntryList { get { return _entryList; } }
    public Action HandleDragBegin = delegate { };
    public Action HandleDragEnd = delegate { };
    public Action<bool> HandleDragging = delegate { };

    public int DataCount { get { return _dataCount; } }

    public bool IsDragUp { get { return _isDragUp; } }

    public void Init(int size, GameObject prefab)
    {
        _size = size;
        _gridLayout = content.GetComponent<GridLayoutGroup>();
        _verticalLayout = content.GetComponent<VerticalLayoutGroup>();

        _startIndex = 0;
        _entryList = new List<LoopEntry>();

        int bufferSize = _bufferSize;
        if (_gridLayout != null)
        {
            bufferSize = _bufferSize * _gridLayout.constraintCount;
        }
        for (int i = 0; i < _size + bufferSize; i++)
        {
            ApendEntry(i, prefab);
        }
        _haveInited = true;
    }

    public void DestroyEntry()
    {
        if (_entryList == null) return;
        for (int i = 0; i < _entryList.Count; i++)
        {
            Destroy(_entryList[i].gameObject);
        }
        _entryList.Clear();
    }

    public void UpdateData(int dataCount)
    {
        _dataCount = dataCount;
        Refresh();
    }

    public LoopEntry InitStartIndex(int dataCount, int startIndex)
    {
        _isStart = true;
        content.anchoredPosition = new Vector2(content.anchoredPosition.x, 0);

        _dataCount = dataCount;

        _startIndex = startIndex;
        _endIndex = (_startIndex + _entryList.Count - 1) % _dataCount;

        for (int i = 0; i < _entryList.Count; i++)
        {
            _entryList[i].gameObject.SetActive(true);
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
                _entryList[i].UpdateIndex((_startIndex + i) % _dataCount);
            }
        }

        UpdateRealBounds();

        return _entryList[1];
    }

    public void Refresh()
    {
        StartCoroutine(DelayStartMovement());
        content.anchoredPosition = new Vector2(content.anchoredPosition.x, 0);
        _endIndex = _startIndex + _entryList.Count - 1;
        _endIndex = Mathf.Clamp(_endIndex, 0, _dataCount - 1);
        _startIndex = Mathf.Max(_endIndex - _entryList.Count + 1, 0);

        for (int i = 0; i < _entryList.Count; i++)
        {
            _entryList[i].gameObject.SetActive(true);
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
                _entryList[i].UpdateIndex(_startIndex + i);
            }
        }

        UpdateRealBounds();
    }

    private System.Collections.IEnumerator DelayStartMovement()
    {
        yield return null;
        canMovement = true;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        _isStart = false;
        _isDragUp = eventData.delta.y > 0 ? true : false;

        base.OnBeginDrag(eventData);
        _beginPosition = eventData.position;
        HandleDragBegin();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        _isDragUp = eventData.delta.y > 0 ? true : false;
        if (verticalNormalizedPosition < 0 && _entryList != null && _entryList.Count > 0)
        {
            float height = -verticalNormalizedPosition * (_dataCount / _entryList.Count);
            if (height > 0.5f / _entryList.Count)
            {
                HandleDragging(_beginPosition.y < eventData.position.y);
            }
        }
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

        float size = GetSize(_entryList[0]);
        if (_gridLayout != null)
        {
            _startIndex = Mathf.Max(0, (Mathf.CeilToInt(newPosition / size) / _gridLayout.constraintCount) * _gridLayout.constraintCount);
        }
        else
        {
            _startIndex = Mathf.Max(0, Mathf.CeilToInt(newPosition / size));
        }
        _endIndex = _startIndex + _entryList.Count - 1;
        _endIndex = Mathf.Clamp(_endIndex, 0, _dataCount - 1);
        _startIndex = Mathf.Max(_endIndex - _entryList.Count + 1, 0);

        if (_dataCount > 0)
        {
            for (int i = 0; i <= (_endIndex - _startIndex); i++)
            {
                _entryList[i].UpdateIndex(_startIndex + i);
            }
        }

        ForceSetContentPosition(new Vector2(content.anchoredPosition.x, (newPosition -
            _startIndex * size)));

        UpdateRealBounds();
    }

    private void Update()
    {
        if (_dataCount == 0 || _entryList.Count==0)
        {
            return;
        }
        if (NeedMoveStartToEnd())
        {
            if (!_isStart)
            {
                if (content.anchoredPosition.y > 2 * GetSize(_entryList[_startIndex % _entryList.Count]))
                {
                    ForceSetContentPosition(new Vector2(content.anchoredPosition.x,
                    2 * GetSize(_entryList[_startIndex % _entryList.Count])));
                }
            }

            MoveStartToEnd();
        }
        else if (NeedMoveEndToStart())
        {
            if (!_isStart)
            {
                if (content.anchoredPosition.y < GetSize(_entryList[_endIndex % _entryList.Count]))
                {
                    ForceSetContentPosition(new Vector2(content.anchoredPosition.x,
                    GetSize(_entryList[_endIndex % _entryList.Count])));
                }
            }

            MoveEndToStart();
        }
    }

    private void MoveStartToEnd()
    {
        if (_gridLayout != null)
        {
            for (int i = 0; i < _gridLayout.constraintCount; i++)
            {
                LoopEntry entry = _entryList[i];
                entry.transform.SetAsLastSibling();
                _entryList.Remove(entry);
                _entryList.Add(entry);

                MoveDataIndexDown();
                if (_endIndex > _dataCount - 1)
                {
                    entry.gameObject.SetActive(false);
                }
                else
                {
                    entry.UpdateIndex(_endIndex);
                }
            }

            ForceSetContentPosition(new Vector2(content.anchoredPosition.x,
                content.anchoredPosition.y - GetSize(_entryList[0])));
        }
        else
        {
            LoopEntry startEntry = _entryList[0];
            startEntry.transform.SetAsLastSibling();
            _entryList.Remove(startEntry);
            _entryList.Add(startEntry);

            ForceSetContentPosition(new Vector2(content.anchoredPosition.x,
                content.anchoredPosition.y - GetSize(startEntry)));
            MoveDataIndexDown();
            startEntry.UpdateIndex(_endIndex);
        }

        UpdateRealBounds();
    }

    private void MoveEndToStart()
    {
        if (_gridLayout != null)
        {
            for (int i = 0; i < _gridLayout.constraintCount; i++)
            {
                LoopEntry entry = _entryList[_entryList.Count - 1];
                entry.transform.SetAsFirstSibling();
                _entryList.Remove(entry);
                _entryList.Insert(0, entry);

                MoveDataIndexUp();
            }
            Refresh();
            ForceSetContentPosition(new Vector2(content.anchoredPosition.x,
                content.anchoredPosition.y + GetSize(_entryList[0])));
        }
        else
        {
            LoopEntry endEntry = _entryList[_entryList.Count - 1];
            endEntry.transform.SetAsFirstSibling();
            _entryList.Remove(endEntry);
            _entryList.Insert(0, endEntry);

            ForceSetContentPosition(new Vector2(content.anchoredPosition.x,
                content.anchoredPosition.y + GetSize(endEntry)));
            MoveDataIndexUp();
            endEntry.UpdateIndex(_startIndex);
        }

        UpdateRealBounds();
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
            endDelta = ((_dataCount - _endIndex) / _gridLayout.constraintCount) * GetSize(_entryList[0]);
        }
        else
        {
            startDelta = _startIndex * GetSize(_entryList[0]);
            endDelta = (_dataCount - 1 - _endIndex) * GetSize(_entryList[0]);
        }

        UpdateRealContentBoundsInfo(new Vector3(0, startDelta, 0), new Vector3(0, endDelta, 0));
    }

    private bool NeedMoveStartToEnd()
    {
        if (content.anchoredPosition.y >
            2 * GetSize(_entryList[_startIndex % _entryList.Count]))
        {
            return EnableDataLoop || (_endIndex < _dataCount - 1);
        }

        return false;
    }

    private bool NeedMoveEndToStart()
    {
        if (content.anchoredPosition.y
            < GetSize(_entryList[_endIndex % _entryList.Count]))
        {
            if (EnableDataLoop)
            {
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
        GameObject gameObject = GameObject.Instantiate(prefab);
        gameObject.transform.SetParent(content.transform);
        gameObject.transform.SetLocalPositionZ(0f);
        gameObject.transform.localScale = Vector3.one;
        gameObject.name = index.ToString();
        gameObject.SetActive(false);
        _entryList.Add(gameObject.GetComponent<LoopEntry>());
    }

    private float GetSize(LoopEntry entry)
    {
        float size = 0;
        if (_gridLayout != null)
        {
            size = _gridLayout.cellSize.y + _gridLayout.spacing.y;
        }
        else
        {
            size = entry.GetComponent<LayoutElement>().preferredHeight + _verticalLayout.spacing;
        }

        return size;
    }

    private float GetPosition(RectTransform item)
    {
        return -item.localPosition.x - content.localPosition.x;
    }

    private float GetSpacing()
    {
        if (_gridLayout != null)
        {
            return _gridLayout.spacing.y;
        }
        else
        {
            return _verticalLayout.spacing;
        }
    }

    private GridLayoutGroup _gridLayout;
    private VerticalLayoutGroup _verticalLayout;

    private List<LoopEntry> _entryList;
    private int _dataCount;
    private int _startIndex;
    private int _endIndex;

    private const int _bufferSize = 3;
    private int _size;
    private Vector2 _beginPosition;
    private bool _flag;
    private bool _haveInited = false;
    private bool _isDragUp = false;

    private bool _isStart = false;
}

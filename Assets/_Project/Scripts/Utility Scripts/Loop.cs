
using System;
using UnityEngine;
//Класс для возможности бесконечного смещения по индексу ограниченной длины.
class Loop
{
    private int _index;
    private int _offSet;
    private int _length;

    public Loop(int index, int length, int offSet)
    {

        _index = index > -1 ? index : throw new ArgumentException("Negative index not allowed");
        _index = index < length ? index : index - (index * (index / length));
        _length = length > 0 ? length : throw new ArgumentException("Length must be more than 0");
        _offSet = Mathf.Abs(offSet) <= length / 2 ? offSet : throw new ArgumentException("OffSet must be no more than half of Length (rounded down) with any sign.");
    }
    public int Set
    {
        get => _index;
        set
        {
            if (value < 0) _index = 0;
            else if (value >= _length) _index = _length - 1;
            else _index = value;
        }
    }
    public int SetOffSet
    {
        get => _offSet;
        set
        {
            if (Mathf.Abs(value) > _length / 2) _offSet = (_length / 2) * (int)Mathf.Sign(value);
            else _offSet = value;
        }
    }
    //=========================================================
    public int Next()
    {
        _index++;
        if (_index == _length) _index = 0;
        return _index;

    }
    public int Next(out int offSetIndex)
    {
        Next();
        offSetIndex = CalculateOffset();
        return _index;

    }
    //=========================================================
    public int Previous()
    {
        _index--;
        if (_index < 0) _index = _length + 1;
        return _index;
    }
    public int Previous(out int offSetIndex)
    {
        Previous();
        offSetIndex = CalculateOffset();
        return _index;
    }
    //=========================================================
    public int Current()
    {
        return _index;
    }
    public int Current(out int offSetIndex)
    {
        offSetIndex = CalculateOffset();
        return _index;
    }
    //=========================================================
    private int CalculateOffset()
    {
        int offSetIndex = _index + _offSet;
        if (offSetIndex >= _length) return offSetIndex -= _length;
        if (offSetIndex < 0) return offSetIndex += _length;
        return offSetIndex;
    }
}


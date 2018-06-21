//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//09 Dec 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heap<T> where T : IHeapItem<T>{

    T[] items;
    int currentItemCount;

    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    //Add an item to the heap
    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortHeapUp(item);
        currentItemCount++;
    }

    //Remove and return the first item
    public T RemoveFirst()
    {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortHeapDown(items[0]);
        return firstItem;
    }

    //Check if an item exists
    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }

    //To change priority of an item
    public void UpdateItem(T item)
    {
        SortHeapUp(item);
    }

    //Return the count
    public int Count
    {
        get
        {
            return currentItemCount;
        }
    }

    //Move the item down the heap to its propper position
    void SortHeapDown(T item)
    {
        while(true)
        {
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapindex = 0;

            if(childIndexLeft < currentItemCount)
            {
                swapindex = childIndexLeft;

                if(childIndexRight < currentItemCount)
                {
                    if(items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                    {
                        swapindex = childIndexRight;
                    }
                }

                if(item.CompareTo(items[swapindex]) < 0)
                {
                    Swap(item, items[swapindex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    //Move the item up the heap to its propper position
    void SortHeapUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;

        while(true)
        {
            T parentItem = items[parentIndex];
            if(item.CompareTo(parentItem) > 0)
            {
                Swap(item, parentItem);
            }
            else
            {
                break;
            }

            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    //Swap two item positions in the heap
    void Swap(T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        int tempIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = tempIndex;
    }

}


//Helps store indexes
public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}

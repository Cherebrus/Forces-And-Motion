using System;
using UnityEngine;

public class ItemFanctionData : MonoBehaviour
{
    public Item ItemData;
    public GameObject me;
    public HolderAndMove addingList;

    private void Awake()
    {
        me = gameObject;
        
    }

    private void OnMouseDown()
    {
        if (ItemData.isUseing)
        {
            addingList.itemsUseing.Add(this);
            ItemData.isUseing = false;
        }

        else
        {
            addingList.itemsUseing.Remove(this);
            ItemData.isUseing = true;
        }
    }
}


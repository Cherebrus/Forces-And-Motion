using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HolderAndMove : MonoBehaviour
{
    public itemManager Manager;
    public Rigidbody rb;
    public float speed = 2000;
    public List<ItemFanctionData> itemsUseing = new List<ItemFanctionData>();


    private void Update()
    {
        MoveAndMass();
        addMass();
    }

    public void MoveAndMass()
    {
        Vector3 derection = new Vector3(1, 0, 0);
       
       if (Input.GetKey(KeyCode.A))
       {
           rb.AddForce(-derection * Time.deltaTime  * speed);
           Debug.Log("A");
       }
       else if (Input.GetKey(KeyCode.D))
       {
           rb.AddForce(derection * Time.deltaTime * speed);
           Debug.Log("D");
       }
   }

    void addMass()
    {
        var massAdd = 1;

        foreach (var i in itemsUseing)
        {
            massAdd += i.ItemData.mass;
        }

        rb.mass = massAdd;

    }
    
}

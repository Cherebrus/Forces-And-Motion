﻿using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;



public class Rope : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField ]int nutonRatio=100;
    [SerializeField] List<BodySpot> redTeamSpotList = new List<BodySpot>(4);
    [SerializeField] List<BodySpot> blueTeamSpotList = new List<BodySpot>(4);
     List<Body> bodysConnected = new List<Body>();

    internal float blueForce;
    internal float redForce;
    internal UIManager uIManager;
    Vector3 forceActivated;
   




    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Update()
    {
        CalculateForces();
    }
    public void CalculateForces()
    {
        rb.velocity = forceActivated;
    }
    public void AddBodyToRope(BodyGroup playerGroup,Body body)
    {
        if (playerGroup == BodyGroup.red)
        {
            
            foreach(BodySpot found in redTeamSpotList)
            {
                if (!found.isTaken)
                {
                    body.transform.position = found.transform.position;
                    body.transform.SetParent(found.transform.parent);
                    found.containedBody = body;
                    found.isTaken = true;
                    bodysConnected.Add(body);
                    redForce += body.scaleMass * nutonRatio;
                    ApplyForceToRope(true, true, body.scaleMass);
                    break;
                }
                else
                {
                    Debug.Log("ListIsFull");
                }

                
            }

         
        }
        else if(playerGroup == BodyGroup.blue)
        {
            foreach (BodySpot found in blueTeamSpotList)
            {
                if (!found.isTaken)
                {
                    body.transform.position = found.transform.position;
                    body.transform.SetParent(found.transform.parent);
                    found.containedBody = body;
                    found.isTaken = true;
                    bodysConnected.Add(body);
                    blueForce += body.scaleMass * nutonRatio;
                    ApplyForceToRope(true, false, body.scaleMass);
                    break;
                }
                else
                {
                    Debug.Log("ListIsFull");
                }


            }
        }
        else
        {
            Debug.Log("how the fuck did you do that");
        }


    }
    public void RemoveBodyFromRope(Body body)
    {
        bodysConnected.Remove(body);
        body.BackToPlace();
        if (body.bodyGroup == BodyGroup.red)
        {

            redForce -= body.scaleMass * nutonRatio;
            ApplyForceToRope(false, true, body.scaleMass);
            foreach(BodySpot found in redTeamSpotList)
            {
                if (found.containedBody == body)
                {
                    found.containedBody = null; 
                    found.isTaken = false;
                    break;
                }
            }        
        }
        else
        {
            blueForce -= body.scaleMass * nutonRatio;
            ApplyForceToRope(false, false, body.scaleMass);
            foreach (BodySpot found in blueTeamSpotList)
            {
                if (found.containedBody == body)
                {
                    found.containedBody = null;
                    found.isTaken = false;
                    break;
                }
            }
        }
        body.transform.parent = null;
        
        
        
    }
    public void ApplyForceToRope(bool Add,bool isRed,float mass)
    {
        if (Add)
        {
            if (isRed)
            {
                forceActivated.x += mass;
            }
            else
            {
                forceActivated.x -= mass;

            }
        }
        else
        {
            if (isRed)
            {
                forceActivated.x -= mass;
            }
            else
            {
                forceActivated.x += mass;

            }
        }
        uIManager.ForceUpdate(Mathf.RoundToInt(blueForce), Mathf.RoundToInt(redForce), Mathf.RoundToInt(forceActivated.x*nutonRatio));
    }


    

}

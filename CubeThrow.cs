using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeThrow : MonoBehaviour
{
    Main ScriptMain;
    public float PushPower = 1f, JumpPower, LeftRightJumpPower;
    Rigidbody rg;
    public bool PushObj, Jump, Touch, TouchUp;
    public GameObject NextBox;
    public int NumBox;
    
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Box")
        {
            if (PushObj)
            {
                ScriptMain.NewSpawn = true;
                PushObj = false;
            }
            if (NumBox == col.gameObject.GetComponent<CubeThrow>().NumBox)
            {
                ScriptMain.Score += NumBox;
                Vector3 SpawnBox = col.transform.position;
                ScriptMain.NextBox = NextBox;
                ScriptMain.SpawnBox = transform.position;
                Destroy(col.gameObject);
                Destroy(gameObject);
                ScriptMain.SpawnNext = true;

            }
            else
            {
                GetComponent<Rigidbody>().useGravity = true;
            }
            ScriptMain.Null = false;
        }
        if (col.gameObject.tag == "Wall")
        {
            if (PushObj) ScriptMain.NewSpawn = true;
            GetComponent<Rigidbody>().useGravity = true;
            PushObj = false;
            ScriptMain.Null = false;
        }
    }
    void Awake()
    {
        ScriptMain = Camera.main.GetComponent<Main>();
        rg = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (PushObj)
        {
            if (Touch)
            {
                if (ScriptMain.TouchX > -1.57f && ScriptMain.TouchX < 1.57f)
                {
                    ScriptMain.Aim.SetPosition(0, new Vector3(ScriptMain.TouchX, ScriptMain.Aim.GetPosition(0).y, ScriptMain.Aim.GetPosition(0).z));
                    ScriptMain.Aim.SetPosition(1, new Vector3(ScriptMain.TouchX, ScriptMain.Aim.GetPosition(1).y, ScriptMain.Aim.GetPosition(1).z));
                    transform.position = new Vector3(ScriptMain.TouchX, transform.position.y, transform.position.z);
                }
            }
            if (TouchUp)
            {
                rg.useGravity = false;
                rg.velocity = new Vector3(rg.velocity.x, rg.velocity.y, PushPower);
                TouchUp = false;
            }
        }
        if(!PushObj && transform.position.z < 0.9f)
        {
            ScriptMain.Play = false;
            ScriptMain.TheEnd = true;
        }
        if(Jump)
        {
            rg.velocity = new Vector3(rg.velocity.x + Random.Range(-LeftRightJumpPower, LeftRightJumpPower), JumpPower, rg.velocity.z + Random.Range(0f,3f));
            Jump = false;
        }
    }
}

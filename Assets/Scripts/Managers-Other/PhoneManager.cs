using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    public GameObject Supra, Bone;
    public Collider HeadCollider;
    private Vector3 SupraStartPosition, BoneStartPosition;
    private Quaternion SupraStartRotation, BoneStartRotation;
    private float SupraRatio, BoneRatio;
    private string LastPhone = "empty", NextPhone;
    private int PhoneValuePtrL = 0, PhoneValuePtrR = 0;

    void Start()
    {
        SupraStartPosition = Supra.transform.position;
        BoneStartPosition = Bone.transform.position;

        SupraStartRotation = Supra.transform.rotation;
        BoneStartRotation = Bone.transform.rotation;
    }
    void Update()
    {
        
    }
    public void ChangePhoneObject(string WhichEar)
    {
        if (WhichEar == "left") { PhoneValuePtrL++; if (PhoneValuePtrL == 4) { PhoneValuePtrL = 0; } }
        else if (WhichEar == "right") { PhoneValuePtrR++; if (PhoneValuePtrR == 4) { PhoneValuePtrR = 0; } }

        if (PhoneValuePtrL == 1 || PhoneValuePtrR == 1) { NextPhone = "supra"; }
        else if (PhoneValuePtrL == 2 || PhoneValuePtrR == 2) { NextPhone = "insert"; }
        else if (PhoneValuePtrL == 3 || PhoneValuePtrR == 3) { NextPhone = "bone"; }
        else { NextPhone = "empty"; }

        if(NextPhone == "supra")
        {
            if(LastPhone != "supra")
            {
                ResizePhone(Supra);
                PlacePhone(Supra);
                ResetPhone();
                LastPhone = NextPhone;
            }
        }
        else if(NextPhone == "bone")
        {
            if(LastPhone != "bone")
            {
                ResizePhone(Bone);
                PlacePhone(Bone);
                ResetPhone();
                LastPhone = NextPhone;
            }
        }
        else { ResetPhone(); LastPhone = "empty"; }
    }
    void PlacePhone(GameObject Phone)
    {
        Phone.transform.position = HeadCollider.transform.position;
        Phone.transform.rotation = HeadCollider.transform.rotation;
    }
    void ResizePhone(GameObject Phone)
    {
        Vector3 ColliderSize = HeadCollider.bounds.size;
        Vector3 PhoneSize = Phone.GetComponent<Renderer>().bounds.size;

        Vector3 ScaleRatio = new Vector3(
            ColliderSize.x / PhoneSize.x,
            ColliderSize.y / PhoneSize.y,
            ColliderSize.z / PhoneSize.z
        );
        float AverageScale = (ScaleRatio.x + ScaleRatio.y + ScaleRatio.z) / 3f;
        if (Phone == Supra) { SupraRatio = AverageScale; } else if (Phone == Bone) { BoneRatio = AverageScale; }
        Phone.transform.localScale *= AverageScale;
    }
    void ResetPhone()
    {
        if(LastPhone != "empty")
        {
            if (LastPhone == "supra")
            {
                Supra.transform.position = SupraStartPosition;
                Supra.transform.rotation = SupraStartRotation;
                Supra.transform.localScale /= SupraRatio;
            }
            else if(LastPhone == "bone") 
            {
                Bone.transform.position = BoneStartPosition;
                Bone.transform.rotation = BoneStartRotation;
                Bone.transform.localScale /= BoneRatio;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkManager : MonoBehaviour
{
    public GameObject MarkObjects, AirLeftMask, AirLeftMaskNot, AirRightMask, AirRightMaskNot, BoneLeftMask, BoneLeftMaskNot, BoneRightMask, BoneRightMaskNot;
    private GameObject Icon, EmptyObject;
    public AudiogramManager AudiogramManager;
    public ValueScreenManager ValueScreenManager;
    void Start()
    {
        Mark();
    }
    void Update()
    {
        
    }
    public void Mark()
    {
        if(!(ValueScreenManager.WhichIcon() == 32))
        {
            Icon = WhichObject();
            GameObject IconObject = Instantiate(Icon, MarkObjects.transform);
            Vector3 newPosition = IconObject.transform.localPosition;
            newPosition.x += (AudiogramManager.Get("FQ") - 4f) * 0.07f;
            newPosition.y -= (AudiogramManager.Get("DB") - 8f) * 0.04f;
            IconObject.transform.localPosition = newPosition;
            IconObject.transform.localRotation = Quaternion.AngleAxis(90, Vector3.right);
            IconObject.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        }
    }
    private GameObject WhichObject()
    {
        switch (ValueScreenManager.WhichIcon())
        {
            case 0:
                return AirLeftMask;
            case 1:
                return AirLeftMaskNot;
            case 2:
                return AirRightMask;
            case 3:
                return AirRightMaskNot;
            case 4:
                return BoneLeftMask;
            case 5:
                return BoneLeftMaskNot;
            case 6:
                return BoneRightMask;
            case 7:
                return BoneRightMaskNot;
            default:
                return EmptyObject;
        }
    }
    /*
     * 0 Air Left Mask
     * 1 Air Left NotMask
     * 2 Air Right Mask
     * 3 Air Right NotMask
     * 4 Bone Left Mask
     * 5 Bone Left NotMask
     * 6 Bone Right Mask
     * 7 Bone Right NotMask
     */
}

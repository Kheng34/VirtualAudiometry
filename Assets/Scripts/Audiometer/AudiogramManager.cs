using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiogramManager : MonoBehaviour
{
    public GameObject VerticalPin;
    public GameObject HorizontalPin;
    private float vertPtr = 4f, horzPtr = 8f, DBLeftCache = 8f, DBRightCache = 8f, SetDB;
    private string CurrentMode = "both";
    private string LR, DBorFQ;
    public void FreqUp()
    {
        if (3 <= vertPtr && vertPtr < 7)
        {
            Vector3 newPosition = VerticalPin.transform.localPosition;
            newPosition.x += 0.035f;
            VerticalPin.transform.localPosition = newPosition;
            vertPtr += 0.5f;
        }
        else if (vertPtr < 3)
        {
            Vector3 newPosition = VerticalPin.transform.localPosition;
            newPosition.x += 0.07f;
            VerticalPin.transform.localPosition = newPosition;
            vertPtr++;
        }
    }
    public void FreqDown()
    {
        if (3 < vertPtr)
        {
            Vector3 newPosition = VerticalPin.transform.localPosition;
            newPosition.x -= 0.035f;
            VerticalPin.transform.localPosition = newPosition;
            vertPtr -= 0.5f;
        }
        else if (1 < vertPtr && vertPtr <= 3)
        {
            Vector3 newPosition = VerticalPin.transform.localPosition;
            newPosition.x -= 0.07f;
            VerticalPin.transform.localPosition = newPosition;
            vertPtr--;
        }
    }
    public void DbUp(string LR)
    {
        if (CurrentMode == LR)
        {
            if (horzPtr < 15)
            {
                Vector3 newPosition = HorizontalPin.transform.localPosition;
                newPosition.y -= 0.04f;
                HorizontalPin.transform.localPosition = newPosition;
                horzPtr++;
            }
        }
        else if (LR == "left") { if (DBLeftCache < 15) { DBLeftCache++; } }
        else if (LR == "right") { if (DBRightCache < 15) { DBRightCache++; } }
    }
    public void DbDown(string LR)
    {
        if (CurrentMode == LR)
        {
            if (1 < horzPtr)
            {
                Vector3 newPosition = HorizontalPin.transform.localPosition;
                newPosition.y += 0.04f;
                HorizontalPin.transform.localPosition = newPosition;
                horzPtr--;
            }
        }
        else if (LR == "left") { if (1 < DBLeftCache) { DBLeftCache--; } }
        else if (LR == "right") { if (1 < DBRightCache) { DBRightCache--; } }
    }
    public void ChangeMode(string newMode)
    {
        if (!(CurrentMode == newMode))
        {
            if (CurrentMode == "both")
            {
                if (newMode == "left") { Set(DBLeftCache); }
                else if (newMode == "right") { Set(DBRightCache); }
            }
            else if (CurrentMode == "left") { if (newMode == "right") { DBLeftCache = horzPtr; Set(DBRightCache); } }
            else if (CurrentMode == "right") { if (newMode == "left") { DBRightCache = horzPtr; Set(DBLeftCache); } }
            if (newMode == "both")
            {
                if (CurrentMode == "left") { DBLeftCache = horzPtr; }
                else if (CurrentMode == "right") { DBRightCache = horzPtr; }
                Set(8f);
            }
            CurrentMode = newMode;
        }
    }
    public void Set(float SetDB)
    {
        if (horzPtr < SetDB) { while (horzPtr < SetDB) { DbUp(CurrentMode); } }
        else if (SetDB < horzPtr) { while (SetDB < horzPtr) { DbDown(CurrentMode); } }
        /*if(!(SetFq == 100f))
        {
            if (vertPtr < SetFq) { while (vertPtr < SetFq) { FreqUp(); } }
            else if (SetFq < vertPtr) { while (SetFq < vertPtr) { FreqDown(); } }
        }*/
    }
    public float Get(string DBorFQ)
    {
        if (DBorFQ == "DB") { return horzPtr; }
        else if (DBorFQ == "FQ") { return vertPtr; }
        else { return 0; }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValueScreenManager : MonoBehaviour
{
    public GameObject TextDBValueL, TextDBValueR, TextFQValue, EarSelectValue, PhoneSelectValueL, PhoneSelectValueR, ToneSelectValueL, ToneSelectValueR, MaskSelectValueL, MaskSelectValueR, PlayValue;
    private TextMeshPro DBLeftCp, DBRightCp, FqCp, EarSelectValueCp, PhoneModeLCp, PhoneSelectValueLCp, PhoneSelectValueRCp, ToneSelectValueLCp, ToneSelectValueRCp, MaskSelectValueLCp, MaskSelectValueRCp, PlayValueCp;
    private int DBValueL = 60, DBValueR = 60, FqPtr = 4, PhoneValuePtrL = 0, PhoneValuePtrR = 0, MaskValuePtrL = 0, MaskValuePtrR = 0, PlayValuePtr, NewPlayStat, WhichIconPtr;
    private int[] FqValue = { 125, 250, 500, 750, 1000, 1500, 2000, 3000, 4000, 6000, 8000 };
    private string CurrentEarMode = "BOTH", NewEarMode, PhoneToEar, ToneValueL = "PURE", ToneValueR = "PURE", NewToneMode, PhoneForAPI;
    private string[] PhoneValue = { "NONE", "SUPRA AURAL", "INSERT", "VIBRATOR" }, MaskValue = {"NONE", "WHITE", "BLACK"};
    public APIManager APIManager;
    public bool TestStarted;


    void Start()
    {
        DBLeftCp = TextDBValueL.GetComponent<TextMeshPro>();
        DBRightCp = TextDBValueR.GetComponent<TextMeshPro>();
        FqCp = TextFQValue.GetComponent<TextMeshPro>();
        EarSelectValueCp = EarSelectValue.GetComponent<TextMeshPro>();
        PhoneSelectValueLCp = PhoneSelectValueL.GetComponent<TextMeshPro>();
        PhoneSelectValueRCp = PhoneSelectValueR.GetComponent<TextMeshPro>();
        ToneSelectValueLCp = ToneSelectValueL.GetComponent<TextMeshPro>();
        ToneSelectValueRCp = ToneSelectValueR.GetComponent<TextMeshPro>();
        MaskSelectValueLCp = MaskSelectValueL.GetComponent<TextMeshPro>();
        MaskSelectValueRCp = MaskSelectValueR.GetComponent<TextMeshPro>();
        PlayValueCp = PlayValue.GetComponent<TextMeshPro>();
    }
    void Update()
    {
        DBLeftCp.text = DBValueL.ToString();
        DBRightCp.text = DBValueR.ToString();
        FqCp.text = FqValue[FqPtr].ToString();
        EarSelectValueCp.text = CurrentEarMode;
        PhoneSelectValueLCp.text = PhoneValue[PhoneValuePtrL];
        PhoneSelectValueRCp.text = PhoneValue[PhoneValuePtrR];
        ToneSelectValueLCp.text = ToneValueL;
        ToneSelectValueRCp.text = ToneValueR;
        MaskSelectValueLCp.text = MaskValue[MaskValuePtrL];
        MaskSelectValueRCp.text = MaskValue[MaskValuePtrR];
        if(PlayValuePtr == 0) { PlayValueCp.text = ""; }
        else if(PlayValuePtr == 1) { PlayValueCp.text = "Playing..."; }
    }
    public void DBLeftUp() { if (DBValueL < 130) { DBValueL += 10; } }
    public void DBLeftDown() { if (-10 < DBValueL) { DBValueL -= 10; } }
    public void DBRightUp() { if (DBValueR < 130) { DBValueR += 10; } }
    public void DBRightDown() { if (-10 < DBValueR) { DBValueR -= 10; } }
    public void FqUp() { if (FqPtr < 10) { FqPtr++; } }
    public void FqDown() { if (0 < FqPtr) { FqPtr--; } }
    public void ChangeEarMode(string NewEarMode) { CurrentEarMode = NewEarMode; }
    public void ChangePhoneMode(string PhoneToEar)
    {
        if (PhoneToEar == "left") { PhoneValuePtrL++; if (PhoneValuePtrL == 4) { PhoneValuePtrL = 0; } }
        else if (PhoneToEar == "right") { PhoneValuePtrR++; if (PhoneValuePtrR == 4) { PhoneValuePtrR = 0; } }
    }
    public void ChangeToneMode(string NewToneMode, string ToneToEar)
    {
        if(ToneToEar == "left") { ToneValueL = NewToneMode; }
        else if(ToneToEar == "right") {  ToneValueR = NewToneMode; }
    }
    public void ChangeMaskMode(string MaskToEar)
    {
        if(MaskToEar == "left") { MaskValuePtrL++; if (MaskValuePtrL == 3) { MaskValuePtrL = 0; } }
        else if (MaskToEar == "right") { MaskValuePtrR++; if (MaskValuePtrR == 3) { MaskValuePtrR = 0; } }
    }
    public void ChangePlayStat(int NewPlayStat)
    {
        if (NewPlayStat == 0) { PlayValuePtr = 0; }
        else if (NewPlayStat == 1) { PlayValuePtr = 1; }
    }
    public int WhichIcon()
    {
        if (CurrentEarMode == "LEFT")
        {
            if (PhoneValuePtrL == 3)
            {
                if (0 < MaskValuePtrL && MaskValuePtrL < 3) { return 4; }
                else if (MaskValuePtrL == 0) { return 5; }
                else { return 32; }
            }
            else if (0 < PhoneValuePtrL && PhoneValuePtrL < 3)
            {
                if (0 < MaskValuePtrL && MaskValuePtrL < 3) { return 0; }
                else if (MaskValuePtrL == 0) { return 1; }
                else { return 32; }
            }
            else { return 32; }
        }
        else if (CurrentEarMode == "RIGHT") 
        {

            if (PhoneValuePtrR == 3)
            {
                if (0 < MaskValuePtrR && MaskValuePtrR < 3) { return 6; }
                else if (MaskValuePtrR == 0) { return 7; }
                else { return 32; }
            }
            else if (0 < PhoneValuePtrR && PhoneValuePtrR < 3)
            {
                if (0 < MaskValuePtrR && MaskValuePtrR < 3) { return 2; }
                else if (MaskValuePtrR == 0) { return 3; }
                else { return 32; }
            }
            else { return 32; }
        }
        else { return 32; }
    }
    public void Play()
    {
        if (TestStarted)
        {
            if (CurrentEarMode == "LEFT") 
            { 
                if(!(PhoneValuePtrL == 0))
                {
                    if (PhoneValuePtrL == 3)
                    {
                        PhoneForAPI = "bone";
                    }
                    else if (0 < PhoneValuePtrL && PhoneValuePtrL < 3)
                    {
                        PhoneForAPI = "air";
                    }
                    APIManager.PostAndTriggerAnimationAsync("1", DBValueL, "left", FqValue[FqPtr], PhoneForAPI);
                }
            }
            else if (CurrentEarMode == "RIGHT") 
            { 
                if (!(PhoneValuePtrR == 0))
                {
                    if (PhoneValuePtrR == 3)
                    {
                        PhoneForAPI = "bone";
                    }
                    else if (0 < PhoneValuePtrR && PhoneValuePtrR < 3)
                    {
                        PhoneForAPI = "air";
                    }
                    APIManager.PostAndTriggerAnimationAsync("1", DBValueR, "right", FqValue[FqPtr], PhoneForAPI);
                }
            }
        }
    }
    public void MarkToAPI()
    {
        if (TestStarted)
        {
            if (CurrentEarMode == "LEFT")
            {
                if (!(PhoneValuePtrL == 0))
                {
                    if (PhoneValuePtrL == 3)
                    {
                        PhoneForAPI = "bone";
                    }
                    else if (0 < PhoneValuePtrL && PhoneValuePtrL < 3)
                    {
                        PhoneForAPI = "air";
                    }
                    StartCoroutine(APIManager.HandlePutRequest("1", DBValueL, "left", FqValue[FqPtr], PhoneForAPI));
                }
            }
            else if (CurrentEarMode == "RIGHT")
            {
                if (!(PhoneValuePtrR == 0))
                {
                    if (PhoneValuePtrR == 3)
                    {
                        PhoneForAPI = "bone";
                    }
                    else if (0 < PhoneValuePtrR && PhoneValuePtrR < 3)
                    {
                        PhoneForAPI = "air";
                    }
                    StartCoroutine(APIManager.HandlePutRequest("1", DBValueR, "right", FqValue[FqPtr], PhoneForAPI));
                }
            }
        }
    }
    public void GetResultFromAPI()
    {
        StartCoroutine(APIManager.HandleGetRequest("1"));
        StartCoroutine(APIManager.CallPostTeach());
    }
}

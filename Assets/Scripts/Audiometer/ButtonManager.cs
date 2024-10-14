using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonManager : MonoBehaviour
{
    public InputActionReference TriggerAction;
    private bool LeftEarHovering, RightEarHovering, BothEarHovering, LeftDbUpHovering, LeftDbDownHovering, RightDbUpHovering, RightDbDownHovering, FreqUpHovering, FreqDownHovering, LeftPhoneModeHovering, RightPhoneModeHovering, LeftMaskHovering, RightMaskHovering, PlayHovering, LeftPureHovering, LeftMarbleHovering, LeftPulseHovering, RightPureHovering, RightMarbleHovering, RightPulseHovering, MarkHovering;
    public AudiogramManager AudiogramManager;
    public ValueScreenManager ValueScreenManager;
    public MarkManager MarkManager;
    public PhoneManager PhoneManager;
    private float CurrentDelay;
    private bool TriggerReleased = true;
    public GameObject LeftEar, RightEar, BothEar, LeftPure, LeftMarble, LeftPulse, RightPure, RightMarble, RightPulse;
    private Renderer LeftEarRnd, RightEarRnd, BothEarRnd, LeftPureRnd, LeftMarbleRnd, LeftPulseRnd, RightPureRnd, RightMarbleRnd, RightPulseRnd;
    private Color EarOriginColor, LeftToneOriginColor, RightToneOriginColor;
    private HighlightWhenHover LeftEarCp, RightEarCp, BothEarCp, LeftPureCp, LeftMarbleCp, LeftPulseCp, RightPureCp, RightMarbleCp, RightPulseCp;
    private int LeftOrigin, RightOrigin, BothOrigin;

    void OnEnable() { TriggerAction.action.Enable(); }
    void OnDisable() { TriggerAction.action.Disable(); }

    void Start()
    {
        LeftEarRnd = LeftEar.GetComponent<Renderer>();
        RightEarRnd = RightEar.GetComponent<Renderer>();
        BothEarRnd = BothEar.GetComponent<Renderer>();
        EarOriginColor = LeftEarRnd.material.color;
        LeftEarCp = LeftEar.GetComponent<HighlightWhenHover>();
        RightEarCp = RightEar.GetComponent<HighlightWhenHover>();
        BothEarCp = BothEar.GetComponent<HighlightWhenHover>();
        BothEarRnd.material.color = Color.blue;
        BothEarCp.OriginalColor = Color.blue;

        LeftPureRnd = LeftPure.GetComponent<Renderer>();
        LeftMarbleRnd = LeftMarble.GetComponent<Renderer>();
        LeftPulseRnd = LeftPulse.GetComponent<Renderer>();
        LeftToneOriginColor = LeftMarbleRnd.material.color;
        LeftPureCp = LeftPure.GetComponent<HighlightWhenHover>();
        LeftMarbleCp = LeftMarble.GetComponent<HighlightWhenHover>();
        LeftPulseCp = LeftPulse.GetComponent<HighlightWhenHover>();
        LeftPureRnd.material.color = Color.blue;
        LeftPureCp.OriginalColor = Color.blue;

        RightPureRnd = RightPure.GetComponent<Renderer>();
        RightMarbleRnd = RightMarble.GetComponent<Renderer>();
        RightPulseRnd = RightPulse.GetComponent<Renderer>();
        RightToneOriginColor = RightMarbleRnd.material.color;
        RightPureCp = RightPure.GetComponent<HighlightWhenHover>();
        RightMarbleCp = RightMarble.GetComponent<HighlightWhenHover>();
        RightPulseCp = RightPulse.GetComponent<HighlightWhenHover>();
        RightPureRnd.material.color = Color.blue;
        RightPureCp.OriginalColor = Color.blue;
    }
    void Update()
    {
        if ((0.1f < TriggerAction.action.ReadValue<float>()) && TriggerReleased)
        {
            TriggerReleased = false;
            if (LeftEarHovering)
            {
                LeftEarCp.OriginalColor = Color.blue;
                RightEarCp.OriginalColor = EarOriginColor;
                RightEarRnd.material.color = RightEarCp.OriginalColor;
                BothEarCp.OriginalColor = EarOriginColor;
                BothEarRnd.material.color = BothEarCp.OriginalColor;
                AudiogramManager.ChangeMode("left");
                ValueScreenManager.ChangeEarMode("LEFT");
            }
            if (RightEarHovering)
            {
                LeftEarCp.OriginalColor = EarOriginColor;
                LeftEarRnd.material.color = LeftEarCp.OriginalColor;
                RightEarCp.OriginalColor = Color.blue;
                BothEarCp.OriginalColor = EarOriginColor;
                BothEarRnd.material.color = BothEarCp.OriginalColor;
                AudiogramManager.ChangeMode("right");
                ValueScreenManager.ChangeEarMode("RIGHT");
            }
            if (BothEarHovering)
            {
                LeftEarCp.OriginalColor = EarOriginColor;
                LeftEarRnd.material.color = LeftEarCp.OriginalColor;
                RightEarCp.OriginalColor = EarOriginColor;
                RightEarRnd.material.color = RightEarCp.OriginalColor;
                BothEarCp.OriginalColor = Color.blue;
                AudiogramManager.ChangeMode("both");
                ValueScreenManager.ChangeEarMode("BOTH");
            }

            if (LeftDbUpHovering) { ValueScreenManager.DBLeftUp(); AudiogramManager.DbUp("left"); }
            if (LeftDbDownHovering) { ValueScreenManager.DBLeftDown(); AudiogramManager.DbDown("left"); }
            if (RightDbUpHovering) { ValueScreenManager.DBRightUp(); AudiogramManager.DbUp("right"); }
            if (RightDbDownHovering) { ValueScreenManager.DBRightDown(); AudiogramManager.DbDown("right"); }
            if (FreqUpHovering) { ValueScreenManager.FqUp(); AudiogramManager.FreqUp(); }
            if (FreqDownHovering) { ValueScreenManager.FqDown(); AudiogramManager.FreqDown(); }
            if (LeftPhoneModeHovering) { ValueScreenManager.ChangePhoneMode("left"); PhoneManager.ChangePhoneObject("left"); }
            if (RightPhoneModeHovering) { ValueScreenManager.ChangePhoneMode("right"); PhoneManager.ChangePhoneObject("right"); }
            if (LeftMaskHovering) { ValueScreenManager.ChangeMaskMode("left"); }
            if (RightMaskHovering) { ValueScreenManager.ChangeMaskMode("right"); }
            if (PlayHovering) 
            { 
                ValueScreenManager.ChangePlayStat(1);
                ValueScreenManager.Play();
            }
            if (!PlayHovering) { ValueScreenManager.ChangePlayStat(0); }

            if (LeftPureHovering)
            {
                LeftPureCp.OriginalColor = Color.blue;
                LeftMarbleCp.OriginalColor = LeftToneOriginColor;
                LeftMarbleRnd.material.color = LeftMarbleCp.OriginalColor;
                LeftPulseCp.OriginalColor = LeftToneOriginColor;
                LeftPulseRnd.material.color = LeftPulseCp.OriginalColor;
                ValueScreenManager.ChangeToneMode("PURE", "left");
            }
            if (LeftMarbleHovering)
            {
                LeftPureCp.OriginalColor = LeftToneOriginColor;
                LeftPureRnd.material.color = LeftPureCp.OriginalColor;
                LeftMarbleCp.OriginalColor = Color.blue;
                LeftPulseCp.OriginalColor = LeftToneOriginColor;
                LeftPulseRnd.material.color = LeftPulseCp.OriginalColor;
                ValueScreenManager.ChangeToneMode("MARBLE", "left");
            }
            if (LeftPulseHovering)
            {
                LeftPureCp.OriginalColor = LeftToneOriginColor;
                LeftPureRnd.material.color = LeftPureCp.OriginalColor;
                LeftMarbleCp.OriginalColor = LeftToneOriginColor;
                LeftMarbleRnd.material.color = LeftMarbleCp.OriginalColor;
                LeftPulseCp.OriginalColor = Color.blue;
                ValueScreenManager.ChangeToneMode("PULSE", "left");
            }

            if(RightPureHovering)
            {
                RightPureCp.OriginalColor = Color.blue;
                RightMarbleCp.OriginalColor = RightToneOriginColor;
                RightMarbleRnd.material.color = RightMarbleCp.OriginalColor;
                RightPulseCp.OriginalColor = RightToneOriginColor;
                RightPulseRnd.material.color = RightPulseCp.OriginalColor;
                ValueScreenManager.ChangeToneMode("PURE", "right");
            }
            if (RightMarbleHovering)
            {
                RightPureCp.OriginalColor = RightToneOriginColor;
                RightPureRnd.material.color = RightPureCp.OriginalColor;
                RightMarbleCp.OriginalColor = Color.blue;
                RightPulseCp.OriginalColor = RightToneOriginColor;
                RightPulseRnd.material.color = RightPulseCp.OriginalColor;
                ValueScreenManager.ChangeToneMode("MARBLE", "right");
            }
            if (RightPulseHovering)
            {
                RightPureCp.OriginalColor = RightToneOriginColor;
                RightPureRnd.material.color = RightPureCp.OriginalColor;
                RightMarbleCp.OriginalColor = RightToneOriginColor;
                RightMarbleRnd.material.color = RightMarbleCp.OriginalColor;
                RightPulseCp.OriginalColor = Color.blue;
                ValueScreenManager.ChangeToneMode("PULSE", "right");
            }
            if (MarkHovering)
            {
                MarkManager.Mark();
                ValueScreenManager.MarkToAPI();
            }
        }
        if(TriggerAction.action.ReadValue<float>() < 0.1f) { TriggerReleased = true; ValueScreenManager.ChangePlayStat(0); }
    }

    public void LeftEarEntered() { LeftEarHovering = true; }
    public void LeftEarExited() { LeftEarHovering = false; }
    public void RightEarEntered() { RightEarHovering = true; }
    public void RightEarExited() { RightEarHovering = false; }
    public void BothEarEntered() { BothEarHovering = true; }
    public void BothEarExited() { BothEarHovering = false; }
    public void LeftDbUpEntered() { LeftDbUpHovering = true; }
    public void LeftDbUpExited() { LeftDbUpHovering = false; }
    public void LeftDbDownEntered() { LeftDbDownHovering = true; }
    public void LeftDbDownExited() { LeftDbDownHovering = false; }
    public void RightDbUpEntered() { RightDbUpHovering = true; }
    public void RightDbUpExited() { RightDbUpHovering = false; }
    public void RightDbDownEntered() { RightDbDownHovering = true; }
    public void RightDbDownExited() { RightDbDownHovering = false; }
    public void FreqUpEntered() { FreqUpHovering = true; }
    public void FreqUpExited() { FreqUpHovering = false; }
    public void FreqDownEntered() { FreqDownHovering = true; }
    public void FreqDownExited() { FreqDownHovering = false; }
    public void LeftPhoneModeEntered() { LeftPhoneModeHovering = true; }
    public void LeftPhoneModeExited() { LeftPhoneModeHovering = false; }
    public void RightPhoneModeEntered() { RightPhoneModeHovering = true; }
    public void RightPhoneModeExited() { RightPhoneModeHovering = false; }
    public void LeftMaskEntered() { LeftMaskHovering = true; }
    public void LeftMaskExited() { LeftMaskHovering = false; }
    public void RightMaskEntered() { RightMaskHovering = true; }
    public void RightMaskExited() { RightMaskHovering = false; }
    public void PlayEntered() { PlayHovering = true; }
    public void PlayExited() { PlayHovering = false; }
    public void LeftPureEntered() { LeftPureHovering = true; }
    public void LeftPureExited() { LeftPureHovering = false; }
    public void LeftMarbleEntered() { LeftMarbleHovering = true; }
    public void LeftMarbleExited() { LeftMarbleHovering = false; }
    public void LeftPulseEntered() { LeftPulseHovering = true; }
    public void LeftPulseExited() { LeftPulseHovering = false; }
    public void RightPureEntered() { RightPureHovering = true; }
    public void RightPureExited() { RightPureHovering = false; }
    public void RightMarbleEntered() { RightMarbleHovering = true; }
    public void RightMarbleExited() { RightMarbleHovering = false; }
    public void RightPulseEntered() { RightPulseHovering = true; }
    public void RightPulseExited() { RightPulseHovering = false; }
    public void MarkEntered() { MarkHovering = true; }
    public void MarkExited() { MarkHovering = false; }
}

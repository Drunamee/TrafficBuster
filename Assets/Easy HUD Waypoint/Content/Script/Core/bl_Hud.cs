using UnityEngine;
using System.Collections;

public class bl_Hud : MonoBehaviour {

    public bl_HudInfo HudInfo;

    /// <summary>
    /// Instantiate a new Hud
    /// add hud to hud manager in start
    /// </summary>
    void Start()
    {
        if (bl_HudManager.instance != null)
        {
            if (HudInfo.m_Target == null) { HudInfo.m_Target = this.GetComponent<Transform>(); }
            if (HudInfo.ShowDynamically) { HudInfo.Hide = true; }
            bl_HudManager.instance.CreateHud(this.HudInfo);
        }
        else
        {
            Debug.LogError("Need have a Hud Manager in scene");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void Show()
    {
        if (bl_HudManager.instance != null)
        {
            bl_HudManager.instance.HideStateHud(HudInfo, false);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void Hide()
    {
        if (bl_HudManager.instance != null)
        {
            bl_HudManager.instance.HideStateHud(HudInfo, true);
        }
    }

}
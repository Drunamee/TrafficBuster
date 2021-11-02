using UnityEngine;

[System.Serializable]
public class bl_HudInfo  {
    [Tooltip("Is HUD hide for default?, you can change it in runtime.")]
    public bool ShowDynamically = false;
    [HideInInspector] public bool Hide = false;
    [Tooltip("Transform to HUD follow, is emty this will be take this transform.")]
    public Transform m_Target = null;
    public Texture2D m_Icon = null;
    public Color m_Color = new Color(1, 1, 1, 1);
    [Tooltip("Modify the target position.")]
    public Vector3 Offset;
    [TextArea(3,7)]
    public string m_Text = null;
    [Tooltip("When player is it approaches to the target, the icon It becomes smaller (Decreasing) or large(Increasing).")]
    public TypeHud m_TypeHud = TypeHud.Decreasing;
    [Tooltip("Max size to the hud can scale.")]
    public float m_MaxSize = 50f;
    public bool ShowDistance = true;
    [Tooltip("hud is fade.")]
    public bool isPalpitin = true;
    [System.Serializable]
    public class m_Arrow
    {
        public bool ShowArrow = true;
        public Texture ArrowIcon = null;
        public Vector3 ArrowOffset = Vector3.zero;
        public float ArrowSize = 30;
    }
    [Space(5)]
    [Header("Arrow")]
    public m_Arrow Arrow;
    [HideInInspector]
    public bool tip = false;
    
}

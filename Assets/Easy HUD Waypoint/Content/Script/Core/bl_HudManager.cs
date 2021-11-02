using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bl_HudManager : MonoBehaviour
{

    /// <summary>
    /// All huds in scene
    /// </summary>
    [Tooltip("Hud list manager, you can add a new hud directly here.")]
    public List<bl_HudInfo> Huds = new List<bl_HudInfo>();
    /// <summary>
    /// Player transform
    /// </summary>
    [Space(5)]
    [Tooltip("You can use MainCamera or the root of your player")]
    public Transform LocalPlayer = null;
    [Space(5)]
    public float clampBorder = 12;
    public bool useGizmos = true;
    [Space(5)]
    [Header("GUI Scaler")]
    [Tooltip("The resolution the UI layout is designed for. If the screen resolution is larger, the GUI will be scaled up, and if it's smaller, the GUI will be scaled down. This is done in accordance with the Screen Match Mode.")]
    public Vector2 m_ReferenceResolution = new Vector2(800f, 600f);
    [Range(0f, 1f), Tooltip("Determines if the scaling is using the width or height as reference, or a mix in between."), SerializeField]
    public float m_MatchWidthOrHeight;
     [Tooltip("Select Reference Resolution automatically in run time.")]
    public bool AutoScale = true;
    public GUIStyle TextStyle;


    private static bl_HudManager _instance;

    //Get singleton
    public static bl_HudManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<bl_HudManager>();
            }
            return _instance;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    void OnDestroy()
    {
        _instance = null;
    }
    /// <summary>
    /// 
    /// </summary>
    void FixedUpdate() { if (AutoScale) { m_ReferenceResolution.x = Screen.width; m_ReferenceResolution.y = Screen.height; } }
    /// <summary>
    /// Draw a icon foreach in list
    /// </summary>
    void OnGUI()
    {
        if (bl_HudUtility.mCamera == null)
            return;
        if (LocalPlayer == null)
            return;
        //pass test :)

        for (int i = 0; i < Huds.Count; i++)
        {
            if (!Huds[i].Hide)
            {
                OnScreen(i);
                OffScreen(i);
            }
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="i"></param>
    void OnScreen(int i)
    {
        //if transform destroy, then remove from list
        if (Huds[i].m_Target == null)
        {
            Huds.Remove(Huds[i]);
            return;
        }

        //Calculate Position of target
        Vector3 RelativePosition = Huds[i].m_Target.position + Huds[i].Offset;
        if ((Vector3.Dot(this.LocalPlayer.forward, RelativePosition - this.LocalPlayer.position) > 0f))
        {
            //Calculate the 2D position of the position where the icon should be drawn
            Vector3 point = bl_HudUtility.mCamera.WorldToViewportPoint(RelativePosition);

            //The viewportPoint coordinates are between 0 and 1, so we have to convert them into screen space here
            Vector2 drawPosition = new Vector2(point.x * Screen.width, Screen.height * (1 - point.y));

            if (!Huds[i].Arrow.ShowArrow)
            {
                //Clamp the position to the edge of the screen in case the icon would be drawn outside the screen
                drawPosition.x = Mathf.Clamp(drawPosition.x, clampBorder, Screen.width - clampBorder);
                drawPosition.y = Mathf.Clamp(drawPosition.y, clampBorder, Screen.height - clampBorder);
            }
            //Caculate distance from player to waypoint
            float Distance = Vector3.Distance(this.LocalPlayer.position, RelativePosition);
            //Cache distance
            float CompleteDistance = Distance;
            //Max Hud Increment 
            if (Distance > 50) // if more than "50" no increase more
            {
                Distance = 50;
            }
            float n;
            //Calculate depend of type 
            if (Huds[i].m_TypeHud == TypeHud.Decreasing)
            {
                n = (((50 + Distance) / (50 * 2f)) * 0.9f) + 0.1f;
            }
            else
            {
                n = (((50 - Distance) / (50 * 2f)) * 0.9f) + 0.1f;
            }
            //Calculate Size of Hud
            float sizeX = Huds[i].m_Icon.width * n;
            if (sizeX >= Huds[i].m_MaxSize)
            {
                sizeX = Huds[i].m_MaxSize;
            }
            float sizeY = Huds[i].m_Icon.height * n;
            if (sizeY >= Huds[i].m_MaxSize)
            {
                sizeY = Huds[i].m_MaxSize;
            }
            //palpiting effect
            if (Huds[i].isPalpitin)
            {
                if (Huds[i].m_Color.a <= 0)
                {
                    Huds[i].tip = false;
                }
                else if (Huds[i].m_Color.a >= 1)
                {
                    Huds[i].tip = true;
                }
                //Create a loop
                if (Huds[i].tip == false)
                {
                    Huds[i].m_Color.a += Time.deltaTime * 0.5f;
                }
                else
                {
                    Huds[i].m_Color.a -= Time.deltaTime * 0.5f;
                }
            }

            //Draw Huds
            GUI.color = Huds[i].m_Color;
            GUI.DrawTexture(new Rect(drawPosition.x - ((sizeX) / 2), drawPosition.y - ((sizeY) / 2), sizeX, sizeY), Huds[i].m_Icon);
            if (!Huds[i].ShowDistance)
            {
                GUI.Label(new Rect((drawPosition.x - Huds[i].m_Text.Length), (drawPosition.y - n) - 35f, 300, 50), Huds[i].m_Text, TextStyle);
            }
            else
            {
                GUI.Label(new Rect((drawPosition.x - Huds[i].m_Text.Length), (drawPosition.y - n) - 35f, 300, 60), Huds[i].m_Text + "\n <color=whitte>[" + string.Format("{0:N0}m",CompleteDistance) + "]</color>", TextStyle);
            }
        }
    }
    /// <summary>
    /// When the target if not in Player sight 
    /// </summary>
    /// <param name="i"></param>
    void OffScreen(int i)
    {
        if (Huds[i].Arrow.ArrowIcon != null && Huds[i].Arrow.ShowArrow)
        {
            //Get the relative position of arrow
            Vector3 ArrowPosition = Huds[i].m_Target.position + Huds[i].Arrow.ArrowOffset;
            Vector3 pointArrow = bl_HudUtility.mCamera.WorldToScreenPoint(ArrowPosition);

            pointArrow.x = pointArrow.x / bl_HudUtility.mCamera.pixelWidth;
            pointArrow.y = pointArrow.y / bl_HudUtility.mCamera.pixelHeight;

            Vector3 mForward = Huds[i].m_Target.position - bl_HudUtility.mCamera.transform.position;
            Vector3 mDir = bl_HudUtility.mCamera.transform.InverseTransformDirection(mForward);
            mDir = mDir.normalized / 5;
            pointArrow.x = 0.5f + mDir.x * 20f / bl_HudUtility.mCamera.aspect;
            pointArrow.y = 0.5f + mDir.y * 20f;

            if (pointArrow.z < 0)
            {
                pointArrow *= -1f;
                pointArrow *= -1f;
            }
            //Arrow
            GUI.color = Huds[i].m_Color;

            float Xpos = bl_HudUtility.mCamera.pixelWidth * pointArrow.x;
            float Ypos = bl_HudUtility.mCamera.pixelHeight * (1f - pointArrow.y);
            //Check target if OnScreen
            if (!bl_HudUtility.isOnScreen(bl_HudUtility.ScreenPosition(Huds[i].m_Target), Huds[i].m_Target))
            {
                //Calculate area to rotate guis
                float mRot = bl_HudUtility.GetRotation(bl_HudUtility.mCamera.pixelWidth / (2), bl_HudUtility.mCamera.pixelHeight / (2), Xpos, Ypos);
              //Get pivot from area
                Vector2 mPivot = bl_HudUtility.GetPivot(Xpos, Ypos, Huds[i].Arrow.ArrowSize);
                //Arrow
                Matrix4x4 matrix = GUI.matrix;
                GUIUtility.RotateAroundPivot(mRot, mPivot);
                GUI.DrawTexture(new Rect(mPivot.x - bl_HudUtility.HalfSize(Huds[i].Arrow.ArrowSize), mPivot.y - bl_HudUtility.HalfSize(Huds[i].Arrow.ArrowSize), Huds[i].Arrow.ArrowSize, Huds[i].Arrow.ArrowSize), Huds[i].Arrow.ArrowIcon);
                GUIUtility.RotateAroundPivot(-mRot, mPivot);
                GUI.matrix = matrix;

                //Icons and Text
                Vector2 marge = bl_HudUtility.Marge(mPivot, 25);
                if (!Huds[i].ShowDistance)
                {
                    GUI.Label(bl_HudUtility.ScalerRect(new Rect(mPivot.x + marge.x, mPivot.y + marge.y, 300, 75)), Huds[i].m_Text, TextStyle);
                }
                else
                {
                    float Distance = Vector3.Distance(this.LocalPlayer.position, Huds[i].m_Target.position);
                    GUI.Label(bl_HudUtility.ScalerRect(new Rect(mPivot.x + marge.x, mPivot.y + marge.y, 300, 75)), Huds[i].m_Text + "\n <color=whitte>[" + string.Format("{0:N0}m", Distance) + "]</color>", TextStyle);
                }

                GUI.DrawTexture(bl_HudUtility.ScalerRect(new Rect(mPivot.x + marge.x,(mPivot.y + ((!Huds[i].ShowDistance) ? 10 : 20)) + marge.y, 25, 25)), Huds[i].m_Icon);
            }
            GUI.color = Color.white;
        }
    }

    //Add a new Huds from instance to the list
    public void CreateHud(bl_HudInfo info)
    {
        Huds.Add(info);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="i"></param>
    public void RemoveHud(int i)
    {
        Huds.RemoveAt(i);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="hud"></param>
    public void RemoveHud(bl_HudInfo hud)
    {
        if (Huds.Contains(hud))
        {
            Huds.Remove(hud);
        }
        else
        {
            Debug.Log("Huds list dont contain this hud!");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="i">Id of hud in list</param>
    public void HideStateHud(int i,bool hide = false)
    {
        if (Huds[i] != null)
        {
            Huds[i].Hide = hide;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="i">Id of hud in list</param>
    public void HideStateHud(bl_HudInfo hud, bool hide = false)
    {
        if (Huds.Contains(hud))
        {
            for (int i = 0; i < Huds.Count; i++)
            {
                if (Huds[i] == hud)
                {
                    Huds[i].Hide = hide;
                }
            }
        }
    }
    /// <summary>
    /// Visual Debug of all waypoints in scene
    /// </summary>
    void OnDrawGizmosSelected()
    {
        if (!useGizmos)
            return;

        for (int i = 0; i < Huds.Count; i++)
        {
            if (Huds[i].m_Target != null)
            {
                Gizmos.color = new Color(0, 0.35f, 0.9f, 0.9f);
                Gizmos.DrawWireSphere(Huds[i].m_Target.position, 3);
                Gizmos.color = new Color(0, 0.35f, 0.9f, 0.3f);
                Gizmos.DrawSphere(Huds[i].m_Target.position, 3);
                if (i < Huds.Count - 1)
                {
                    Gizmos.DrawLine(Huds[i].m_Target.position, Huds[i + 1].m_Target.position);
                }
                else
                {
                    Gizmos.DrawLine(Huds[i].m_Target.position, Huds[0].m_Target.position);
                }
            }
        }
    }

}
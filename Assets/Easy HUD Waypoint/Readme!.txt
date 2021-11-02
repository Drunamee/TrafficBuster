Thanks for Buying Hud System.

Get Starting-----------------------------------------------------

1- Import Hud System package into the project.
2- Drap the HudSystem Prefabs from Prefab folder (Easy HUD Waypoint -> Content -> Prefabs) , into scene / map.
3- Add your huds in list of HudManager or add bl_Hud to a gameObject in scene.
4- Ready, enjoy!

HudManager:
Huds = List with all huds in scene.
LocalPlayer = reference position player (add the camera of Player)
ClampBoder = limited distance before the screen end (When not show arrow)
UseGizmo = Show debug gizmos in editor.
ReferenceResolution = Scale GUI from screen resolution (this is modyfi in runTime).
MatchWidthOrHeight = Determines if the scaling is using the width or height as reference, or a mix in between.
AutoScale = Select Reference Resolution automatically in run time.
TextStyle = Global hud text style.

HudInfo:
ShowDynamically = Show hud only when detect is front camera.
Target = the GameObject that will follow the gui.
Icon = Icon to draw in target.
Color = Gui color.
TypeHud = Effect type when the player moves toward or away from the target.
offset = Should the icon be drawn at a different position relative to the GameObject. Expected coordinates are in world space.
Text = text to be will show in Hud.
isPalpitin = Bliking effect?


How Create a new hud in prefabs?
- add the script bl_Hud to the prefabs and complete the HudInfo.
when the prefab is instantiated, it will be added automatically to Hud Manager.

Tips:

How hide / show a hud in runTime:
- Firts you need access the "bl_Hud.cs" you want to hide / show or have the id of list of hud in bl_HudManager.cs
- If you have the bl_Hud reference, just call to publics functions "Hide();" or "Show()"; eg: bl_HudManager.instance.HideStateHud(HudInfoReference, false); or bl_HudManager.instance.HideStateHud((int)hudId, false);
- If you have the id of list of bl_HudManager, do like this bl_HudManager.instance.huds[id].Hide = true / false;



Support: http://lovattostudio.com/Forum/index.php
Email: brinerjhonson.lc@gmail.com
Lovatto Studio 2015.
using UnityEngine;
using System.Collections;

public class bl_RayHelper : MonoBehaviour {

    public float DistanceCheck = 50f;

    private bl_Hud cacheHud = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        
        Vector3 fwr = this.transform.forward;
        Debug.DrawRay(this.transform.position,fwr, Color.green);

        if (Physics.Raycast(this.transform.position, fwr, out hit, DistanceCheck))
        {
            if (hit.transform.GetComponent<bl_Hud>() != null)
            {
                if (hit.transform.GetComponent<bl_Hud>().HudInfo.ShowDynamically)
                {
                    cacheHud = hit.transform.GetComponent<bl_Hud>();
                    cacheHud.Show();
                }
            }
        }
        else
        {
            if (cacheHud != null)
            {
                cacheHud.Hide();
                cacheHud = null;
            }
        }
    }
}
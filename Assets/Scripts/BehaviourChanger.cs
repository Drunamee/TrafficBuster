using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourChanger : MonoBehaviour
{
    public BCG_EnterExitVehicle Vehicle;
    public int BehaviourType;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        BCG_EnterExitPlayer.OnBCGPlayerEnteredAVehicle += OnSitInCar;
    }
    void OnSitInCar(BCG_EnterExitPlayer player, BCG_EnterExitVehicle vehicle)
    {
        if(vehicle == this.Vehicle)
        {
            RCC_SceneManager.SetBehavior(BehaviourType);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class CheeseSystem : AbstractSystem
{
    private CheeseModel cheeseModel;
    
    protected override void OnInit()
    {
        cheeseModel = Global.Interface.GetModel<CheeseModel>();
    }
    
    //We should generate the Cheese gameplay here - leave Model for reading the raw data
    
}

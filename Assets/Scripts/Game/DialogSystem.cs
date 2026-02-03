using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class DialogSystem : AbstractSystem, IController
{
    public IArchitecture GetArchitecture() => Global.Interface;
    
    protected override void OnInit()
    {
    }
}

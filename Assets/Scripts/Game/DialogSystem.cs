using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class DialogSystem : AbstractSystem, IController
{
    public IArchitecture GetArchitecture() => Global.Interface;
    private DialogModel dialogModel => this.GetModel<DialogModel>();
    protected override void OnInit()
    {
    }
    
    public void LoadNewScene(int level)
    {
        dialogModel.LoadCurrentScene(level);
    }
    
}

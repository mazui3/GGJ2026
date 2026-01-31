using System.Collections;
using System.Collections.Generic;
using cfg.Data;
using cfg.Enum;
using QFramework;
using UnityEngine;

public class CheeseSystem : AbstractSystem
{
    /*
    StateReset (逻辑启动)：调用 CheeseSystem 来准备数据。
    CheeseSystem (逻辑处理)：计算该关卡需要多少芝士，并将数据存入 CheeseModel。
    CheeseModel (数据持久化)：数据更新后，发送一个事件（或通过 BindableProperty）。
    CheesePanel (表现层)：监听到数据变化的信号，从 System 或 Model 中读取数据并渲染。
    */
    
    private CheeseModel cheeseModel => this.GetModel<CheeseModel>();
    protected override void OnInit()
    {
    }
    
    //We should generate the Cheese gameplay here - leave Model for reading the raw data

    public void LoadNewScene(int level)
    {
        cheeseModel.LoadCurrentScene(level);
    }

    public void EnableCheeseDrag()
    {
        cheeseModel.EnableCheeseDrag();
    }
 
}

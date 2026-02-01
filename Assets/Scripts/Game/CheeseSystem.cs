using System.Collections;
using System.Collections.Generic;
using cfg.Data;
using cfg.Enum;
using QFramework;
using QFramework.Example;
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

    public void UpdateCheeseWords(List<CheeseWord> resultWords)
    {
        int resultWordCount = resultWords.Count;
        int expectWordCount = cheeseModel.CurrentScene.Value.SentenceStructure.Count;

        if (resultWordCount < expectWordCount)
        {
            return;
        }
        
        foreach (var expectedWord in cheeseModel.CurrentScene.Value.SentenceStructure)
        {
            
        }
    }
 
}

public class DropCheeseCommand : AbstractCommand
{
    private readonly List<CheeseWord> words;
    
    public DropCheeseCommand(List<CheeseWord> resultWords)
    {
        words = resultWords;
    }
    
    protected override void OnExecute()
    {
        // 1. 让 CheeseSystem 处理计算逻辑（比如是否掉进了洞里）
        // var isSuccess = this.GetSystem<CheeseSystem>().HandleDropResult();
        //
        // // 2. 根据计算结果，直接告诉 GamePlaySystem 切换状态
        // // 假设你的 IGamePlaySystem 接口里有 ChangeState 方法
        // if (isSuccess)
        // {
        //     this.GetSystem<GamePlaySystem>().ChangeState(GamePlayType.Dialog); 
        // }
        // else
        // {
        //     // 失败了可能去另一个状态，或者重试
        //     this.GetSystem<GamePlaySystem>().ChangeState(GamePlayType.Reset);
        // }
    }
}
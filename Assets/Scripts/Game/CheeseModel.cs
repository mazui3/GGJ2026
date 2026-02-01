using System.Collections;
using System.Collections.Generic;
using cfg.Data;
using cfg.Enum;
using QFramework;
using UnityEngine;

public class CheeseModel : AbstractModel
{
    // 使用 BindableProperty，这样 UI 可以监听它的变化
    public BindableProperty<CheeseScene> CurrentScene = new BindableProperty<CheeseScene>();
    public BindableProperty<bool> CheeseDrag = new BindableProperty<bool>();
    public BindableProperty<bool> IsAnswerVisible = new BindableProperty<bool>(false);
    public BindableProperty<bool> IsQuestionVisible = new BindableProperty<bool>(false);

    protected override void OnInit()
    {
    }

    public void LoadCurrentScene(int level)
    {
        CurrentScene.Value = TableManager.Instance.Tables.TbCheeseScene.Get(level);
        // if change here, ui panel should load - using the current cheese list below
    }

    public void EnableCheeseDrag()
    {
        CheeseDrag.Value = true;
    }
    
    public void DisableCheeseDrag()
    {
        CheeseDrag.Value = false;
    }
    
    public List<CheeseDictionary> CurrentCheeseList()
    {
        List<CheeseDictionary> results = new List<CheeseDictionary>();
        
        if (CurrentScene == null)
            return null;
        else
        {
            foreach (var word in CurrentScene.Value.Words)
            {
                results.Add(TableManager.Instance.Tables.TbCheeseDictionary.Get(word));
            }
        }

        return results;
    }
}

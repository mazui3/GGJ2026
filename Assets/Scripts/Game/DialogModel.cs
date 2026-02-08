using System.Collections;
using System.Collections.Generic;
using cfg.Data;
using QFramework;
using UnityEngine;

public class DialogModel : AbstractModel
{
    // 使用 BindableProperty，这样 UI 可以监听它的变化
    public BindableProperty<Dialog> CurrentDialog = new BindableProperty<Dialog>();
    public BindableProperty<bool> IsDialogVisible = new BindableProperty<bool>(false);
    
    public BindableProperty<int> CurrentDialogIndex = new BindableProperty<int>(0);
    protected override void OnInit()
    {
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Current each id has one dialog, may change later
    public void LoadCurrentScene(int id)
    {
        //index value change do not have onChangeTrigger, but others do, need to pay attention to the order
        CurrentDialogIndex.Value = 0;
        CurrentDialog.Value = TableManager.Instance.Tables.TbDialog.Get(id);
        IsDialogVisible.Value = true;
        // if change here, ui panel should load
    }

    public void NextDialog()
    {
        if (CurrentDialogIndex.Value >= CurrentDialog.Value.DialogData.Count - 1)
        {
            IsDialogVisible.Value = false;
            // Global.Interface.GetSystem<CheeseSystem>().DisplayQuestion(true);
            Global.Interface.GetSystem<GamePlaySystem>().ChangeState(GamePlayType.PickCheese);
        }
        else
        {
            CurrentDialogIndex.Value++;
        }
        
    }

}

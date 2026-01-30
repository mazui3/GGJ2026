using System.Collections;
using System.Collections.Generic;
using cfg.Data;
using QFramework;
using UnityEngine;

public enum GamePlayType
{
    Reset,//generate set up
    Dialog,
    PickCheese,
    DropCheese,
    GenerateSentence//return result
}

public class GamePlayRuntimeData
{
    
}

public class GamePlaySystem : AbstractSystem
{
    private FSM<GamePlayType> fsm = new FSM<GamePlayType>();
    //     
    public FSM<GamePlayType> Fsm => fsm;
    
    public GamePlayRuntimeData Data;
    public CheeseScene SceneData;
    
    protected override void OnInit()
    {
        ClearData();
        SetUpFsm();
    }
    
    private void ClearData()
    {
        Data = new GamePlayRuntimeData();
    }

    private void SetUpFsm()
    {
        fsm.AddState(GamePlayType.Reset, new StateReset(fsm, this));
        fsm.AddState(GamePlayType.Dialog, new StateDialog(fsm, this));
        fsm.AddState(GamePlayType.PickCheese, new StatePickCheese(fsm, this));
        fsm.AddState(GamePlayType.DropCheese, new StateDropCheese(fsm, this));
        fsm.AddState(GamePlayType.GenerateSentence, new StateGenerateSentence(fsm, this));
        fsm.StartState(GamePlayType.Reset);
    }
    
    public class StateReset : AbstractState<GamePlayType, GamePlaySystem>
    {
        public StateReset(FSM<GamePlayType> fsm, GamePlaySystem target) : base(fsm, target)
        {
        }
        
        protected override void OnEnter()
        {
            base.OnEnter();
        }

        protected override void OnExit()
        {
            base.OnExit();
        }
    }
    
    public class StateDialog : AbstractState<GamePlayType, GamePlaySystem>
    {
        public StateDialog(FSM<GamePlayType> fsm, GamePlaySystem target) : base(fsm, target)
        {
        }
        
        protected override void OnEnter()
        {
            // 监听结束事件
            // mTarget.RegisterEvent<ASMRStartFinishEvent>(OnStartStageFinishEventReceive);
            // 打开ASMR主页面
            // var panel = UIKit.OpenPanel<ASMRMainPanel>();
            // panel.InitStepGroup(mTarget.Data.Config.Groups.Count);
            // panel.SetConfig(mTarget.Data.Config);
            // panel.SetCloseBtn(TableManager.Instance.TbASMRLevel.Get(mTarget.Data.LevelId).CanExit);
            // mTarget.SendEvent(new ASMRStartFinishEvent());
        }
     
        // private void OnStartStageFinishEventReceive(ASMRStartFinishEvent evt)
        // {
        //     mFSM.ChangeState(ASMRGamePlayState.PlaySpine);
        // }
        
        protected override void OnExit()
        {
            // mTarget.UnRegisterEvent<ASMRStartFinishEvent>(OnStartStageFinishEventReceive);
        }
    }
    
    public class StatePickCheese : AbstractState<GamePlayType, GamePlaySystem>
    {
        public StatePickCheese(FSM<GamePlayType> fsm, GamePlaySystem target) : base(fsm, target)
        {
        }

        protected override bool OnCondition()
        {
            return true;
        }

        protected override void OnEnter()
        {
            // mTarget.RegisterEvent<ASMRInteractionFinishEvent>(OnInteractionFinishEventReceive);
            // DoPlayerInterAction();
        }
   

        protected override void OnExit()
        {
          
        }
        
    }
    
    public class StateDropCheese : AbstractState<GamePlayType, GamePlaySystem>
    {
        public StateDropCheese(FSM<GamePlayType> fsm, GamePlaySystem target) : base(fsm, target)
        {
        }
    }
    
    public class StateGenerateSentence : AbstractState<GamePlayType, GamePlaySystem>
    {
        public StateGenerateSentence(FSM<GamePlayType> fsm, GamePlaySystem target) : base(fsm, target)
        {
        }
    }
    
    
}

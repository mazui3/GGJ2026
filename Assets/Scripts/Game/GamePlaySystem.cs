using System.Collections;
using System.Collections.Generic;
using cfg.Data;
using QFramework;
using Unity.VisualScripting;
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
    public int CurrentLevel;
}

public interface IGamePlaySystem : ISystem
{
    void ChangeState(GamePlayType newState);
    void UpdateState(); // 由 Controller 驱动
}

public class GamePlaySystem : AbstractSystem
{ 
    public FSM<GamePlayType> mFSM = new FSM<GamePlayType>();
    
    public GamePlayRuntimeData Data { get; set; } = new GamePlayRuntimeData();
    public CheeseScene SceneData;// do I really need this
    
    protected override void OnInit()
    {
        ClearData();
        SetUpFsm();
    }
    
    public void Start()
    {       
        ClearData();
        StartLevel(0);
    }
    
    private void ClearData()
    {
        Data = new GamePlayRuntimeData();
    }

    private void StartLevel(int level)
    {
        Data.CurrentLevel = level;
    }

    public void ChangeState(GamePlayType newState)
    {
        mFSM.ChangeState(newState);
    }
    
    private void SetUpFsm()
    {
        mFSM.AddState(GamePlayType.Reset, new StateReset(mFSM, this));
        mFSM.AddState(GamePlayType.Dialog, new StateDialog(mFSM, this));
        mFSM.AddState(GamePlayType.PickCheese, new StatePickCheese(mFSM, this));
        mFSM.AddState(GamePlayType.DropCheese, new StateDropCheese(mFSM, this));
        mFSM.AddState(GamePlayType.GenerateSentence, new StateGenerateSentence(mFSM, this));
        mFSM.StartState(GamePlayType.Reset);
    }
    
    public class StateReset : AbstractState<GamePlayType, GamePlaySystem>
    {
        public StateReset(FSM<GamePlayType> fsm, GamePlaySystem target) : base(fsm, target)
        {
        }
        
        protected override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Reset State");
            
            // mOwner.SendEvent(new ResetGameEvent { Level = mOwner.Data.CurrentLevel });
            // mOwner.GetSystem<CheeseSystem>().CreateCheese();
            mOwner.GetSystem<CheeseSystem>().LoadNewScene(mOwner.Data.CurrentLevel);
            mFSM.ChangeState(GamePlayType.Dialog);
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
            base.OnEnter();
            Debug.Log("Dialog State");
            
            //TODO
            mFSM.ChangeState(GamePlayType.PickCheese);
            
            // 监听结束事件
            // mTarget.RegisterEvent<ASMRStartFinishEvent>(OnStartStageFinishEventReceive);
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
        
        // mTarget.RegisterEvent<ASMRInteractionFinishEvent>(OnInteractionFinishEventReceive);
        // DoPlayerInterAction();
        protected override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("PickCheese State");
            // 你可以通过 mOwner 访问 System 里的数据

            mOwner.GetSystem<CheeseSystem>().EnableCheeseDrag();
        }

        protected override void OnUpdate()
        {
            // 如果当前状态超过 5 秒还没拿起，可以做个提示
            if (mFSM.SecondsOfCurrentState > 5.0f)
            {
                // Debug.Log("提示：请拖动屏幕上的芝士");
            }

            if (Input.GetMouseButtonDown(0))
            {
                // 逻辑处理...
                // 切换状态
                // mFSM.ChangeState(GamePlayType.DropCheese);
            }
        }
        
    }
    
    public class StateDropCheese : AbstractState<GamePlayType, GamePlaySystem>
    {
        public StateDropCheese(FSM<GamePlayType> fsm, GamePlaySystem target) : base(fsm, target)
        {
        }
        
        protected override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("DropCheese State");
            // 你可以通过 mOwner 访问 System 里的数据
        }
    }
    
    public class StateGenerateSentence : AbstractState<GamePlayType, GamePlaySystem>
    {
        public StateGenerateSentence(FSM<GamePlayType> fsm, GamePlaySystem target) : base(fsm, target)
        {
        }
        
        protected override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("GenerateSentence State");
            // 你可以通过 mOwner 访问 System 里的数据
        }
    }
    
    
}

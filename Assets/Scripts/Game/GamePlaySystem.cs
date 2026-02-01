using System.Collections;
using System.Collections.Generic;
using cfg.Data;
using QFramework;
using Unity.VisualScripting;
using UnityEngine;

public enum GamePlayType
{
    Idle,
    Reset,//generate set up
    Dialog,
    PickCheese,
    DropCheese,
    GenerateAnswer//return result
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

public class GamePlaySystem : AbstractSystem, IController
{ 
    public IArchitecture GetArchitecture() => Global.Interface;
    
    public FSM<GamePlayType> mFSM = new FSM<GamePlayType>();
    
    public GamePlayRuntimeData Data { get; set; } = new GamePlayRuntimeData();
 
    protected override void OnInit()
    {
        ClearData();
        SetUpFsm();
        
        ActionKit.OnUpdate.Register(() => 
        {
            // 只要 System 存在，就每帧驱动一次状态机
            mFSM.Update(); 
        });
    }
    
    public void Start()
    {       
        ClearData();
    }
    
    private void ClearData()
    {
        Data = new GamePlayRuntimeData();
    }

    public void StartLevel(int level)
    {
        Data.CurrentLevel = level;
    }

    public void ChangeState(GamePlayType newState)
    {
        mFSM.ChangeState(newState);
    }
    
    private void SetUpFsm()
    {
        mFSM.AddState(GamePlayType.Idle, new StateIdle(mFSM, this));
        mFSM.AddState(GamePlayType.Reset, new StateReset(mFSM, this));
        mFSM.AddState(GamePlayType.Dialog, new StateDialog(mFSM, this));
        mFSM.AddState(GamePlayType.PickCheese, new StatePickCheese(mFSM, this));
        mFSM.AddState(GamePlayType.DropCheese, new StateDropCheese(mFSM, this));
        mFSM.AddState(GamePlayType.GenerateAnswer, new StateGenerateAnswer(mFSM, this));
        mFSM.StartState(GamePlayType.Idle);
    }
    
    public class StateIdle : AbstractState<GamePlayType, GamePlaySystem>
    {
        public StateIdle(FSM<GamePlayType> fsm, GamePlaySystem target) : base(fsm, target)
        {
        }
        
        protected override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Idle State");
        }

        protected override void OnExit()
        {
            base.OnExit();
        }
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
            mOwner.GetSystem<CheeseSystem>().DisplayQuestion();
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
        
        protected override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("PickCheese State");
       
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
            mFSM.ChangeState(GamePlayType.GenerateAnswer);
        }
    }
    
    public class StateGenerateAnswer : AbstractState<GamePlayType, GamePlaySystem>
    {
        public StateGenerateAnswer(FSM<GamePlayType> fsm, GamePlaySystem target) : base(fsm, target)
        {
        }
        private float mTimer = 0;
        
        protected override void OnEnter()
        {
            mTimer = 0;
            base.OnEnter();
            Debug.Log("GenerateSentence State");
            // 你可以通过 mOwner 访问 System 里的数据
            mOwner.GetSystem<CheeseSystem>().DisplayAnswer();
        }
        
        protected override void OnUpdate()
        {
            mTimer += Time.deltaTime;
            if (mTimer >= 3.0f) // 等待 3 秒
            {
                mOwner.Data.CurrentLevel += 1;
                mFSM.ChangeState(GamePlayType.Reset);
            }
        }
    }
    
}

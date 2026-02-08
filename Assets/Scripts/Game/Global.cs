using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class Global : Architecture<Global>
{
    public static bool IsAlive;
    
    protected override void Init()
    {
        AudioKit.PlaySoundMode = AudioKit.PlaySoundModes.IgnoreSameSoundInSoundFrames;
        RegisterModel(new CheeseModel());
        RegisterSystem(new CheeseSystem());
        
        RegisterSystem(new GamePlaySystem());
        
        RegisterModel(new DialogModel());
        RegisterSystem(new DialogSystem());
        // Command
        IsAlive = true;
    }
    
    protected override void OnDeinit()
    {
        IsAlive = false;
    }
    
}

public class RestartGameCommand : AbstractCommand
{
    private readonly int level;
    
    public RestartGameCommand(int targetLevel)
    {
        level = targetLevel;
    }

    protected override void OnExecute()
    {
        this.GetSystem<CheeseSystem>().ResetData();
        this.GetSystem<GamePlaySystem>().StartLevel(1);
        this.GetSystem<GamePlaySystem>().ChangeState(GamePlayType.Reset);
    }
}
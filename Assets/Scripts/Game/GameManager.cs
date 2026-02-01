using System.Collections;
using System.Collections.Generic;
using QFramework;
using QFramework.Example;
using UnityEngine;

public class GameManager : MonoBehaviour, IController
{
    #region Singleton
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }

    private static GameManager _instance = null;
    #endregion
    
    public IArchitecture GetArchitecture() => Global.Interface;
    void Awake()
    {
       
    }
    
    // Start is called before the first frame update
    void Start()
    {
        this.GetSystem<GamePlaySystem>().Start();
        UIKit.OpenPanel<UIBasicPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        // gamePlaySystem.UpdateState();
    }
}

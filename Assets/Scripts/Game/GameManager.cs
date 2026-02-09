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
        Application.targetFrameRate = 60;
    }
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return ResKit.InitAsync();
        Debug.Log(typeof(QFramework.IRes).GetProperty("Asset"));
        
        Debug.Log("ResKit init finished");
        
        this.GetSystem<GamePlaySystem>().Start();
        yield return UIKit.OpenPanelAsync<UIStartPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        // gamePlaySystem.UpdateState();
    }
}

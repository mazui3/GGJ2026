using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public class UIStartPanelData : UIPanelData
	{
	}
	public partial class UIStartPanel : UIPanel, IController
	{
		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
		
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIStartPanelData ?? new UIStartPanelData();
			// please add init code here
			
			StartBtn.onClick.RemoveAllListeners();
			StartBtn.onClick.AddListener(() =>
			{
				Debug.Log("Button is working");
				StartCoroutine(OnStart());
			});
			
			ExitBtn.onClick.RemoveAllListeners();
			ExitBtn.onClick.AddListener(() =>
			{
				Application.Quit();
			});
		}

		IEnumerator OnStart()
		{
			this.SendCommand(new RestartGameCommand(1));
			yield return UIKit.OpenPanelAsync<UICheesePanel>();
			yield return UIKit.OpenPanelAsync<UIBasicPanel>();
			this.CloseSelf();
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}

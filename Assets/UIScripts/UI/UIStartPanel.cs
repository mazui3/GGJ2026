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
				this.SendCommand(new RestartGameCommand(1));
				UIKit.OpenPanel<UICheesePanel>();
				UIKit.OpenPanel<UIBasicPanel>();
				this.CloseSelf();
			});
			
			ExitBtn.onClick.RemoveAllListeners();
			ExitBtn.onClick.AddListener(() =>
			{
				Application.Quit();
			});
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

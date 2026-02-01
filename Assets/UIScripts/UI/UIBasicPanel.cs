using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public class UIBasicPanelData : UIPanelData
	{
	}
	public partial class UIBasicPanel : UIPanel, IController
	{
		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
		
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIBasicPanelData ?? new UIBasicPanelData();
			// please add init code here
			
			StartBtn.onClick.RemoveAllListeners();
			StartBtn.onClick.AddListener(() =>
			{
				this.SendCommand(new RestartGameCommand(1));
				UIKit.OpenPanel<UICheesePanel>();
				this.CloseSelf();
			});
			
			ExitBtn.onClick.RemoveAllListeners();
			ExitBtn.onClick.AddListener(() =>
			{
				Application.Quit();
			});
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
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

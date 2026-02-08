using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public class UIEndPanelData : UIPanelData
	{
	}
	public partial class UIEndPanel : UIPanel, IController
	{
		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIEndPanelData ?? new UIEndPanelData();
			// please add init code here
			this.GetSystem<CheeseSystem>().CheeseAnwsers.RegisterWithInitValue(theAnswers =>
			{
				Endroll.text = theAnswers;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			BackBtn.onClick.RemoveAllListeners();
			BackBtn.onClick.AddListener(() =>
			{
				UIKit.OpenPanel<UIStartPanel>();
				this.CloseSelf();
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

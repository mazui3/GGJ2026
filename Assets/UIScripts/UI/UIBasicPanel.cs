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
			
			this.GetModel<CheeseModel>().IsAnswerVisible.Register(isVisible => 
			{
				if (isVisible)
				{
					DisplayAnswer();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			this.GetModel<CheeseModel>().IsQuestionVisible.Register(isVisible => 
			{
				if (isVisible)
				{
					DisplayQuestion();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
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
		
		private void DisplayAnswer()
		{
			// 更新 UI 表现，比如播放个特效或者变色
			AnswerText.text = this.GetSystem<CheeseSystem>().GetCheeseAnswer(this.GetModel<CheeseModel>().CurrentScene.Value.Id);
		}

		private void DisplayQuestion()
		{
			QuestionText.text = this.GetModel<CheeseModel>().CurrentScene.Value.Question;
		}
	}
}

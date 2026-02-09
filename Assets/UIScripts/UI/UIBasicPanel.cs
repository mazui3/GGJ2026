using cfg.Enum;
using DG.Tweening;
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

		// private Transform DialogTransformForReset;
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIBasicPanelData ?? new UIBasicPanelData();
			// please add init code here

			// DialogTransformForReset = Dialog.transform;
			
			this.GetModel<DialogModel>().IsDialogVisible.Register(isDialogVisible =>
			{
				if (!isDialogVisible)
				{
					PopupHide(Dialog.transform);
				}
				else
				{
					Dialog.SetActive(true);
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			this.GetModel<DialogModel>().CurrentDialog.RegisterWithInitValue(dialog => 
			{
				if (dialog != null)
				{
					InitDialog(); 
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			this.GetModel<DialogModel>().CurrentDialogIndex.Register(dialogIndex => 
			{
				DisplayNextDialog(); 
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			//why I put it in the CheeseModel, should I?
			this.GetModel<CheeseModel>().IsQuestionVisible.Register(isEnable =>
			{
				DisplayQuestion(isEnable);
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			this.GetModel<CheeseModel>().IsAnswerVisible.Register(isEnable =>
			{
				DisplayAnswer(isEnable);
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			// DialogBtn
			DialogBtn.onClick.RemoveAllListeners();
			DialogBtn.onClick.AddListener(() =>
			{
				LoadNextDialog();
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
		
		#region move to cheese panel
		private void DisplayAnswer(bool isEnable)
		{
			// 更新 UI 表现，比如播放个特效或者变色
			if (isEnable)
			{
				Answer.gameObject.SetActive(true);
				AnswerText.text = this.GetSystem<CheeseSystem>().GetCheeseAnswer(this.GetModel<CheeseModel>().CurrentScene.Value.Id);
				PopupOpenAnswer(Answer.transform);
			}
			else
			{
				PopupHideAnswer(Answer.transform);
			}
			
		}
		private void DisplayQuestion(bool isEnable)
		{
			if (isEnable)
			{
				Question.gameObject.SetActive(true);
				QuestionText.text = this.GetModel<CheeseModel>().CurrentScene.Value.Question;
				PopupOpen(Question.transform);
			}
			else
			{
				PopupHide(Question.transform);
			}
		
		}
		#endregion
		private void InitDialog()
		{
			//let model to handle the data change first then fresh ui
			DisplayNextDialog();
		}
		
		//ui change model data - for btn action
		private void LoadNextDialog()
		{
			this.SendCommand(new LoadNextDialogCommand());
		}
		
		// then model data bind ui change
		private void DisplayNextDialog()
		{
			var currentDialog = this.GetModel<DialogModel>().CurrentDialog.Value.DialogData
				[this.GetModel<DialogModel>().CurrentDialogIndex.Value];
			LeftDialog.SetActive(false);
			RightDialog.SetActive(false);
			if (currentDialog != null)
			{
				if (currentDialog.DialogBoxType == DialogType.Left)
				{
					LeftDialog.SetActive(true);
					LeftDialogText.text = currentDialog.DialogContent;
				}
				else if (currentDialog.DialogBoxType == DialogType.Right)
				{
					RightDialog.SetActive(true);
					RightDialogText.text = currentDialog.DialogContent;
				}
			}
			PopupOpen(Dialog.transform);
		}

		#region dotween part
		
		private float animationTime = 0.3f;
		private float slideDistance = 20f; // 滑动距离
		private Vector3 originalPosition;
		
		
		public void PopupOpen(Transform theTransform)
		{
			var canvasGroup = theTransform.GetComponent<CanvasGroup>();
			canvasGroup.alpha = 1f;
			originalPosition = theTransform.position;
			
			theTransform.position = originalPosition - new Vector3(0, slideDistance, 0);
			theTransform.DOMoveY(originalPosition.y, animationTime)
				.SetEase(Ease.OutSine)
				.From(theTransform.position);
		}
		
		public void PopupHide(Transform theTransform)
		{
			var canvasGroup = theTransform.GetComponent<CanvasGroup>();
			canvasGroup.DOFade(0f, animationTime)
				.SetEase(Ease.OutQuad)
				.OnComplete(() =>
				{
					theTransform.position = originalPosition + new Vector3(0, slideDistance, 0);
					theTransform.gameObject.SetActive(false);
				});
		}
		
		//there should be a better way to do this, right?
		private Vector3 originalPositionAnswer;
		public void PopupOpenAnswer(Transform theTransform)
		{
			var canvasGroup = theTransform.GetComponent<CanvasGroup>();
			canvasGroup.alpha = 1f;
			originalPositionAnswer = theTransform.position;
			
			theTransform.position = originalPositionAnswer - new Vector3(0, slideDistance, 0);
			theTransform.DOMoveY(originalPositionAnswer.y, animationTime)
				.SetEase(Ease.OutSine)
				.From(theTransform.position);
		}
		
		public void PopupHideAnswer(Transform theTransform)
		{
			var canvasGroup = theTransform.GetComponent<CanvasGroup>();
			canvasGroup.DOFade(0f, animationTime)
				.SetEase(Ease.OutQuad)
				.OnComplete(() =>
				{
					theTransform.position = originalPositionAnswer + new Vector3(0, slideDistance, 0);
					theTransform.gameObject.SetActive(false);
				});
		}
		#endregion
	}
}

public class LoadNextDialogCommand : AbstractCommand
{
	public LoadNextDialogCommand()
	{
	}
	protected override void OnExecute()
	{
		this.GetModel<DialogModel>().NextDialog();
	}
}

//DialogModel NextDialog handled the communication, command is not used
// public class EndDialogCommand : AbstractCommand
// {
// 	public EndDialogCommand()
// 	{
// 	}
// 	
// 	protected override void OnExecute()
// 	{
// 		this.GetSystem<CheeseSystem>().DisplayQuestion(true);
// 		this.GetSystem<GamePlaySystem>().ChangeState(GamePlayType.PickCheese);
// 	}
// }

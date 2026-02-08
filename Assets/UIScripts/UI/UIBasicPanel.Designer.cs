using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:aafa8b20-fe00-4b26-b1b0-7fab59a2832b
	public partial class UIBasicPanel
	{
		public const string Name = "UIBasicPanel";
		
		[SerializeField]
		public UnityEngine.RectTransform Answer;
		[SerializeField]
		public TMPro.TextMeshProUGUI AnswerText;
		[SerializeField]
		public RectTransform Question;
		[SerializeField]
		public TMPro.TextMeshProUGUI QuestionText;
		[SerializeField]
		public UnityEngine.GameObject Dialog;
		[SerializeField]
		public UnityEngine.GameObject LeftDialog;
		[SerializeField]
		public TMPro.TextMeshProUGUI LeftDialogText;
		[SerializeField]
		public UnityEngine.GameObject RightDialog;
		[SerializeField]
		public TMPro.TextMeshProUGUI RightDialogText;
		[SerializeField]
		public UnityEngine.UI.Button DialogBtn;
		[SerializeField]
		public UnityEngine.UI.Image SettingPanel;
		
		private UIBasicPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Answer = null;
			AnswerText = null;
			Question = null;
			QuestionText = null;
			Dialog = null;
			LeftDialog = null;
			LeftDialogText = null;
			RightDialog = null;
			RightDialogText = null;
			DialogBtn = null;
			SettingPanel = null;
			
			mData = null;
		}
		
		public UIBasicPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIBasicPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIBasicPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}

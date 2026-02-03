using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:9022bfd4-7c2c-4a85-bad7-cededd7259a2
	public partial class UIBasicPanel
	{
		public const string Name = "UIBasicPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image Answer;
		[SerializeField]
		public TMPro.TextMeshProUGUI AnswerText;
		[SerializeField]
		public RectTransform Question;
		[SerializeField]
		public TMPro.TextMeshProUGUI QuestionText;
		
		private UIBasicPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Answer = null;
			AnswerText = null;
			Question = null;
			QuestionText = null;
			
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

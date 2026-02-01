using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:4b912db1-60af-4d7d-8b7f-74ad1d6002c2
	public partial class UIBasicPanel
	{
		public const string Name = "UIBasicPanel";
		
		[SerializeField]
		public TMPro.TextMeshProUGUI Answer;
		[SerializeField]
		public TMPro.TextMeshProUGUI Question;
		
		private UIBasicPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Answer = null;
			Question = null;
			
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

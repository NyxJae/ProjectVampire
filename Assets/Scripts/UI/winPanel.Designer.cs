using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectVampire
{
    // Generate Id:34086b5c-7cf7-4d95-aea6-b8388b9ccefe
    public partial class winPanel
    {
        public const string Name = "winPanel";

        [SerializeField]
        public UnityEngine.UI.Button BtnReplay;

        private winPanelData mPrivateData = null;

        protected override void ClearUIComponents()
        {
            BtnReplay = null;

            mData = null;
        }

        public winPanelData Data
        {
            get
            {
                return mData;
            }
        }

        winPanelData mData
        {
            get
            {
                return mPrivateData ?? (mPrivateData = new winPanelData());
            }
            set
            {
                mUIData = value;
                mPrivateData = value;
            }
        }
    }
}

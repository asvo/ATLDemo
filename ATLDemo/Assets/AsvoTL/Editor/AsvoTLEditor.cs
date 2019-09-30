/* ***********************************************
 * AsvoTLEditor
 * author :  created by asvo
 * function: 
 * history:  created at .
 * ***********************************************/
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Asvo
{
	public class AsvoTLEditor : EditorWindow {

        private string mCurrentTLAssetName = string.Empty;
        protected ATimelineProcesser mTLPlayer;
        protected ATimeLine mData;

        private GenericMenu menu_track = new GenericMenu();

        #region res
        private Texture tex_icon_track;
        #endregion

        [MenuItem("Asvo Tools/Timeline Editor")]
        static void OpenTLWnd()
        {
            EditorWindow.GetWindow<AsvoTLEditor>().Show();
        }

        private void OnEnable()
        {
            //titile
            this.titleContent = new GUIContent("TL Editor");
            InitAssets();

            mCurrentTLAssetName = string.Empty;
            GameObject go = GameObject.Find("ATimelinePlayer");
            if (go != null)
            {
                mTLPlayer = go.GetComponent<ATimelineProcesser>();
                if (mTLPlayer)
                {
                    mData = mTLPlayer.m_TimelineData;
                    if (mData)
                    {
                        if (!string.IsNullOrEmpty(mData.name))
                            mCurrentTLAssetName = mData.name;
                    }
                }
            }
            buildAddTrackMenu();
        }

        private void InitAssets()
        {
            tex_icon_track = (Texture)Resources.Load("am_icon_track");
        }

        private void buildAddTrackMenu()
        {
            menu_track.AddItem(new GUIContent("Add Track"), false, AddTrackFromMenu);
        }

        private void OnGUI()
        {
            CheckNullGobj();
            RenderHead();
            RenderOptions();
            RenderTracks();
        }

        private void CheckNullGobj()
        {
            if (null == mTLPlayer)
            {
                GameObject go = GameObject.Find("ATimelinePlayer");
                if (go != null)
                {
                    mTLPlayer = go.GetComponent<ATimelineProcesser>();
                }
                else
                {
                    go = new GameObject("ATimelinePlayer");
                    mTLPlayer = go.AddComponent<ATimelineProcesser>();
                }
            }
            if (null == mData)
            {
                mData = mTLPlayer.m_TimelineData;
                if (!mData)
                {
                    mData = ScriptableObject.CreateInstance<ATimeLine>();
                    mData.name = "A New Timeline";
                    AssetDatabase.CreateAsset(mData, AssetPaths.TEMP_TL_ASSET_PATH);
                    mTLPlayer.m_TimelineData = mData;
                }
                if (!string.IsNullOrEmpty(mData.name))
                    mCurrentTLAssetName = mData.name;
            }
        }

        private void RenderHead()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Name");
            GUILayout.Label(mCurrentTLAssetName);
            GUILayout.Label("UpdateMode");
            GUILayout.Label("GameMode");
            GUILayout.EndHorizontal();
        }

        private void RenderOptions()
        {
            GUILayout.BeginHorizontal();
            Rect rectAreaPlay = new Rect(0, 20, 600, 400);
            GUI.BeginGroup(rectAreaPlay);
            #region new-track
            int heigth_foot = 40;
            Rect rectNewTrack = new Rect(5f, heigth_foot/2 - 15, 15, 15);
            Rect rectBtnNewTrack = new Rect(rectNewTrack.x, 0f, rectNewTrack.width, heigth_foot);
            if (GUI.Button(rectBtnNewTrack, new GUIContent("", "NewTrack"), "label"))
            {
                if (menu_track.GetItemCount() <= 0) buildAddTrackMenu();
                menu_track.ShowAsContext();
            }
            GUI.DrawTexture(rectNewTrack, tex_icon_track);
            #endregion
            GUI.EndGroup();
            GUILayout.EndHorizontal();
        }
        
        private void AddTrackFromMenu()
        {
            AddNewTrack();
        }

        private void AddNewTrack()
        {
            ATrack track = ATrack.CreateTrack();
            mData.AddTrackInEditor(track);
            LogHelper.Log("Add New Tack");
        }

        private void RenderTracks()
        {
            if (null == mData)
                return;
            GUILayout.BeginVertical();
            foreach(var track in mData.Tracks)
            {

            }
            GUILayout.EndVertical();
        }
    }

    public static class LogHelper
    {
        public static void Log(string log, params object[] args)
        {
            UnityEngine.Debug.LogFormat(log, args);
        }
        public static void Warning(string log, params object[] args)
        {
            UnityEngine.Debug.LogFormat(log, args);
        }
        public static void Error(string log, params object[] args)
        {
            UnityEngine.Debug.LogFormat(log, args);
        }
    }

    public static class AssetPaths
    {
        public const string TEMP_TL_ASSET_PATH = "Assets/TLAssets";
    }
}
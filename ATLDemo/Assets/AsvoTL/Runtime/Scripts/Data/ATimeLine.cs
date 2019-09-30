/* ***********************************************
 * ATimeLine
 * author :  created by asvo
 * function: 
 * history:  created at .
 * ***********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asvo
{
    [System.Serializable]
    public class ATimeLine : ScriptableObject {

        public string Name;
        public int Frames;
        public float Length;
        //public UpdateMode UpdateMode;
        public List<ATrack> Tracks = new List<ATrack>();

        #region runtime-data        
        //TODO, 先锁定30
        public const int FRAME_RATE = 30;
        #endregion runtime-data

        //暂时只考虑正序播放
        public void Process(int frame)
        {
            foreach (var track in Tracks)
            {
                track.ProcessTrack(frame);
            }            
        }

        public bool CheckIsAllTrackOver()
        {
            foreach (var track in Tracks)
            {
                if (!track.IsTrackOver)
                    return false;
            }
            return true;
        }

#if UNITY_EDITOR
        public void AddTrackInEditor(ATrack track)
        {
            Tracks.Add(track);
        }
#endif
    }
}
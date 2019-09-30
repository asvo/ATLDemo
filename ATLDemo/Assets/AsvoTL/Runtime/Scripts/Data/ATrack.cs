/* ***********************************************
 * ATrack
 * author :  created by asvo
 * function: 
 * history:  created at .
 * ***********************************************/
using System.Collections.Generic;

namespace Asvo
{
    [System.Serializable]
	public class ATrack {

        public int TrackId { get; private set; }
        public string TrackName;
        //取clips和events最早的frame
        //TODO, 这里先偷懒取第一帧
        public int StartFrame = 0;
        //取clips和events最晚的frame
        public int EndFrame;
        public List<AClip> Clips = new List<AClip>();
        public List<AEvent> Events = new List<AEvent>();

        #region runtime-data
        public bool IsTrackOver = false;
        #endregion runtime-data
        //add logic here for temp
        public void ProcessTrack(int frame)
        {
            if (IsTrackOver)
                return;
            foreach(var clip in Clips)
            {
                if (clip.IsInClip(frame))
                {
                    clip.Execute(frame);
                }
            }
            foreach(var eve in Events)
            {
                if (eve.CheckIfReachFrame(frame))
                {
                    eve.TriggerEvent();
                }
            }
            if (!IsTrackOver && frame >= EndFrame)
            {
                IsTrackOver = true;
                //track over, can dispatch event here
            }
        }

        protected static int S_TrackIDCnt = 0;
        public static ATrack CreateTrack()
        {
            ATrack track = new ATrack();
            track.TrackId = ++S_TrackIDCnt;
            track.TrackName = "New Track";



            return track;
        }
    }
}

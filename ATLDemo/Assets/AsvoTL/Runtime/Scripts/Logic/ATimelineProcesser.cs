/* ***********************************************
 * ATimelineProcesser
 * author :  created by asvo
 * function: 
 * history:  created at .
 * ***********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asvo
{
	public class ATimelineProcesser : MonoBehaviour {

        public ATimeLine m_TimelineData;        
        public bool IsPlaying { get; private set; }
        protected float m_time = 0;

        protected int m_lastFrame = 0;
        protected int m_curFrame = 0;

        public void Play()
        {
            IsPlaying = true;
        }

        public void Stop()
        {
            IsPlaying = false;
        }

        private void Update()
        {
            if (null != m_TimelineData)
                ProcessTimeLine(Time.deltaTime);
        }

        //暂时只考虑正序播放
        private void ProcessTimeLine(float deltaT)
        {
            m_time += deltaT;
            m_lastFrame = m_curFrame;
            m_curFrame = Mathf.CeilToInt(m_time * ATimeLine.FRAME_RATE);
            //处理跳帧情况（即，因卡顿等原因，一个deltaT时间过长，导致这里的m_curFrame对比上一次结果大于1.)
            for (int frame = m_lastFrame; frame <= m_curFrame; ++frame)
            {
                m_TimelineData.Process(frame);
                if (m_TimelineData.CheckIsAllTrackOver())
                {
                    //timeline over, can dispatch event here

                }
            }            
        }
    }
}

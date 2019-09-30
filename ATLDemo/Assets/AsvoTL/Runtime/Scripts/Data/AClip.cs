/* ***********************************************
 * AClip
 * author :  created by asvo
 * function: AClip, 定义为行为/动作/动画。持续一段时间的操作。
 * history:  created at .
 * ***********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asvo
{
    [System.Serializable]
    public class AClip {

        public string Name;
        public int StartFrame;
        public int EndFrame;

        public ABaseAction ClipAction;


        #region runtime-data
        protected int mCurrentFrame = 0;
        protected bool mHasEnterClip = false;
        #endregion runtime-data

        //logics
        public bool IsInClip(int frame)
        {
            return frame >= StartFrame && frame < EndFrame;
        }

        //TODO,先不考虑倒放
        public void Execute(int frame)
        {
            //过滤同一帧不执行，减少消耗
            if (mHasEnterClip && frame == mCurrentFrame)
                return;
            mCurrentFrame = frame;
            if (frame == StartFrame)
            {
                mHasEnterClip = true;
                OnEnterClip();
            }
            if (null != ClipAction)
            {
                ClipAction.OnProcessAction(frame);
            }
            if (frame == EndFrame)
            {
                mHasEnterClip = false;
                OnExitClip();
            }
        }

        protected virtual void OnEnterClip() {            
            if (null != ClipAction)
            {
                ClipAction.OnEnter();
            }
        }

        protected virtual void OnExitClip()
        {
            if (null != ClipAction)
            {
                ClipAction.OnExit();
            }
        }
    }

    /// <summary>
    /// 拿去扩展
    /// </summary>
    public abstract class ABaseAction {
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void OnProcessAction(int frame) { }
    }
}

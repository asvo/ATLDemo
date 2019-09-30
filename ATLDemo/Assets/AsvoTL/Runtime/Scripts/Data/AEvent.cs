/* ***********************************************
 * AEvent
 * author :  created by asvo
 * function: AEvent，定义为事件。瞬发，一帧内执行完的操作。
 * history:  created at .
 * ***********************************************/

namespace Asvo
{
    [System.Serializable]
    public class AEvent {

        public int Frame;
        public int ID;

        //自由发挥. 比如最土的办法，分类型和参数
        public AEventType EventType;
        public float FParam1;
        public string SParam1;

        
        //logics
        public bool CheckIfReachFrame(int frame)
        {
            return frame == Frame;
        }

        public void TriggerEvent()
        {

        }
	}

    public enum AEventType
    {
        None = 0,
        ColorChange
    }
}
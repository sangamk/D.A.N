using System;

namespace Assets.Engine.DataModels
{
    [Serializable]
    public class TimelineEvent
    {
        public long time;
        public string type;
        public Offset offset;
    }
}
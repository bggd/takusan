using System.Collections.Generic;

namespace Takusan.Scene.Game
{
    enum TriggerType
    {
        After,
        CoolDown,
        Every
    }

    public delegate void ActionFn();
    public delegate void ActionAfterFn();
    public delegate bool ConditionFn();

    class TriggerObject
    {
        public TriggerType Type;
        public double Timer;
        public double Delay;
        public uint Times;
        public uint MaxTimes;
        public ActionFn Action;
        public ActionAfterFn ActionAfter;
        public ConditionFn Condition;

        public TriggerObject(TriggerType type, double timer, double delay, uint times, ActionAfterFn actionAfter, ActionFn action, ConditionFn condition = null)
        {
            Type = type; ;
            Timer = timer;
            Delay = delay;
            Times = times;
            MaxTimes = times;
            ActionAfter = actionAfter;
            Action = action;
            Condition = condition;
        }
    }

    class Trigger
    {
        public Dictionary<string, TriggerObject> TriggerObjects = new Dictionary<string, TriggerObject>();

        public void After(double delay, ActionFn action, string tag = null)
        {
            if (tag == null)
            {
                tag = System.Guid.NewGuid().ToString();
            }
            var obj = new TriggerObject(TriggerType.After, 0.0, delay, 0, null, action);
            TriggerObjects[tag] = obj;
        }

        public void CoolDown(double delay, ConditionFn condition, ActionFn action)
        {
            CoolDown(delay, condition, action, 0, () => { }, System.Guid.NewGuid().ToString());
        }

        public void CoolDown(double delay, ConditionFn condition, ActionFn action, uint times, ActionAfterFn actionAfter, string tag)
        {
            var obj = new TriggerObject(TriggerType.CoolDown, 0.0, delay, times, actionAfter, action, condition);
            TriggerObjects[tag] = obj;
        }

        public void Every(double delay, ActionFn action)
        {
            Every(delay, action, 0, () => { }, System.Guid.NewGuid().ToString());
        }

        public void Every(double delay, ActionFn action, uint times, ActionAfterFn actionAfter, string tag)
        {
            var obj = new TriggerObject(TriggerType.Every, 0.0, delay, times, actionAfter, action);
            TriggerObjects[tag] = obj;
        }

        public void Cancel(string tag)
        {
            TriggerObjects.Remove(tag);
        }

        public void Update(double dt)
        {

            List<string> removeList = new List<string>();

            foreach (var kvp in TriggerObjects)
            {
                var v = kvp.Value;
                v.Timer += dt;
                if (v.Timer > v.Delay)
                {
                    if (v.Type == TriggerType.After)
                    {
                        v.Action();
                        removeList.Add(kvp.Key);
                    }
                    else if (v.Type == TriggerType.CoolDown && v.Condition())
                    {
                        v.Action();
                        v.Timer = 0.0;
                        if (v.MaxTimes > 0)
                        {
                            v.Times -= 1;
                            if (v.Times <= 0)
                            {
                                v.ActionAfter();
                                removeList.Add(kvp.Key);
                            }
                        }
                    }
                    else if (v.Type == TriggerType.Every)
                    {
                        v.Action();
                        v.Timer -= v.Delay;
                        if (v.MaxTimes > 0)
                        {
                            v.Times -= 1;
                            if (v.Times <= 0)
                            {
                                v.ActionAfter();
                                removeList.Add(kvp.Key);
                            }
                        }
                    }
                }
            }

            foreach (var key in removeList)
            {
                TriggerObjects.Remove(key);
            }
        }
    }
}

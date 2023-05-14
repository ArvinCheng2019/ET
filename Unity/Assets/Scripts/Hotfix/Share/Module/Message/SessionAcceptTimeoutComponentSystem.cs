﻿using System;

namespace ET
{
    [FriendOf(typeof(SessionAcceptTimeoutComponent))]
    public static class SessionAcceptTimeoutComponentHelper
    {
        [Invoke(TimerInvokeType.SessionAcceptTimeout)]
        public class SessionAcceptTimeout: ATimer<SessionAcceptTimeoutComponent>
        {
            protected override void Run(SessionAcceptTimeoutComponent self)
            {
                try
                {
                    self.Parent.Dispose();
                }
                catch (Exception e)
                {
                    Log.Error($"move timer error: {self.Id}\n{e}");
                }
            }
        }
    
        [EntitySystem]
        public class SessionAcceptTimeoutComponentAwakeSystem: AwakeSystem<SessionAcceptTimeoutComponent>
        {
            protected override void Awake(SessionAcceptTimeoutComponent self)
            {
                self.Awake();
            }
        }

        [EntitySystem]
        public class SessionAcceptTimeoutComponentDestroySystem: DestroySystem<SessionAcceptTimeoutComponent>
        {
            protected override void Destroy(SessionAcceptTimeoutComponent self)
            {
                self.Destroy();
            }
        }

        private static void Awake(this SessionAcceptTimeoutComponent self)
        {
            self.Timer = TimerComponent.Instance.NewOnceTimer(TimeHelper.ServerNow() + 5000, TimerInvokeType.SessionAcceptTimeout, self);
        }

        private static void Destroy(this SessionAcceptTimeoutComponent self)
        {
            TimerComponent.Instance.Remove(ref self.Timer);
        }
        
    }
    
    
}
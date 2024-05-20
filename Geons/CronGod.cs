namespace _UTIL_
{
    public class CronGod : GeonGod
    {
        float time;

        //----------------------------------------------------------------------------------------------------------

        public void AddFromLeft(in Cron cron, in float timer)
        {
            Pop(cron);
            cron.cronTime = timer + time;

            if (_geon == null)
                _geon = cron;
            else
                lock (_geon)
                {
                    Cron p = (Cron)_geon;
                    if (cron.cronTime < p.cronTime)
                    {
                        _geon = cron;
                        cron.next = p;
                        p.prev = cron;
                    }
                    else
                        while (cron.cronTime >= p.cronTime)
                            if (p.next == null)
                            {
                                p.next = cron;
                                cron.prev = p;
                                break;
                            }
                            else
                                p = (Cron)p.next;
                }
        }

        public void OnUpdate(in float deltaTime)
        {
            time += deltaTime;
            while (_geon != null)
                lock (_geon)
                {
                    Cron cron = (Cron)_geon;
                    if (time < cron.cronTime)
                        break;
                    else
                    {
                        Pop(cron);
                        if (!cron.cronZombie)
                            cron.OnGeon();
                    }
                }
        }
    }
}
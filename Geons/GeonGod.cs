namespace _UTIL_
{
    public class GeonGod
    {
        public Geon _geon;

        //----------------------------------------------------------------------------------------------------------

        public void Pop(in Geon self)
        {
            if (_geon == self)
            {
                _geon = _geon.next;
                if (_geon != null)
                    _geon.prev = null;
            }
            else
            {
                if (self.prev != null)
                    self.prev.next = self.next;
                if (self.next != null)
                    self.next.prev = self.prev;
            }
            self.prev = null;
            self.next = null;
        }

        public void AddRight(in Geon geon)
        {
            Pop(geon);
            if (_geon == null)
                _geon = geon;
            else
            {
                geon.prev = _geon.Rightest;
                geon.prev.next = geon;
            }
        }
    }
}
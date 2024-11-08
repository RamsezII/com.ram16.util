using System.Collections.Generic;

namespace _UTIL_
{
    public interface IUserGroup
    {
    }

    public class UserGroup<T> where T : IUserGroup
    {
        public readonly OnValue<bool> isUsed = new();
        readonly HashSet<IUserGroup> group = new();

        //--------------------------------------------------------------------------------------------------------------

        void AutoUse() => isUsed.Update(group.Count > 0);

        public void Clear()
        {
            group.Clear();
            AutoUse();
        }

        public void ToggleUser(in IUserGroup user, in bool toggle)
        {
            if (group.Contains(user))
            {
                if (!toggle)
                    Remove(user);
            }
            else if (toggle)
                Add(user);
        }

        public void ToggleUser(in IUserGroup user)
        {
            if (group.Contains(user))
                Remove(user);
            else
                Add(user);
        }

        public void Add(IUserGroup user)
        {
            group.Add(user);
            AutoUse();
        }

        public void Remove(IUserGroup user)
        {
            group.Remove(user);
            AutoUse();
        }
    }
}
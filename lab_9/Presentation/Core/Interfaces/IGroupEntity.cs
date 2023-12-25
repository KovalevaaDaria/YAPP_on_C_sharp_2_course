using System.ComponentModel;
using DatabaseModels;

namespace Core.Interfaces
{
    public interface IGroupEntity
    {
        List<string> GroupNamesGet();
        void AddGroup(string name, DateTime time);
        BindingList<Group> TakeAllGroups();
        void UpdateGroupName(string newName, string tmpId);
        void UpdateGroupTime(DateTime time, string tmpId);
        void DeleteGroup(string tmpId);
        List<string> SearchGroupsCurator(string curatorName);
    }
}

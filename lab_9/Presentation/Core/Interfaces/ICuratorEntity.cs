using System.ComponentModel;
using DatabaseModels;

namespace Core.Interfaces
{
    public interface ICuratorEntity
    {
        void AddCurator(string name, int groupId, string email);
        void UpdateCuratorName(string newName, string tmpId);
        void UpdateCuratorEmail(string email, string tmpId);
        void UpdateCuratorGroup(int newGroupId, string tmpId);
        void DeleteCurator(string tmpId);
        string FindCuratorsName(int groupId);
        BindingList<Curator> TakeAllCurators();
    }
}

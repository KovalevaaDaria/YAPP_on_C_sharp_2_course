using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using Core.Interfaces;
using DatabaseContext;
using DatabaseModels;

namespace Core
{
    class CuratorEntity : ICuratorEntity
    {
        readonly DataBaseContext _dataBase;

        public CuratorEntity(DataBaseContext dB)
        {
            _dataBase = dB;
        }

        public void AddCurator(string name, int groupId, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Curator name cannot be empty or null!");
                return;
            }

            try
            {
                _dataBase.AddCurator(name, groupId, email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding curator: {ex.Message}");
            }
        }

        public BindingList<Curator> TakeAllCurators()
        {
            _dataBase.curators.Load();
            return _dataBase.curators.Local.ToBindingList();
        }

        public void UpdateCuratorName(string newName, string tmpId)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("New curator name cannot be empty or null!");
                return;
            }

            try
            {
                var changeId = Convert.ToInt32(tmpId);
                _dataBase.UpdateCuratorName(newName, changeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating curator name: {ex.Message}");
            }
        }

        public void UpdateCuratorEmail(string email, string tmpId)
        {
            try
            {
                var changeId = Convert.ToInt32(tmpId);
                _dataBase.UpdateCuratorEmail(email, changeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating curator email: {ex.Message}");
            }
        }
        
        public void UpdateCuratorGroup(int newGroupId, string tmpId)
        {
            try
            {
                if (newGroupId <= 0)
                {
                    Console.WriteLine("Invalid group ID!");
                    return;
                }

                var changeCuratorId = Convert.ToInt32(tmpId);
                _dataBase.UpdateCuratorGroup(newGroupId, changeCuratorId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating curator group: {ex.Message}");
            }
        }


        public void DeleteCurator(string tmpId)
        {
            try
            {
                var changeId = Convert.ToInt32(tmpId);
                _dataBase.DeleteCurator(changeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting curator: {ex.Message}");
            }
        }

        public string FindCuratorsName(int groupId)
        {
            var name = "No curator!";
            var curator = _dataBase.curators.Where(u => u.GroupId == groupId).ToList();
            if (curator.Count > 0)
                name = curator[0].Name;
            return name;
        }
    }
}
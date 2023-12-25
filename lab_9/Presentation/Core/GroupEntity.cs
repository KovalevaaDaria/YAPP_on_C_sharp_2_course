using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using Core.Interfaces;
using DatabaseContext;

namespace Core
{
    class GroupEntity : IGroupEntity
    {
        private readonly DataBaseContext _dataBase;

        public GroupEntity(DataBaseContext dB)
        {
            _dataBase = dB;
        }

        public List<string> GroupNamesGet()
        {
            return _dataBase.GroupNamesGet();
        }

        public void AddGroup(string name, DateTime time)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Group name cannot be empty or null!");
                return;
            }

            try
            {
                time = DateTime.SpecifyKind(time, DateTimeKind.Utc);
                _dataBase.AddGroup(name, time);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding group: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
        }
        
        public BindingList<DatabaseModels.Group> TakeAllGroups()
        {
            _dataBase.groups.Load();
            return _dataBase.groups.Local.ToBindingList();
        }

        public void UpdateGroupName(string newName, string tmpId)
        {
            var group = _dataBase.groups.FirstOrDefault(g => g.Id == Convert.ToInt32(tmpId));

            if (group != null)
            {
                group.Name = newName;
                _dataBase.SaveChanges();
            }
            else
            {
                Console.WriteLine($"Error updating group name: Group not found for ID {tmpId}");
            }
        }

        public void UpdateGroupTime(DateTime time, string tmpId)
        {
            try
            {
                if (int.TryParse(tmpId, out var changeId))
                {
                    _dataBase.UpdateGroupTime(time, changeId);
                }
                else
                {
                    Console.WriteLine("Invalid group ID. Please enter a valid integer.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating group time: {ex.Message}");
            }
        }

        public void DeleteGroup(string tmpId)
        {
            try
            {
                if (int.TryParse(tmpId, out var changeId))
                {
                    _dataBase.DeleteGroup(changeId);
                }
                else
                {
                    Console.WriteLine("Invalid group ID. Please enter a valid integer.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting group: {ex.Message}");
            }
        }

        public List<string> SearchGroupsCurator(string curatorName)
        {
            return _dataBase.SearchGroupsCurator(curatorName);
        }
    }
}
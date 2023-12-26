using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using Core.Interfaces;
using DatabaseContext;

namespace Core
{
    public class GroupEntity : IGroupEntity
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
            try
            {
                if (int.TryParse(tmpId, out var groupId))
                {
                    var existingGroup = _dataBase.groups.Any(g => g.Name == newName && g.Id != groupId);

                    if (existingGroup)
                    {
                        Console.WriteLine($"\nGroup with name '{newName}' already exists! Please choose a different name!");
                    }
                    else
                    {
                        _dataBase.UpdateGroupName(newName, groupId);
                        Console.WriteLine("\nGroup name updated successfully!");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid group ID! Please enter a valid integer!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating group name: {ex.Message}");
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
                    Console.WriteLine("Invalid group ID! Please enter a valid integer!");
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
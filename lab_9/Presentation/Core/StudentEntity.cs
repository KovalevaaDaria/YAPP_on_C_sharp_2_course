using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using Core.Interfaces;
using DatabaseContext;
using DatabaseModels;

namespace Core
{
    public class StudentEntity : IStudentEntity
    {
        private DataBaseContext _dataBase;

        public StudentEntity(DataBaseContext dB)
        {
            _dataBase = dB;
        }

        public void AddStudent(string name, int groupId, int age)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("\nStudent name cannot be empty or null!");
                    return;
                }

                var groupExists = _dataBase.FindGroupWithId(groupId);

                if (groupExists)
                {
                    try
                    {
                        _dataBase.AddStudent(name, groupId, age);
                        Console.WriteLine("\nStudent added successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nError adding student: {ex.Message}!");
                    }
                }
                else
                {
                    Console.WriteLine($"\nGroup not found for ID: {groupId}! Please add new group for this student!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding student: {ex.Message}");
            }
        }


        public BindingList<Student> TakeAllStudents()
        {
            _dataBase.students.Load();
            return _dataBase.students.Local.ToBindingList();
        }

        public void UpdateStudentName(string newName, int tmpId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(newName))
                {
                    Console.WriteLine("New student name cannot be empty or null!");
                    return;
                }

                var changeId = tmpId;
                _dataBase.UpdateStudentName(newName, changeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating student name: {ex.Message}");
            }
        }

        public void UpdateStudentAge(string newAge, string tmpId)
        {
            try
            {
                if (int.TryParse(newAge, out var parsedAge))
                {
                    int changeId = Convert.ToInt32(tmpId);
                    _dataBase.UpdateStudentAge(parsedAge, changeId);
                }
                else
                {
                    Console.WriteLine("Invalid age. Please enter a valid integer for age.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating student age: {ex.Message}");
            }
        }

        public void UpdateStudentGroup(int newGroupId, string tmpId)
        {
            try
            {
                if (newGroupId <= 0)
                {
                    Console.WriteLine("Invalid group ID!");
                    return;
                }

                int changeStudentId = Convert.ToInt32(tmpId);
                _dataBase.UpdateStudentGroup(newGroupId, changeStudentId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating student group: {ex.Message}");
            }
        }

        public void DeleteStudent(string tmpId)
        {
            try
            {
                var changeId = Convert.ToInt32(tmpId);
                _dataBase.DeleteStudent(changeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting student: {ex.Message}");
            }
        }

        public int CountAll(int groupId)
        {
            var groupExists = _dataBase.FindGroupWithId(groupId);

            if (!groupExists)
            {
                Console.WriteLine($"\nGroup not found for ID: {groupId}!");
                return 0; 
            }

            var studentsInGroup = _dataBase.students.Where(u => u.GroupId == groupId).ToList();
            return studentsInGroup.Count;
        }
        
        public double? CountAges(string groupId)
        {
            var result = _dataBase.CountAgesByGroupName(groupId);
            return result.HasValue ? result.Value : null;
        }
    }
}

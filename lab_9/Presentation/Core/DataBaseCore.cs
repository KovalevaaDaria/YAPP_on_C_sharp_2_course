using DatabaseContext;
using DatabaseModels;
using System.ComponentModel;
using Core.Interfaces;

namespace Core
{
    public class DataBaseCore : IDataBaseCore
    {
        private readonly DataBaseContext _dataBase;
        private readonly StudentEntity _student;
        private readonly CuratorEntity _curator;
        private readonly GroupEntity _group;

        public DataBaseCore()
        {
            _dataBase = new DataBaseContext();
            _student = new StudentEntity(_dataBase);
            _curator = new CuratorEntity(_dataBase);
            _group = new GroupEntity(_dataBase);
        }

        public void AddStudent(string name, int groupId, int age)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Student name cannot be empty or null!");
                return;
            }

            if (groupId <= 0)
            {
                Console.WriteLine("\nInvalid group ID! Please add new group for student!");
                return;
            }

            _student.AddStudent(name, groupId, age);
        }

        public void AddCurator(string name, int groupId, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Curator name cannot be empty or null!");
                return;
            }

            if (groupId <= 0)
            {
                Console.WriteLine("Invalid group ID!");
                return;
            }

            _curator.AddCurator(name, groupId, email);
        }

        public void AddGroup(string name, DateTime time)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Group name cannot be empty or null!");
                return;
            }

            _group.AddGroup(name, time);
        }

        public BindingList<Group> TakeAllGroups()
        {
            return _group.TakeAllGroups();
        }

        public BindingList<Student> TakeAllStudents()
        {
            return _student.TakeAllStudents();
        }

        public BindingList<Curator> TakeAllCurators()
        {
            return _curator.TakeAllCurators();
        }

        public void UpdateGroupName(string newName, string tmpId)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("New group name cannot be empty or null!");
                return;
            }

            _group.UpdateGroupName(newName, tmpId);
        }

        public void UpdateGroupTime(DateTime time, string tmpId)
        {
            _group.UpdateGroupTime(time, tmpId);
        }

        public void UpdateStudentName(string newName, string tmpId)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("New student name cannot be empty or null!");
                return;
            }

            _student.UpdateStudentName(newName, Convert.ToInt32(tmpId));
        }

        public void UpdateStudentAge(string newAge, string tmpId)
        {
            if (int.TryParse(newAge, out int parsedAge))
            {
                _student.UpdateStudentAge(parsedAge.ToString(), tmpId);
            }
            else
            {
                Console.WriteLine("Invalid age! Please enter a valid integer for age!");
            }
        }

        public void UpdateStudentGroupWithId(int newGroupId, string tmpId)
        {
            if (newGroupId <= 0)
            {
                Console.WriteLine("Invalid group ID!");
                return;
            }

            _student.UpdateStudentGroup(newGroupId, tmpId);
        }

        public void UpdateCuratorName(string newName, string tmpId)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("New curator name cannot be empty or null!");
                return;
            }

            _curator.UpdateCuratorName(newName, tmpId);
        }

        public void UpdateCuratorEmail(string email, string tmpId)
        {
            _curator.UpdateCuratorEmail(email, tmpId);
        }

        public void UpdateCuratorGroupWithId(int newGroupId, string tmpId)
        {
            if (newGroupId <= 0)
            {
                Console.WriteLine("Invalid group ID!");
                return;
            }

            _curator.UpdateCuratorGroup(newGroupId, tmpId);
        }

        public void DeleteGroup(string tmpId)
        {
            _group.DeleteGroup(tmpId);
        }

        public void DeleteStudent(string tmpId)
        {
            _student.DeleteStudent(tmpId);
        }

        public void DeleteCurator(string tmpId)
        {
            _curator.DeleteCurator(tmpId);
        }

        public int CountAll(int groupId)
        {
            return _student.CountAll(groupId);
        }

        public string FindCuratorsName(int groupId)
        {
            return _curator.FindCuratorsName(groupId);
        }

        public double CountAverageAge(string curatorName)
        {
            if (int.TryParse(curatorName, out var curatorId))
            {
                curatorName = _curator.FindCuratorsName(curatorId);

                if (!curatorName.Equals("No curator!"))
                {
                    var groupNames = _group.SearchGroupsCurator(curatorName);
                    return _student.CountAges(groupNames[0]);
                }
                Console.WriteLine($"\nCurator not found with ID: {curatorId}");
                return 0;
            }
            Console.WriteLine($"\nInvalid curator ID! Please enter a valid integer!");
            return 0;
        }
    }
}
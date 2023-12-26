using System.ComponentModel;
using DatabaseModels;

namespace Core.Interfaces
{
    public interface IStudentEntity
    {
        void AddStudent(string name, int groupId, int age);
        BindingList<Student> TakeAllStudents();
        void UpdateStudentName(string newName, int tmpId);
        void UpdateStudentAge(string newAge, string tmpId);
        void UpdateStudentGroup(int newGroupId, string tmpId);
        void DeleteStudent(string tmpId);
        int CountAll(int groupId);
        double? CountAges(string groupName);
    }
}

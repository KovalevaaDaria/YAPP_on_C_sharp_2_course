using System;
using System.ComponentModel;
using DatabaseModels;

namespace Core.Interfaces
{
    public interface IDataBaseCore
    {
        void AddStudent(string name, int groupId, int age);
        void AddCurator(string name, int groupId, string email);
        void AddGroup(string name, DateTime time);
        BindingList<Group> TakeAllGroups();
        BindingList<Student> TakeAllStudents();
        BindingList<Curator> TakeAllCurators();
        void UpdateGroupName(string newName, string tmpId);
        void UpdateGroupTime(DateTime time, string tmpId);
        void UpdateStudentName(string newName, string tmpId);
        void UpdateStudentAge(string newAge, string tmpId);
        void UpdateStudentGroupWithId(int newGroupId, string tmpId);
        void UpdateCuratorName(string newName, string tmpId);
        void UpdateCuratorEmail(string email, string tmpId);
        void UpdateCuratorGroupWithId(int newGroupId, string tmpId);
        void DeleteGroup(string tmpId);
        void DeleteStudent(string tmpId);
        void DeleteCurator(string tmpId);
        int CountAll(int groupId);
        double? CountAverageAge(string curatorId);
    }
}

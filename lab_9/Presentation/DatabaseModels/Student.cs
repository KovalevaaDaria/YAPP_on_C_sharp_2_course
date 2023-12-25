namespace DatabaseModels
{
    public class Student
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public int GroupId { get; set; }
        
        public int Age { get; set; }
        
        
        public string GroupName { get; set; }
        public Group Group { get; set; }
        

        public Student(int age, int groupId, string name, Group group, string groupName)
        { 
            Age = age;
            GroupId = groupId;
            Name = name;
            Group = group;
            GroupName = groupName;
        }

        public Student()
        {
        }
    }
}
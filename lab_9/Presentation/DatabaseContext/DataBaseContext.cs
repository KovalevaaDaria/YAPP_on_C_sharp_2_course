using Microsoft.EntityFrameworkCore;
using DatabaseModels;

namespace DatabaseContext
{
    public class DataBaseContext : DbContext
    {
        private const string SqlServer = "Host=localhost;Port=5432;Database=DataBaseContext;Username=daria;Password=";

        public DbSet<Student> students { get; set; }
        public DbSet<Curator> curators { get; set; }
        public DbSet<Group> groups { get; set; }

        public DataBaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(SqlServer);
        }

        public void AddStudent(string name, int groupId, int age)
        {
            if (FindGroupWithId(groupId))
            {
                students.Add(new Student(age, groupId, name, groups.Find(groupId)!, groups.Find(groupId)!.Name));
            }
            else
            {
                throw new Exception();
            }
            SaveChanges();
        }

        public void AddCurator(string name, int groupId, string email)
        {
            if (FindGroupWithId(groupId))
            {
                curators.Add(new Curator(groupId, name, email, groups.Find(groupId)!.Name, groups.Find(groupId)!));
            }
            else
            {
                throw new Exception();
            }
            SaveChanges();
        }

        public bool FindGroupWithId(int id)
        {
            var res = false;
            var group = groups.Where(u => u.Id == id).ToList();
            if (group.Count != 0)
                res = true;
            return res;
        }

        public void UpdateGroupName(string name, int id)
        {
            groups.Find(id)!.Name = name;
            var student = students.Where(c => c.GroupId == id).ToList<Student>();
            for (var i = 0; i < student.Count; i++)
            {
                students.Find(student[i].Id)!.GroupName = name;
            }
            var curator = curators.Where(c => c.GroupId == id).ToList<Curator>();
            curators.Find(curator[0].Id)!.GroupName = name;
            SaveChanges();
        }

        public void UpdateStudentGroup(int groupId, int studentId)
        {
            students.Find(studentId)!.Group = groups.Find(groupId)!;
            students.Find(studentId)!.GroupName = groups.Find(groupId)!.Name;
            students.Find(studentId)!.GroupId = groupId;
            SaveChanges();
        }

        public void UpdateCuratorGroup(int groupId, int curatorId)
        {
            var tmp = curators.Find(curatorId);
            tmp!.Group = groups.Find(groupId)!;
            tmp.GroupName = groups.Find(groupId)!.Name;
            tmp.GroupId = groupId;
            SaveChanges();
        }

        public void DeleteGroup(int id)
        {
            var student = students.Where(c => c.GroupId == id).ToList<Student>();
            for (var i = 0; i < student.Count; i++)
                students.Remove(student[i]);
            var curator = curators.Where(c => c.GroupId == id).ToList<Curator>();
            if (curator.Count != 0)
                curators.Remove(curator[0]);
            groups.Remove(groups.Find(id)!);
            SaveChanges();
        }

        public void AddGroup(string name, DateTime time)
        {
            groups.Add(new Group(name, time));
            SaveChanges();
        }

        public void UpdateGroupTime(DateTime time, int id)
        {
            groups.Find(id)!.CreationDate = DateTime.SpecifyKind(time, DateTimeKind.Utc);
            SaveChanges();
        }

        public void UpdateStudentAge(int age, int id)
        {
            students.Find(id)!.Age = age;
            SaveChanges();
        }

        public void UpdateStudentName(string name, int id)
        {
            students.Find(id)!.Name = name;
            SaveChanges();
        }

        public void DeleteStudent(int id)
        {
            students.Remove(students.Find(id)!);
            SaveChanges();
        }

        public void DeleteCurator(int id)
        {
            curators.Remove(curators.Find(id)!);
            SaveChanges();
        }

        public void UpdateCuratorEmail(string email, int id)
        {
            curators.Find(id)!.Email = email;
            SaveChanges();
        }

        public void UpdateCuratorName(string name, int id)
        {
            curators.Find(id)!.Name = name;
            SaveChanges();
        }

        public List<string> GroupNamesGet()
        {
            groups.Load();
            var result = new List<string>(groups.Local.Count);
            foreach (var group in groups)
                result.Add(group.Name);
            return result;
        }

        public List<string> SearchGroupsCurator(string curatorName)
        {
            var curator = curators.Where(u => u.Name == curatorName).ToList();
            var group = new List<string>();
            for (var i = 0; i < curator.Count; i++)
                group.Add(curator[i].GroupName);
            return group;
        }

        public double CountAges(string groupName)
        {
            var group = groups.FirstOrDefault(group => group.Name == groupName);
        
            if (group == null)
            {
                Console.WriteLine($"\nGroup not found with name: {groupName}");
                return 0;
            }
        
            var studentsInGroup = students.Where(student => student.GroupId == group.Id).ToList();
        
            if (!studentsInGroup.Any())
            {
                Console.WriteLine($"\nNo students found in group '{groupName}'");
                return 0;
            }
        
            return studentsInGroup.Select(student => student.Age).Average();
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasKey(s => s.Id);
            modelBuilder.Entity<Student>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<Student>().Property(s => s.Age).IsRequired();
            modelBuilder.Entity<Student>().Property(s => s.GroupName).IsRequired();
            modelBuilder.Entity<Group>().HasKey(g => g.Id);
            modelBuilder.Entity<Group>().Property(g => g.Name).IsRequired();
            modelBuilder.Entity<Group>().Property(g => g.CreationDate).IsRequired();
            modelBuilder.Entity<Curator>().HasKey(c => c.Id);
            modelBuilder.Entity<Curator>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<Curator>().Property(s => s.Email).IsRequired();
            modelBuilder.Entity<Curator>().Property(s => s.GroupName).IsRequired();
            modelBuilder.Entity<Group>().HasMany<Student>().WithOne(u => u.Group).IsRequired(true).HasForeignKey(t => t.GroupId);
            modelBuilder.Entity<Group>().HasOne<Curator>().WithOne(u => u.Group).IsRequired(true);
            base.OnModelCreating(modelBuilder);
        }
    }
}
using Core;

namespace Presentation
{
    public class StudentFacade
    {
        private readonly DataBaseCore _dataBaseCore;

        public StudentFacade(DataBaseCore dataBaseCore)
        {
            _dataBaseCore = dataBaseCore;
        }

        public void Run()
        {
            Console.WriteLine("\nStudent class:");
            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Update Student");
                Console.WriteLine("3. Delete Student");
                Console.WriteLine("4. Show All Students");
                Console.WriteLine("5. Show All Students by group ID");
                Console.WriteLine("6. Count average age of students");
                Console.WriteLine("0. Back to main menu");
                
                Console.WriteLine("\nSelected action:");

                if (int.TryParse(Console.ReadLine(), out var choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddStudent();
                            break;
                        case 2:
                            UpdateStudent();
                            break;
                        case 3:
                            DeleteStudent();
                            break;
                        case 4:
                            ShowAllStudents();
                            break;
                        case 5:
                            CountAll();
                            break;
                        case 6:
                            CountAverageAge();
                            break;
                        case 0:
                            return;
                        default:
                            Console.WriteLine("\nInvalid choice! Please try again!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid input! Please enter a number!");
                }
            }
        }

        private void AddStudent()
        {
            Console.WriteLine("\nEnter student name:");
            var name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("\nInvalid input! Student name cannot be empty or null. Please enter a valid name:");
                name = Console.ReadLine();
            }

            Console.WriteLine("\nEnter group ID:");
            int groupId;
            while (!int.TryParse(Console.ReadLine(), out groupId))
            {
                Console.WriteLine("\nInvalid input! Please enter a valid integer for group ID!");
            }

            Console.WriteLine("\nEnter student age:");
            int age;
            while (!int.TryParse(Console.ReadLine(), out age) || age <= 0 || age > 100)
            {
                Console.WriteLine("\nInvalid input! Please enter a valid positive integer for student age!");
            }

            _dataBaseCore.AddStudent(name!, groupId, age);
        }


        private void UpdateStudent()
        {
            Console.WriteLine("\nEnter student ID to update:");
            var studentId = Console.ReadLine();

            Console.WriteLine("\nChoose what to update:");
            Console.WriteLine("1. Update name");
            Console.WriteLine("2. Update age");
            Console.WriteLine("2. Update group");
            Console.WriteLine("\nSelected action:");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("\nInvalid choice! Please enter 1 or 3!");
            }

            if (choice == 1)
            {
                Console.WriteLine("\nEnter new name:");
                var newName = Console.ReadLine();
                _dataBaseCore.UpdateStudentName(newName!, studentId!);
                Console.WriteLine("\nStudent name updated successfully!");
            }
            if (choice == 2)
            {
                Console.WriteLine("\nEnter new age:");
                var newAge = Console.ReadLine();
                _dataBaseCore.UpdateStudentAge(newAge!, studentId!);
                Console.WriteLine("\nStudent age updated successfully!");
            }
            if (choice == 3)
            {
                Console.WriteLine("\nEnter new group:");
                var newGroup = Console.ReadLine();
                _dataBaseCore.UpdateStudentGroupWithId(Convert.ToInt32(newGroup), studentId!);
                Console.WriteLine("\nStudent age updated successfully!");
            }
        }

        private void DeleteStudent()
        {
            Console.WriteLine("\nEnter student ID to delete:");
            var studentId = Console.ReadLine();

            _dataBaseCore.DeleteStudent(studentId!);
            Console.WriteLine("\nStudent deleted successfully!");
        }
    
        private void ShowAllStudents()
        {
            var students = _dataBaseCore.TakeAllStudents();
            Console.WriteLine("\nAll Students:");
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Group ID: {student.GroupId}, Age: {student.Age}");
            }
        }

        private void CountAll()
        {
            Console.WriteLine("\nEnter group ID to count students:");
            var groupId = Console.ReadLine();

            if (int.TryParse(groupId, out int groupIntId))
            {
                var count = _dataBaseCore.CountAll(groupIntId);
                Console.WriteLine($"\nTotal number of students in group {groupId}: {count}");
            }
            else
            {
                Console.WriteLine("\nInvalid group ID! Please enter a valid integer!");
            }
        }

        private void CountAverageAge()
        {
            Console.WriteLine("\nEnter group ID to count average age:");
            var groupId = Console.ReadLine();

            if (int.TryParse(groupId, out var groupIntId))
            {
                var averageAge = _dataBaseCore.CountAverageAge(groupIntId.ToString());
                if (averageAge >= 0)
                {
                    Console.WriteLine($"\nAverage age of students in group {groupId}: {averageAge}");
                }
                else
                {
                    Console.WriteLine($"\nNo students found for group {groupId}!");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid group ID! Please enter a valid integer!");
            }
        }
    }
}

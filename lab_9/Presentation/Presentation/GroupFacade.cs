using Core;

namespace Presentation
{
    public class GroupFacade
    {
        private readonly DataBaseCore _dataBaseCore;

        public GroupFacade(DataBaseCore dataBaseCore)
        {
            _dataBaseCore = dataBaseCore;
        }

        public void Run()
        {
            Console.WriteLine("\nGroup class:");
            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Add Group");
                Console.WriteLine("2. Update Group");
                Console.WriteLine("3. Delete Group");
                Console.WriteLine("4. Show All Groups");
                Console.WriteLine("0. Back to main menu");
                
                Console.WriteLine("\nSelected action:");

                if (int.TryParse(Console.ReadLine(), out var choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddGroup();
                            break;
                        case 2:
                            UpdateGroup();
                            break;
                        case 3:
                            DeleteGroup();
                            break;
                        case 4:
                            ShowAllGroups();
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
        
        
        private void AddGroup()
        {
            Console.WriteLine("\nEnter group name:");
            var name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("\nInvalid input! Group name cannot be empty or null. Please enter a valid name:");
                name = Console.ReadLine();
            }

            Console.WriteLine("\nEnter group creation date (YYYY-MM-DD):");
            DateTime creationDate;
            while (!DateTime.TryParse(Console.ReadLine(), out creationDate))
            {
                Console.WriteLine("\nInvalid input! Please enter a valid date (YYYY-MM-DD)!");
            }

            _dataBaseCore.AddGroup(name!, creationDate);
            Console.WriteLine("\nGroup added successfully!");
        }
        

        private void UpdateGroup()
        {
            Console.WriteLine("\nEnter group ID to update:");
            var groupId = Console.ReadLine();

            Console.WriteLine("\nChoose what to update:");
            Console.WriteLine("1. Update name");
            Console.WriteLine("2. Update creation date");
            Console.WriteLine("\nSelected action:");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 2)
            {
                Console.WriteLine("\nInvalid choice! Please enter 1 or 2!");
            }

            if (choice == 1)
            {
                Console.WriteLine("\nEnter new name:");
                var newName = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(newName))
                {
                    Console.WriteLine("\nGroup name cannot be empty or null!");
                }

                _dataBaseCore.UpdateGroupName(newName!, groupId!);
                Console.WriteLine("\nGroup name updated successfully!");
            }
            else
            {
                Console.WriteLine("\nEnter new creation date (YYYY-MM-DD):");
                DateTime newCreationDate;
                while (!DateTime.TryParse(Console.ReadLine(), out newCreationDate))
                {
                    Console.WriteLine("\nInvalid input! Please enter a valid date (YYYY-MM-DD)!");
                }
                _dataBaseCore.UpdateGroupTime(newCreationDate, groupId!);
                Console.WriteLine("\nGroup creation date updated successfully!");
            }
        }


        private void DeleteGroup()
        {
            Console.WriteLine("\nEnter group ID to delete:");
            var groupId = Console.ReadLine();

            _dataBaseCore.DeleteGroup(groupId!);
            Console.WriteLine("\nGroup deleted successfully!");
        }
        

        private void ShowAllGroups()
        {
            var groups = _dataBaseCore.TakeAllGroups();
            Console.WriteLine("\nAll Groups:");
            foreach (var group in groups)
            {
                Console.WriteLine($"ID: {group.Id}, Name: {group.Name}, Creation Date: {group.CreationDate}");
            }
        }
    }
}

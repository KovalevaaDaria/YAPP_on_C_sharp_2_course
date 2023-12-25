using Core;

namespace Presentation
{
    public class CuratorFacade
    {
        private readonly DataBaseCore _dataBaseCore;

        public CuratorFacade(DataBaseCore dataBaseCore)
        {
            _dataBaseCore = dataBaseCore;
        }

        public void Run()
        {
            Console.WriteLine("\nCurator class:");
            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Add Curator");
                Console.WriteLine("2. Update Curator");
                Console.WriteLine("3. Delete Curator");
                Console.WriteLine("4. Show All Curators");
                Console.WriteLine("5. Find curator by group ID");
                Console.WriteLine("0. Back to main menu");
                
                Console.WriteLine("\nSelected action:");

                if (int.TryParse(Console.ReadLine(), out var choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddCurator();
                            break;
                        case 2:
                            UpdateCurator();
                            break;
                        case 3:
                            DeleteCurator();
                            break;
                        case 4:
                            ShowAllCurators();
                            break;
                        case 5:
                            FindCuratorsName();
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

        private void AddCurator()
        {
            Console.WriteLine("\nEnter curator name:");
            var name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("\nInvalid input! Curator name cannot be empty or null. Please enter a valid name:");
                name = Console.ReadLine();
            }

            Console.WriteLine("\nEnter group ID:");
            int groupId;
            while (!int.TryParse(Console.ReadLine(), out groupId))
            {
                Console.WriteLine("\nInvalid input! Please enter a valid integer for group ID!");
            }
            
            var existingCurator = _dataBaseCore.FindCuratorsName(groupId);
            if (!string.IsNullOrEmpty(existingCurator))
            {
                Console.WriteLine($"\nCurator '{existingCurator}' already exists for group {groupId}.");
                Console.WriteLine("To add a new curator for this group, first remove the existing one.");
                return;
            }

            Console.WriteLine("\nEnter curator email:");
            var email = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                Console.WriteLine("\nInvalid input! Please enter a valid email address:");
                email = Console.ReadLine();
            }

            _dataBaseCore.AddCurator(name, groupId, email);
            Console.WriteLine("\nCurator added successfully!");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void UpdateCurator()
        {
            Console.WriteLine("\nEnter curator ID to update:");
            var curatorId = Console.ReadLine();

            Console.WriteLine("\nChoose what to update:");
            Console.WriteLine("1. Update name");
            Console.WriteLine("2. Update email");
            Console.WriteLine("3. Update group");
            Console.WriteLine("\nSelected action:");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("\nInvalid choice! Please enter a number between 1 and 3!");
            }

            switch (choice)
            {
                case 1:
                    Console.WriteLine("\nEnter new name:");
                    var newName = Console.ReadLine();
                    _dataBaseCore.UpdateCuratorName(newName!, curatorId!);
                    Console.WriteLine("\nCurator name updated successfully!");
                    break;
                
                case 2:
                    Console.WriteLine("\nEnter new email:");
                    var newEmail = Console.ReadLine();
                    _dataBaseCore.UpdateCuratorEmail(newEmail!, curatorId!);
                    Console.WriteLine("\nCurator email updated successfully!");
                    break;
                
                case 3:
                    Console.WriteLine("\nEnter new group ID:");
                    int newGroupId;
                    while (!int.TryParse(Console.ReadLine(), out newGroupId))
                    {
                        Console.WriteLine("\nInvalid input! Please enter a valid integer for group ID!");
                    }
                    _dataBaseCore.UpdateCuratorGroupWithId(newGroupId, curatorId!);
                    break;
            }
        }

        private void DeleteCurator()
        {
            Console.WriteLine("\nEnter curator ID to delete:");
            var curatorId = Console.ReadLine();

            _dataBaseCore.DeleteCurator(curatorId!);
            Console.WriteLine("\nCurator deleted successfully!");
        }

        private void ShowAllCurators()
        {
            var curators = _dataBaseCore.TakeAllCurators();
            Console.WriteLine("\nAll Curators:");
            foreach (var curator in curators)
            {
                Console.WriteLine($"ID: {curator.Id}, Name: {curator.Name}, Group ID: {curator.GroupId}, Email: {curator.Email}");
            }
        }

        private void FindCuratorsName()
        {
            Console.WriteLine("\nEnter group ID to find the curator:");
            var groupId = Console.ReadLine();

            if (int.TryParse(groupId, out var groupIntId))
            {
                var curatorName = _dataBaseCore.FindCuratorsName(groupIntId);
                if (!string.IsNullOrEmpty(curatorName))
                {
                    Console.WriteLine($"\nThe curator for group {groupId} is: {curatorName}");
                }
                else
                {
                    Console.WriteLine($"\nNo curator found for group {groupId}!");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid group ID! Please enter a valid integer!");
            }
        }
    }
}

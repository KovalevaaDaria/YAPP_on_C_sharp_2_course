using Core;

namespace Presentation
{
    public class Facade
    {
        private readonly DataBaseCore _dataBaseCore;
        private readonly StudentFacade _studentFacade;
        private readonly CuratorFacade _curatorFacade;
        private readonly GroupFacade _groupFacade;

        public Facade()
        {
            _dataBaseCore = new DataBaseCore();
            _studentFacade = new StudentFacade(_dataBaseCore);
            _curatorFacade = new CuratorFacade(_dataBaseCore);
            _groupFacade = new GroupFacade(_dataBaseCore);
        }

        public void Run()
        {
            Console.WriteLine("\nStart the program:");

            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. View actions for Student class");
                Console.WriteLine("2. View actions for Curator class");
                Console.WriteLine("3. View actions for Group class");
                Console.WriteLine("0. Exit");

                Console.WriteLine("\nSelected action:");

                if (int.TryParse(Console.ReadLine(), out var choice))
                {
                    switch (choice)
                    {
                        case 1:
                            _studentFacade.Run();
                            break;
                        case 2:
                            _curatorFacade.Run();
                            break;
                        case 3:
                            _groupFacade.Run();
                            break;
                        case 0:
                            Console.WriteLine("\nEnd the program!");
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
    }
}

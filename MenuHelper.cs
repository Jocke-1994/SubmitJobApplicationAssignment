using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmitJobApplicationAssignment
{
    public class MenuHelper
    {
        public static void Menu()
        {
            JobManager manager = new JobManager();

            bool isActive = true;
            while (isActive)
            {
                Console.Clear();
                //Programmet ska innehålla minst följande alternativ:
                Console.WriteLine("Välkommen till Job Tracker!");
                Console.WriteLine("Välj nedan vad du vill göra");
                Console.WriteLine("Dina val är 1-9");
                Console.WriteLine("1. Lägga till en ny ansökan");
                Console.WriteLine("2. Visa alla ansökningar");
                Console.WriteLine("3. Sortera ansökningar efter status");
                Console.WriteLine("4. Sortera ansökningar efter datum");
                Console.WriteLine("5. Visa statistik \n Totalt antal ansökningar\n Antal per status \n Genomsnittlig svarstid");
                Console.WriteLine("6. Uppdatera status på en ansökan");
                Console.WriteLine("7. Ta bort en ansökan");
                Console.WriteLine("8. Avsluta programmet");

                Console.WriteLine("Vad vill du göra?");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        JobManager.AddJob();
                        break;
                    case "2":
                        JobManager.ShowAllApplications();
                        break;
                    case "3":
                        JobManager.SortByStatus();
                        break;
                    case "4":
                        JobManager.SortByDate();
                        break;
                    case "5":
                        JobManager.ShowStatistics();
                        break;
                    case "6":
                        JobManager.UpdateStatus();
                        break;
                    case "7":
                        JobManager.RemoveApplication();
                        break;
                    case "8":
                        isActive = false;
                        Console.WriteLine("Tack för att du använt programmet!");
                        break;
                    default:
                        Console.WriteLine("Du måste göra ett giltigt val");
                        break;

                }
                Console.ReadLine();
            }
        }
    }
}

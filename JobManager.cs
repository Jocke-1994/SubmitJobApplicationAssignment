using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmitJobApplicationAssignment
{
    public class JobManager
    {
        public class JobManager
        {
            public static List<JobApplication> ListOfApplications { get; set; } = new List<JobApplication>();

            public JobManager()
            {
                ListOfApplications.Add(new JobApplication("Atlantångare", "Krabbdansare", 55000, new DateTime(2025, 10, 19), ApplicationStatus.Rejected));
                ListOfApplications.Add(new JobApplication("Partykungen", "PartyHatt", 47000, new DateTime(2025, 9, 12), ApplicationStatus.Rejected));
                ListOfApplications.Add(new JobApplication("Liseberg", "Pralintestare", 38000, new DateTime(2025, 8, 25), ApplicationStatus.Interview));
                ListOfApplications.Add(new JobApplication("SwedishMatch", "Cigarettrullare", 15000, new DateTime(2025, 1, 02), ApplicationStatus.Offer));
                ListOfApplications.Add(new JobApplication("H&M", "Plaggbrännare", 38000, new DateTime(2025, 7, 10), ApplicationStatus.Applied));
                ListOfApplications.Add(new JobApplication("ICA", "Förste dasspapper testare", 42000, new DateTime(2025, 2, 6), ApplicationStatus.Rejected));
            }


            //AddJob() //– lägger till en ny ansökan 
            public static void AddJob()
            {
                Console.WriteLine("Vad heter företaget?");
                string CompanyName = Console.ReadLine();
                Console.WriteLine("Vilken roll har du sökt?");
                string PositionTitle = Console.ReadLine();
                Console.WriteLine("Vilken är den förväntade brutto lönen? (Innan skatt)");
                int SalaryExpectation = Convert.ToInt32(Console.ReadLine());

                // Skapa nytt objekt
                JobApplication newApp = new JobApplication(CompanyName, PositionTitle, SalaryExpectation, DateTime.Now) { };


                ListOfApplications.Add(newApp);// Lägg till i listan

                Console.WriteLine($"Jobbet på {CompanyName} som {PositionTitle} har lagts till i listan.");
            }

            //ShowAll() //– visar alla ansökningar
            public static void ShowAllApplications()
            {
                var apps = JobManager.ListOfApplications;

                if (apps.Count == 0)
                {
                    Console.WriteLine("Inga jobb finns i listan.");

                }

                Console.WriteLine("\nAlla jobbansökningar:");
                for (int i = 0; i < apps.Count; i++)
                {
                    var app = apps[i];

                    // index och företag och position
                    Console.Write($"{i + 1}. Företag: {app.CompanyName}. Roll: {app.PositionTitle}. Ansökningsdatum: {app.ApplicationDate:yyyy-MM-dd}. Status: ");

                    // metod som färgar status
                    PrintStatusColored(app.Status);

                    Console.WriteLine();
                }
            }
            //ShowByStatus() //– filtrerar med LINQ efter status(VG del)
            public static void SortByStatus()
            {
                Console.WriteLine("Vilken status vill du visa överst?");
                Console.WriteLine("1. Applied");
                Console.WriteLine("2. Interview");
                Console.WriteLine("3. Offer");
                Console.WriteLine("4. Rejected");

                string choice = Console.ReadLine();

                if (!int.TryParse(choice, out int statusChoice) || statusChoice < 1 || statusChoice > 4)
                {
                    Console.WriteLine("Ogiltigt val.");

                }

                // Konvertera valet (1–4) till enum-värdet
                ApplicationStatus selectedStatus = (ApplicationStatus)(statusChoice - 1);

                // Sortera: först de med vald status, sen resten
                var sorted = JobManager.ListOfApplications
                    .OrderByDescending(app => app.Status == selectedStatus)
                    .ThenBy(app => app.CompanyName)
                    .ToList();

                Console.WriteLine($"Alla jobb (status {selectedStatus} visas överst):");
                foreach (var app in sorted)
                {
                    Console.Write($"Företag: {app.CompanyName}. Roll: {app.PositionTitle} Status: ");
                    PrintStatusColored(app.Status);
                    Console.WriteLine();
                }

            }


            public static void SortByDate()
            {
                if (JobManager.ListOfApplications.Count == 0)
                {
                    Console.WriteLine("Inga jobb finns i listan.");

                }

                Console.WriteLine("Hur vill du sortera efter datum?");
                Console.WriteLine("1. Äldst först");
                Console.WriteLine("2. Nyast först");

                string choice = Console.ReadLine();

                List<JobApplication> sorted; //för att deklarera variabeln sorted

                if (choice == "2")
                {
                    sorted = JobManager.ListOfApplications
                        .OrderByDescending(app => app.ApplicationDate)
                        .ToList();
                }
                else
                {
                    sorted = JobManager.ListOfApplications
                        .OrderBy(app => app.ApplicationDate)
                        .ToList();
                }
                Console.WriteLine("Jobb sorterade efter datum:");
                foreach (var app in sorted)
                {
                    Console.Write($"{app.ApplicationDate:yyyy-MM-dd} | Företag: {app.CompanyName}.  Roll: {app.PositionTitle} Status: ");
                    PrintStatusColored(app.Status);
                    Console.WriteLine();
                }
            }
            public static void ShowStatistics()
            {
                var apps = JobManager.ListOfApplications;

                Console.WriteLine($"Totalt antal ansökningar: {apps.Count}");

                // Antal per status
                var groupedByStatus = apps.GroupBy(a => a.Status);
                foreach (var group in groupedByStatus)
                {
                    Console.WriteLine($"{group.Key}: {group.Count()} st");
                }

                // Genomsnittlig svarstid
                var responseTimes = new List<double>();

                foreach (var a in apps)
                {
                    if (a.ResponseDate != null)
                    {
                        TimeSpan difference = a.ResponseDate.Value - a.ApplicationDate;
                        responseTimes.Add(difference.TotalDays);
                    }
                }

                double averageResponseTime = 0;

                if (responseTimes.Count > 0)
                {
                    averageResponseTime = responseTimes.Average();
                }

                Console.WriteLine($"Genomsnittlig svarstid: {averageResponseTime:F1} dagar");
            }
            //UpdateStatus() //– ändrar status på en befintlig ansökan
            public static void UpdateStatus()
            {
                if (ListOfApplications.Count == 0)
                {
                    Console.WriteLine("Det finns inga jobb att uppdatera!");

                }

                // 1. Lista alla jobb
                for (int i = 0; i < ListOfApplications.Count; i++)
                {
                    var app = ListOfApplications[i];
                    Console.WriteLine($"{i + 1}. {app.CompanyName} - {app.PositionTitle} ({app.Status})");
                }

                // 2. Välj jobb
                Console.Write("Ange numret på jobbet du vill uppdatera: ");
                int index = Convert.ToInt32(Console.ReadLine()) - 1;

                if (index < 0 || index >= ListOfApplications.Count)
                {
                    Console.WriteLine("Ogiltigt val!");

                }

                var selectedJob = ListOfApplications[index];

                // 3. Lista statusalternativ
                Console.WriteLine("Välj ny status:");
                foreach (var s in Enum.GetValues(typeof(ApplicationStatus)))
                {
                    Console.WriteLine($"{(int)s} - {s}");
                }

                // 4. Uppdatera
                int newStatus = Convert.ToInt32(Console.ReadLine());
                selectedJob.Status = (ApplicationStatus)newStatus;

                // 5. Bekräftelse
                Console.WriteLine($"Status för {selectedJob.PositionTitle} hos {selectedJob.CompanyName} uppdaterades till {selectedJob.Status}.");
            }
            public static void RemoveApplication()
            {
                var app = JobManager.ListOfApplications;
                if (app.Count == 0)
                {
                    Console.WriteLine("Det finns inga ansökningar att ta bort");
                }

                Console.WriteLine("Vilken ansökan vill du ta bort?");
                for (int i = 0; i < app.Count; i++)
                {
                    Console.Write($" {i + 1}. Företag: {app[i].CompanyName}. Roll: {app[i].PositionTitle}.");
                    Console.WriteLine();

                }
                Console.Write("Ange numret för den ansökan som du vill ta bort: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int index) || index < 1 || index > app.Count)
                {
                    Console.WriteLine("Ogiltigt val");
                }
                var removedApp = app[index - 1];

                Console.WriteLine($"Är du säker på att du vill ta bort ansökan för rollen som {removedApp.PositionTitle} hos {removedApp.CompanyName}? j/n");
                string confirm = Console.ReadLine().ToLower();

                if (confirm == "j")
                {
                    app.RemoveAt(index - 1);
                    Console.WriteLine("Ansökan har tagits bort");
                }
                else
                {
                    Console.WriteLine("Ingen ansökan har tagits bort");
                }
            }
            public static void PrintStatusColored(ApplicationStatus status)
            {
                switch (status)
                {
                    case ApplicationStatus.Applied:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case ApplicationStatus.Interview:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case ApplicationStatus.Offer:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case ApplicationStatus.Rejected:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                    default:
                        Console.ResetColor();
                        break;
                }

                Console.Write(status);
                Console.ResetColor();
            }
        }
    }
}

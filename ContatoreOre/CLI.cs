using System;
using System.Collections.Generic;
using System.IO;

namespace ContatoreOre {
    class CLI {
        private LinkedList<Activity> activities;
        private int lastId = 0;
        private string path = ".\\dump.txt";

        public CLI() {
            activities = new LinkedList<Activity>();
            LoadData();
            ShowCLI();
        }

        private void LoadData() {
            if (File.Exists(path)) {
                using (StreamReader sr = File.OpenText(path)) {
                    string s;
                    while ((s = sr.ReadLine()) != null) {
                        string[] fields = s.Split(";");
                        Activity act = new Activity(Convert.ToInt32(fields[0]), Convert.ToDateTime(fields[1]),
                            Convert.ToDouble(fields[2]), fields[3], Convert.ToDouble(fields[4]), Convert.ToBoolean(fields[5]));
                        activities.AddLast(act);
                        lastId = Convert.ToInt32(fields[0]);
                    }

                    sr.Close();
                }
            } else
                File.Create(path).Close();
        }

        private void ShowCLI() {
            string text = 
                "-- First C# project --\n" +
                " 1. Add Activity\n" +
                " 2. See All Activities\n" +
                " 3. Closa Activity\n" +
                " 0. Exit\n" +
                "Enter your choice >> ";
            int choice = -1;

            while(choice != 0) {
                Console.WriteLine(text);
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice) {
                    case 1:
                        AddActivity();
                        Console.WriteLine("Activity added with Id: "+ lastId +"\n");
                        break;
                    case 2:
                        DumpActivities();
                        break;
                    case 3:
                        if (CloseActivity())
                            Console.WriteLine("Activity Ended Succesfully\n");
                        else
                            Console.WriteLine("Activity not Found, Retry\n");
                        break;
                    case 0:
                        SaveData();
                        break;
                    default:
                        Console.WriteLine("Not a valid choice!\n");
                        break;
                }
            }
            Console.WriteLine("Goodbye!\n");
        }

        private void AddActivity() {
            string title;
            int pay;
            Console.WriteLine("Insert Activity Title: ");
            title = Console.ReadLine();
            Console.WriteLine("Insert Hourly Pay: ");
            pay = Convert.ToInt32(Console.ReadLine());

            activities.AddLast(new Activity(lastId + 1, title, pay));
            lastId += 1;
        }

        private void DumpActivities() {
            foreach (Activity act in activities)
                Console.WriteLine(act.ToString());
        }

        private bool CloseActivity() {
            bool found = false;
            Console.WriteLine("Insert Activity Id: ");
            int id = Convert.ToInt32(Console.ReadLine());

            foreach(Activity act in activities) {
                if (act.GetID() == id) {
                    act.EndActivity();
                    found = true;
                }
            }
            return found;
        }

        private void SaveData() {
            if (!File.Exists(path))
                File.Create(path);

            using (StreamWriter sw = File.CreateText(path)) {
                foreach (Activity act in activities)
                    sw.Write(act.Serialize());

                sw.Close();
            }
        }
    }
}

using System;

namespace ContatoreOre {
    class Activity {
        private int id;
        private DateTime start;
        private double duration = 0;
        private string title { get; set; }
        private double hourlyPay;
        private bool editable;

        public Activity(int id, string title, double hourlyPay) {
            this.id = id;
            this.title = title;
            this.hourlyPay = hourlyPay;
            start = DateTime.Now;
            editable = true;
        }

        public Activity(int id, DateTime start, double duration, string title, double hourlyPay, bool editable) {
            this.id = id;
            this.start = start;
            this.duration = duration;
            this.title = title;
            this.hourlyPay = hourlyPay;
            this.editable = editable;
        }

        public int GetID() {
            return id;
        }

        public void EndActivity() {
            duration = (DateTime.Now - start).TotalHours;
            editable = false;
        }

        public double GetPay() {
            return hourlyPay * duration;
        }

        public string Serialize() {
            return id + "; " + start + "; " + duration + "; " + title + "; " + hourlyPay + "; " + editable + "\n";
        }

        public override string ToString() {
            string dump = "";

            dump += "------------------\n";
            dump += "Activity no. " + id + "\n";
            dump += "Title: " + title + "\n";
            if(duration != 0) {
                dump += "Time Span in Hours: " + duration + "\n";
                dump += "Total Pay : " + GetPay() + "\n";
            } else
                dump += "Activity Still in progress\n";
            
            dump += "------------------\n";

            return dump;
        }
    }
}

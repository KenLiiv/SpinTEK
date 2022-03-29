using System;
using System.Text;

namespace SpinTEKyl
{
    public class PaymentTableHelper
    {

        private DateHelper DateHelperInstance { get; set; }
        
        public PaymentTableHelper(DateHelper dateHelperInstance)
        {
            DateHelperInstance = dateHelperInstance;
        }
        
        public string CreatePaymentTableCsvString()
        {
            var csv = new StringBuilder();
            csv.AppendLine("maksmise_kuupaev, meeldetuletuse_kuupaev");
            
            int year = DateHelperInstance.GetYear();
            const int dayToBePaid = 10;

            for (int i = 1; i <= 12; i++)
            {
                DateTime dateToBePaid = DateHelperInstance.GetClosestWorkingDayDateTime(new DateTime(year, i, dayToBePaid));
                DateTime reminderDate = DateHelperInstance.GetClosestWorkingDayDateTime(dateToBePaid.AddDays(-3));
                
                string newLine = string.Format("{0}, {1}",
                    DateHelperInstance.GetDateTimeDateString(dateToBePaid), 
                    DateHelperInstance.GetDateTimeDateString(reminderDate));

                csv.AppendLine(newLine);
            }

            return csv.ToString();
        }

        public void WritePaymentTableCsvFile(string csvString, string filePath)
        {
            var fileNameCsv = filePath + System.IO.Path.DirectorySeparatorChar + DateHelperInstance.GetYear() + ".csv";
            Console.WriteLine($"Saving to {fileNameCsv}");
            System.IO.File.WriteAllText(fileNameCsv, csvString);
        }
    }
}
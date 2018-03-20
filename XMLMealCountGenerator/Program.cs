using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace XMLMealCountGenerator
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Classroom> schoolEnrollment = ParseXml.getEnrollment();
            for (int i = 0; i < schoolEnrollment.Count; i++)
            {
                //    schoolEnrollment[i].displayData();
                GeneratePage.generateFile(schoolEnrollment[i]);
            }
            GeneratePage.copyStyleSheet();
            Console.ReadLine();
            
        }
            
    }
}

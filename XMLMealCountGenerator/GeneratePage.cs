using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLMealCountGenerator
{
    public static class GeneratePage
    {
        public static void copyStyleSheet()
        {
            String path = System.IO.Directory.GetCurrentDirectory();
            String sheet = System.IO.File.ReadAllText(path + "\\default.css");
            String filePath = (path + "\\MealCountForms_" + getDate() + "\\default.css");
            System.IO.File.WriteAllText(filePath, sheet);

        }

        public static void generateFile(Classroom group)
        {
            String currentPath = System.IO.Directory.GetCurrentDirectory();
            String template = loadTemplate(currentPath);          
            String childLoop = System.IO.File.ReadAllText(currentPath + "\\loop.txt");         
            String newRoster = insertNames(template, childLoop, group);
            newRoster = newRoster.Replace("CLASSROOM TITLE", group.getName());
            newRoster = newRoster.Replace("CLASSROOM", group.getName());
            System.IO.Directory.CreateDirectory(currentPath + "\\MealCountForms_" + getDate());
            String filePath = currentPath + "\\MealCountForms_" + getDate() + "\\" + group.getName() + "_" + getTime() + ".html";
            System.IO.File.WriteAllText(filePath, newRoster);
            Console.WriteLine("Output saved to "+ filePath);
        }

        private static String insertNames(String file, String loop, Classroom room) {
            int current = 0;
            String child;
            int prevCat = 1; //used to insert extra blank row when category changes
            int extraLines = 0; //used to change the rowspan at the end, default being 26

            for (int i = 0; i < room.getCount(); i++ )
            {
                current = file.IndexOf("<!--ROSTER START-->");
                //Console.WriteLine("Value of Current: " + current);
                child = getChildRow(loop, room.getChild(i).getName(), room.getChild(i).getCategoryNumber().ToString());
                if (prevCat != room.getChild(i).getCategoryNumber())
                {
                    file = file.Insert(current, "<tr><td colspan=\"27\"></td></tr>");
                    extraLines++;
                    current = file.IndexOf("<!--ROSTER START-->");
                };
                file = file.Insert(current, child);
                prevCat = room.getChild(i).getCategoryNumber();
            }
            for (int i = room.getCount(); i < 25; i++)
            {
                current = file.IndexOf("<!--ROSTER START-->");
                child = getChildRow(loop, "&nbsp", "&nbsp");
                file = file.Insert(current, child);
            }
                file = file.Replace("26", (26+extraLines).ToString());
                return file;
        }

        private static String getChildRow(String rowCode, String name, String cat)
        {
            rowCode = rowCode.Replace("CHILD", name);
            rowCode = rowCode.Replace("0", cat);
            //Console.WriteLine("child row after insert:");
            //Console.WriteLine(rowCode);
            return rowCode;
        }

        private static String loadTemplate(String path) {
            return System.IO.File.ReadAllText(path + "\\source.html");
        }

        private static string getDate()
        {
            return String.Format("{0:MM-dd-yyyy}", DateTime.Now);
        }

        private static string getTime()
        {
            return String.Format("{0:MM-dd-yyyy_hh-mm-ss}", DateTime.Now);
        }
    }
}

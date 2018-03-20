using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLMealCountGenerator
{
    public class Classroom
    {
        private String roomName;
        private List<Child> childRoster;

        public Classroom(String name)
        {
            roomName = name;
            childRoster = new List<Child>();
        }

        public String getName()
        {
            String nameOut = roomName.Replace("/", "");
            return nameOut;
        }

        public void addChild(Child newChild)
        {
            childRoster.Add(newChild);
        }

        public Child getChild(int index)
        {
            return childRoster[index];
        }

        public int getCount()
        {
            return childRoster.Count;
        }

        public void sortByCategory()
        {
            childRoster.Sort((x, y) => x.getCategoryNumber().CompareTo(y.getCategoryNumber()));
        }

        public void displayData() {
            Console.WriteLine("=============================");
            Console.WriteLine(roomName);
            Console.WriteLine("-----------------------------");
            for (int i = 0; i < childRoster.Count; i++)
            {
                Console.WriteLine((i+1) + ". " + childRoster[i].getName() + " " + childRoster[i].getCategoryNumber());
            }
            Console.WriteLine("=============================");
        }
    }
}

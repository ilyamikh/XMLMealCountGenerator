using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLMealCountGenerator
{
    public class Child
    {
        String name;
        String category;

        public Child(String inName, String inCategory)
        {
            name = inName;
            category = inCategory;
        }

        public String getName()
        {
            return name;
        }

        public String getCategory()
        {
            return category;
        }

        public int getCategoryNumber()
        {
            int catNum = -1;
            switch (category)
            {
                    
                case "Free": catNum = 1; break;
                case "Reduced": catNum = 2; break;
                case "Paid": catNum = 3; break;
                case "Not Enrolled": catNum = 3; break;               
            }
            return catNum;
        }
    }
}

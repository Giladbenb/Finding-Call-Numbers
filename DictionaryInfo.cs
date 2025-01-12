using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ST10090336 - Gilad Benbenishti PROG7312-Part Two
namespace Prog3B
{
    public class DictionaryInfo
    {
        // Declaring the Callnumber and Description getter and setter 
        public string CallNumber { get; set; }
        public string Description { get; set; }
    }
}
// Dictionary list including all Call-Numbers and Descriptions 
   public static class DictionaryData
{
    public static Dictionary<string, string> Information = new Dictionary<string, string>
        {
            { "000", "Computer science, information + general works" },
            { "100", "Philosophy + psychology" },
            { "200", "Religion" },
            { "300", "Social sciences" },
            { "400", "Languages" },
            { "500", "Sciences" },
            { "600", "Technology" },
            { "700", "Arts + recreation" },
            { "800", "Literature" },
            {"900", "History and Geography" }

        };
}



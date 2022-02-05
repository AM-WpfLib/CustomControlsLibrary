using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomControlTestShow.Models
{
    public class ItemsControlTestModel
    {
        public ItemsControlTestModel(string sampleText)
        {
            SampleText = sampleText;
        }

        public string SampleText { get; set; }
    }
}

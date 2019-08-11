using System;
using System.Collections.Generic;
using System.Text;

namespace GRINTSYS.SAPMiddleware.Products.Dto
{
    public class FilterOutput
    {
        public int Id { get; set; }
        public String Type { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }

        public double Min { get; set; }
        public double Max { get; set; }
    }
}

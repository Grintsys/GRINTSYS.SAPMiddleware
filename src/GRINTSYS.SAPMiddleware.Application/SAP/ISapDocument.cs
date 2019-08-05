using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;

namespace GRINTSYS.SAPMiddleware.SAP
{
    public interface ISapDocument
    {
        Company Connect(SapInput input);
        Task Execute(SapDocumentInput input);
    }
}

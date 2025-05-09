using System;

namespace Dwapi.Vmmc.SharedKernel.Exceptions
{
    public class DocketNotFoundException : Exception
    {
        public DocketNotFoundException(string docketId) : base($"Docket {docketId} does not exist")
        {

        }
    }
}
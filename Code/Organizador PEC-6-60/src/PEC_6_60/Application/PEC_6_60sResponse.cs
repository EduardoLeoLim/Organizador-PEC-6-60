using System.Collections.Generic;

namespace Organizador_PEC_6_60.PEC_6_60.Application
{
    public class PEC_6_60sResponse
    {
        public IEnumerable<PEC_6_60Response> PEC_6_60s { get; }
        
        public PEC_6_60sResponse(IEnumerable<PEC_6_60Response> pec660S)
        {
            PEC_6_60s = pec660S;
        }
    }
}


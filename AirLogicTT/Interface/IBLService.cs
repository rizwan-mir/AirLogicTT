using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLogicTT.Interface
{
    public interface IBLService
    {
        int CountWords(string text);

        List<string> GetTitles(string strDetails);

        double CalcAverage(List<int> noOfWords);

        double Variance(List<int> noOfWords, double avg);

        double StdDev(double var);
    }
}

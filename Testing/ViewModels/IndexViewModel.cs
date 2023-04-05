using Testing.Models;

namespace Testing.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Test> Tests { get; set; }
        public IEnumerable<Result> Results { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public static int GetNum(int iteration,int pageNumber)
        {
            return iteration + (pageNumber-1)*5;
        }
    }

}

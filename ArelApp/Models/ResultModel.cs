using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArelApp.UI.Models
{
    public class ResultModel
    {
        public ResultModel()
        {
            DepartmentwithLectures = new List<ResultDepartmentModel>();
        }
        public List<ResultDepartmentModel> DepartmentwithLectures { get; set; }
    }

    public class ResultDepartmentModel
    {
        public ResultDepartmentModel()
        {
            Lectures = new List<ResultLectureModel>();
        }
        public string DepartmentName { get; set; }
        public List<ResultLectureModel> Lectures { get; set; }
    }

    public class ResultLectureModel
    {
        public string LectureName { get; set; }
        public string Midterm { get; set; }
        public string Final { get; set; }
    }
}

using ArelApp.Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ArelApp.UI.Models
{
    public class UserLectureModel
    {
        public int LectureId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Credit { get; set; }
        public string Department { get; set; }
        public EnumApprovalStatus ApprovalStatus { get; set; }
        public List<Lecture> SelectedLectures { get; set; }
        public List<Lecture> Lectures { get; set; }
        public List<Department> Departments { get; set; }
        public User User { get; set; }

    }
}

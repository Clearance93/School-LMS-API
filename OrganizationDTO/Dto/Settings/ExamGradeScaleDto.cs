namespace OrganizationDTO.Dto.Settings
{
    public class ExamGradeScaleDto
    {
        public Guid ExamGradeScaleId { get; set; }

        public Guid OrganizationId { get; set; }

        public int? PassMark { get; set; }

        public int? ExcellentMark { get; set; }

        public int? AverageMark { get; set; }

        public int? PoorMark { get; set; }

        public int? DistinctionMark { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}

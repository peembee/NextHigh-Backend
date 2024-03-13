namespace GoApptechBackend.Models.DTO.QuizDTO
{
    public class CreateQuizDTO
    {
        public string QuizHeading { get; set; } = string.Empty;

        public string AltOne { get; set; } = string.Empty;

        public string AltTwo { get; set; } = string.Empty;

        public string AltThree { get; set; } = string.Empty;

        public int Points { get; set; }

        public string CorrectAnswer { get; set; } = string.Empty;
    }
}

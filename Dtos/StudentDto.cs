namespace StudentApi.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }           // included so CreatedAtAction can return id
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}

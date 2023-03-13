namespace OneClass.Domain.DbModels;

public class ClassroomData {
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string TeacherId { get; set; }
    public string[] StudentIds { get; set; }
    public string JoinCode { get; set; }
}
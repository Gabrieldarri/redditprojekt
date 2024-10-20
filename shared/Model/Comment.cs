namespace shared.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }

        public int PostId { get; set; }  // Relaterer kommentaren til en post
        public string User { get; set; }  // Ændret fra User til string for brugernavn
        public DateTime CreatedAt { get; set; }

        public Comment(string content = "", int upvotes = 0, int downvotes = 0, string user = "") // Ændret parameter fra User til string
        {
            Content = content;
            Upvotes = upvotes;
            Downvotes = downvotes;
            User = user; // Tildel user string
            CreatedAt = DateTime.Now;
        }

        public Comment()
        {
            Id = 0;
            Content = "";
            Upvotes = 0;
            Downvotes = 0;
            User = ""; // Initialiser user string
            CreatedAt = DateTime.Now;
        }
    }
}

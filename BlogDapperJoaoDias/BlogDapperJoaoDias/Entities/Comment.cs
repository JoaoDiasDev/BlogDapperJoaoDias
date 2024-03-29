﻿namespace BlogDapperJoaoDias.Entities
{
    public class Comment
    {
        [Dapper.Contrib.Extensions.Key]
        public int CommentId { get; set; }
        public int ArticleId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Status { get; set; }
    }
}

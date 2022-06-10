namespace Blog.Services
{
    using Blog.Dtos;

    using System;
    using System.Linq;

    public static class BlogService
    {
        public static void NumberOfCommentsPerUser(MyDbContext dbContext)
        {
            var userComments = dbContext.BlogComments
                .GroupBy(x => x.UserName)
                .Select(x => new UserCommentsDto(x.Key, x.Count()))
                .ToList();

            userComments.ForEach(x => Console.WriteLine($"{x.Name}: {x.CountComments}"));
        }

        public static void PostsOrderedByLastCommentDate(MyDbContext dbContext)
        {
            var posts = dbContext.BlogPosts
                .Select(p => p.Comments.OrderByDescending(x => x.CreatedDate).First())
                .OrderByDescending(x => x.CreatedDate)
                .Select(c => new PostDto(c.BlogPost.Title, c.CreatedDate, c.Text))
                .ToList();

            posts.ForEach(x => Console.WriteLine($"{x.PostName}: '{x.DateLastComment.Date}' - '{x.TextLastComments}'"));
        }

        public static void NumberOfLastCommentsLeftByUser(MyDbContext dbContext)
        {
            var latsUsersComments = dbContext.BlogPosts
                .Select(p => p.Comments.OrderByDescending(x => x.CreatedDate).First())
                .OrderByDescending(x => x.CreatedDate)
                .GroupBy(x => x.UserName)
                .Select(c => new UserCommentsDto(c.Key, c.Count()))
                .ToList();

            latsUsersComments.ForEach(x => Console.WriteLine($"{x.Name}: {x.CountComments}"));
        }
    }
}
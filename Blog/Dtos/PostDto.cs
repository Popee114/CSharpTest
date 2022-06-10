namespace Blog.Dtos
{
    using System;

    public record PostDto(string PostName, DateTime DateLastComment, string TextLastComments);
}
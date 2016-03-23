using NhibernateTest.Service;

namespace NhibernateTest
{
    public static class ServiceFactory
    {
        public static IMessageService MessageService { get; set; }

        public static ICommentService CommentService { get; set; }

        public static IUserService UserService { get; set; }
    }
}
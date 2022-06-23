namespace Blog.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }


        /// A3 -
        public override string ToString()
        {
            return
                $"Id: {Id} FirstName: {FirstName}  LastName: {LastName} UserName: {Username} Salt: {Salt} Password: {Password}";
        }


    }
}
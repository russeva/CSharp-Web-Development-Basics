namespace WebServerV._2.ByTheCakeApplication.Views.Home
{
    using WebServerV._2.Server.Http.Contracts;

    public class FileView : IView
    {
        private readonly string htmlFile;

        public FileView(string htmlFile)
        {
            this.htmlFile = htmlFile;
        }

        public string View() => this.htmlFile;
        
    }
}

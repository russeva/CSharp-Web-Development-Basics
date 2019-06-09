namespace WebServerV._2.Server.Common
{
    using System;
    using Server.Http.Contracts;

    public class InternalServerErrorView : IView
    {
        private readonly Exception exception;

        public InternalServerErrorView(Exception ex)
        {
            this.exception = ex;
        }

        public string View()
        {
            return $"<h1>{this.exception.Message}</h1>";
        }
    }
}

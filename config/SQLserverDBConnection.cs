namespace CalculadoraNET_JuanCastro.config
{
    public class SQLserverDBConnection
    {
        private string connectionString = string.Empty;

        public SQLserverDBConnection() 
        
        {

            var build = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            connectionString = build.GetSection("ConnectionStrings:SQLserverConnectionString").Value;

        }


        public string dbConnection()
        {

            return connectionString;
        }
       

    }
}

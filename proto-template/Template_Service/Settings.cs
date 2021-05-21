namespace Template_Service {
    public class Settings {
        public DatabaseSettings Database { get; set; } = new DatabaseSettings();

        public class DatabaseSettings {
            public string ConnectionString { get; set; }
            public string Name { get; set; }
        }
    }
}
using System;


namespace CarsFunctionApp
{
    public class Car
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("n");
        public string name { get; set; }
        public string maxSpeed { get; set; }
    }

    public class CarCreateModel
    {
        public string name { get; set; }
        public string maxSpeed { get; set; }
    }

    public class CarUpdateModel
    {
        public string name { get; set; }
        public string maxSpeed { get; set; }
    }
}

using ValidenatorXSD.DTO;
using ValidenatorXSD.IC;

namespace ValidenatorXSD
{
    public class ValidnatorXSD
    {
        private readonly IConfig _config;

        public ValidnatorXSD(IConfig config)
        {
            _config = config;
        }


        public static async void Start()
        {
        }
    }
}
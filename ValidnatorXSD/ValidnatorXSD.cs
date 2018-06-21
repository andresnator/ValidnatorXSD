using ValidenatorXSD.DTO;

namespace ValidenatorXSD
{
    public class ValidnatorXSD
    {
        private readonly IConfig _config;

        public ValidnatorXSD(IConfig config)
        {
            _config = config;
        }
    }
}
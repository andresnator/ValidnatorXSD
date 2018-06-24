using System;
using System.Collections.Generic;
using System.Xml;
using ValidnatorXSD.BL;
using ValidnatorXSD.IC;
using ValidnatorXSD.Models;

namespace ValidnatorXSD
{
    public class ValidnatorXsd
    {
        private readonly IConfig _config;

        public ValidnatorXsd(IConfig config)
        {
            _config = config;
        }


        public List<ResponseErrorsModel> Start()
        {

            if (_config.ShemaReader == null)
            {
                throw new ArgumentException("Not Found 'ShemaReader' ");
            }
            var responseErrors = new List<ResponseErrorsModel>();

            var data = new DataFile(_config).GetDataFileModelList();
            var validRowsQuantity = new ValidateFileToXsd(_config).ValidQuantityRows(data);
            var validQuantityColumns = new ValidateFileToXsd(_config).ValidQuantityColumns(data);

            var xmElementFromData = new DataFile(_config).GetXElementFromData(data);

            var errorsValidate = new ValidateFileToXsd(_config).GetErrorsFromXmlFile(xmElementFromData);

            responseErrors.AddRange(errorsValidate);

            if (!validRowsQuantity) responseErrors.Add(new ValidateFileToXsd(_config).GetRowsErrorQuantity(data));

            if (!validQuantityColumns)
                responseErrors.AddRange(new ValidateFileToXsd(_config).GetColumnsErrorQuantity(data));

            return responseErrors;
        }
    }
}
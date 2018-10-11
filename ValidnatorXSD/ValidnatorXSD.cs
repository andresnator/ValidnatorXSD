using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ValidnatorXSD.BL;
using ValidnatorXSD.IC;
using ValidnatorXSD.Models;

namespace ValidnatorXSD
{
    public class ValidnatorXsd
    {
        private readonly IConfig config;

        public ValidnatorXsd(IConfig config)
        {
            this.config = config;
        }


        public List<ResponseErrorsModel> Start()
        {
            if (config.ShemaReader == null)
            {
                throw new ArgumentException("Not Found 'ShemaReader' ");
            }

            List<ResponseErrorsModel> responseErrors = new List<ResponseErrorsModel>();
            List<DataFileModel> data = new DataFile(config).GetDataFileModelList();

            bool validRowsQuantity = new ValidateFileToXsd(config).ValidQuantityRows(data);
            bool validQuantityColumns = new ValidateFileToXsd(config).ValidQuantityColumns(data);

            XElement xmElementFromData = new DataFile(config).GetXElementFromData(data);

            List<ResponseErrorsModel> errorsValidate =
                new ValidateFileToXsd(config).GetErrorsFromXmlFile(xmElementFromData);

            responseErrors.AddRange(errorsValidate);

            if (!validRowsQuantity)
            {
                responseErrors.Add(new ValidateFileToXsd(config).GetRowsErrorQuantity(data));
            }

            if (!validQuantityColumns)
            {
                responseErrors.AddRange(new ValidateFileToXsd(config).GetColumnsErrorQuantity(data));
            }

            return responseErrors.Distinct().OrderBy(c => c.RowPos).ThenBy(c => c.ColumnPos).ToList();
        }

        public List<ResponseErrorsModel> Start(List<DataFileModel> data)
        {
            if (config.ShemaReader == null)
            {
                throw new ArgumentException("Not Found 'ShemaReader' ");
            }

            List<ResponseErrorsModel> responseErrors = new List<ResponseErrorsModel>();


            bool validRowsQuantity = new ValidateFileToXsd(config).ValidQuantityRows(data);
            bool validQuantityColumns = new ValidateFileToXsd(config).ValidQuantityColumns(data);

            XElement xmElementFromData = new DataFile(config).GetXElementFromData(data);

            List<ResponseErrorsModel> errorsValidate =
                new ValidateFileToXsd(config).GetErrorsFromXmlFile(xmElementFromData);

            responseErrors.AddRange(errorsValidate);

            if (!validRowsQuantity)
            {
                responseErrors.Add(new ValidateFileToXsd(config).GetRowsErrorQuantity(data));
            }

            if (!validQuantityColumns)
            {
                responseErrors.AddRange(new ValidateFileToXsd(config).GetColumnsErrorQuantity(data));
            }

            return responseErrors.Distinct().OrderBy(c => c.RowPos).ThenBy(c => c.ColumnPos).ToList();
        }
    }
}
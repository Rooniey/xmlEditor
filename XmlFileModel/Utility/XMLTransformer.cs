using Saxon.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlFileModel.Utility
{
    public enum XsltOutputType
    {
        Txt,
        Svg,
        Report
    }

    public class XMLTransformer
    {
        public static string XsltFolderPath = "../../../xml_files";

        public static string TxtXsltFileName = "series_txt.xsl";
        public static string SvgXsltFileName = "series_svg.xsl";
        public static string ReportXsltFileName = "series_list.xsl";


        public string XmlReportFilePath { get; set; } = "../../../xml_files/series_report.xml";

        public string XmlFilePath { get; set; } = "../../../xml_files/series_list.xml";

        public XMLTransformer()
        {
            InitReport();
        }

        public void InitReport()
        {
            Transform(XmlFilePath, XmlReportFilePath, XsltOutputType.Report);
        }

        public void InitReport(string xmlPath, string reportPath)
        {
            Transform(xmlPath, reportPath, XsltOutputType.Report);
        }

        public void Transform(string outputPath, XsltOutputType outputType = XsltOutputType.Txt)
        {
            Transform(XmlReportFilePath, outputPath, outputType);
        }

        public void Transform(string inputPath, string outputPath, XsltOutputType outputType = XsltOutputType.Txt)
        {
            string xsltPath = "";
            switch (outputType)
            {
                case XsltOutputType.Txt:
                    xsltPath = $"{XsltFolderPath}/{TxtXsltFileName}";
                    break;
                case XsltOutputType.Svg:
                    xsltPath = $"{XsltFolderPath}/{SvgXsltFileName}";
                    break;
                case XsltOutputType.Report:
                    xsltPath = $"{XsltFolderPath}/{ReportXsltFileName}";
                    break;
                default:
                    xsltPath = $"{XsltFolderPath}/{TxtXsltFileName}";
                    break;
            }


            var processor = new Processor();
            XsltCompiler compiler = processor.NewXsltCompiler();
            using (var xsltReader = new StreamReader(xsltPath))
            {
                XsltExecutable xsltExecutable = compiler.Compile(xsltReader);
                XsltTransformer xsltTransformer = xsltExecutable.Load();

                using (var xmlReader = new StreamReader(inputPath))
                {
                    xsltTransformer.SetInputStream(xmlReader.BaseStream, new Uri("file://"));

                    using (var sw = new StreamWriter(outputPath))
                    {
                        Serializer serializer = processor.NewSerializer(sw);

                        xsltTransformer.Run(serializer);

                    }
                }

            }


        }
    }
}

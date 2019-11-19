using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXI.Filters.Options;
using UXI.Serialization;

namespace UXC.Utils.InvalidateZeroPosGazeSamples
{
    public class FilterOptions
        : IInputOptions
        , IOutputOptions
    {
        [Value(0, HelpText = "Path to the input gaze data file. If omitted, standard input stream is used.", MetaName = "input file", MetaValue = "FILE", Required = false)]
        public virtual string InputFilePath { get; set; }


        [Option("format", Default = FileFormat.Default, HelpText = "Data format of the input.")]
        public virtual FileFormat InputFileFormat { get; set; }


        public virtual FileFormat DefaultInputFileFormat => FileFormat.JSON;


        [Option('o', "output", Default = null, HelpText = "Path to the output file. If omitted, standard output stream is used.", Required = false)]
        public virtual string OutputFilePath { get; set; }


        [Option("output-format", Default = FileFormat.Default, HelpText = "Data format of the output.", Required = false)]
        public virtual FileFormat OutputFileFormat { get; set; }


        public virtual FileFormat DefaultOutputFileFormat => FileFormat.JSON;



        [Option('t', "threshold", Default = 0.1d, HelpText = "Minimum distance of positions in data to the zero position.", Required = false)]
        public virtual double Threshold { get; set; }
    }
}

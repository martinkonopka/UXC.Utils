﻿using CommandLine;
using CommandLine.Text;
using GazeDataTimestampCorrection.Serialization.Json.Converters;
using GazeDataTimestampCorrection.Statistics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using UXI.GazeFilter;
using UXI.GazeFilter.Statistics;
using UXI.GazeToolkit.Serialization;
using UXI.GazeToolkit.Serialization.Csv;
using UXI.GazeToolkit.Serialization.Json;

namespace GazeDataTimestampCorrection
{
    class Options : BaseOptions { }

    class Program
    {
        static int Main(string[] args)
        {
            return new SingleFilterHost<Options>
            (
                context => Configure(context),
                new RelayFilter<GazeDataTimestamp, GazeDataTimestamp, Options>(CorrectTimestamps)
            ).Execute(args);
        }


        private static void Configure(FilterContext context)
        {
            context.Formats = new Collection<IDataSerializationFactory>()
            {
                new JsonSerializationFactory(new GazeDataTimestampJsonConverter()),
                new CsvSerializationFactory()
            };

            context.Statistics = new Collection<IFilterStatisticsFactory>()
            {
                new TimestampsDiffStatisticsFactory()
            };
        }


        private static IObservable<GazeDataTimestamp> CorrectTimestamps(IObservable<GazeDataTimestamp> timestamps, Options options)
        {
            return Observable.Create<GazeDataTimestamp>(observer =>
            {
                List<GazeDataTimestamp> data = new List<GazeDataTimestamp>();

                timestamps.Do(d => data.Add(d)).Wait();

                data.Sort((a, b) => a.ReferenceTicks.CompareTo(b.ReferenceTicks));

                long minTicksDiff = data.Select(d => d.Ticks - d.ReferenceTicks).DefaultIfEmpty(0).Min();

                return data.Select(d => new GazeDataTimestamp(d.Payload, d.ReferenceTicks + minTicksDiff, d.ReferenceTicks, d.Offset))
                           .ToObservable()
                           .Publish().RefCount()
                           .Subscribe(observer);
            });
        }
    }
}

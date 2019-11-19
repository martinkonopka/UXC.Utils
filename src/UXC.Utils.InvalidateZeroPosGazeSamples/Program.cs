using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using UXC.Core.Data;
using UXC.Core.Data.Compatibility.GazeToolkit;
using UXC.Utils.InvalidateZeroPosGazeSamples.Configuration;
using UXI.Filters;
using UXI.Filters.Configuration;
using UXI.GazeToolkit.Utils;

namespace UXC.Utils.InvalidateZeroPosGazeSamples
{
    class Program
    {
        static int Main(string[] args)
        {
            return new SingleFilterHost<FilterOptions>
            (
                new RelayFilter<GazeData, GazeData, FilterOptions>("Invalidate Gaze Data Samples with Zero Positions", Invalidate),
                new UXCDataSerializationFilterConfiguration()
            ).Execute(args);
        }


        private static IObservable<GazeData> Invalidate(IObservable<GazeData> data, FilterOptions options, FilterContext context)
        {
            EyeGazeData invalidData = new EyeGazeData(EyeGazeDataValidity.Invalid, Point2.Default, Point3.Default, Point3.Default, Point3.Default, 0);

            return data.Select(gaze =>
            {
                var leftEye = HasZeroPosition(gaze.LeftEye, options.Threshold) ? invalidData : gaze.LeftEye;
                var rightEye = HasZeroPosition(gaze.RightEye, options.Threshold) ? invalidData : gaze.RightEye;

                return new GazeData
                (
                    EyeValidityEx2.MergeToEyeValidity(leftEye.Validity, rightEye.Validity),
                    leftEye,
                    rightEye,
                    gaze.TrackerTicks,
                    gaze.Timestamp    
                );
            });
        }


        private static bool HasZeroPosition(EyeGazeData eye, double thresholdDistance)
        {
            return PointUtils.EuclidianDistance(eye.GazePoint3D.ToToolkit(), UXI.GazeToolkit.Point3.Zero) <= thresholdDistance
                || PointUtils.EuclidianDistance(eye.EyePosition3D.ToToolkit(), UXI.GazeToolkit.Point3.Zero) <= thresholdDistance;
        }
    }
}

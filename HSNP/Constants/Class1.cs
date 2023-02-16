using System;
using System.Collections.Generic;
using System.Text;

namespace IeX.Mobile.Constants
{
    /// <summary>
    /// Specifies the expected format for AdditionalAnswerData on AnswerChoices with a free-form or additional data field.
    /// </summary>
    public enum AnswerFormat: int
    {
        /// <summary>
        /// This answer choice needs to additional answer data.
        /// </summary>
        None = 0,

        /// <summary>
        /// This answer choice expects additional string data.
        /// </summary>
        String = 1,

        /// <summary>
        /// This answer choice expects additional integer data.
        /// </summary>
        Int = 2,

        /// <summary>
        /// This answer choice expects an additional date value.
        /// </summary>
        Date = 3
    }

    public enum WellKnownQuestion: int
    {
        None = 0,
        NameOrInitials = 1,
        Gender = 2,
        DOB = 3
    }

    [Obsolete("This no longer needs to be passed, as the auth mechanism returns it.")]
    public enum AuthMethod: int
    {
        Google = 1,
        Facebook = 2,
        Microsoft = 3
    }


    public class GPSLocation
    {
        public double? Lat { get; set; }
        public double? Lon { get; set; }

        /// <summary>
        /// Identifies the estimated accuracy of the GPS location info. Represented as a "circle of confidence".
        /// </summary>
        /// <remarks>
        /// For Android:
        ///        We define accuracy as the radius of 68% confidence. In other words, if you draw a circle centered
        ///        at this location's latitude and longitude, and with a radius equal to the accuracy, then there is
        ///        a 68% probability that the true location is inside the circle.
        ///
        ///        In statistical terms, it is assumed that location errors are random with a normal distribution,
        ///        so the 68% confidence circle represents one standard deviation.Note that in practice, location
        ///        errors do not always follow such a simple distribution.
        ///
        ///        This accuracy estimation is only concerned with horizontal accuracy, and does not indicate the
        ///        accuracy of bearing, velocity or altitude if those are included in this Location.
        ///
        ///     If this location does not have an accuracy, then 0.0 is returned.
        /// </remarks>
        public double? Accuracy { get; set; }
    }
    public class Address
    {
        public String Street { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String ZipCode { get; set; }
    }
}

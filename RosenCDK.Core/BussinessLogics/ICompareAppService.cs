using System.Collections.Generic;
using Abp.Domain.Services;

namespace RosenCDK.BussinessLogics
{
    public interface ICompareAppService : IDomainService
    {
        /// <summary>
        /// Get a array id of competencies that the sourceCompetence Array are missing from the targetCompetence Array
        /// </summary>
        /// <param name="sourceCompetence">A array id of competencies</param>
        /// <param name="targetCompetence">A array id of competencies</param>
        /// <returns>A array id of competencies that the sourceCompetence Array are missing from the targetCompetence Array</returns>
        int[] missingCompetencies(int[] sourceCompetence, int[] targetCompetence);

        /// <summary>
        /// Get an array of integers that the array X are missing from the array Y
        /// </summary>
        /// <param name="x">An array integers</param>
        /// <param name="y">An array integers</param>
        /// <returns>An array integers</returns>
        int[] GetMissingIntegers(int[] x, int[] y);

        /// <summary>
        /// Check a integer is in an array integers or not
        /// </summary>
        /// <param name="x">A integer used to check</param>
        /// <param name="y">An array integer used to compare</param>
        /// <returns>True if x in y. Otherwise, return False</returns>
        bool Duplicate(int x, int[] y);

        /// <summary>
        /// Determine whether the sourceCompetence Array are missing any competence from the targetCompetence Array
        /// </summary>
        /// <param name="sourceCompetence">A array id of competencies</param>
        /// <param name="targetCompetence">A array id of competencies</param>
        /// <returns>True if sourceCompetence are missing some competencies from targetCompetence</returns>
        bool IsMissingCompetencies(int[] sourceCompetence, int[] targetCompetence);

        /// <summary>
        /// Determine whether an array integers x have any integer from an array integers y
        /// </summary>
        /// <param name="x">An array integers</param>
        /// <param name="y">An array integers</param>
        /// <returns>True if any of x in y. Otherwise, return False</returns>
        bool IsContainAny(int[] x, int[] y);

        /// <summary>
        /// Calculate the end date
        /// </summary>
        /// <param name="startDateRaw">A string represent the start date</param>
        /// <param name="days">total days to plus</param>
        /// <param name="dayOff">List of integers represent the days off in a week</param>
        /// <returns>End date</returns>
        string CalculateEndDate(string startDateRaw, double days, List<int> dayOff);
    }
}

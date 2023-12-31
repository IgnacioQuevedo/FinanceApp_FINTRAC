using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using BusinessLogic.ExchangeHistory_Components;
using BusinessLogic.Report_Components;
using Mappers;
using System.Collections.Generic;

namespace Controller.Mappers
{

    public abstract class MapperResumeOfCategoryReport
    {
        #region To Resume Of Category Report

        public static ResumeOfCategoryReport ToResumeOfCategoryReport(ResumeOfCategoryReportDTO myResumeDTO)
        {
            ResumeOfCategoryReport myResume = new ResumeOfCategoryReport(MapperCategory.ToCategory(myResumeDTO.CategoryRelated), myResumeDTO.TotalSpentInCategory, myResumeDTO.PercentajeOfTotal);

            return myResume;
        }

        #endregion

        #region To Resume Of Category Report DTO

        public static ResumeOfCategoryReportDTO ToResumeOfCategoryReportDTO(ResumeOfCategoryReport myResume)
        {
            ResumeOfCategoryReportDTO myResumeDTO = new ResumeOfCategoryReportDTO(MapperCategory.ToCategoryDTO(myResume.CategoryRelated), myResume.TotalSpentInCategory, myResume.PercentajeOfTotal);

            return myResumeDTO;
        }

        #endregion

        #region To List Resume Of CategoryReport

        public static List<ResumeOfCategoryReport> ToListResumeOfCategoryReport(List<ResumeOfCategoryReportDTO> myListDTO)
        {
            List<ResumeOfCategoryReport> myResumeList = new List<ResumeOfCategoryReport>();

            foreach (ResumeOfCategoryReportDTO myResumeDTO in myListDTO)
            {
                myResumeList.Add(ToResumeOfCategoryReport(myResumeDTO));
            }

            return myResumeList;
        }

        #endregion

        #region To Resume Of Category Report DTO

        public static List<ResumeOfCategoryReportDTO> ToListResumeOfCategoryReportDTO(List<ResumeOfCategoryReport> myList)
        {
            List<ResumeOfCategoryReportDTO> myResumeListDTO = new List<ResumeOfCategoryReportDTO>();

            foreach (ResumeOfCategoryReport myResume in myList)
            {
                myResumeListDTO.Add(ToResumeOfCategoryReportDTO(myResume));
            }

            return myResumeListDTO;
        }

        #endregion
    }
}
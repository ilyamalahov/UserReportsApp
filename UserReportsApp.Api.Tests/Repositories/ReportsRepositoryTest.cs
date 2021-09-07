using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using UserReportsApp.Api.Data;
using UserReportsApp.Api.Entities;
using UserReportsApp.Api.Repositories;
using Xunit;

namespace UserReportsApp.Api.Tests
{
    public class ReportsRepositoryTest
    {
        public ReportsRepositoryTest()
        {
            Seed();
        }

        private void Seed()
        {
            using (var context = new UserReportsContext(CreateContextOptions()))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureDeleted();

                var report1 = new Report { Id = 1, NumberOfHours = 1, Remark = "Report #1 remark", UserId = 1 };
                var report2 = new Report { Id = 2, NumberOfHours = 2, Remark = "Report #2 remark", UserId = 1 };
                var report3 = new Report { Id = 3, NumberOfHours = 3, Remark = "Report #3 remark", UserId = 1 };
                var report4 = new Report { Id = 4, NumberOfHours = 4, Remark = "Report #4 remark", UserId = 3 };

                //var user1 = new User 
                //{
                //    Id = 1,
                //    Email = "user1@mail.com",
                //    LastName = "User #1 last name",
                //    FirstName = "User #1 first name",
                //    MiddleName = "User #1 middle name",
                //    Reports = new List<Report>
                //    {
                //        report1, report2, report3
                //    }
                //};

                //var user2 = new User
                //{
                //    Id = 2,
                //    Email = "user2@mail.com",
                //    LastName = "User #2 last name",
                //    FirstName = "User #2 first name",
                //    MiddleName = "User #2 middle name"
                //};

                //var user3 = new User
                //{
                //    Id = 3,
                //    Email = "user3@mail.com",
                //    LastName = "User #3 last name",
                //    FirstName = "User #3 first name",
                //    MiddleName = "User #3 middle name",
                //    Reports = new List<Report> { report4 }
                //};


                //context.Users.AddRange(user1, user2, user3);

                context.Reports.AddRange(report1, report2, report3, report4);

                context.SaveChanges();
            }
        }

        #region GetReportsByUserIdAsync

        [Fact]
        public async Task GetReportsByUserId_ReportsWithUserIdMustNotBeEmpty()
        {
            using (var context = new UserReportsContext(CreateContextOptions()))
            {
                var reportsRepository = new ReportsRepository(context);

                var userId = 1;

                var reports = await reportsRepository.GetReportsByUserIdAsync(userId);

                Assert.NotEmpty(reports);
            }
        }

        [Fact]
        public async Task GetReportsByUserId_ReportsWithUserIdMustBeEmpty()
        {
            using (var context = new UserReportsContext(CreateContextOptions()))
            {
                var reportsRepository = new ReportsRepository(context);

                var userId = 2;

                var reports = await reportsRepository.GetReportsByUserIdAsync(userId);

                Assert.Empty(reports);
            }
        }

        [Fact]
        public async Task GetReportsByUserId_MustContainReportsWithUserId()
        {
            using (var context = new UserReportsContext(CreateContextOptions()))
            {
                var reportsRepository = new ReportsRepository(context);

                var userId = 3;

                var reports = await reportsRepository.GetReportsByUserIdAsync(userId);

                Assert.Contains(reports, r => r.UserId == userId);
            }
        }

        [Fact]
        public async Task GetReportsByUserId_NumberOfReportsWithUserIdMustBeEqual()
        {
            using (var context = new UserReportsContext(CreateContextOptions()))
            {
                var reportsRepository = new ReportsRepository(context);

                var userId = 1;
                var reportsCount = 3;

                var reports = await reportsRepository.GetReportsByUserIdAsync(userId);

                Assert.Equal(reportsCount, reports.Count());
            }
        }

        #endregion

        #region CreateReport

        [Fact]
        public async Task CreateReport_CreatedReportShouldBeValid()
        {
            using (var context = new UserReportsContext(CreateContextOptions()))
            {
                var reportsRepository = new ReportsRepository(context);

                var newReport = CreateNewReport(1);

                await reportsRepository.CreateReportAsync(newReport);

                Assert.NotEqual(0, newReport.Id);
                Assert.NotEqual(DateTime.MinValue, newReport.CreatedDate);
            }
        }

        [Fact]
        public async Task CreateReport_ReportsMustContainCreatedReport()
        {
            using (var context = new UserReportsContext(CreateContextOptions()))
            {
                var reportsRepository = new ReportsRepository(context);

                var newReport = CreateNewReport(1);

                await reportsRepository.CreateReportAsync(newReport);

                Assert.Contains(newReport, context.Reports);
            }
        }

        [Fact]
        public async Task CreateReport_ReportsCountMustBeEqual()
        {
            using (var context = new UserReportsContext(CreateContextOptions()))
            {
                var reportsRepository = new ReportsRepository(context);

                var userId = 1;
                var reportsCount = 5;
                var newReport = CreateNewReport(userId);

                await reportsRepository.CreateReportAsync(newReport);

                var newReportsCount = await context.Reports.CountAsync();
                Assert.Equal(reportsCount, newReportsCount);
            }
        }

        private Report CreateNewReport(int userId)
        {
            return new Report
            {
                //NumberOfHours = 1,
                Remark = "Remark",
                UserId = userId
            };
        }

        #endregion

        #region UpdateReport

        [Fact]
        public async Task UpdateReport_NumberOfHoursShouldBeEqual()
        {
            using (var context = new UserReportsContext(CreateContextOptions()))
            {
                var repository = new ReportsRepository(context);

                var reportId = 1;
                var numberOfHours = 150;

                var report = await context.Reports.FindAsync(reportId);

                Assert.NotNull(report);
                Assert.Equal(reportId, report.Id);

                report.NumberOfHours = numberOfHours;

                await repository.UpdateReportAsync(report);

                Assert.Equal(numberOfHours, report.NumberOfHours);
            }
        }

        [Fact]
        public async Task UpdateReport_RemarkShouldBeEqual()
        {
            using (var context = new UserReportsContext(CreateContextOptions()))
            {
                var repository = new ReportsRepository(context);

                var reportId = 1;
                var remark = "New remark";

                var report = await context.Reports.FindAsync(reportId);

                Assert.NotNull(report);
                Assert.Equal(reportId, report.Id);

                report.Remark = remark;

                await repository.UpdateReportAsync(report);

                Assert.Equal(remark, report.Remark);
            }
        }

        #endregion

        #region DeleteReport

        [Fact]
        public async Task DeleteReport_ReportsMustNotContainRemovedReport()
        {
            using (var context = new UserReportsContext(CreateContextOptions()))
            {
                var reportsRepository = new ReportsRepository(context);

                var reportId = 1;

                var report = await context.Reports.FindAsync(reportId);

                Assert.NotNull(report);
                Assert.Equal(reportId, report.Id);

                await reportsRepository.DeleteReportAsync(report);

                Assert.DoesNotContain(report, context.Reports);
            }
        }

        [Fact]
        public async Task DeleteReport_ReportsCountMustBeEqual()
        {
            using (var context = new UserReportsContext(CreateContextOptions()))
            {
                var reportsRepository = new ReportsRepository(context);

                var reportId = 1;

                var report = await context.Reports.FindAsync(reportId);

                Assert.NotNull(report);
                Assert.Equal(reportId, report.Id);

                await reportsRepository.DeleteReportAsync(report);

                Assert.DoesNotContain(context.Reports, r => r.Id == reportId);
            }
        }

        #endregion

        private DbContextOptions CreateContextOptions()
        {
            return new DbContextOptionsBuilder<UserReportsContext>()
                .UseInMemoryDatabase("UserReportsTest")
                .Options;
        }
    }
}

using Quartz;
using RentStudio.Services.EmailService;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
namespace RentStudio.Jobs
{
    public class LogMonitorJob : IJob
    {
        private readonly EmailService _emailService;
        private readonly string _logDirectory = @"D:\personalDatas\log\"; // Directory where log files are stored

        public LogMonitorJob(EmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var currentDate = DateTime.Now.ToString("yyyyMMdd"); // Get current date in format yyyyMMdd
            var logFileName = $"log{currentDate}.txt"; // Construct the log file name for today
            var logFilePath = Path.Combine(_logDirectory, logFileName);

            if (File.Exists(logFilePath))
            {
                try
                {
                    // Read the log file in read-only mode with shared access
                    using (var fileStream = new FileStream(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var reader = new StreamReader(fileStream))
                    {
                        // Read all lines and filter only the ones containing "[ERR]"
                        var errorLines = reader
                            .ReadToEnd()
                            .Split(Environment.NewLine)
                            .Where(line => line.Contains("[ERR]")) // Filter for lines containing "[ERR]"
                            .ToList();

                        if (errorLines.Any())
                        {
                            // Join all error lines into a single string for email body
                            string logContent = string.Join(Environment.NewLine, errorLines);

                            // Send the filtered log lines via email
                            await _emailService.SendEmailWithAttachmentAsync(
                                toEmail: "daria.lapadus17@gmail.com",
                                subject: $"Error Logs for {DateTime.Now:yyyy-MM-dd}",
                                body: logContent
                                );
                        }
                        else
                        {
                            Console.WriteLine("No error logs found for today.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (log or handle errors)
                    Console.WriteLine($"Error reading the log file: {ex.Message}");
                }
            }
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using ScheduledTasks.Interface;
using ScheduledTasks.ScheduledTasks;

namespace ScheduledTasks
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddScheduledTasks(this IServiceCollection services)
        {
            //services.AddSingleton<IScheduledTask, StockTransferVouchers>();
            services.AddSingleton<IScheduledTask, PushDataService>();
            services.AddSingleton<IScheduledTask, PullDataService>();
            services.AddSingleton<IScheduledTask, backup>();
            services.AddSingleton<IScheduledTask, AutoPurchaseOrder>();
            services.AddSingleton<IScheduledTask, PushFileService>();
            services.AddSingleton<IScheduledTask, PullFileService>();
            services.AddSingleton<IScheduledTask, AttendanceSms>();
            services.AddSingleton<IScheduledTask, InvoiceSms>();
            services.AddSingleton<IScheduledTask, ShiftRun>();
            services.AddSingleton<IScheduledTask, PushPullTransactionLog>();
            //services.AddSingleton<IScheduledTask, AutoDeleteHoldSetup>();
            //services.AddSingleton<IScheduledTask, AutoSoftDeleteHoldSetup>();

            services.AddHostedService<SchedulerHostedService>();

            return services;
        }
    }
}

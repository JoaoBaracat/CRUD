using CRUD.Net.Infra.Data.Contexts;
using CRUD.Net.UI.WF.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace CRUD.Net.UI.WF
{
    static class Program
    {
        public static IServiceProvider ServiceProvider { get; set; }

        static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddDbContext<CrudDbContext>(options =>
            {
                options.UseSqlServer(ConfigurationManager
                    .ConnectionStrings["DefaultConnection"].ConnectionString);
            });

            services.AddDependencyInjectionConfiguration();
            ServiceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ConfigureServices();

            LoginForm formLogin = new LoginForm();
            if (formLogin.ShowDialog() == DialogResult.OK)
            {
                var login = formLogin.Login;
                formLogin.Dispose();                
                Application.Run(new MainForm(login));
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
